using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Data;
using ShoppingList.Extensions;
using ShoppingList.Models;

namespace ShoppingList.Pages.Lists
{
    public class IndexModel : PageModel
    {
        private readonly ShoppingList.Data.GroceryContext _context;

        public IndexModel(ShoppingList.Data.GroceryContext context)
        {
            _context = context;
        }

        public IList<GroceryList> GroceryList { get;set; }
        public int? FamilyId { get; private set; }

        public async Task OnGetAsync()
        {
            var familyId = HttpContext.Session.GetFamilyId();
            FamilyId = familyId;
            if (familyId != null)
                GroceryList = await _context.GroceryList
                    .Include(g => g.Family).Where(f => f.FamilyId == familyId.Value).ToListAsync();
            else
                GroceryList = new List<GroceryList>();
        }
    }
}
