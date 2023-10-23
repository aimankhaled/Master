using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS.Server.Domain.Entities
{
    public class OrderItems
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; }
        public Orders Order { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
    }
}
