namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMucTinTuc")]
    public partial class DanhMucTinTuc
    {
        [Key]
        public long ID_DMTT { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public long? ParentID { get; set; }

        public int? DisplayOder { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public bool? Status { get; set; }
    }
}
