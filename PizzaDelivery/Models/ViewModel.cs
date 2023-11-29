using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PizzaDelivery.Models
{
    public class ViewModel
    {
        public List<Pizza> pizzas { get; set; }
        public List<UserDetail> userDetails { get; set; }
        public List<UserOrder> userOrders { get; set; }

        public List<AddtoCart> addtoCarts { get; set;}

        public IEnumerable<ShowCartModel> CartItems { get; set; }

        public IEnumerable<CheckOutDetails> checkOutItems { get; set; }
        public int TotalPrice { get; set; }
       

    }
}