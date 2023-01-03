namespace Shop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LienHe")]
    public partial class LienHe
    {
        [Key]
        public int malienhe { get; set; }

        [Required]
        [StringLength(50)]
        public string hoten { get; set; }

        [StringLength(254)]
        public string email { get; set; }

        [StringLength(50)]
        public string dienthoai { get; set; }

        [StringLength(100)]
        public string website { get; set; }

        [Column(TypeName = "ntext")]
        public string noidung { get; set; }

        public bool? trangthai { get; set; }
    }
}
