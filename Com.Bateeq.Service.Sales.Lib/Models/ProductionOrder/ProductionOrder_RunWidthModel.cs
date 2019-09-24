using Com.Bateeq.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Bateeq.Service.Sales.Lib.Models.ProductionOrder
{
    public class ProductionOrder_RunWidthModel : BaseModel
    {
        public virtual ProductionOrderModel ProductionOrderModel { get; set; }
        public double Value { get; set; }
    }
}
