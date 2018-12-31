using MobileStore.Common;
using Models.EF;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileStore.Areas.Admin.Controllers
{
    public class LayoutAdminController : Controller
    {
        // GET: Admin/Layout
        public PartialViewResult TopbarUser()
        {
            var sessionUser = (LoginModelDisplay)Session["USER_SESSION"];
            if (sessionUser != null)
            {
                ViewBag.DisplayUserName = sessionUser.MemberName;
            }
            return PartialView("_TopbarUser");
        }

        public PartialViewResult LeftTopbarUser()
        {
            var sessionUser = (LoginModelDisplay)Session["USER_SESSION"];
            if (sessionUser != null)
            {
                ViewBag.DisplayUserName = sessionUser.MemberName;
            }
            return PartialView("_LeftTopbarUser");
        }
    }
}