using BlogProje.Areas.Admin.ViewModels;
using BlogProje.Models;
using BlogProje.Utility;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            db.Posts.Remove(post);
            // todo: yorumlar cascade delete yap
            db.SaveChanges();

            return Json(new { success = true });
        }

        public ActionResult Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");

            PostEditViewModel vm = db.Posts.Select(x => new PostEditViewModel
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Content = x.Content,
                Title = x.Title,
                Slug = x.Slug
            }).FirstOrDefault(x => x.Id == id);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Post post = db.Posts.Find(model.Id);

                post.Content = model.Content;
                post.CategoryId = model.CategoryId;
                post.Title = model.Title;
                post.Slug = model.Slug;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");
            return View();
        }

        public ActionResult New()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");

            return View("Edit", new PostEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult New(PostEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    CategoryId = model.CategoryId,
                    AuthorId = User.Identity.GetUserId(),
                    CreationTime = DateTime.Now,
                    Slug = model.Slug,
                    Publish = false
                };

                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");

            return View("Edit", new PostEditViewModel());
        }

        [HttpPost]
        public ActionResult AjaxImageUpload(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0 || !file.ContentType.StartsWith("image/"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var saveFolderPath = Server.MapPath("~/upload/Posts");
            var ext = Path.GetExtension(file.FileName);
            var saveFileName = Guid.NewGuid() + ext;
            var saveFilePath = Path.Combine(saveFolderPath, saveFileName);
            file.SaveAs(saveFilePath);

            return Json(new { url = Url.Content("~/upload/Posts/" + saveFileName) });
        }

        [HttpPost]
        public ActionResult GenerateSlug(string title)
        {
            return Json(UrlService.URLFriendly(title));
        }
    }
}