using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class ShoppingCartItemBrowser
    {
        public int ProductDetailId { get; set; }
        public int ProductId { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }
        public Nullable<int> MemorySize { get; set; }
        public Nullable<int> RamSize { get; set; }
        public Nullable<decimal> Price { get; set; }
        public String FileImage { get; set; }
        public int Quantiny { get; set; }
        public String ProductTypeName { get; set; }
    }
}
