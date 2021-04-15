using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProje.Areas.Admin.Controllers
{
    public class DashboardController : AdminBaseController
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");

            return View(db
                .Posts
                .OrderByDescending(x => x.CreationTime)
                .ToList());

        }

        public ActionResult Publish(int id, bool publish)
        {
            var post = db.Posts.Find(id);

            if (post != null)
            {
                post.Publish = publish;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new { success = publish });
        }
    }
}