using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Model
{
    [Table("accounts")]
    public class Accounts
    {
        [Key]
        public Guid id { get; set; }
        public string? name { get; set; }
        public int? age { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public string password { get; set; }

    }
}
