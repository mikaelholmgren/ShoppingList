using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Data;
using ShoppingList.Models;

namespace ShoppingList.Pages.Families
{
    public class IndexModel : PageModel
    {
        private readonly ShoppingList.Data.GroceryContext _context;

        public IndexModel(ShoppingList.Data.GroceryContext context)
        {
            _context = context;
        }

        public IList<Family> Family { get;set; }

        public async Task OnGetAsync()
        {
            Family = await _context.Family.ToListAsync();
        }
    }
}
