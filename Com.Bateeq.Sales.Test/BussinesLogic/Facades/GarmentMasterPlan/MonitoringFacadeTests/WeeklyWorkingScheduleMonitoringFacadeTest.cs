﻿using Com.Bateeq.Sales.Test.BussinesLogic.DataUtils.GarmentBookingOrderDataUtils;
using Com.Bateeq.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.GarmentSewingBlockingPlanDataUtils;
using Com.Bateeq.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.MaxWHConfirmDataUtils;
using Com.Bateeq.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.WeeklyPlanDataUtils;
using Com.Bateeq.Service.Sales.Lib;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.GarmentSewingBlockingPlanFacades;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MaxWHConfirmFacades;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MonitoringFacades;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.WeeklyPlanFacades;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MonitoringInterfaces;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.GarmentSewingBlockingPlanLogics;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MaxWHConfirmLogics;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.WeeklyPlanLogics;
using Com.Bateeq.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Bateeq.Sales.Test.BussinesLogic.Facades.GarmentMasterPlan.MonitoringFacadeTests
{
    public class WeeklyWorkingScheduleMonitoringFacadeTest
    {
        private const string ENTITY = "WeeklyWorkingScheduleMonitoring";

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private SalesDbContext DbContext(string testName)
        {
            DbContextOptionsBuilder<SalesDbContext> optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            SalesDbContext dbContext = new SalesDbContext(optionsBuilder.Options);

            return dbContext;
        }

        protected virtual Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentSewingBlockingPlanLogic)))
                .Returns(new GarmentSewingBlockingPlanLogic(identityService, dbContext));

            serviceProviderMock
                .Setup(x => x.GetService(typeof(WeeklyWorkingScheduleMonitoringLogic)))
                .Returns(new WeeklyWorkingScheduleMonitoringLogic(identityService, dbContext));

            return serviceProviderMock;
        }

        private GarmentSewingBlockingPlanDataUtil DataUtil(GarmentSewingBlockingPlanFacade facade, SalesDbContext dbContext)
        {
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var WeekserviceProvider = GetWeekServiceProviderMock(dbContext).Object;
            var BOserviceProvider = GetBOServiceProviderMock(dbContext).Object;
            var WHServiceProviderMock = GetWHServiceProviderMock(dbContext).Object;

            var weeklyPlanFacade = new WeeklyPlanFacade(WeekserviceProvider, dbContext);
            var weeklyPlanDataUtil = new WeeklyPlanDataUtil(weeklyPlanFacade);

            var bookingOrderFacade = new GarmentBookingOrderFacade(BOserviceProvider, dbContext);
            var garmentBookingOrderDataUtil = new GarmentBookingOrderDataUtil(bookingOrderFacade);

            var maxWHConfirmFacade = new MaxWHConfirmFacade(WHServiceProviderMock, dbContext);
            var maxWHConfirmDataUtil = new MaxWHConfirmDataUtil(maxWHConfirmFacade);

            var garmentSewingBlockingPlanFacade = new GarmentSewingBlockingPlanFacade(serviceProvider, dbContext);
            var garmentSewingBlockingPlanDataUtil = new GarmentSewingBlockingPlanDataUtil(garmentSewingBlockingPlanFacade, weeklyPlanDataUtil, garmentBookingOrderDataUtil, maxWHConfirmDataUtil);



            return garmentSewingBlockingPlanDataUtil;
        }

        protected virtual Mock<IServiceProvider> GetWeekServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(WeeklyPlanLogic)))
                .Returns(new WeeklyPlanLogic(identityService, dbContext));

            return serviceProviderMock;
        }
        protected virtual Mock<IServiceProvider> GetWHServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(MaxWHConfirmLogic)))
                .Returns(new MaxWHConfirmLogic(identityService, dbContext));

            return serviceProviderMock;
        }

        protected virtual Mock<IServiceProvider> GetBOServiceProviderMock(SalesDbContext dbContext)
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
        public async void Get_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSewingBlockingPlanFacade facade = new GarmentSewingBlockingPlanFacade(serviceProvider, dbContext);

            var dataNew = await DataUtil(facade, dbContext).GetNewData();
            dataNew.Items.First().DeliveryDate = dataNew.Items.First().StartDate.AddDays(-30);
            await facade.CreateAsync(dataNew);

            IWeeklyWorkingScheduleMonitoringFacade weeklyWorkingScheduleMonitoringFacade = new WeeklyWorkingScheduleMonitoringFacade(serviceProvider, dbContext);

            //data.Items.First().DeliveryDate = data.Items.First().StartDate.AddDays(-30);

            var filter = new
            {
                year = dataNew.Items.First().Year,
                week= dataNew.Items.First().WeekNumber
            };
            var Response = weeklyWorkingScheduleMonitoringFacade.Read(filter: JsonConvert.SerializeObject(filter));

            Assert.NotEqual(Response.Item2, 0);
        }

        [Fact]
        public async void Get_Excel_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSewingBlockingPlanFacade facade = new GarmentSewingBlockingPlanFacade(serviceProvider, dbContext);
            var dataNew = await DataUtil(facade, dbContext).GetNewData();
            dataNew.Items.First().DeliveryDate = dataNew.Items.First().StartDate.AddDays(-30);
            await facade.CreateAsync(dataNew);

            IWeeklyWorkingScheduleMonitoringFacade weeklyWorkingScheduleMonitoringFacade = new WeeklyWorkingScheduleMonitoringFacade(serviceProvider, dbContext);

            var filter = new
            {
                year = dataNew.Items.First().Year,
                week = dataNew.Items.First().WeekNumber
            };
            var Response = weeklyWorkingScheduleMonitoringFacade.GenerateExcel(filter: JsonConvert.SerializeObject(filter));

            Assert.NotNull(Response.Item2);
        }
    }
}
