﻿using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Bateeq.Service.Sales.Lib.Helpers;
using Com.Bateeq.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Bateeq.Service.Sales.Lib.Services;
using Com.Bateeq.Service.Sales.Lib.Utilities;
using Com.Bateeq.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Com.Bateeq.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments
{
    public class CostCalculationGarmentLogic : BaseLogic<CostCalculationGarment>
	{ 
		private CostCalculationGarmentMaterialLogic costCalculationGarmentMaterialLogic;

		private readonly SalesDbContext DbContext; 
		public CostCalculationGarmentLogic(CostCalculationGarmentMaterialLogic costCalculationGarmentMaterialLogic, IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
		{
			this.costCalculationGarmentMaterialLogic = costCalculationGarmentMaterialLogic;
			this.DbContext = dbContext; 
		}

		public override ReadResponse<CostCalculationGarment> Read(int page, int size, string order, List<string> select, string keyword, string filter)
		{
			IQueryable<CostCalculationGarment> Query = DbSet;

			List<string> SearchAttributes = new List<string>()
			{
                "PreSCNo", "RO_Number","Article","UnitName"
			};

			Query = QueryHelper<CostCalculationGarment>.Search(Query, SearchAttributes, keyword);

			Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
			Query = QueryHelper<CostCalculationGarment>.Filter(Query, FilterDictionary);

			List<string> SelectedFields = new List<string>()
			{
				  "Id", "Code", "PreSCNo", "RO_Number", "Quantity", "ConfirmPrice", "Article", "Unit", "LastModifiedUtc","UnitName",
					"Comodity", "UOM", "Buyer", "DeliveryDate", "BuyerBrand"
            };

			Query = Query
				 .Select(ccg => new CostCalculationGarment
				 {
					 Id = ccg.Id,
					 Code = ccg.Code,
					 RO_Number = ccg.RO_Number,
					 Article = ccg.Article,
					 UnitId = ccg.UnitId,
					 UnitCode=ccg.UnitCode,
					 UnitName=ccg.UnitName,
					 Quantity = ccg.Quantity,
					 ConfirmPrice = ccg.ConfirmPrice,
                     BuyerCode=ccg.BuyerCode,
                     BuyerId=ccg.BuyerId,
                     BuyerName=ccg.BuyerName,
                     BuyerBrandCode=ccg.BuyerBrandCode,
                     BuyerBrandId=ccg.BuyerBrandId,
                     BuyerBrandName=ccg.BuyerBrandName,
                     Commodity=ccg.Commodity,
                     ComodityCode=ccg.ComodityCode,
                     CommodityDescription=ccg.CommodityDescription,
                     ComodityID=ccg.ComodityID,
                     DeliveryDate=ccg.DeliveryDate,
                     UOMCode=ccg.UOMCode,
                     UOMID=ccg.UOMID,
                     UOMUnit=ccg.UOMUnit,
					 LastModifiedUtc = ccg.LastModifiedUtc,
                                          
                     PreSCNo = ccg.PreSCNo
				 });

			Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
			Query = QueryHelper<CostCalculationGarment>.Order(Query, OrderDictionary);

			Pageable<CostCalculationGarment> pageable = new Pageable<CostCalculationGarment>(Query, page - 1, size);
			List<CostCalculationGarment> data = pageable.Data.ToList<CostCalculationGarment>();
			int totalData = pageable.TotalCount;

			return new ReadResponse<CostCalculationGarment>(data, totalData, OrderDictionary, SelectedFields);
		}

		 
		public override void Create(CostCalculationGarment model)
		{
			GeneratePONumbers(model);
			foreach (var detail in model.CostCalculationGarment_Materials)
			{
				costCalculationGarmentMaterialLogic.Create(detail);
				//EntityExtension.FlagForCreate(detail, IdentityService.Username, "sales-service");
			}

            var preSC = DbContext.GarmentPreSalesContracts.Single(w => w.Id == model.PreSCId);
            preSC.IsCC = true;

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
			DbSet.Add(model);
		}
	 
		private void GeneratePONumbers(CostCalculationGarment model)
		{
			int lastFabricNumber = GetLastMaterialFabricNumberByCategoryName(model.UnitCode);
			int lastNonFabricNumber = GetLastMaterialNonFabricNumberByCategoryName(model.UnitCode);
            List<string> convectionOption = new List<string> { "C2A", "C2B", "C2C", "C1A", "C1B" };
            int convectionCode = convectionOption.IndexOf(model.UnitCode) + 1;

            DateTime Now = DateTime.Now;
			string Year = Now.ToString("yy");

			foreach (CostCalculationGarment_Material item in model.CostCalculationGarment_Materials)
			{
				if (string.IsNullOrWhiteSpace(item.PO_SerialNumber))
				{
					string Number = "";
					if (item.CategoryName.ToUpper().Equals("FABRIC"))
					{
						lastFabricNumber += 1;
						Number = lastFabricNumber.ToString().PadLeft(5, '0');
						item.PO_SerialNumber = $"PM{Year}{convectionCode}{Number}";
						item.AutoIncrementNumber = lastFabricNumber;
					}
					else
					{
						lastNonFabricNumber += 1;
						Number = lastNonFabricNumber.ToString().PadLeft(5, '0');
						item.PO_SerialNumber = $"PA{Year}{convectionCode}{Number}";
						item.AutoIncrementNumber = lastNonFabricNumber;
					}
				}
			}
		}
		
		private int GetLastMaterialNonFabricNumberByCategoryName(string convection)
		{
			var result = (from a in DbContext.CostCalculationGarments
						  join b in DbContext.CostCalculationGarment_Materials on a.Id equals b.CostCalculationGarmentId
													  where !b.CategoryName.ToUpper().Equals("FABRIC") && a.UnitCode.Equals(convection)
													  select b).AsNoTracking()
													 .OrderByDescending(o => o.CreatedUtc.Year).ThenByDescending(t => t.AutoIncrementNumber).FirstOrDefault();
			return result == null ? 0 : result.AutoIncrementNumber;
		}

		private int GetLastMaterialFabricNumberByCategoryName(string convection)
		{
			var result = (from a in DbContext.CostCalculationGarments
						  join b in DbContext.CostCalculationGarment_Materials on a.Id equals b.CostCalculationGarmentId
													  where b.CategoryName.ToUpper()=="FABRIC" && a.UnitCode==(convection)
													  select b).AsNoTracking().OrderByDescending(o => o.CreatedUtc.Year).ThenByDescending(t => t.AutoIncrementNumber).FirstOrDefault();
			return result == null ? 0 : result.AutoIncrementNumber;
		}
		public override void UpdateAsync(long id, CostCalculationGarment model)
		{
            GeneratePONumbers(model);
            if (model.CostCalculationGarment_Materials != null)
			{
				HashSet<long> detailIds = costCalculationGarmentMaterialLogic.GetCostCalculationIds(id);
				foreach (var itemId in detailIds)
				{
					CostCalculationGarment_Material data = model.CostCalculationGarment_Materials.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                    {
                        CostCalculationGarment_Material dataItem = DbContext.CostCalculationGarment_Materials.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        EntityExtension.FlagForDelete(dataItem, IdentityService.Username, "sales-service");
                        //await costCalculationGarmentMaterialLogic.DeleteAsync(itemId);
                    }
                    else
                    {
                        costCalculationGarmentMaterialLogic.UpdateAsync(itemId, data);
                    }

					foreach (CostCalculationGarment_Material item in model.CostCalculationGarment_Materials)
					{
						if (item.Id <= 0)
							costCalculationGarmentMaterialLogic.Create(item);
					}
				}
			}

			EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
			DbSet.Update(model);
		}

        public override async Task DeleteAsync(long id)
        {
            var model = await DbSet.Include(d => d.CostCalculationGarment_Materials).FirstOrDefaultAsync(d => d.Id == id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);

            var countPreSCinOtherCC = DbSet.Count(c => c.Id != id && c.PreSCId == model.PreSCId);
            if (countPreSCinOtherCC == 0)
            {
                var preSC = DbContext.GarmentPreSalesContracts.Single(w => w.Id == model.PreSCId);
                preSC.IsCC = false;
            }

            foreach (var material in model.CostCalculationGarment_Materials)
            {
                await costCalculationGarmentMaterialLogic.DeleteAsync(material.Id);
            }
        }

        public async Task<Dictionary<long, string>> GetProductNames(List<long> productIds)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, IdentityService.Token);

            var param = "";
            foreach (var id in productIds)
            {
                param = string.Concat(param, $"garmentProductList[]={id}&");
            }
            param = param.Trim('&');

            var httpResponseMessage = await httpClient.GetAsync($@"{APIEndpoint.Core}master/garmentProducts/byId?{param}");

            if (httpResponseMessage.StatusCode.Equals(HttpStatusCode.OK))
            {
                var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);

                var data = resultDict.SingleOrDefault(p => p.Key.Equals("data")).Value;

                List<Dictionary<string, object>> dataDicts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data.ToString());

                var productDicts = new Dictionary<long, string>();

                foreach (var dataDict in dataDicts)
                {
                    var Id = dataDict["Id"] != null ? Convert.ToInt64(dataDict["Id"].ToString()) : 0;
                    var Name = dataDict["Name"] != null ? dataDict["Name"].ToString() : "";

                    productDicts.Add(Id, Name);
                }

                return productDicts;
            }
            else
            {
                return new Dictionary<long, string>();
            }
        }
    }
}
