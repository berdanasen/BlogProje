using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProje.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Author")]
        [Display(Name = "Yazar")]
        public string AuthorId { get; set; }

        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [MaxLength(200)]
        [Display(Name = "Url")]
        public string Slug { get; set; }

        [Display(Name = "İçerik")]
        [AllowHtml]
        public string Content { get; set; }

        
        public DateTime? CreationTime { get; set; }

        public bool Publish { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}