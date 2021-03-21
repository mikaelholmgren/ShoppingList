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
    public class IndexModel : PageModel
    {
        private readonly ShoppingList.Data.GroceryContext _context;

        public IndexModel(ShoppingList.Data.GroceryContext context)
        {
            _context = context;
        }

        public IList<GroceryList> GroceryList { get;set; }

        public async Task OnGetAsync()
        {
            GroceryList = await _context.GroceryList
                .Include(g => g.Family).ToListAsync();
        }
    }
}
