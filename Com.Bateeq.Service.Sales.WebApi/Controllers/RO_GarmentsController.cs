﻿using AutoMapper;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface.ROGarmentInterface;
using Com.Bateeq.Service.Sales.Lib.Models.ROGarments;
using Com.Bateeq.Service.Sales.Lib.PDFTemplates;
using Com.Bateeq.Service.Sales.Lib.Services;
using Com.Bateeq.Service.Sales.Lib.ViewModels.GarmentROViewModels;
using Com.Bateeq.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Bateeq.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/ro-garments")]
    [Authorize]
    public class RO_GarmentsControllerprivate : BaseController<RO_Garment, RO_GarmentViewModel, IROGarment>
    {
        readonly static string apiVersion = "1.0";
        private readonly IIdentityService Service;
        public RO_GarmentsControllerprivate(IIdentityService identityService, IValidateService validateService, IROGarment facade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, facade, mapper, apiVersion)
        {
            Service = identityService;
        }
        [HttpGet("pdf/{id}")]
        public async Task<IActionResult> GetPDF([FromRoute]int Id)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                RO_Garment model = await Facade.ReadByIdAsync(Id);
                RO_GarmentViewModel viewModel = Mapper.Map<RO_GarmentViewModel>(model);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    ROGarmentPdfTemplate PdfTemplate = new ROGarmentPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);

                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = "RO Garment " + viewModel.Code + ".pdf"
                    };

                }
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        
    }
}