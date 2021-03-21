using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingList.Models
{
    public class GroceryItem
    {
        public int Id { get; set; }
        [Required]
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }
        public bool Purchased { get; set; }
        [Required]
        public string ByUser { get; set; }
        public int GroceryListId { get; set; }
    }
}
