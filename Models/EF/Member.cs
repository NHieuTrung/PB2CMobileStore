namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Member")]
    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            AddressMembers = new HashSet<AddressMember>();
            Imports = new HashSet<Import>();
            ShoppingCarts = new HashSet<ShoppingCart>();
            ShoppingCarts1 = new HashSet<ShoppingCart>();
        }

        public int MemberId { get; set; }

        [StringLength(100)]
        public string MemberEmail { get; set; }

        [StringLength(50)]
        public string MemberPassword { get; set; }

        [Required]
        [StringLength(120)]
        public string FullName { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public int? GenderTypeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [Column(TypeName = "date")]
        public DateTime RegDate { get; set; }

        [StringLength(100)]
        public string FileImage { get; set; }

        public int? StoreId { get; set; }

        [StringLength(200)]
        public string MemberGoogleId { get; set; }

        [StringLength(200)]
        public string MemberFacebookId { get; set; }

        public int MemberTypeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AddressMember> AddressMembers { get; set; }

        public virtual GenderType GenderType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Import> Imports { get; set; }

        public virtual MemberType MemberType { get; set; }

        public virtual Store Store { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCart> ShoppingCarts1 { get; set; }
    }
}
