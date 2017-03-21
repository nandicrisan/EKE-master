namespace EKE.Data.DataViewModels
{
    public class ArticleSearch : BaseSearch
    {
        public int PublishYear { get; set; }
        public string PublishSection { get; set; }

        public ArticleSearch() : base()
        {
            PublishYear = 0;
            PublishSection = string.Empty;
        }
    }
}
