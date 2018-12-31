namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StoreImei")]
    public partial class StoreImei
    {
        [Key]
        [StringLength(100)]
        public string ImeiPhone { get; set; }

        public int ProductPortableDeviceId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateActiveWarranty { get; set; }

        public int StatusPhone { get; set; }

        public virtual ProductPortableDevice ProductPortableDevice { get; set; }
    }
}
