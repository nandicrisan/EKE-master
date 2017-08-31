using EKE.Data.Entities.Base;
using EKE.Data.Entities.Enums;
using System;

namespace EKE.Data.Entities.Gyopar
{
    public class MediaElement : IEntityBase
    {
        public int Id { get; set; }
        public string OriginalName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MediaTypesEnum Type { get; set; }
        public MediaTypesScope Scope { get; set; }
        public Nullable<int> AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
