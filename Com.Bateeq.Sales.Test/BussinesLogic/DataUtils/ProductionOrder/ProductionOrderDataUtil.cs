﻿using Com.Bateeq.Sales.Test.BussinesLogic.Utils;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Facades.ProductionOrder;
using Com.Bateeq.Service.Sales.Lib.Models.ProductionOrder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Bateeq.Sales.Test.BussinesLogic.DataUtils.ProductionOrder
{
    public class ProductionOrderDataUtil : BaseDataUtil<ProductionOrderFacade, ProductionOrderModel>
    {
        public ProductionOrderDataUtil(ProductionOrderFacade facade) : base(facade)
        {
        }

        public override async Task<ProductionOrderModel> GetNewData()
        {
            return new ProductionOrderModel()
            {
                AccountId = 1,
                AccountUserName = "username",
                Active = true,
                ArticleFabricEdge = "fabric",
                AutoIncreament = 1,
                BuyerCode = "code",
                BuyerId = 1,
                BuyerName = "name",
                BuyerType = "type",
                Code = "code",
                DeliveryDate = DateTimeOffset.UtcNow,
                DesignCode = "code",
                DesignMotiveCode = "code",
                DesignMotiveID = 1,
                DesignMotiveName = "name",
                DesignNumber = "number",
                DistributedQuantity = 1,
                FinishTypeCode = "code",
                FinishTypeId = 1,
                FinishTypeName = "name",
                FinishTypeRemark = "r",
                FinishWidth = "1",
                HandlingStandard = "handling",
                MaterialCode = "code",
                MaterialConstructionCode = "code",
                OrderQuantity = 100,
                Details = new List<ProductionOrder_DetailModel>() {
                    new ProductionOrder_DetailModel()
                    {
                        ColorRequest = "c",
                        Quantity = 10,
                        ColorTemplate = "ct",
                        ColorType = "type",
                        UomUnit = "unit"
                    }
                },
                OrderTypeName = "oname",
                LampStandards = new List<ProductionOrder_LampStandardModel>(),
                RunWidths = new List<ProductionOrder_RunWidthModel>()

            };
        }

    }
}
