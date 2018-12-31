namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductAccessory")]
    public partial class ProductAccessory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductAccessory()
        {
            ImportItems = new HashSet<ImportItem>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }

        public int ProductAccessoryId { get; set; }

        public int ProductId { get; set; }

        [Required]
        [StringLength(20)]
        public string Color { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [StringLength(100)]
        public string ImageFile { get; set; }

        public int Quantiny { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImportItem> ImportItems { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
