namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_joomlawatch_goals")]
    public partial class jos_joomlawatch_goals
    {
        public int id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public int? parentId { get; set; }

        [StringLength(255)]
        public string uri_condition { get; set; }

        [StringLength(255)]
        public string get_var { get; set; }

        [StringLength(255)]
        public string get_condition { get; set; }

        [StringLength(255)]
        public string post_var { get; set; }

        [StringLength(255)]
        public string post_condition { get; set; }

        [StringLength(255)]
        public string title_condition { get; set; }

        [StringLength(255)]
        public string username_condition { get; set; }

        [StringLength(255)]
        public string ip_condition { get; set; }

        [StringLength(255)]
        public string came_from_condition { get; set; }

        [StringLength(255)]
        public string country_condition { get; set; }

        [StringLength(255)]
        public string block { get; set; }

        [StringLength(255)]
        public string redirect { get; set; }

        public bool? disabled { get; set; }
    }
}
