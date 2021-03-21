using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Data;
using ShoppingList.Models;

namespace ShoppingList.Pages.Lists
{
    public class DetailsModel : PageModel
    {
        private readonly ShoppingList.Data.GroceryContext _context;

        public DetailsModel(ShoppingList.Data.GroceryContext context)
        {
            _context = context;
        }

        public GroceryList GroceryList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroceryList = await _context.GroceryList
                .Include(g => g.Family).FirstOrDefaultAsync(m => m.Id == id);

            if (GroceryList == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
