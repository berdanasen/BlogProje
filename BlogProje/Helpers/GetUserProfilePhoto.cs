using BlogProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BlogProje.Helpers
{
    public static class GetUserProfilePhoto
    {
        public static string GetUserImage(this IIdentity identity)
        {
            ApplicationDbContext db;
            
            using (db = new ApplicationDbContext())
            {
                return db.Users.FirstOrDefault(x => x.UserName == identity.Name).Photo;
            }
        }
    }
}