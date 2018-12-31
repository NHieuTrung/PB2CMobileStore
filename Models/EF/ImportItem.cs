namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImportItem")]
    public partial class ImportItem
    {
        public int ImportItemId { get; set; }

        public int? ImportId { get; set; }

        public int? ProductPortableDeviceId { get; set; }

        public int? ProductAccessoriesId { get; set; }

        public int Quantiny { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual Import Import { get; set; }

        public virtual ProductPortableDevice ProductPortableDevice { get; set; }

        public virtual ProductAccessory ProductAccessory { get; set; }
    }
}
