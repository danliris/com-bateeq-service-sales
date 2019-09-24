using Com.Bateeq.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Bateeq.Service.Sales.Lib.Utilities;
using Com.Bateeq.Service.Sales.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface
{
    public interface IExpiredGarmentBookingOrder : IBaseFacade<GarmentBookingOrder>
    {
        int BOCancelExpired(List<GarmentBookingOrder> list, string user);
        ReadResponse<GarmentBookingOrder> ReadExpired(int page, int size, string order, List<string> select, string keyword, string filter);
    }
}
