using BlogProje.Models;
using BlogProje.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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

            return View(db.Posts.Include(x => x.Category).Include(x=>x.Likes).Where(x => x.Publish == true).ToList());
        }
        [HttpPost]
        public ActionResult AddFavourite(int id)
        {
            var post = db.Posts.Find(id);
            bool status = true;
            if (post != null)
            {
                var like = post.Likes.FirstOrDefault(x => x.UserId == User.Identity.GetUserId());
                if (like != null)
                {
                    db.Likes.Remove(like);
                    status = false;
                }
                else
                {
                    post.Likes.Add(new Like
                    {
                        PostId = post.Id,
                        UserId = User.Identity.GetUserId(),
                    });
                }
                
            }
            db.SaveChanges();
            return Json(new { count = db.Likes.Count(x => x.PostId == post.Id), status = status });
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

        public ActionResult ShowPost(int id, string slug)
        {
            Post post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            // veritabanındakiyle aynı değilse doğrusuna yönlendir
            if (post.Slug != slug)
            {
                return RedirectToRoute("PostRoute", new { id = id, slug = post.Slug });
            }

            return View(post);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SendComment(SendCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment
                {
                    AuthorId = User.Identity.GetUserId(),
                    AuthorName = model.AuthorName,
                    AuthorEmail = model.AuthorEmail,
                    Content = model.Content,
                    CreationTime = DateTime.Now,
                    ParentId = model.ParentId,
                    PostId = model.PostId
                };

                db.Comments.Add(comment);
                db.SaveChanges();

                return Json(comment);
            }

            var errorList = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList();

            return Json(new { Errors = errorList });
        }

        public ActionResult RecentPosts()
        {
            return PartialView("_RecentPosts", db.Posts.Include(x => x.Category).Include(x => x.Likes).Where(x => x.Publish == true).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePhoto(HttpPostedFileBase resim)
        {
            if (resim != null && resim?.ContentLength > 0)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user != null)
                {
                    var filePath = Guid.NewGuid() + Path.GetExtension(resim.FileName);
                    var savePath = Path.Combine(Server.MapPath("~/images/Uploads"), filePath);
                    var deletedPath = Path.Combine(Server.MapPath("~/images/Uploads"), user.Photo);
                    user.Photo = filePath;
                    if (System.IO.File.Exists(deletedPath))
                    {
                        System.IO.File.Delete(deletedPath);
                    }
                    resim.SaveAs(savePath);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Manage");
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