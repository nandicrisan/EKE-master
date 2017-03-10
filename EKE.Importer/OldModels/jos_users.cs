namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_users")]
    public partial class jos_users
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(150)]
        public string username { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }

        [Required]
        [StringLength(25)]
        public string usertype { get; set; }

        public sbyte block { get; set; }

        public sbyte? sendEmail { get; set; }

        public byte gid { get; set; }

        public DateTime registerDate { get; set; }

        public DateTime lastvisitDate { get; set; }

        [Required]
        [StringLength(100)]
        public string activation { get; set; }

        [Column("params", TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string name_params { get; set; }
    }
}
