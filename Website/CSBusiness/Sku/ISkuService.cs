using System.Collections.Generic;

namespace CSBusiness
{
    public interface ISkuService
    {
        List<Sku> GetAllSkus();
        List<Sku> GetAllSkus(int startRec, int endRec, out int totalCount);
        void  InsertSku(Sku skuItem);
        Sku GetSkuByID(int skuID);
    }
}