namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_jlord_todo")]
    public partial class jos_jlord_todo
    {
        public int id { get; set; }

        public int uid { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string message { get; set; }

        public sbyte msg_priority { get; set; }

        public sbyte msg_status { get; set; }
    }
}
