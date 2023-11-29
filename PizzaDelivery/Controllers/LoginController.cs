using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections.Specialized;


namespace PizzaDelivery.Controllers
{
    public class LoginController : Controller
    {
        Pizza_DeliveryEntities dbObj = new Pizza_DeliveryEntities();
        // GET: Login
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(UserDetail loginObj, String ReturnUrl)
        {
            

            var MatchCount = (from user in dbObj.UserDetails.ToList()
                              where user.Email.ToLower() == loginObj.Email.ToLower() &&
                              user.Password == loginObj.Password
                              select user).ToList().Count();
            if (MatchCount == 1)
            {
                
                
                FormsAuthentication.SetAuthCookie(loginObj.Email, false);
                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);

                }
                else
                {
                    return Redirect("/Home/index");
                }
            }
            else
            {
                return View();
            }

        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }
    }
}