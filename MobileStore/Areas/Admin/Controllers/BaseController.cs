using MobileStore.Common;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MobileStore.Areas.Admin.Controllers
{
    public class BaseController:Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (LoginModelDisplay)Session[CommonConstants.USER_SESSION];
            if(session == null || session.MemberTypeId != 1 && session.MemberTypeId != 2)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "PageNotFound", Area = "" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}