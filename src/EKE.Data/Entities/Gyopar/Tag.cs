using EKE.Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EKE.Data.Entities.Gyopar
{
    public class Tag : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
        public virtual ICollection<MagazineTag> MagazineTags { get; set; }
    }
}
