using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace EcommerceWebSiteAPIs.Models
{
    public class Product
    {
        public int id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("Category")]
        public int CateogryID { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Img { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }

    }
}
