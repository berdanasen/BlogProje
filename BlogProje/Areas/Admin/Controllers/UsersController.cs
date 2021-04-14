using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProje.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [HttpPost]
        public ActionResult ChangeStatus(string userId, bool isEnabled)
        {
            var user = db.Users.Find(userId);
            user.IsEnabled = isEnabled;
            db.SaveChanges();
            return Json(new { success = true });
        }


    }
}