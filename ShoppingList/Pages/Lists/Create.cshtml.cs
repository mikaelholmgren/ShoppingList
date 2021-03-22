using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingList.Data;
using ShoppingList.Extensions;
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
            FamilyId = HttpContext.Session.GetFamilyId();
            return Page();
        }

        [BindProperty]
        public GroceryList GroceryList { get; set; }
        public int? FamilyId { get; private set; }

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
