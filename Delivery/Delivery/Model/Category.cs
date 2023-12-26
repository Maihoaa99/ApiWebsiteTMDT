using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Model
{
    [Table("categories")]
    public class Category
    {
        [Key]
        public Guid id { get; set; }
        public int code { get; set; }
        public string name { get; set; }
        public ICollection<Product> products { get; set; }

        public Category()
        {
            products = new HashSet<Product>();
            id = Guid.NewGuid();
        }
    }
}
