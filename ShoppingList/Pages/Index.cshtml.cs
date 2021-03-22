using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppingList.Data;
using ShoppingList.Extensions;
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

        public GroceryList GroceryList { get; private set; }
        public IList<GroceryItem> GroceryItems { get; set; }
        public bool IsSignedIn { get; set; }
        public IdentityUser CurrentUser { get; set; }
        public Family CurrentFamily { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool KeepOpen { get; set; }
        [BindProperty]
        public InputModel NewInput { get; set; }
        public IList<GroceryList> FamilyLists { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? changeId, int? listId)
        {
            IsSignedIn = _sim.IsSignedIn(User);
            var familyId = HttpContext.Session.GetFamilyId();
            var selectedListId = HttpContext.Session.GetSelectedListId();
            if (listId != null)
            {
                HttpContext.Session.SetSelectedListId(listId.Value);
            }
            if (IsSignedIn && familyId == null)
            {
                CurrentUser = await _um.GetUserAsync(User);
                FamilyMember member = await _context.FamilyMembers.FirstOrDefaultAsync(i => i.Id == CurrentUser.Id);
                if (member != null)
                {
                    CurrentFamily = await _context.Family.Include(l => l.Lists).FirstOrDefaultAsync(f => f.Id == member.FamilyId);
                    HttpContext.Session.SetFamilyId(CurrentFamily.Id);
                    HttpContext.Session.SetUserId(CurrentUser.Id);
                    HttpContext.Session.SetIsOwner(CurrentFamily.OwnerUserId == CurrentUser.Id);
                    familyId = CurrentFamily.Id;
                }
            }
            else if (IsSignedIn)
            {
                CurrentUser = await _um.GetUserAsync(User);
                CurrentFamily = await _context.Family.Include(l => l.Lists).FirstOrDefaultAsync(f => f.Id == familyId);
            }
            if (selectedListId != null && selectedListId.Value != 0)
            {
                GroceryList = await _context.GroceryList.FirstOrDefaultAsync(i => i.Id == selectedListId);
                GroceryItems = await _context.GroceryItems.Where(l => l.GroceryListId == selectedListId.Value).ToListAsync();
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
            }
            else if (familyId != null)
            {
                FamilyLists = await _context.GroceryList.Where(f => f.FamilyId == familyId).ToListAsync();
            }
            if (listId != null)
                return RedirectToPage("./Index");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            IsSignedIn = _sim.IsSignedIn(User);
            if (IsSignedIn)
                CurrentUser = await _um.GetUserAsync(User);
            var selectedListId = HttpContext.Session.GetSelectedListId();
            if (NewInput != null)
            {
                if (NewInput.Id == 0)
                {
                    GroceryItem item = new()
                    {
                        ItemDescription = NewInput.ItemDescription,
                        Quantity = NewInput.Quantity,
                        ByUser = CurrentUser.Id,
                        GroceryListId = selectedListId.Value
                    };
                    var list = await _context.GroceryList.Include(i => i.Items).FirstOrDefaultAsync(l => l.Id == selectedListId.Value);
                    list.Items.Add(item);
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
