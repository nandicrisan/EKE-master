namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_phocadownload_user_stat")]
    public partial class jos_phocadownload_user_stat
    {
        public int id { get; set; }

        public int fileid { get; set; }

        public int userid { get; set; }

        public int count { get; set; }

        public DateTime date { get; set; }

        public bool published { get; set; }

        public int ordering { get; set; }
    }
}
