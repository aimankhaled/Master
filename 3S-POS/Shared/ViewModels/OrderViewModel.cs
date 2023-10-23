using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        [Required]
        public string OrderName { get; set; }
        [Required]
        public double Amount { get; set; }
        public bool IsPaid { get; set; }
        public List<OrderItemsViewModel> Items { get; set; }
    }
}
