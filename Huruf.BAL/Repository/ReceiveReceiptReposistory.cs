namespace Huruf.BAL.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Huruf.DAL;

    public class ReceiveReceiptReposistory : Repository<VendorDelivery>
    {
        public List<GetReceiveReceiptInfo_Result> GetReceiveReceiptInfo(int? vendorDeliveryId = null, string pOWorkListId = null, bool? isException = null, string vendorDeliveryCode = null)
        {
            var lst = DbContext.GetReceiveReceiptInfo(vendorDeliveryId, pOWorkListId, vendorDeliveryCode, isException).ToList();
            return lst;
        }

        public List<GetListOfPOs_Result> GetListOfPOs(int? vendorDeliveryId = null)
        {
            var lst = DbContext.GetListOfPOs(vendorDeliveryId).ToList();
            List<GetListOfPOs_Result> objlst = new List<GetListOfPOs_Result>();

            foreach (string obj1 in lst)
            {
                GetListOfPOs_Result obj = new GetListOfPOs_Result();
                obj.SAP_PO_ID = obj1;
                objlst.Add(obj);

            }
            return objlst;
        }

        public List<VendorDelivery> GetListOfDeliveries()
        {
            var lst = Query(i => i.DeliveryStatus.Equals("INPROGRESS"));
            return lst.ToList();
        }
    }
}
