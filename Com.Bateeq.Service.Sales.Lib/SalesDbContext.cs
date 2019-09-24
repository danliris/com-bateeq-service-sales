﻿using Com.Bateeq.Service.Sales.Lib.Models.Spinning;
using Com.Bateeq.Service.Sales.Lib.Models.Weaving;
using Com.Bateeq.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Moonlay.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Com.Bateeq.Service.Sales.Lib.Models.ProductionOrder;
using Com.Bateeq.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Bateeq.Service.Sales.Lib.Models.GarmentSalesContractModel;
using Com.Bateeq.Service.Sales.Lib.Models.ROGarments;
using Com.Bateeq.Service.Sales.Lib.Models;
using Com.Bateeq.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Bateeq.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Bateeq.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Bateeq.Service.Sales.Lib.Models.GarmentMasterPlan.MaxWHConfirmModel;
using Com.Bateeq.Service.Sales.Lib.Models.GarmentPreSalesContractModel;

namespace Com.Bateeq.Service.Sales.Lib
{
    public class SalesDbContext : StandardDbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
        }

        public DbSet<WeavingSalesContractModel> WeavingSalesContract { get; set; }
        public DbSet<SpinningSalesContractModel> SpinningSalesContract { get; set; }
        public DbSet<FinishingPrintingSalesContractModel> FinishingPrintingSalesContracts { get; set; }
        public DbSet<FinishingPrintingSalesContractDetailModel> FinishingPrintingSalesContractDetails { get; set; }
		public DbSet<CostCalculationGarment> CostCalculationGarments { get; set; }
		public DbSet<CostCalculationGarment_Material> CostCalculationGarment_Materials { get; set; }
        public DbSet<GarmentSalesContract> GarmentSalesContracts { get; set; }
        public DbSet<GarmentSalesContractItem> GarmentSalesContractItems { get; set; }

        #region PRODUCTION ORDER DBSET
        public DbSet<ProductionOrderModel> ProductionOrder { get; set; }
        public DbSet<ProductionOrder_DetailModel> ProductionOrder_Details { get; set; }
        public DbSet<ProductionOrder_LampStandardModel> ProductionOrder_LampStandard { get; set; }
        public DbSet<ProductionOrder_RunWidthModel> ProductionOrder_RunWidth { get; set; }

        #endregion

        public DbSet<RO_Garment> RO_Garments { get; set; }
        public DbSet<RO_Garment_SizeBreakdown> RO_Garment_SizeBreakdowns { get; set; }
        public DbSet<RO_Garment_SizeBreakdown_Detail> RO_Garment_SizeBreakdown_Details { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<ArticleColor> ArticleColors { get; set; }
        public DbSet<Efficiency> Efficiencies { get; set; }
        public DbSet<GarmentBookingOrder> GarmentBookingOrders { get; set; }
        public DbSet<GarmentBookingOrderItem> GarmentBookingOrderItems { get; set; }
        public DbSet<GarmentWeeklyPlan> GarmentWeeklyPlans { get; set; }
        public DbSet<GarmentWeeklyPlanItem> GarmentWeeklyPlanItems { get; set; }

        public DbSet<GarmentSewingBlockingPlan> GarmentSewingBlockingPlans { get; set; }
        public DbSet<GarmentSewingBlockingPlanItem> GarmentSewingBlockingPlanItems { get; set; }

        public DbSet<MaxWHConfirm> MaxWHConfirms { get; set; }
        public DbSet<GarmentPreSalesContract> GarmentPreSalesContracts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<FinishingPrintingSalesContractModel>()
                .HasIndex(h => h.SalesContractNo)
                .IsUnique();

            modelBuilder.Entity<RO_Garment>()
            .Ignore(c => c.ImagesFile);
        }
    }
}
