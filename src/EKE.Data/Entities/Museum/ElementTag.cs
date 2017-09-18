using EKE.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKE.Data.Entities.Museum
{
    public class ElementTag : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }

        public ElementTag()
        {
            DateCreated = DateTime.Now;
        }
    }
}
