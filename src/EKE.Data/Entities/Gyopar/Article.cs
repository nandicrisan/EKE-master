using EKE.Data.Entities.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EKE.Data.Entities.Gyopar
{
    public class Article : IEntityBase
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public string Title { get; set; }
        public string Subtitle { get; set; }
        [Required]
        public string Slug { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public string PublishedBy { get; set; }
        public string Content { get; set; }
        public string OldContent { get; set; }
        public int OrderNo { get; set; }
        public virtual Magazine Magazine { get; set; }
        public virtual ICollection<ArticleTag> ArticleTag { get; set; }
        public virtual ICollection<MediaElement> MediaElement { get; set; }
        [NotMapped]
        public List<IFormFile> Files { get; set; }
        [NotMapped]
        public string[] ArticleTags { get; set; }
    }
}
