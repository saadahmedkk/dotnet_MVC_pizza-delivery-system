//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PizzaDelivery.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AddtoCart
    {
        public int ID { get; set; }
        public int PizzaID { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Email { get; set; }
    
        public virtual Pizza Pizza { get; set; }
        public virtual UserDetail UserDetail { get; set; }
    }
}