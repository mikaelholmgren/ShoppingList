using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingList.Models
{
    public class GroceryList
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<GroceryItem> Items { get; set; }
        [Required]
        public int FamilyId { get; set; }
        public Family Family { get; set; }
    }

}
