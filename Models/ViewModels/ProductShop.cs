using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class ProductShop
    {
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public string FileImage { get; set; }
        public Nullable<decimal> PriceCheapestType { get; set; }
        public bool Stocking { get; set; } = true;
        public Nullable<System.DateTime> RealeaseDate { get; set; }
        public Nullable<int> CheapestProductId { get; set; }
        public String Brand { get; set; }
    }
}