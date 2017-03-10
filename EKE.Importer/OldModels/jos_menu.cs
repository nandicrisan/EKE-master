namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_menu")]
    public partial class jos_menu
    {
        public int id { get; set; }

        [StringLength(75)]
        public string menutype { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string alias { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string link { get; set; }

        [Required]
        [StringLength(50)]
        public string type { get; set; }

        public bool published { get; set; }

        [Column(TypeName = "uint")]
        public long parent { get; set; }

        [Column(TypeName = "uint")]
        public long componentid { get; set; }

        public int? sublevel { get; set; }

        public int? ordering { get; set; }

        [Column(TypeName = "uint")]
        public long checked_out { get; set; }

        public DateTime checked_out_time { get; set; }

        public int pollid { get; set; }

        public sbyte? browserNav { get; set; }

        public byte access { get; set; }

        public byte utaccess { get; set; }

        [Column("params", TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string name_params { get; set; }

        [Column(TypeName = "uint")]
        public long lft { get; set; }

        [Column(TypeName = "uint")]
        public long rgt { get; set; }

        [Column(TypeName = "uint")]
        public long home { get; set; }
    }
}
