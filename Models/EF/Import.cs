namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Import")]
    public partial class Import
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Import()
        {
            ImportItems = new HashSet<ImportItem>();
        }

        public int ImportId { get; set; }

        public int? VendorId { get; set; }

        public DateTime? ImportDate { get; set; }

        public int? EmployeeId { get; set; }

        [StringLength(200)]
        public string Note { get; set; }

        public virtual Member Member { get; set; }

        public virtual Vendor Vendor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImportItem> ImportItems { get; set; }
    }
}
