using EKE_Gyopar.Web.ViewModels;
using System.Collections.Generic;

namespace EKE_Gyopar.Web.ViewModels
{
    public class ArticleSerchItemVM
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public string Content { get; set; }
        public int? MagazineId { get; set; }
        public string Slug { get; set; }
        public string Subtitle { get; set; }
        public string Title { get; set; }
        public string PublishedBy { get; set; }
        public int PublishYear { get; set; }
        public string PublishSection { get; set; }
        public string AuthorName { get; set; }
    }
}

public class ArticleSerchResultVM
{
    public List<ArticleSerchItemVM> Result { get; set; }
    public int FoundItem { get; set; }
}