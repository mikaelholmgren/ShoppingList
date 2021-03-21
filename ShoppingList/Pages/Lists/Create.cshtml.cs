using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingList.Data;
using ShoppingList.Models;

namespace ShoppingList.Pages.Lists
{
    public class CreateModel : PageModel
    {
        private readonly ShoppingList.Data.GroceryContext _context;

        public CreateModel(ShoppingList.Data.GroceryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FamilyId"] = new SelectList(_context.Family, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public GroceryList GroceryList { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.GroceryList.Add(GroceryList);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
