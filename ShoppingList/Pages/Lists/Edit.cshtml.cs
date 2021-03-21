using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Data;
using ShoppingList.Models;

namespace ShoppingList.Pages.Lists
{
    public class EditModel : PageModel
    {
        private readonly ShoppingList.Data.GroceryContext _context;

        public EditModel(ShoppingList.Data.GroceryContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["FamilyId"] = new SelectList(_context.Family, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(GroceryList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroceryListExists(GroceryList.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GroceryListExists(int id)
        {
            return _context.GroceryList.Any(e => e.Id == id);
        }
    }
}
