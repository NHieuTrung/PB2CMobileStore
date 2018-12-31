using Models.ViewModels;
using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileStore.Controllers
{
    public class ShopPageController : Controller
    {
        private MobileStoreContext db = new MobileStoreContext();
        // GET: ShopPage
        public ActionResult Index(string productType, string sortOrder,string brand, string currentFilter, string searchString, int? page)
        {
            

            
            int productTypeId = db.ProductTypes.Where(m => m.ProductTypeName == productType).Select(m=>m.ProductTypeId).FirstOrDefault();
            ViewBag.ProductTypeName = productType;
            ViewBag.ProductTypeId = productTypeId;

            List<ProductShop> listPrd= new List<ProductShop>();

            //--Create a list to show Product Shop List
            if (productType != null)
            {
                foreach (var item in db.Products.Where(m => m.ProductTypeId == productTypeId))
                {

                    var prdId = item.ProductId ;
                    ProductShop productShop = new ProductShop();
                    Nullable<decimal> PriceCheapestType = Decimal.MaxValue;
                    int count = 0;

                    //Find branch cheapest in Product to show page shop, Sale marketting
                    if (productType == "Smartphone")
                    {
                        //Search in PortableDevice by product id
                        foreach (var item1 in db.ProductPortableDevices.Where(m => m.ProductId == prdId))
                        {
                            if (item1.Price < PriceCheapestType)
                            {
                                PriceCheapestType = item1.Price;
                            }
                            //Check Quantiny this product to show nofication
                            count += item1.Quantiny;
                        }
                    }
                    else if (productType == "Accessory")
                    {
                        //Search in AccessoryDevice by product id
                            foreach (var item1 in db.ProductAccessories.Where(m => m.ProductId == prdId))
                            {
                                if (item1.Price < PriceCheapestType)
                                {
                                    PriceCheapestType = item1.Price;
                                }
                                count += item1.Quantiny;
                            }
                            
                    }

                    //Assign variable Price Cheapest branch
                    if (productType == "Smartphone")
                    {
                        //Search in PortableDevice by product id
                        foreach (var item1 in db.ProductPortableDevices.Where(m => m.ProductId == prdId))
                        {
                            if (item1.Price == PriceCheapestType)
                            {
                                productShop.CheapestProductId = item1.ProductPortableDeviceId;
                            }
                        }
                    }
                    else if (productType == "Accessory")
                    {
                        //Search in AccessoryDevice by product id
                            foreach (var item1 in db.ProductAccessories.Where(m => m.ProductId == prdId))
                            {
                                if (item1.Price == PriceCheapestType)
                                {
                                productShop.CheapestProductId = item1.ProductAccessoryId;
                                }
                            }
                    }

                    productShop.ProductId = prdId;
                    productShop.ProductName = item.ProductName;
                    productShop.RealeaseDate = item.RealeaseDate;
                    productShop.FileImage = item.FileImage;
                    productShop.PriceCheapestType = PriceCheapestType;
                    productShop.Brand = db.BrandTypes.Where(m=>m.BrandTypeId==item.BrandTypeId).Select(m=>m.BrandName).FirstOrDefault();
                    if (count == 0)
                    {
                        productShop.Stocking = false;
                    }
                    listPrd.Add(productShop);
                }
            }
            //----------------------------------------

            ViewBag.CountProduct = listPrd.Count();
            ViewBag.ProductType = productType;
            ViewBag.BrandList = listPrd.Select(m => m.Brand).Distinct();

            //Paging, sorting part
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                listPrd = listPrd.Where(s => s.ProductName.Contains(searchString)).ToList();
            }

            if (!String.IsNullOrEmpty(brand))
            {
                listPrd = listPrd.Where(m => m.Brand == brand).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    listPrd = listPrd.OrderByDescending(s => s.ProductName).ToList();
                    break;
                case "Date":
                    listPrd = listPrd.OrderBy(s => s.RealeaseDate).ToList();
                    break;
                case "date_desc":
                    listPrd = listPrd.OrderByDescending(s => s.RealeaseDate).ToList();
                    break;
                default:
                    listPrd = listPrd.OrderBy(s => s.ProductName).ToList();
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(listPrd.ToPagedList(pageNumber, pageSize));
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}