namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_modules")]
    public partial class jos_modules
    {
        public int id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string content { get; set; }

        public int ordering { get; set; }

        [StringLength(50)]
        public string position { get; set; }

        [Column(TypeName = "uint")]
        public long checked_out { get; set; }

        public DateTime checked_out_time { get; set; }

        public bool published { get; set; }

        [StringLength(50)]
        public string module { get; set; }

        public int numnews { get; set; }

        public byte access { get; set; }

        public byte showtitle { get; set; }

        [Column("params", TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string name_params { get; set; }

        public sbyte iscore { get; set; }

        public sbyte client_id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string control { get; set; }
    }
}
