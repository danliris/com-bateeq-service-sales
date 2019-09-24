﻿using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments;
using Com.Bateeq.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Bateeq.Service.Sales.Lib.Services;
using Com.Bateeq.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface;

namespace Com.Bateeq.Service.Sales.Lib.BusinessLogic.Facades.CostCalculationGarments
{
    public class CostCalculationGarmentFacade : ICostCalculationGarment
	{
		private readonly SalesDbContext DbContext;
		private readonly DbSet<CostCalculationGarment> DbSet;
		private readonly IdentityService identityService;
		private readonly CostCalculationGarmentLogic costCalculationGarmentLogic ;
		public IServiceProvider ServiceProvider;

		public CostCalculationGarmentFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
		{
			DbContext = dbContext;
			DbSet = DbContext.Set<CostCalculationGarment>();
			identityService = serviceProvider.GetService<IdentityService>();
			costCalculationGarmentLogic = serviceProvider.GetService<CostCalculationGarmentLogic>();
			ServiceProvider = serviceProvider;
		}

		public async Task<CostCalculationGarment> CustomCodeGenerator(CostCalculationGarment Model)
		{
			List<string> convectionOption = new List<string> { "C2A", "C2B", "C2C", "C1A", "C1B" };
			int convectionCode = convectionOption.IndexOf(Model.UnitCode) + 1;

			var lastData = await this.DbSet.Where(w => w.IsDeleted == false && w.UnitCode == Model.UnitCode).OrderByDescending(o => o.CreatedUtc).FirstOrDefaultAsync();

			DateTime Now = DateTime.Now;
			string Year = Now.ToString("yy");

			if (lastData == null)
			{
				Model.AutoIncrementNumber = 1;
				string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
				Model.RO_Number = $"{Year}{convectionCode.ToString()}{Number}";
			}
			else
			{
				if (lastData.CreatedUtc.Year < Now.Year)
				{
					Model.AutoIncrementNumber = 1;
					string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
					Model.RO_Number = $"{Year}{convectionCode.ToString()}{Number}";
				}
				else
				{
					Model.AutoIncrementNumber = lastData.AutoIncrementNumber + 1;
					string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
					Model.RO_Number = $"{Year}{convectionCode.ToString()}{Number}";
				}
			}

			return Model;
		}
		public async Task<int> CreateAsync(CostCalculationGarment model)
		{
            int Created = 0;
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    do
                    {
                        model.Code = CodeGenerator.Generate();
                        await CustomCodeGenerator(model);
                    }
                    while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

                    model.ImagePath = await this.AzureImageFacade.UploadImage(model.GetType().Name, model.Id, model.CreatedUtc, model.ImageFile);
                    costCalculationGarmentLogic.Create(model);
                    if (model.ImagePath != null)
                    {
                        model.ImagePath = await this.AzureImageFacade.UploadImage(model.GetType().Name, model.Id, model.CreatedUtc, model.ImageFile);
                    }
                    costCalculationGarmentLogic.Create(model);
                    Created = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return Created;
		}

        public async Task<int> DeleteAsync(int id)
		{
            int Deleted = 0;
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    await costCalculationGarmentLogic.DeleteAsync(id);
                    Deleted = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return Deleted;
		}

		public ReadResponse<CostCalculationGarment> Read(int page, int size, string order, List<string> select, string keyword, string filter)
		{
			return costCalculationGarmentLogic.Read(page, size, order, select, keyword, filter);
		}
		private IAzureImageFacade AzureImageFacade
		{
			get { return this.ServiceProvider.GetService<IAzureImageFacade>(); }
		}
		public async Task<CostCalculationGarment> ReadByIdAsync(int id)
		{
			CostCalculationGarment read = await this.DbSet
			   .Where(d => d.Id.Equals(id) && d.IsDeleted.Equals(false))
			   .Include(d => d.CostCalculationGarment_Materials)
			   .FirstOrDefaultAsync();

            if (read.ImagePath != null)
            {
                read.ImageFile = await this.AzureImageFacade.DownloadImage(read.GetType().Name, read.ImagePath);
            }

            return read;
		}

		public async Task<int> UpdateAsync(int id, CostCalculationGarment model)
		{
            model.ImagePath = await this.AzureImageFacade.UploadImage(model.GetType().Name, model.Id, model.CreatedUtc, model.ImageFile);
            costCalculationGarmentLogic.UpdateAsync(id, model);
            if (model.ImagePath != null)
            {
                model.ImagePath = await this.AzureImageFacade.UploadImage(model.GetType().Name, model.Id, model.CreatedUtc, model.ImageFile);
            }
            return await DbContext.SaveChangesAsync();
		}

        public async Task<Dictionary<long, string>> GetProductNames(List<long> productIds)
        {
            return await costCalculationGarmentLogic.GetProductNames(productIds);
        }
    }
}
