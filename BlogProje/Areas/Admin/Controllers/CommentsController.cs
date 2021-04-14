using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProje.Areas.Admin.Controllers
{
    public class CommentsController : AdminBaseController
    {
        // GET: Admin/Comments
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }
    }
}