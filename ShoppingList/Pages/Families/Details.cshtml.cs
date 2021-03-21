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
    public class DetailsModel : PageModel
    {
        private readonly ShoppingList.Data.GroceryContext _context;

        public DetailsModel(ShoppingList.Data.GroceryContext context)
        {
            _context = context;
        }

        public Family Family { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Family = await _context.Family.FirstOrDefaultAsync(m => m.Id == id);

            if (Family == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
