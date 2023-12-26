using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.DTO
{
    [Table("accounts")]
    public class AccountLogin
    {
        [Key]
        public Guid id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
    }
}
