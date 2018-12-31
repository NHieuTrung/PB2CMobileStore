namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShoppingCartItem")]
    public partial class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }

        public int ShoppingCartId { get; set; }

        public int? ProductPortableDeviceId { get; set; }

        public int? ProductAccessoryId { get; set; }

        public int Quantiny { get; set; }

        [StringLength(200)]
        public string Note { get; set; }

        public int? Discount { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public virtual ProductAccessory ProductAccessory { get; set; }

        public virtual ProductPortableDevice ProductPortableDevice { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
