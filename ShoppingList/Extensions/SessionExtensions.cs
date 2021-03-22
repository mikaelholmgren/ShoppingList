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
        public static bool? GetIsOwner(this ISession session)
        {
            string o = session.GetString("IsOwner");
            if (o != null)
                return bool.Parse(o);
            return null;
        }
        public static void SetIsOwner(this ISession session, bool o)
        {
            session.SetString("IsOwner", o.ToString());
        }
        public static int? GetSelectedListId(this ISession session)
        {
            string id = session.GetString("SelectedListId");
            if (id != null)
                return int.Parse(id);
            return null;
        }
        public static void SetSelectedListId(this ISession session, int id)
        {
            session.SetString("SelectedListId", id.ToString());
        }
        public static string GetUserId(this ISession session)
        {
            string id = session.GetString("UserId");
            if (id != null)
                return id;
            return null;
        }
        public static void SetUserId(this ISession session, string id)
        {
            session.SetString("UserId", id);
        }

    }
}
