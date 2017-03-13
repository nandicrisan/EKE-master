namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_xmap_sitemap")]
    public partial class jos_xmap_sitemap
    {
        public int id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public int? expand_category { get; set; }

        public int? expand_section { get; set; }

        public int? show_menutitle { get; set; }

        public int? columns { get; set; }

        public int? exlinks { get; set; }

        [StringLength(255)]
        public string ext_image { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string menus { get; set; }

        [StringLength(255)]
        public string exclmenus { get; set; }

        public int? includelink { get; set; }

        public int? usecache { get; set; }

        public int? cachelifetime { get; set; }

        [StringLength(255)]
        public string classname { get; set; }

        public int? count_xml { get; set; }

        public int? count_html { get; set; }

        public int? views_xml { get; set; }

        public int? views_html { get; set; }

        public int? lastvisit_xml { get; set; }

        public int? lastvisit_html { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string excluded_items { get; set; }

        public int? compress_xml { get; set; }
    }
}
