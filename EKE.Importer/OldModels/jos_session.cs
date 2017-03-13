namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_session")]
    public partial class jos_session
    {
        [StringLength(150)]
        public string username { get; set; }

        [StringLength(14)]
        public string time { get; set; }

        [Key]
        [StringLength(200)]
        public string session_id { get; set; }

        public sbyte? guest { get; set; }

        public int? userid { get; set; }

        [StringLength(50)]
        public string usertype { get; set; }

        public byte gid { get; set; }

        public byte client_id { get; set; }

        [StringLength(1073741823)]
        public string data { get; set; }
    }
}
