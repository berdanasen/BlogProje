using BlogProje.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProje.Areas.Admin.Controllers
{
    public class CategoriesController : AdminBaseController
    {
        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(Category model)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(model);
                db.SaveChanges();
                TempData["successMessage"] = "Kategori başarıyla oluşturuldu.";
                return RedirectToAction("Index");
            }

            return View();
        }
        
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            if (category.Posts.Count > 0)
            {
                return Json(new { success = false, error = "Silmek istediğiniz kategoriye ait yazılar bulunduğundan silinememektedir." });
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Json(new { success = true });
        }

        public ActionResult Edit(int id)
        {
            return View("Edit", db.Categories.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                TempData["successMessage"] = "Kategori ismi başarıyla değiştirildi.";
                return RedirectToAction("Index");
            }

            return View("Edit", model);
        }
    }
}