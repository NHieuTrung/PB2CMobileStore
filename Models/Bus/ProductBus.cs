using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bus
{
    class ProductBus
    {
        private MobileStoreContext db = null;
        public ProductBus()
        {
            db = new MobileStoreContext();
        }
        public int findProductInformationId(string productType,int productId,int ram, int memory,string color)
        {
            int idPrd = 0;
            if (productType == "Smartphone")
            {
                idPrd=db.ProductPortableDevices.Where(m => m.ProductId == productId && m.RamSize == m.RamSize && m.MemorySize == memory && m.Color == color).Select(m=>m.ProductPortableDeviceId).FirstOrDefault();
            }
            else if (productType == "Accessory")
            {
                idPrd = db.ProductAccessories.Where(m => m.ProductId == productId&& m.Color == color).Select(m => m.ProductAccessoryId).FirstOrDefault();
            }
            return idPrd;
        }
    }
}
