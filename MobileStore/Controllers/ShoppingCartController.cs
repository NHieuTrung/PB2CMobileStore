using Models.ViewModels;
using Models.EF;
using Models.Bus;

using MobileStore.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MobileStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private MobileStoreContext db = new MobileStoreContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            List<ShoppingCartItemBrowser> list = null;
            if (Session["shoppingcart"] == null)
            {

                list = new List<ShoppingCartItemBrowser>();
            }
            else
            {

                list = (List<ShoppingCartItemBrowser>)Session["shoppingcart"];
                ViewBag.TotalPrice = list.Sum(m => m.Price * m.Quantiny);
            }
            var sessionUser = (LoginModelDisplay)Session["USER_SESSION"];
            ViewBag.ListAddress = db.AddressMembers.Where(m => m.MemberId == sessionUser.MemberAccountId);
            return View(list);
        }
        public ActionResult AddItem(int productTypeId, int currentProductDetailId, int? ram, int? memory, string color, decimal price, int quantity,int productId)
        {
            FormatBus format = new FormatBus();
            
            string productType = db.ProductTypes.Where(m => m.ProductTypeId == productTypeId).Select(m => m.ProductTypeName).FirstOrDefault();
            string nameProduct = db.Products.Where(m => m.ProductId == productId).Select(m => m.ProductName).FirstOrDefault();

            ViewBag.ProductId = productId;
            ViewBag.ProductType = productType;


            if (Session["shoppingcart"] == null)
            {
                List<ShoppingCartItemBrowser> listShoppingCart = new List<ShoppingCartItemBrowser>();
                if (productType == "Smartphone")
                {
                    string fileImage = "/Assets/user/images/" + productType + "/" + productId + "-" + color + "-1.jpg";
                    ShoppingCartItemBrowser item = new ShoppingCartItemBrowser { ProductDetailId = currentProductDetailId, ProductId=productId, Name= nameProduct, RamSize = ram, MemorySize = memory, Color = color, Price = price, FileImage=fileImage, Quantiny = quantity, ProductTypeName=productType};
                    listShoppingCart.Add(item);
                }
                else if (productType == "Accessory")
                {
                    string fileImage = "/Assets/user/images/" + productType + "/" + productId + "-" + color + "-1.jpg";
                    ShoppingCartItemBrowser item = new ShoppingCartItemBrowser { ProductDetailId = currentProductDetailId, ProductId = productId, Name = nameProduct, Color = format.ColorFormat(color), Price = price, FileImage = fileImage, Quantiny = quantity, ProductTypeName = productType };
                    listShoppingCart.Add(item);
                }
                Session["shoppingcart"] = listShoppingCart;

            }
            else
            {
                List<ShoppingCartItemBrowser> listShoppingCart = (List<ShoppingCartItemBrowser>)Session["shoppingcart"];
                if (productType == "Smartphone")
                {
                    if (listShoppingCart.Find(m => m.ProductDetailId == currentProductDetailId) == null)
                    {
                        string fileImage = "/Assets/user/images/" + productType + "/" + productId + "-" + color + "-1.jpg";
                        ShoppingCartItemBrowser item = new ShoppingCartItemBrowser { ProductDetailId = currentProductDetailId, ProductId = productId, Name = nameProduct, RamSize = ram, MemorySize = memory, Color = format.ColorFormat(color), Price = price, Quantiny=1, FileImage = fileImage, ProductTypeName = productType };
                        listShoppingCart.Add(item);
                    }
                    else
                    {
                        foreach (var item in listShoppingCart)
                        {
                            if (item.ProductDetailId == currentProductDetailId)
                            {
                                item.Quantiny += quantity;
                            }
                        }
                    }

                }
                else if (productType == "Accessory")
                {
                    if (listShoppingCart.Find(m => m.ProductDetailId == currentProductDetailId) == null)
                    {
                        string fileImage = "/Assets/user/images/" + productType + "/" + productId + "-" + color + "-1.jpg";
                        ShoppingCartItemBrowser item = new ShoppingCartItemBrowser { ProductDetailId = currentProductDetailId, ProductId = productId, Name = nameProduct, Color = color, Price = price, Quantiny = 1, FileImage = fileImage, ProductTypeName = productType };
                        listShoppingCart.Add(item);
                    }
                    else
                    {
                        foreach (var item in listShoppingCart)
                        {
                            if (item.ProductDetailId == currentProductDetailId)
                            {
                                item.Quantiny += quantity;
                            }
                        }
                    }
                }
                Session["shoppingcart"] = listShoppingCart;
            }

            return RedirectToAction("Index");
        }

        public JsonResult UpdateCart(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<JsonCart>>(cartModel);
            var sessionCart= (List<ShoppingCartItemBrowser>)Session["shoppingcart"];
            foreach(var item in sessionCart)
            {
                var itemIdSession = item.ProductId.ToString()+"-"+ item.ProductDetailId.ToString();
                var jsonItem = jsonCart.SingleOrDefault(x => x.ItemId == itemIdSession);
                if (jsonItem!=null &&jsonItem.Quantity>0)
                {
                    item.Quantiny = jsonItem.Quantity;
                }
                else if (jsonItem.Quantity == 0)
                {
                    sessionCart.RemoveAll(m => m.ProductDetailId == item.ProductDetailId);
                }
            }
            Session["shoppingcart"] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteItem(string itemId)
        {
            var sessionCart = (List<ShoppingCartItemBrowser>)Session["shoppingcart"];
            sessionCart.RemoveAll(x => x.ProductId.ToString() + "-" + x.ProductDetailId.ToString() == itemId);
            Session["shoppingcart"] = sessionCart;
            
            if (sessionCart.Where(x => x.ProductId.ToString() + "-" + x.ProductDetailId.ToString() == itemId).FirstOrDefault() != null)
            {
                return Json(new
                {
                    status = false
                });
            }
            return Json(new
            {
                status = true
            });
        }

        public ActionResult CheckItOut()
        {
            var sessionUser = (LoginModelDisplay)Session["USER_SESSION"];
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "Accounts", new { area = "Admin" });
            }
            db.ShoppingCarts.Add(new ShoppingCart { CustomerId=sessionUser.MemberAccountId, OrderDate = DateTime.Now, StatusCart = 0 });
            int shoppingCartId = db.ShoppingCarts.OrderByDescending(m => m.ShoppingCartId).Select(m=>m.ShoppingCartId).FirstOrDefault();
            var sessionCart = (List<ShoppingCartItemBrowser>)Session["shoppingcart"];
            foreach(var item in sessionCart)
            {
                if (item.ProductTypeName == "Smartphone")
                {
                    db.ShoppingCartItems.Add(new ShoppingCartItem { ShoppingCartId = shoppingCartId, ProductPortableDeviceId = item.ProductDetailId, Quantiny = item.Quantiny, Price = item.Price });

                }
                else if (item.ProductTypeName == "Accessory")
                {
                    db.ShoppingCartItems.Add(new ShoppingCartItem { ShoppingCartId = shoppingCartId, ProductAccessoryId = item.ProductDetailId, Quantiny = item.Quantiny, Price = item.Price });
                }
            }
            db.SaveChanges();
            Session["shoppingcart"] = null;
            return View();
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