﻿using AutoMapper;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface.GarmentPreSalesContractInterface;
using Com.Bateeq.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Bateeq.Service.Sales.Lib.PDFTemplates;
using Com.Bateeq.Service.Sales.Lib.Services;
using Com.Bateeq.Service.Sales.Lib.ViewModels.GarmentPreSalesContractViewModels;
using Com.Bateeq.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.JsonPatch;
using Com.Bateeq.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Com.Bateeq.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/merchandiser/garment-pre-sales-contracts")]
    [Authorize]
    public class GarmentPreSalesContractController : BaseController<GarmentPreSalesContract, GarmentPreSalesContractViewModel, IGarmentPreSalesContract>
    {
        private readonly IHttpClientService HttpClientService;
        private readonly static string apiVersion = "1.0";

        public GarmentPreSalesContractController(IIdentityService identityService, IValidateService validateService, IGarmentPreSalesContract facade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, facade, mapper, apiVersion)
        {
            HttpClientService = serviceProvider.GetService<IHttpClientService>();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute]int id, [FromBody]JsonPatchDocument<GarmentPreSalesContract> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var model = await Facade.ReadByIdAsync(id);
                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    patch.ApplyTo(model);

                    var viewModel = Mapper.Map<GarmentPreSalesContractViewModel>(model);
                    ValidateService.Validate(viewModel);

                    IdentityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
                    IdentityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");

                    if (id != viewModel.Id)
                    {
                        Dictionary<string, object> Result =
                            new ResultFormatter(ApiVersion, Common.BAD_REQUEST_STATUS_CODE, Common.BAD_REQUEST_MESSAGE)
                            .Fail();
                        return BadRequest(Result);
                    }
                    await Facade.UpdateAsync(id, model);

                    return NoContent();
                }
            }
            catch (ServiceValidationException e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.BAD_REQUEST_STATUS_CODE, Common.BAD_REQUEST_MESSAGE)
                    .Fail(e);
                return BadRequest(Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpPost("post")]
        public async Task<IActionResult> PreSalesPost([FromBody]List<long> listId)
        {
            try
            {
                IdentityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                await Facade.PreSalesPost(listId, IdentityService.Username);

                return Ok();
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpPut("unpost/{id}")]
        public async Task<IActionResult> PreSalesUnpost([FromRoute]long id)
        {
            try
            {
                IdentityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                await Facade.PreSalesUnpost(id, IdentityService.Username);

                return Ok();
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