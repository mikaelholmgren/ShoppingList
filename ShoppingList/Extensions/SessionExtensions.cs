using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingList.Extensions
{
    public static class SessionExtensions
    {
        public static int? GetFamilyId(this ISession session)
        {
            string id = session.GetString("FamilyId");
            if (id != null)
                return int.Parse(id);
            return null;
        }
        public static void SetFamilyId(this ISession session, int id)
        {
            session.SetString("FamilyId", id.ToString());
        }
    }
}
