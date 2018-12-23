namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMucSP")]
    public partial class DanhMucSP
    {
        [Key]
        public long ID_DMSP { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        public long? ParentID { get; set; }

        public int? DisplayOder { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public bool? Status { get; set; }

        public DateTime? TopHot { get; set; }
    }
}
