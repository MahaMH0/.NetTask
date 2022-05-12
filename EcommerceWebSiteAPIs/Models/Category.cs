using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceWebSiteAPIs.Models
{
    public class Category
    {
        public int id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
