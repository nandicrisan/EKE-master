using EKE.Data.Entities.Enums;
using System;
using System.Collections.Generic;

namespace EKE.Data.DataViewModels
{
    public class ArticleSearch : BaseSearch
    {
        public List<String> PublishYearRange { get; set; }
        public List<String> PublishSectionRange { get; set; }
        public ObjectTypeEnum ObjectType { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public bool RangeTypeYear { get; set; }
        public bool RangeTypeSection { get; set; }

        public ArticleSearch() : base()
        {
            ObjectType = ObjectTypeEnum.Article;
            Text = string.Empty;
            PublishYearRange = new List<String>();
            PublishSectionRange = new List<String>();
            Author = string.Empty;
            RangeTypeYear = true;
            RangeTypeSection = true;
        }
    }
}
