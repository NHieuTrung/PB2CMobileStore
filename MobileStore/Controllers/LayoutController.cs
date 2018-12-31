
using MobileStore.Common;
using Models.EF;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileStore.Controllers
{
    public class LayoutController: Controller
    {
        private MobileStoreContext db = new MobileStoreContext();

        public PartialViewResult HeaderCart()
        {
            List<ShoppingCartItemBrowser> list = null;
            if (Session["shoppingcart"] == null)
            {

                list = new List<ShoppingCartItemBrowser>();
                ViewBag.CountItem = 0;
                ViewBag.SumMoney = 0;
            }
            else
            {

                list = (List<ShoppingCartItemBrowser>)Session["shoppingcart"];
                int countItem = 0;
                decimal sumMoney = 0;
                foreach (var item in list)
                {
                    countItem+=1*item.Quantiny;
                    var price = item.Price.GetValueOrDefault();
                    sumMoney += price * item.Quantiny;
                }
                ViewBag.CountItem = countItem;
                ViewBag.SumMoney = sumMoney;
            }
            return PartialView("_HeaderCart");
        }
        public PartialViewResult CategoryMenu()
        {
            //create a listCategoriesTemp
            var listCategoriesTemp = new List<Categories>();
            var Categories = new Categories();
            //star product type id with x=1
            int x = 1;
            Categories.ProductTypeName = db.ProductTypes.Where(m => m.ProductTypeId == x).Select(m => m.ProductTypeName).SingleOrDefault();
            //create a list with product type id asc
            var listProduct = db.Products.OrderBy(m=>m.BrandTypeId).OrderBy(m => m.ProductTypeId);

            List<string> listS1 = new List<string>();
            foreach (var item in listProduct)
            {
                if (item.ProductTypeId == x)
                {
                    listS1.Add(item.BrandType.BrandName);
                }
                else
                {
                    Categories.ListBrandName = listS1;
                    listCategoriesTemp.Add(Categories);
                    x++;
                    Categories = new Categories();
                    Categories.ProductTypeName = db.ProductTypes.Where(m => m.ProductTypeId == x).Select(m => m.ProductTypeName).SingleOrDefault();
                    listS1 = new List<string>();
                    listS1.Add(item.BrandType.BrandName);
                }
            }
            Categories.ListBrandName = listS1;
            listCategoriesTemp.Add(Categories);

            //create listCategories by listCategoriesTemp distinc and brand name asc
            var listCategories = new List<Categories>();
            foreach (var item in listCategoriesTemp)
            {
                Categories = new Categories();
                Categories.ProductTypeName = item.ProductTypeName;
                Categories.ListBrandName = item.ListBrandName.Distinct().ToList();
                listCategories.Add(Categories);
            }

            ViewBag.Categories = listCategories;
            return PartialView("_Categories");
        }

        public PartialViewResult ProductTypeMenu()
        {
            ViewBag.ProductType = db.ProductTypes.Select(m => m.ProductTypeName);
            return PartialView("_ProductType");
        }

        public PartialViewResult AllColorProductMenu(string typeProduct)
        {
            string tbProductType = "Product" + typeProduct;
            ViewBag.AllColorProduct = db.ProductPortableDevices.Select(m => m.Color).Union(db.ProductAccessories.Select(m=>m.Color).Distinct());
            return PartialView("_AllColorProduct");
        }

        public PartialViewResult RecentlyView()
        {
            return PartialView("_RecentlyView");
        }

        public PartialViewResult BranchSlider()
        {
            
            return PartialView("_BranchSlider");
        }

        public PartialViewResult TopbarUser()
        {
            var sessionUser = (LoginModelDisplay)Session["USER_SESSION"];
            if (sessionUser != null)
            {
                ViewBag.DisplayUserName = sessionUser.MemberName;
            }
            return PartialView("_TopbarUser");
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