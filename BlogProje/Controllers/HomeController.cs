using BlogProje.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProje.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            ViewBag.TopBir = db.Posts.Include(x => x.Category).OrderByDescending(x => x.CreationTime).FirstOrDefault();
            ViewBag.TopIki = db.Posts.Include(x => x.Category).OrderByDescending(x => x.CreationTime).Skip(1).FirstOrDefault();
            ViewBag.TopUc = db.Posts.Include(x => x.Category).OrderByDescending(x => x.CreationTime).Skip(2).FirstOrDefault();

            return View(db.Posts.Include(x => x.Category).Where(x => x.Publish == true).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}