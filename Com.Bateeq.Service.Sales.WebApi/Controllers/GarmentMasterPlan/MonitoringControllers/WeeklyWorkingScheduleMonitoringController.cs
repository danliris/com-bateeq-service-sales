using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MonitoringInterfaces;
using Com.Bateeq.Service.Sales.Lib.Helpers;
using Com.Bateeq.Service.Sales.Lib.Models;
using Com.Bateeq.Service.Sales.Lib.Services;
using Com.Bateeq.Service.Sales.Lib.ViewModels;
using Com.Bateeq.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Com.Bateeq.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Com.Bateeq.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/weekly-working-schedule-monitoring")]
    [Authorize]
    public class WeeklyWorkingScheduleMonitoringController : BaseMonitoringController<WeeklyWorkingScheduleMonitoringViewModel, IWeeklyWorkingScheduleMonitoringFacade>
    {
        private readonly static string apiVersion = "1.0";

        public WeeklyWorkingScheduleMonitoringController(IIdentityService identityService, IWeeklyWorkingScheduleMonitoringFacade facade) : base(identityService, facade, apiVersion)
        {
        }

    }
}