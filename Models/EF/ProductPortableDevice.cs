namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductPortableDevice")]
    public partial class ProductPortableDevice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductPortableDevice()
        {
            ImportItems = new HashSet<ImportItem>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
            StoreImeis = new HashSet<StoreImei>();
        }

        public int ProductPortableDeviceId { get; set; }

        public int ProductId { get; set; }

        [Required]
        [StringLength(20)]
        public string Color { get; set; }

        public int MemorySize { get; set; }

        public int RamSize { get; set; }

        [Required]
        [StringLength(80)]
        public string Display { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [StringLength(100)]
        public string FileImage { get; set; }

        public int Quantiny { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImportItem> ImportItems { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreImei> StoreImeis { get; set; }
    }
}
