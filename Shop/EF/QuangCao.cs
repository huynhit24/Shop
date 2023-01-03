namespace Shop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuangCao")]
    public partial class QuangCao
    {
        [Key]
        public int maqc { get; set; }

        [Required]
        [StringLength(255)]
        public string tenqc { get; set; }

        [Required]
        [StringLength(200)]
        public string tencongty { get; set; }

        [StringLength(100)]
        public string hinhnen { get; set; }

        [StringLength(100)]
        public string link { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngaybatdau { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngayhethan { get; set; }

        public bool trangthai { get; set; }
    }
}
