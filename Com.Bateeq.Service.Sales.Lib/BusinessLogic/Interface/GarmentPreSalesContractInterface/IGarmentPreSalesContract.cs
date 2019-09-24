﻿using Com.Bateeq.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Bateeq.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Bateeq.Service.Sales.Lib.ViewModels.GarmentPreSalesContractViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface.GarmentPreSalesContractInterface
{
    public interface IGarmentPreSalesContract : IBaseFacade<GarmentPreSalesContract>
    {
        Task<int> PreSalesPost(List<long> listId, string user);
        Task<int> PreSalesUnpost(long id, string user);
    }
}