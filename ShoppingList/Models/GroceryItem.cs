using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingList.Models
{
    public class GroceryItem
    {
        public int Id { get; set; }
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }
        public bool Purchased { get; set; }
        public string ByUser { get; set; }
    }
}
