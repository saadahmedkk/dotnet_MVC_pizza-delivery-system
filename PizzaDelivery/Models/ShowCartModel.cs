using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaDelivery.Models
{
    public class ShowCartModel

    {
        public int PizzaID { get; set; }

        public int CartID { get; set;}
        public string Size { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public string Title { get; set;}

        
    }
}