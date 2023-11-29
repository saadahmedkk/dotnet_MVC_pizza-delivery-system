using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaDelivery.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        Pizza_DeliveryEntities dbObj = new Pizza_DeliveryEntities();
        [Authorize]
        public ActionResult Details()
        {
            var usertoshow = (from users in dbObj.UserDetails.ToList()
                                  where users.Email == User.Identity.Name 
                                  select users).ToList();
            
                
                return View(usertoshow);

            
            
        }

        [Authorize]
        public ActionResult EditUser(int id)
        {
            var usertobeEdited = (from user in dbObj.UserDetails.ToList()
                                  where user.UserID == id
                                  select user).First();
            return View(usertobeEdited);
        }

        [Authorize]
        public ActionResult AfterEditUser(UserDetail entireForm) 
        {
            
            var usertobeEdited = (from user in dbObj.UserDetails.ToList()
                                  where user.UserID == entireForm.UserID
                                  select user).First();
            usertobeEdited.UserFirstName = entireForm.UserFirstName;
            usertobeEdited.UserLastName = entireForm.UserLastName;
            usertobeEdited.Email = entireForm.Email;
            usertobeEdited.Password = entireForm.Password;
            usertobeEdited.Address = entireForm.Address;
            usertobeEdited.DOB = entireForm.DOB;
            dbObj.SaveChanges();

            return Redirect("/User/Details");
        }

        [Authorize]
        public ActionResult AddUser()
        {
            
            return View();
        }

        [Authorize]
        public ActionResult AfterAddUser(UserDetail addentireForm)
        {
            dbObj.UserDetails.Add(addentireForm);
            dbObj.SaveChanges();
            return Redirect("/User/Details");
        }

    }
}