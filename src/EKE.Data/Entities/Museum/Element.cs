using EKE.Data.Entities.Base;
using EKE.Data.Entities.Gyopar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKE.Data.Entities.Museum
{
    public class Element : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public bool Selected { get; set; }

        public virtual ElementCategory Category { get; set; }
        public virtual ICollection<ElementTag> Tags { get; set; }
        public virtual ICollection<MediaElement> MediaElement { get; set; }

        public Element()
        {
            DateCreated = DateTime.Now;
        }
    }
}
