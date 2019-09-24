using Com.Bateeq.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser;
using Com.Bateeq.Sales.Test.BussinesLogic.Utils;
using Com.Bateeq.Service.Sales.Lib;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Logic;
using Com.Bateeq.Service.Sales.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Bateeq.Sales.Test.BussinesLogic.Facades.Garment.GarmentMerchandiser
{
    public class RateFacadeTest : BaseFacadeTest<SalesDbContext, RateFacade, RateLogic, Rate, RateDataUtil>
    {
        private const string ENTITY = "Rate";

        public RateFacadeTest() : base(ENTITY)
        {
        }
    }
}
