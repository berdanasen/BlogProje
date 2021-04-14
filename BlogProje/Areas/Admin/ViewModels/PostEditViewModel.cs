using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProje.Areas.Admin.ViewModels
{
    public class PostEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name = "İçerik")]
        [AllowHtml]
        public string Content { get; set; }

        [MaxLength(200)]
        [Display(Name = "Kısa URL")]
        public string Slug { get; set; }
    }
}