using Models.ViewModels;
using Models.EF;
using Models.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileStore.Controllers
{
    public class ProductInformationPageController : Controller
    {
        private MobileStoreContext db = new MobileStoreContext();
        // GET: ProductInformation
        public ActionResult Index(int productTypeId, int productId, int? currentProductDetailId,int? ram, int? memory,string color)
        {
            FormatBus format = new FormatBus();
            ViewBag.ProductTypeId = productTypeId;
            ViewBag.ProductId = productId;

            ViewBag.ProductType = db.ProductTypes.Where(m => m.ProductTypeId == productTypeId).Select(m => m.ProductTypeName).FirstOrDefault();
            //Use this for view index
            ViewBag.ProductName = db.Products.Where(m => m.ProductId == productId).Select(m => m.ProductName).FirstOrDefault();

            //Find ProductId version by memory-ram-color
            if (currentProductDetailId == null)
            {
                if (ViewBag.ProductType == "Smartphone")
                {
                    ViewBag.CurrentProductDetailId = db.ProductPortableDevices.Where(m => m.ProductId == productId && m.RamSize == ram && m.MemorySize == memory && m.Color == color).Select(m => m.ProductPortableDeviceId).FirstOrDefault();
                }
                else if (ViewBag.ProductType == "Accessory")
                {
                    ViewBag.CurrentProductDetailId = db.ProductAccessories.Where(m => m.ProductId == productId && m.Color == color).Select(m => m.ProductAccessoryId).FirstOrDefault();
                }
            }
            else
            {
                ViewBag.CurrentProductDetailId = currentProductDetailId;
            }


            //Filter list All of version in product
            var productInformationList = new List<ProductInformation>();
            if (ViewBag.ProductType == "Smartphone")
            {
                var productSmartphoneList = db.ProductPortableDevices.Where(m => m.ProductId == productId).ToList();
                foreach (var item in productSmartphoneList)
                {
                    productInformationList.Add(new ProductInformation { ProductDetailId = item.ProductPortableDeviceId, ProductId = item.ProductId, Color = item.Color, Price = item.Price, Quantiny = item.Quantiny, FileImage = item.FileImage, Display = item.Display, MemorySize = item.MemorySize, RamSize = item.RamSize });

                    //This is find properties for Current Product
                    if (item.ProductPortableDeviceId == ViewBag.CurrentProductDetailId)
                    {
                        ViewBag.CurrentProductColor = item.Color;
                        ViewBag.CurrentProductRam = item.RamSize;
                        ViewBag.CurrentProductMemory = item.MemorySize;
                        ViewBag.CurrentProductQuantity = item.Quantiny;
                        ViewBag.CurrentProductPrice = item.Price;
                    }
                }
            }
            else if (ViewBag.ProductType == "Accessory")
            {
                var productAccessoryList = db.ProductAccessories.Where(m => m.ProductId == productId).ToList();
                foreach (var item in productAccessoryList)
                {
                    productInformationList.Add(new ProductInformation { ProductDetailId = item.ProductAccessoryId, ProductId = item.ProductId, Color = item.Color, FileImage = item.ImageFile, Quantiny = item.Quantiny });

                    //This is find properties for Current Product
                    if (item.ProductAccessoryId == ViewBag.CurrentProductDetailId)
                    {
                        ViewBag.CurrentProductColor = item.Color;
                        ViewBag.CurrentProductQuantity = item.Quantiny;
                        ViewBag.CurrentProductPrice = item.Price;
                    }
                }

            }
            ViewBag.ColorList = productInformationList.Select(m=>m.Color).Distinct();

            if (ViewBag.ProductType == "Smartphone")
            {
                //Filter versiom distinc have model by ram-memory
                ViewBag.ProductVersionList = from c in productInformationList
                                             group c by new
                                             {
                                                 c.RamSize,
                                                 c.MemorySize
                                             } into grp
                                             select grp.First();

            }
                
            ViewBag.FileImage1 = "/Assets/user/images/" + ViewBag.ProductType + "/" + productId + "-" + ViewBag.CurrentProductColor + "-1" + ".jpg";
            ViewBag.FileImage2 = "/Assets/user/images/" + ViewBag.ProductType + "/" + productId + "-" + ViewBag.CurrentProductColor + "-2" + ".jpg";
            ViewBag.FileImage3 = "/Assets/user/images/" + ViewBag.ProductType + "/" + productId + "-" + ViewBag.CurrentProductColor + "-3" + ".jpg";

            ViewBag.QuantityPrdOrd = null;

            return View(productInformationList);
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