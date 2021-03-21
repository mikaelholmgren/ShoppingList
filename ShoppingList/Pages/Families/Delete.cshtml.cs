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
    public class DeleteModel : PageModel
    {
        private readonly ShoppingList.Data.GroceryContext _context;

        public DeleteModel(ShoppingList.Data.GroceryContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Family = await _context.Family.FindAsync(id);

            if (Family != null)
            {
                _context.Family.Remove(Family);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
