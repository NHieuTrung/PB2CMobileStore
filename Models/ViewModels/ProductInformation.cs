using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class ProductInformation
    {
        public int ProductDetailId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public String Color { get; set; }
        public Nullable<int> MemorySize { get; set; }
        public Nullable<int> RamSize { get; set; }
        public string Display { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string FileImage { get; set; }
        public int Quantiny { get; set; }
    }
}