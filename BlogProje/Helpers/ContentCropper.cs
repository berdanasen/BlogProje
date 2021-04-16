using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BlogProje.Helpers
{
    public static class ContentCropper
    {
        public static MvcHtmlString Crop(this HtmlHelper helper, string content)
        {
            StringBuilder builder = new StringBuilder();
            if (content.Length > 100)
            {
                builder.AppendLine(content.Substring(0, 100));
            }
            else
            {
                builder.AppendLine(content);
            }

            return new MvcHtmlString(builder.ToString());
        }
    }
}