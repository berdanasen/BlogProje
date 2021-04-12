using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogProje.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Kategori Adı")]
        public string CategoryName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Url")]
        public string Slug { get; set; }


        public virtual ICollection<Post> Posts { get; set; }

    }
}