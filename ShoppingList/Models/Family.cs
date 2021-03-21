using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingList.Models
{
    public class Family
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<FamilyMember> Members { get; set; } = new HashSet<FamilyMember>();
        [Required]
        public string OwnerUserId { get; set; }
        public ICollection<GroceryList> Lists { get; set; }
    }
}
