namespace EKE.Data.DataViewModels
{
    public class BaseSearch
    {
        public string Keyword { get; set; }

        public string ClearKeyword
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Keyword))
                    return null;
                return Keyword.Trim().ToLower();
            }
        }
        public int Page { get; set; }
        public int Display { get; set; }
        //True = orderby, False = orderbyDescending
        public bool OrderDirection { get; set; }
        public int Order { get; set; }
        public int From
        {
            get
            {
                return (Page - 1) * Display;
            }
        }

        public BaseSearch()
        {
            Page = 1;
            Display = 10;
        }
    }
}
