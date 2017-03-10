namespace EKE.Importer.NewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AspNetRoleClaim
    {
        public int Id { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        [Required]
        [StringLength(450)]
        public string RoleId { get; set; }

        public virtual AspNetRole AspNetRole { get; set; }
    }
}
