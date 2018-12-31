using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileStore.Areas.Admin.Controllers
{
    public class ADHomeController : BaseController
    {
        // GET: Admin/ADHome
        public ActionResult Index()
        {
            return View();
        }
    }
}