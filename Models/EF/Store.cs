namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store")]
    public partial class Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store()
        {
            Members = new HashSet<Member>();
        }

        public int StoreId { get; set; }

        [Required]
        [StringLength(50)]
        public string StoreName { get; set; }

        [StringLength(100)]
        public string StoreAddress { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneStore { get; set; }

        [Required]
        [StringLength(50)]
        public string EmailStore { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Members { get; set; }
    }
}
