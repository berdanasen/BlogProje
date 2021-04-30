using BlogProje.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BlogProje.Helpers
{
    public static class HtmlHelpers
    {
        public static string ControllerName(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
        }

        public static string ActionName(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["action"].ToString();
        }

        public static IHtmlString ShowPostIntro(this HtmlHelper htmlHelper, string content)
        {
            if (string.IsNullOrEmpty(content))
                return htmlHelper.Raw("");

            int pos = content.IndexOf("<hr>");

            if (pos == -1)
            {
                return htmlHelper.Raw(content);
            }

            return htmlHelper.Raw(content.Substring(0, pos));
        }

        public static IHtmlString ShowPost(this HtmlHelper htmlHelper, string content)
        {
            if (string.IsNullOrEmpty(content))
                return htmlHelper.Raw("");

            int pos = content.IndexOf("<hr>");

            if (pos == -1)
            {
                return htmlHelper.Raw(content);
            }

            return htmlHelper.Raw(content.Remove(pos, 4));
        }

        public static string ProfilePhotoPath(this HtmlHelper htmlHelper)
        {
            var userId = htmlHelper.ViewContext.HttpContext.User.Identity.GetUserId();

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);

            // giriş yapan kullanıcı varsa
            if (userId != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    var user = db.Users.Find(userId);

                    // fotoğrafı varsa
                    if (user != null && !string.IsNullOrEmpty(user.Photo))
                    {
                        return urlHelper.Content("~/Upload/Profiles/" + user.Photo);
                    }
                }
            }

            return urlHelper.Content("~/Images/avatar.jpg");
        }
    }
}