using Com.Bateeq.Sales.Test.WebApi.Utils;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.GarmentSewingBlockingPlanInterfaces;
using Com.Bateeq.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Bateeq.Service.Sales.Lib.ViewModels.GarmentSewingBlockingPlanViewModels;
using Com.Bateeq.Service.Sales.WebApi.Controllers.GarmentMasterPlan.GarmentSewingBlockingPlanControllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Bateeq.Sales.Test.WebApi.Controllers.GarmentMasterPlan.GarmentSewingBlockingPlanControllerTests
{
    public class GarmentSewingBlockingPlanControllerTest : BaseControllerTest<GarmentSewingBlockingPlanController, GarmentSewingBlockingPlan, GarmentSewingBlockingPlanViewModel, IGarmentSewingBlockingPlan>
    {
        protected override GarmentSewingBlockingPlanViewModel ViewModel
        {
            get
            {
                var viewModel = base.ViewModel;
                viewModel.Items = new List<GarmentSewingBlockingPlanItemViewModel>
                {
                    new GarmentSewingBlockingPlanItemViewModel()
                };
                return viewModel;
            }
        }

        
    }
}
