using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Model
{
    [Table("products")]
    public class Product
    {
        [Key]
        public Guid id { get; set; }
        public int code { get; set; }
        public string? name { get; set; }
        public Guid category_id { get; set; }
        [ForeignKey("category_id")]
        public Category? category { get; set; }
        public Guid unit_id { get; set; }
        public string? unit_price { get; set; }
        public string? image { get; set;}
        public bool is_hidden { get; set; }
    }
}
