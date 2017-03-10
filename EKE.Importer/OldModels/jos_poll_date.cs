namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_poll_date")]
    public partial class jos_poll_date
    {
        public long id { get; set; }

        public DateTime date { get; set; }

        public int vote_id { get; set; }

        public int poll_id { get; set; }
    }
}
