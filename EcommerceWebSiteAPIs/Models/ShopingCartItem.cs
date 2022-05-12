using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceWebSiteAPIs.Models
{
    public class ShopingCartItem
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public double ItemPrice { get; set; }
        public int SelectedQuantity { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}
