namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_em_mysqladmin_log")]
    public partial class jos_em_mysqladmin_log
    {
        public int id { get; set; }

        public long? rand { get; set; }

        public int? time { get; set; }
    }
}
