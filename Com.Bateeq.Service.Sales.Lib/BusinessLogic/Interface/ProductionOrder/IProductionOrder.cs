﻿using Com.Bateeq.Service.Sales.Lib.Models.ProductionOrder;
using Com.Bateeq.Service.Sales.Lib.Utilities;
using Com.Bateeq.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Bateeq.Service.Sales.Lib.ViewModels.Report;
using Com.Bateeq.Service.Sales.Lib.ViewModels.Report.OrderStatusReport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface.ProductionOrder
{
    public interface IProductionOrder : IBaseFacade<ProductionOrderModel>
    {
        Task<Tuple<List<ProductionOrderReportViewModel>, int>> GetReport(string salesContractNo, string orderNo, string orderTypeId, string processTypeId, string buyerId, string accountId, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset);
        Task<MemoryStream> GenerateExcel(string salesContractNo, string orderNo, string orderTypeId, string processTypeId, string buyerId, string accountId, DateTime? dateFrom, DateTime? dateTo, int offset);
        Task<ProductionOrderReportDetailViewModel> GetDetailReport(long no);
        Task<int> UpdateRequestedTrue(List<int> ids);
        Task<int> UpdateRequestedFalse(List<int> ids);
        Task<int> UpdateIsCompletedTrue(int id);
        Task<int> UpdateIsCompletedFalse(int id);
        Task<int> UpdateDistributedQuantity(List<int> id, List<double> distributedQuantity);
        List<YearlyOrderQuantity> GetMonthlyOrderQuantityByYearAndOrderType(int year, int orderTypeId, int timeoffset);
        List<MonthlyOrderQuantity> GetMonthlyOrderIdsByOrderType(int year, int month, int orderTypeId, int timeoffset);
    }
}
