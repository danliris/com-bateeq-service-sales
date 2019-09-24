﻿using Com.Bateeq.Sales.Test.WebApi.Utils;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface.Spinning;
using Com.Bateeq.Service.Sales.Lib.Models.Spinning;
using Com.Bateeq.Service.Sales.Lib.Services;
using Com.Bateeq.Service.Sales.Lib.ViewModels.Spinning;
using Com.Bateeq.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Com.Bateeq.Sales.Test.WebApi.Controllers
{
    public class SpinningSalesContractControllerTest : BaseControllerTest<SpinningSalesContractController, SpinningSalesContractModel, SpinningSalesContractViewModel, ISpinningSalesContract>
    {
        [Fact]
        public void Get_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(SpinningSalesContractModel));
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

        [Fact]
        public void Get_PDF_Local_OK()
        {
            var mocks = GetMocks();

            var vm = new SpinningSalesContractViewModel
            {
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel
                {
                    Id = 1,
                    Type = "Lokal",
                    Country = "a"
                },
                AccountBank = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AccountBankViewModel
                {
                    Id = 1,
                    Currency = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CurrencyViewModel()
                    {
                        Symbol = "a",
                        Description = "a",
                        Code = "a"
                    }
                },
                OrderQuantity = 0.59,
                UomUnit = "unit",
                Comodity = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CommodityViewModel()
                {
                    Name = "comm"
                },
                Quality = new Service.Sales.Lib.ViewModels.IntegrationViewModel.QualityViewModel()
                {
                    Name = "name"
                },
                TermOfPayment = new Service.Sales.Lib.ViewModels.IntegrationViewModel.TermOfPaymentViewModel()
                {
                    Name = "tp"
                },
                Agent = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AgentViewModel()
                {
                    Id = 1,
                    Name = "A",
                    Country = "a"
                },
                DeliverySchedule = DateTimeOffset.UtcNow,

            };

            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(f => f.Map<SpinningSalesContractViewModel>(It.IsAny<SpinningSalesContractModel>())).Returns(vm);

            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_PDF_Ekspor_OK()
        {
            var mocks = GetMocks();
            var mocks2 = GetMocks();
            
            var vm2 = new SpinningSalesContractViewModel
            {
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel
                {
                    Id = 1,
                    Type = "Ekspor",
                    Country = "a"
                },
                AccountBank = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AccountBankViewModel
                {
                    Id = 1,
                    Currency = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CurrencyViewModel()
                    {
                        Symbol = "a",
                        Description = "a",
                        Code = "a"
                    }
                },
                OrderQuantity = 1,
                UomUnit = "unit",
                Comodity = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CommodityViewModel()
                {
                    Name = "comm"
                },
                Quality = new Service.Sales.Lib.ViewModels.IntegrationViewModel.QualityViewModel()
                {
                    Name = "name"
                },
                TermOfPayment = new Service.Sales.Lib.ViewModels.IntegrationViewModel.TermOfPaymentViewModel()
                {
                    Name = "tp"
                },
                Agent = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AgentViewModel()
                {
                    Id = 1,
                    Name = "A",
                    Country = "a"
                },
                DeliverySchedule = DateTimeOffset.UtcNow,

            };


            //mocks.ServiceProvider.Setup(x => x.GetService<IHttpClientService>()).Returns(new HttpClientTestService());
            
            mocks2.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks2.Mapper.Setup(f => f.Map<SpinningSalesContractViewModel>(It.IsAny<SpinningSalesContractModel>())).Returns(vm2);
            var controller2 = GetController(mocks2);

            var response2 = controller2.GetPDF(1).Result;

            Assert.NotNull(response2);

        }

    }
}
