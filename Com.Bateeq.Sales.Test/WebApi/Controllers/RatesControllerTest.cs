﻿using Com.Bateeq.Sales.Test.WebApi.Utils;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Bateeq.Service.Sales.Lib.Models;
using Com.Bateeq.Service.Sales.Lib.Utilities;
using Com.Bateeq.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Bateeq.Service.Sales.WebApi.Controllers;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Com.Bateeq.Sales.Test.WebApi.Controllers
{
    public class RatesControllerTest : BaseControllerTest<RatesController, Rate, RateViewModel, IRate>
    {
        [Fact]
        public void Validate_Default()
        {
            RateViewModel defaultViewModel = new RateViewModel();

            var defaultValidationResult = defaultViewModel.Validate(null);
            Assert.True(defaultValidationResult.Count() > 0);
        }

        [Fact]
        public void Validate_Filled()
        {
            var mock = GetMocks();

            mock.Facade.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ReadResponse<Rate>(new List<Rate>(), 10, new Dictionary<string, string>(), new List<string>()));

            mock.ServiceProvider.Setup(s => s.GetService(typeof(IRate)))
                .Returns(mock.Facade.Object);

            RateViewModel filledViewModel = new RateViewModel
            {
                Name = "Name",
                Unit = new UnitViewModel { Id = 1 },
                Value = 0
            };

            ValidationContext validationContext = new ValidationContext(filledViewModel, mock.ServiceProvider.Object, null);

            var filledValidationResult = filledViewModel.Validate(validationContext);
            Assert.True(filledValidationResult.Count() > 0);
        }
    }
}
