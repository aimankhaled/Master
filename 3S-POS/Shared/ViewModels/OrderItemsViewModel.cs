using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.ViewModels
{
    public class OrderItemsViewModel
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
    }
}
