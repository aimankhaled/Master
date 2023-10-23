using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS.Server.Domain.Entities
{
    public class Products
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
