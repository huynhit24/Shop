namespace Shop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MetaLaptop")]
    public partial class MetaLaptop
    {
        [Key]
        public int mameta { get; set; }

        [StringLength(255)]
        public string keymeta { get; set; }

        [StringLength(255)]
        public string valuemeta { get; set; }

        public int? malaptop { get; set; }

        public virtual Laptop Laptop { get; set; }
    }
}
