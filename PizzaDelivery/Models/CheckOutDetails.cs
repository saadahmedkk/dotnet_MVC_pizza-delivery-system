using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaDelivery.Models
{
    public class CheckOutDetails
    {
        public int AddressId { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public int OrderID { get; set; }
        public int CheckOutPrice { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }
}