using Com.Bateeq.Sales.Test.WebApi.Utils;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MaxWHConfirmInterfaces;
using Com.Bateeq.Service.Sales.Lib.Models.GarmentMasterPlan.MaxWHConfirmModel;
using Com.Bateeq.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MaxWHConfirmViewModels;
using Com.Bateeq.Service.Sales.WebApi.Controllers.GarmentMasterPlan.MaxWHConfirmControllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Bateeq.Sales.Test.WebApi.Controllers.GarmentMasterPlan.MaxWHConfirmControllerTests
{
    public class MaxWHConfirmControllerTest : BaseControllerTest<MaxWHConfirmController, MaxWHConfirm, MaxWHConfirmViewModel, IMaxWHConfirmFacade>
    {
        protected override MaxWHConfirmViewModel ViewModel
        {
            get
            {
                var viewModel = base.ViewModel;
                
                return viewModel;
            }
        }
    }
}
