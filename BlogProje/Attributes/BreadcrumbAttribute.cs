using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProje.Attributes
{
    public class BreadcrumbAttribute : Attribute
    {
        public string Name { get; set; }

        public BreadcrumbAttribute(string name)
        {
            Name = name;
        }
    }
}