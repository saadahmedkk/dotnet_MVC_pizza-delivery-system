using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaDelivery.Controllers
{
    public class UserAddressController : Controller
    {
        Pizza_DeliveryEntities dbObj = new Pizza_DeliveryEntities();
       
        // GET: UserAddress/Details/
        public ActionResult Details()
        {
            var addresstodisplay = (from address in dbObj.DeliveryAddresses.ToList()
                                   where address.Email == User.Identity.Name
                                   select address).ToList();
            return View(addresstodisplay);
        }

        // GET: UserAddress/Create
        public ActionResult Create()
        {
            ViewBag.UserName = User.Identity.Name;
            return View();
        }

        // POST: UserAddress/Create
        
        public ActionResult AfterCreate(DeliveryAddress entireform)
        {

            try
            {


                dbObj.DeliveryAddresses.Add(entireform);
                dbObj.SaveChanges();
                return RedirectToAction("Details");
            }
            catch
            {
                return RedirectToAction("Details");
            }

        }

        

        

       
        // POST: UserAddress/Delete/5
        
        public ActionResult Delete(int id)
        {
            try
            {
                DeliveryAddress addtobedeleted = (from address in dbObj.DeliveryAddresses.ToList()
                                                  where address.AddressId == id && address.Email == User.Identity.Name
                                                  select address).First();
                dbObj.DeliveryAddresses.Remove(addtobedeleted);
                dbObj.SaveChanges();

                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }

        }
    }
}
