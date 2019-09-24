using Com.Bateeq.Service.Sales.Lib.Models.GarmentSalesContractModel;
using Com.Bateeq.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Bateeq.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface.GarmentSalesContractInterface
{
    public interface IGarmentSalesContract : IBaseFacade<GarmentSalesContract>
    {
        Task<int> UpdatePrinted(int id, GarmentSalesContract model);
    }


}
