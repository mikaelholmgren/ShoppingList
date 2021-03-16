using Microsoft.EntityFrameworkCore;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingList.Data
{
    public class GroceryContext : DbContext
    {
        public GroceryContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<GroceryItem> GroceryItems { get; set; }
    }
}
