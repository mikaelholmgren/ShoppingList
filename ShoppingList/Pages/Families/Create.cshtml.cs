using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingList.Data;
using ShoppingList.Models;

namespace ShoppingList.Pages.Families
{
    public class CreateModel : PageModel
    {
        private readonly ShoppingList.Data.GroceryContext _context;
        private readonly UserManager<IdentityUser> _um;

        public CreateModel(ShoppingList.Data.GroceryContext context, UserManager<IdentityUser> um)
        {
            _context = context;
            _um = um;
        }

        public IActionResult OnGet()
        {
            var curUserId = _um.GetUserId(User);
//            Family = _context.FamilyMembers.Find(curUserId).Family;
            return Page();
        }

        [BindProperty]
        public Family Family { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var curUserId = _um.GetUserId(User);
            Family.OwnerUserId = curUserId;
            _context.Family.Add(Family);
            FamilyMember member = new();
            member.Id = curUserId;
            Family.Members.Add(member);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
