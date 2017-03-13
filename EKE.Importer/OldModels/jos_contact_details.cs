namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_contact_details")]
    public partial class jos_contact_details
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string alias { get; set; }

        [StringLength(255)]
        public string con_position { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string address { get; set; }

        [StringLength(100)]
        public string suburb { get; set; }

        [StringLength(100)]
        public string state { get; set; }

        [StringLength(100)]
        public string country { get; set; }

        [StringLength(100)]
        public string postcode { get; set; }

        [StringLength(255)]
        public string telephone { get; set; }

        [StringLength(255)]
        public string fax { get; set; }

        [StringLength(16777215)]
        public string misc { get; set; }

        [StringLength(255)]
        public string image { get; set; }

        [StringLength(20)]
        public string imagepos { get; set; }

        [StringLength(255)]
        public string email_to { get; set; }

        public bool default_con { get; set; }

        public bool published { get; set; }

        [Column(TypeName = "uint")]
        public long checked_out { get; set; }

        public DateTime checked_out_time { get; set; }

        public int ordering { get; set; }

        [Column("params", TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string name_params { get; set; }

        public int user_id { get; set; }

        public int catid { get; set; }

        public byte access { get; set; }

        [Required]
        [StringLength(255)]
        public string mobile { get; set; }

        [Required]
        [StringLength(255)]
        public string webpage { get; set; }
    }
}
