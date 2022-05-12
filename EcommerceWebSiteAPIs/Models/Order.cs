using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceWebSiteAPIs.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CusomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public double TotalPrice { get; set; }
       // [JsonIgnore]
        public virtual List<ShopingCartItem> ShopingCartItems { get; set; }
        public Order()
        {
            this.CreatedDate = DateTime.Now;
        }
    }
}
