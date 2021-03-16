using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppingList.Data;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GroceryContext _context;
        private readonly SignInManager<IdentityUser> _sim;
        private readonly UserManager<IdentityUser> _um;

        public IndexModel(GroceryContext ctx, SignInManager<IdentityUser> sim, UserManager<IdentityUser> um)
        {
            _context = ctx;
            _sim = sim;
            _um = um;
        }
        public IList<GroceryItem> GroceryItems { get; set; }
        public bool IsSignedIn { get; set; }
        public IdentityUser CurrentUser { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool KeepOpen { get; set; }
        [BindProperty]
        public InputModel NewInput { get; set; }
        public async Task<IActionResult> OnGetAsync(int? changeId)
        {
            IsSignedIn = _sim.IsSignedIn(User);
            if (IsSignedIn)
                CurrentUser = await _um.GetUserAsync(User);
            GroceryItems = await _context.GroceryItems.ToListAsync();
            if (changeId != null)
            {
                var item = _context.GroceryItems.Find(changeId.Value);
                if (item != null && CurrentUser.Id == item.ByUser)
                {
                    NewInput = new()
                    {
                        Id = item.Id,
                        ItemDescription = item.ItemDescription,
                        Quantity = item.Quantity
                    };
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            IsSignedIn = _sim.IsSignedIn(User);
            if (IsSignedIn)
                CurrentUser = await _um.GetUserAsync(User);

            if (NewInput != null)
            {
                if (NewInput.Id == 0)
                {
                    GroceryItem item = new()
                    {
                        ItemDescription = NewInput.ItemDescription,
                        Quantity = NewInput.Quantity,
                        ByUser = CurrentUser.Id
                    };
                    _context.GroceryItems.Add(item);
                    _context.SaveChanges();
                }
                else
                {
                    GroceryItem item = _context.GroceryItems.Find(NewInput.Id);
                    item.ItemDescription = NewInput.ItemDescription;
                    item.Quantity = NewInput.Quantity;


                    _context.SaveChanges();
                }

            }
            if (NewInput.Id == 0)
                return RedirectToPage(new { KeepOpen = true });
            else
                return RedirectToPage();
        }
        public async Task<IActionResult> OnGetRemoveItemAsync(int id)
        {
            IsSignedIn = _sim.IsSignedIn(User);
            if (IsSignedIn)
                CurrentUser = await _um.GetUserAsync(User);

            var item = await _context.GroceryItems.FindAsync(id);
            if (item != null && IsSignedIn && CurrentUser.Id == item.ByUser)
            {
                _context.GroceryItems.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnGetUpdateItemStateAsync(int id)
        {
            if (!_sim.IsSignedIn(User))
                return RedirectToPage("Index");
            var item = await _context.GroceryItems.FindAsync(id);
            if (item != null)
            {
                item.Purchased = !item.Purchased;
                _context.SaveChanges();
            }
            return RedirectToPage("Index");
        }
    }
}
