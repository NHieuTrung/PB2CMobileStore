using Facebook;
using MobileStore.Common;
using Models.Bus;
using Models.EF;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MobileStore.Areas.Admin.Controllers
{
    public class AccountsController : Controller
    {
        private MobileStoreContext db = new MobileStoreContext();
        public ActionResult Login()
        {
            ViewBag.GGSigninClientContent = ConfigurationManager.AppSettings["GgAppId"].ToString();
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            ViewBag.GGSigninClientContent = ConfigurationManager.AppSettings["GgAppId"].ToString();
            if (ModelState.IsValid) {
                var userBus = new UserBus();
                var result = userBus.Login(model.MemberEmail, EncryptorMD5.MD5Hash(model.MemberPassword));

                LoginModelDisplay loginModelDisplay = new LoginModelDisplay();
                loginModelDisplay.MemberAccountId = db.Members.Where(m => m.MemberEmail == model.MemberEmail).Select(m => m.MemberId).SingleOrDefault();
                loginModelDisplay.MemberEmail = model.MemberEmail;
                loginModelDisplay.RememberMe = model.RememberMe;
                loginModelDisplay.MemberName = db.Members.Where(m => m.MemberEmail == model.MemberEmail).Select(m => m.FullName).FirstOrDefault();
                loginModelDisplay.MemberTypeId = result;


                if (result == 1 || result == 2)
                {
                    Session.Remove(CommonConstants.USER_SESSION);
                    Session.Add(CommonConstants.USER_SESSION, loginModelDisplay);
                    return RedirectToAction("Index", "ADHome");
                }
                else if (result == 3)
                {
                    Session.Remove(CommonConstants.USER_SESSION);
                    Session.Add(CommonConstants.USER_SESSION, loginModelDisplay);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    ModelState.AddModelError("LoginCheck", "Đăng nhập không đúng");
                    return View(model);
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            ViewBag.GenderTypeId = new SelectList(db.GenderTypes, "GenderTypeId", "GenderName");
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            var jsonAuthenticationEmail = (JsonAthenticationEmail)Session["authenticationemail"];
            if (ModelState.IsValid)
            {
                if (registerModel.MemberEmail == jsonAuthenticationEmail.Email & registerModel.CodeAuth == jsonAuthenticationEmail.AuthenticationCode)
                {

                    Session["shoppingcart"] = null;
                    Member member = new Member() { MemberEmail = registerModel.MemberEmail, MemberPassword = EncryptorMD5.MD5Hash(registerModel.MemberPassword), FullName = registerModel.FullName, GenderTypeId = registerModel.GenderTypeId, PhoneNumber = registerModel.PhoneNumber, MemberTypeId = 3, RegDate = System.DateTime.Now };
                    db.Members.Add(member);
                    AddressMember addressMember = new AddressMember() { AddressMemberName = registerModel.HomeAddress, MemberId = member.MemberId, PriorityStatus = 1 };
                    db.AddressMembers.Add(addressMember);
                    db.SaveChanges();

                    LoginModelDisplay loginModelDisplay = new LoginModelDisplay();
                    loginModelDisplay.MemberAccountId = member.MemberId;
                    loginModelDisplay.MemberEmail = member.MemberEmail;
                    //loginModelDisplay.RememberMe =  ;
                    loginModelDisplay.MemberName = member.FullName;
                    loginModelDisplay.MemberTypeId = 3;

                    Session.Remove(CommonConstants.USER_SESSION);
                    Session.Add(CommonConstants.USER_SESSION, loginModelDisplay);

                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    ModelState.AddModelError("RegisterCheck", "Mã xác thực không hợp lệ");
                    return View(registerModel);
                }
            }
            
            ViewBag.GenderTypeId = new SelectList(db.GenderTypes, "GenderTypeId", "GenderName");
            return View(registerModel);
        }

        public JsonResult GetAuthenticationInEmail(string Email)
        {
            var findThisEmail = db.Members.Where(m => m.MemberEmail == Email).Select(m=>m.MemberEmail).FirstOrDefault();

            if (findThisEmail==null)
            {
                Session["authenticationemail"] = null;
                int authCode = new Random().Next(1000, 9999);
                JsonAthenticationEmail jsonAuthenticationEmail = new JsonAthenticationEmail();
                jsonAuthenticationEmail.Email = Email;
                jsonAuthenticationEmail.AuthenticationCode = authCode.ToString();
                Session["authenticationemail"] = jsonAuthenticationEmail;

                MailHelper.SendMailAuthentication(Email, "Ma xac thuc tu Trung Store", authCode.ToString());

                return Json(new { status=true});
            }
            else
                return Json(new {  status = false});
        }
            

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult LoginFB()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secrect = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?field=email,id,name");
                string emailFB = me.email;
                string nameFB = me.name;
                string idFB = me.id;

                //Create member account add database
                var memberAccount = new Member();
                memberAccount.MemberEmail = emailFB;
                memberAccount.MemberTypeId = 2;
                memberAccount.FullName = nameFB;
                memberAccount.MemberFacebookId = idFB;

                var resultInsertFb = new UserBus().InsertUserFb(memberAccount);

                //Add Session to display view
                LoginModelDisplay loginModelDisplay = new LoginModelDisplay();
                loginModelDisplay.MemberAccountId = resultInsertFb;
                loginModelDisplay.MemberEmail = memberAccount.MemberEmail;
                loginModelDisplay.MemberName = memberAccount.FullName;
                loginModelDisplay.MemberTypeId = 2;

                Session.Remove(CommonConstants.USER_SESSION);
                Session.Add(CommonConstants.USER_SESSION, loginModelDisplay);

            }
            return Redirect("/");
        }

        public JsonResult LoginGoogle(string googleACModel)
        {
            var accountSocialsList = new JavaScriptSerializer().Deserialize<List<AccountSocial>>(googleACModel);
            var accountSocials = accountSocialsList.FirstOrDefault();
            var memberAccount = new Member();
            memberAccount.MemberEmail = accountSocials.Email;
            memberAccount.MemberTypeId = 2;
            memberAccount.FullName = accountSocials.FullName;
            memberAccount.MemberGoogleId = accountSocials.AccountId;
            var resultInsertGg = new UserBus().InsertUserGg(memberAccount);

            //Add Session to display view
            LoginModelDisplay loginModelDisplay = new LoginModelDisplay();
            loginModelDisplay.MemberAccountId = resultInsertGg;
            loginModelDisplay.MemberEmail = memberAccount.MemberEmail;
            loginModelDisplay.MemberName = memberAccount.FullName;
            loginModelDisplay.MemberTypeId = 2;

            Session.Remove(CommonConstants.USER_SESSION);
            Session.Add(CommonConstants.USER_SESSION, loginModelDisplay);



            return Json(new { status = true });
        }



        public ActionResult Logout()
        {
            Session.Remove(CommonConstants.USER_SESSION);
            return RedirectToAction("Index", "Home", new { area = "" });
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