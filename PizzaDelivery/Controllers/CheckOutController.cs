using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaDelivery.Controllers
{
    public class CheckOutController : Controller
    {
        Pizza_DeliveryEntities dbObj = new Pizza_DeliveryEntities();
        // GET: CheckOut
        public ActionResult Details()
        {
            ViewBag.UserName = User.Identity.Name;
            var add = (from address in dbObj.DeliveryAddresses.ToList()
                      where address.Email == User.Identity.Name
                      select address).ToList();
            return View(add);
        }
        public ActionResult Orders()
        {
            
            var details = (from address in dbObj.DeliveryAddresses.ToList()
                           join
                           order in dbObj.UserOrders.ToList() on address.AddressId equals order.DeliveryID
                           where address.Email == User.Identity.Name
                           select new CheckOutDetails
                           {
                               OrderID = order.OrderID,
                               Status = order.Status,
                               Address = address.Address,
                               State = address.State,
                               City = address.City,
                               Country = address.Country,
                               Pincode = address.Pincode,

                           });

            ViewModel viewModel = new ViewModel()
            {
                checkOutItems = details

            };

            return View(viewModel);

        }

        public ActionResult CreateOrder(UserOrder formitem)
        {
            var carttobedeleted = (from cart in dbObj.AddtoCarts.ToList()
                                  where cart.Email == User.Identity.Name
                                  select cart).ToList();
           
            foreach (var item in carttobedeleted)
            {
                dbObj.AddtoCarts.Remove(item);
            }
            dbObj.SaveChanges();
            dbObj.UserOrders.Add(formitem);
            dbObj.SaveChanges();
            
            

            return RedirectToAction("Orders");
        }
    }
}