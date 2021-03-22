using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Data;
using ShoppingList.Extensions;
using ShoppingList.Models;

namespace ShoppingList.Pages.Families
{
    public class MembersModel : PageModel
    {
        private readonly GroceryContext _ctx;
        private readonly UserManager<IdentityUser> _um;

        public MembersModel(GroceryContext ctx, UserManager<IdentityUser> um)
        {
            _ctx = ctx;
            _um = um;
        }

        public bool? IsOwner { get; private set; }
        public int? FamilyId { get; private set; }
        public List<IdentityUser> Users { get; private set; }

        public void OnGet(string addId)
        {
            IsOwner = HttpContext.Session.GetIsOwner();
            FamilyId = HttpContext.Session.GetFamilyId();
            if (FamilyId == null)
                return;
            if (addId != null && IsOwner.Value)
            {
                FamilyMember member = new() {
                Id = addId,
                FamilyId = FamilyId.Value
                };
                _ctx.FamilyMembers.Add(member);
                _ctx.SaveChanges();
                
            }
            var u = _um.Users.ToList();
            Users = new();
            foreach (var user in u)
            {
                var member = _ctx.FamilyMembers.FirstOrDefault(m => m.Id == user.Id);
                if (member == null)
                    Users.Add(user);
            }
        }
    }
}
