namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonDatHang")]
    public partial class DonDatHang
    {
        [Key]
        public long ID_DDH { get; set; }

        public long? UserID { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string ShipName { get; set; }

        [StringLength(50)]
        public string ShipMobie { get; set; }

        [StringLength(250)]
        public string ShipAddress { get; set; }

        [StringLength(50)]
        public string ShipEmail { get; set; }

        public bool? Status { get; set; }
    }
}
