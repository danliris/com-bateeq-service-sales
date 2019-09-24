﻿using Com.Bateeq.Sales.Test.BussinesLogic.DataUtils.GarmentBookingOrderDataUtils;
using Com.Bateeq.Sales.Test.BussinesLogic.Utils;
using Com.Bateeq.Service.Sales.Lib;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics;
using Com.Bateeq.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Bateeq.Service.Sales.Lib.Services;
using Com.Bateeq.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Bateeq.Sales.Test.BussinesLogic.Facades.GarmentBookingOrderFacadeTest
{
    public class GarmetBookingOrderFacadeTest : BaseFacadeTest<SalesDbContext, GarmentBookingOrderFacade, GarmentBookingOrderLogic, GarmentBookingOrder, GarmentBookingOrderDataUtil>
    {
        private const string ENTITY = "GarmentBookingOrders";

        public GarmetBookingOrderFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var garmentBookingOrderItemLogic = new GarmentBookingOrderItemLogic(identityService, serviceProviderMock.Object, dbContext);
            var garmentBookingOrderLogic = new GarmentBookingOrderLogic(garmentBookingOrderItemLogic, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentBookingOrderLogic)))
                .Returns(garmentBookingOrderLogic);

            return serviceProviderMock;
        }

        [Fact]
        public async void BOCancel_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentBookingOrderFacade facade = new GarmentBookingOrderFacade(serviceProvider, dbContext);
            
            var data = await DataUtil(facade).GetTestData();

            var Response = await facade.BOCancel((int)data.Id, data);
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public async void BODelete_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentBookingOrderFacade facade = new GarmentBookingOrderFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            var Response = await facade.BODelete((int)data.Id, data);
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public virtual async void ReadBYNo_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentBookingOrderFacade facade = new GarmentBookingOrderFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            var Response = facade.ReadByBookingOrderNo(1, 25, "{}", new List<string>(), "", "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        [Fact]
        public virtual async void Should_Success_Validate_data()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var iserviceProvider = GetServiceProviderMock(dbContext).Object;
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.
                Setup(x => x.GetService(typeof(SalesDbContext)))
                .Returns(dbContext);

            GarmentBookingOrderFacade facade = new GarmentBookingOrderFacade(serviceProvider.Object, dbContext);

            var data = await DataUtil(facade, dbContext).GetNewData();

            var date = DateTime.Now.AddMonths(1);
            data.DeliveryDate = new DateTime(date.Year, date.Month, 23);
            

            GarmentBookingOrderViewModel vm = new GarmentBookingOrderViewModel
            {
                BookingOrderNo = data.BookingOrderNo,
                BookingOrderDate = data.BookingOrderDate,
                DeliveryDate = data.DeliveryDate,
                Items = new List<GarmentBookingOrderItemViewModel> {
                    new GarmentBookingOrderItemViewModel
                    {
                        DeliveryDate=data.DeliveryDate,
                        
                    },
                    new GarmentBookingOrderItemViewModel
                    {
                        DeliveryDate=data.DeliveryDate.AddDays(-20),
                    }
                }
            };
            ValidationContext validationContext1 = new ValidationContext(vm, serviceProvider.Object, null);
            var validationResultCreate1 = vm.Validate(validationContext1).ToList();
            Assert.True(validationResultCreate1.Count() > 0);

            //data.DeliveryDate = new DateTime(date.Year, date.Month, 3);


            GarmentBookingOrderViewModel vm1 = new GarmentBookingOrderViewModel
            {
                BookingOrderNo = data.BookingOrderNo,
                BookingOrderDate = data.BookingOrderDate,
                DeliveryDate = new DateTime(date.Year, date.Month, 3),

        };

            ValidationContext validationContext = new ValidationContext(vm1, serviceProvider.Object, null);
            var validationResultCreate = vm1.Validate(validationContext).ToList();
            Assert.True(validationResultCreate.Count() > 0);

        }
    }
}
