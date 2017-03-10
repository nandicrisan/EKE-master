namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_messages")]
    public partial class jos_messages
    {
        [Key]
        [Column(TypeName = "uint")]
        public long message_id { get; set; }

        [Column(TypeName = "uint")]
        public long user_id_from { get; set; }

        [Column(TypeName = "uint")]
        public long user_id_to { get; set; }

        [Column(TypeName = "uint")]
        public long folder_id { get; set; }

        public DateTime date_time { get; set; }

        public int state { get; set; }

        [Column(TypeName = "uint")]
        public long priority { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string subject { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string message { get; set; }
    }
}
