namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_sefurls")]
    public partial class jos_sefurls
    {
        public int id { get; set; }

        public int cpt { get; set; }

        [Required]
        [StringLength(255)]
        public string sefurl { get; set; }

        [Required]
        [StringLength(255)]
        public string origurl { get; set; }

        [StringLength(20)]
        public string Itemid { get; set; }

        [StringLength(255)]
        public string metadesc { get; set; }

        [StringLength(255)]
        public string metakey { get; set; }

        [StringLength(255)]
        public string metatitle { get; set; }

        [StringLength(30)]
        public string metalang { get; set; }

        [StringLength(30)]
        public string metarobots { get; set; }

        [StringLength(30)]
        public string metagoogle { get; set; }

        [StringLength(255)]
        public string canonicallink { get; set; }

        [Column(TypeName = "date")]
        public DateTime dateadd { get; set; }

        public int priority { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string trace { get; set; }

        public bool enabled { get; set; }

        public bool locked { get; set; }

        public bool sef { get; set; }

        public bool sm_indexed { get; set; }

        [Column(TypeName = "date")]
        public DateTime sm_date { get; set; }

        [Required]
        [StringLength(20)]
        public string sm_frequency { get; set; }

        [Required]
        [StringLength(10)]
        public string sm_priority { get; set; }
    }
}
