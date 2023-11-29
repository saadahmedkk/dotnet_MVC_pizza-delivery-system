using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaDelivery.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        Pizza_DeliveryEntities dbObj = new Pizza_DeliveryEntities();
        public List<Pizza> GetPizzas()
        {
            return dbObj.Pizzas.ToList();
        }


        public ActionResult About()
        {
           
            return View("About");
        }


        public ActionResult Index()
        {
            ViewBag.UserName = User.Identity.Name;
            var allpizzas = GetPizzas();


            ViewModel viewModel = new ViewModel();
            viewModel.pizzas = allpizzas;


            return View(viewModel);

        }


        [Authorize]
        public ActionResult AddtoCart(AddtoCart items)
        {
            AddtoCart addtoCart = new AddtoCart()
            {
                Size = items.Size,
                PizzaID = items.PizzaID,
                Quantity = items.Quantity,
                Email = User.Identity.Name,
                Price = int.Parse(items.Size.Split(',')[1]),

            };

            var numofPizzaIDpresentCount = (from pizza in dbObj.AddtoCarts.ToList()
                                            where pizza.PizzaID == items.PizzaID && pizza.Size == items.Size && pizza.Email == User.Identity.Name
                                            select pizza).ToList().Count();
            if (numofPizzaIDpresentCount == 0)
            {
                dbObj.AddtoCarts.Add(addtoCart);
                dbObj.SaveChanges();
                return Redirect("/Home/index");
            }
            else
            {
                return Redirect("/Home/ShowCart");
            }




        }


        public ActionResult ShowCart()
        {
            try
            {
                var result = from cart in dbObj.AddtoCarts.ToList()
                             join pizza in dbObj.Pizzas.ToList() on cart.PizzaID equals pizza.PizzaID
                             where cart.Email == User.Identity.Name
                             select new ShowCartModel
                             {
                                 CartID = cart.ID,
                                 PizzaID = pizza.PizzaID,
                                 Title = pizza.Title,
                                 Size = cart.Size,
                                 Quantity = cart.Quantity,
                                 Price = cart.Quantity * cart.Price,
                             };
                int totalprice = result.Sum(cart => cart.Price);
                

                var viewModel = new ViewModel
                {
                    CartItems = result,
                    TotalPrice = totalprice,
                    
                };
                
                return View(viewModel);


            }
            catch
            {
                return Redirect("/Login/Signin");
            }

        }

        public ActionResult DeleteCartItem(int id, String size)
        {
            var itemtobedeleted = (from cart in dbObj.AddtoCarts.ToList()
                                   where cart.PizzaID == id && cart.Size == size
                                   select cart).First();

            dbObj.AddtoCarts.Remove(itemtobedeleted);
            dbObj.SaveChanges();
            return Redirect("/Home/ShowCart");

        }
    }
}