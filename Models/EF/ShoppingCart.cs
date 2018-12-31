namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShoppingCart")]
    public partial class ShoppingCart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShoppingCart()
        {
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }

        public int ShoppingCartId { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ShippedDate { get; set; }

        public int? EmployeeId { get; set; }

        public int? PromotionId { get; set; }

        public int PaymentTypeId { get; set; }

        public int AddressMemberId { get; set; }

        public int StatusCart { get; set; }

        public virtual AddressMember AddressMember { get; set; }

        public virtual Member Member { get; set; }

        public virtual Member Member1 { get; set; }

        public virtual PaymentType PaymentType { get; set; }

        public virtual Promotion Promotion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
