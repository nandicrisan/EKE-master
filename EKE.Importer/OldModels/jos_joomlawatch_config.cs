namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_joomlawatch_config")]
    public partial class jos_joomlawatch_config
    {
        public int id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string value { get; set; }
    }
}
