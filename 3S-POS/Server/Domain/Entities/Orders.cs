using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS.Server.Domain.Entities
{
    public class Orders
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public double Amount { get; set; }
        public bool IsPaid { get; set; }
        public List<OrderItems> Items { get; set; }
    }
}
