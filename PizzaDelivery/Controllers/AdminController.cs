using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace PizzaDelivery.Controllers
{
    public class AdminController : Controller
    {
        // GET: Test
        Pizza_DeliveryEntities dbObj = new Pizza_DeliveryEntities();

        public List<Pizza> GetPizzas()
        {
            return dbObj.Pizzas.ToList();
        }
        [Authorize]
        public ActionResult AdminPizza()
        {
            var allpizzas = GetPizzas();

            ViewModel viewModel = new ViewModel();
            viewModel.pizzas = allpizzas;


            return View(viewModel);
        }
        [Authorize]
        public ActionResult EditPizza(int id)
        {
            Pizza pizzaToBeEdited = (from pizza in dbObj.Pizzas.ToList()
                                     where pizza.PizzaID == id
                                     select pizza).First();
            
            return View(pizzaToBeEdited);
            
        }
        [Authorize]
        public ActionResult AfterEdit(Pizza updatePizza)
        {
            
            Pizza pizzaToBeEdited = (from pizza in dbObj.Pizzas.ToList()
                                     where pizza.PizzaID == updatePizza.PizzaID
                                     select pizza).First();

            pizzaToBeEdited.Title = updatePizza.Title;
            pizzaToBeEdited.Category = updatePizza.Category;
            pizzaToBeEdited.Description = updatePizza.Description;
            pizzaToBeEdited.SmallPrice = updatePizza.SmallPrice;
            pizzaToBeEdited.MediumPrice = updatePizza.MediumPrice;
            pizzaToBeEdited.LargePrice = updatePizza.LargePrice;
            dbObj.SaveChanges();
            return Redirect("/Admin/AdminPizza");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            Pizza pizzaToBeDeleted = (from pizza in dbObj.Pizzas.ToList()
                                     where pizza.PizzaID == id
                                     select pizza).First();
            dbObj.Pizzas.Remove(pizzaToBeDeleted);
            dbObj.SaveChanges();  
            return Redirect("/Admin/AdminPizza");

        }
        [Authorize]
        public ActionResult CreatePizza()
        {
            return View();
        }
        [Authorize]
        public ActionResult AfterCreatePizza(Pizza entireForm)
        {
          
            dbObj.Pizzas.Add(entireForm);
            dbObj.SaveChanges(); 
            return Redirect("/Admin/AdminPizza");
        }
    }
}