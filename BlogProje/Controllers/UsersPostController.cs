using BlogProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProje.Controllers
{
    [Authorize]
    public class UsersPostController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: UsersPost
        public ActionResult Index()
        {      
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");

            return View(db
                .Posts
                .OrderByDescending(x => x.CreationTime)
                .ToList());
        }
    }
}