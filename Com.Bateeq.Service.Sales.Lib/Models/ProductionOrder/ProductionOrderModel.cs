﻿using Com.Bateeq.Service.Sales.Lib.Utilities.BaseClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Bateeq.Service.Sales.Lib.Models.ProductionOrder
{
    public class ProductionOrderModel : BaseModel
    {
        public ProductionOrderModel()
        {
            RunWidths = new HashSet<ProductionOrder_RunWidthModel>();
        }
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(255)]
        public string OrderNo { get; set; }
        public double OrderQuantity { get; set; }
        public double ShippingQuantityTolerance { get; set; }
        //[MaxLength(255)]
        public string MaterialOrigin { get; set; }
        [MaxLength(255)]
        public string FinishWidth { get; set; }
        [MaxLength(255)]
        public string DesignNumber { get; set; }
        //[MaxLength(255)]
        public string DesignCode { get; set; }
        [MaxLength(255)]
        public string HandlingStandard { get; set; }
        [MaxLength(255)]
        public string Run { get; set; }
        [MaxLength(255)]
        public string ShrinkageStandard { get; set; }
        //[MaxLength(1000)]
        public string ArticleFabricEdge { get; set; }
        [MaxLength(1000)]
        public string MaterialWidth { get; set; }
        [MaxLength(1000)]
        public string PackingInstruction { get; set; }
        [MaxLength(1000)]
        public string Sample { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        //[MaxLength(1000)]
        public string Remark { get; set; }
        public double DistributedQuantity { get; set; }
        public bool IsUsed { get; set; }
        public bool IsClosed { get; set; }
        public bool IsRequested { get; set; }
        public bool IsCompleted { get; set; }
        public long AutoIncreament { get; set; }


        public virtual ICollection<ProductionOrder_DetailModel> Details { get; set; }
        public virtual ICollection<ProductionOrder_RunWidthModel> RunWidths { get; set; }
        public virtual ICollection<ProductionOrder_LampStandardModel> LampStandards { get; set; }

        /*sales contract*/
        public long SalesContractId { get; set; }
        [MaxLength(255)]
        public string SalesContractNo { get; set; }

        /*yarn material*/
        public long YarnMaterialId { get; set; }
        [MaxLength(1000)]
        public string YarnMaterialName { get; set; }
        [MaxLength(255)]
        public string YarnMaterialCode { get; set; }
        [MaxLength(1000)]
        public string YarnMaterialRemark { get; set; }

        /*buyer*/
        public long BuyerId { get; set; }
        [MaxLength(255)]
        public string BuyerCode { get; set; }
        [MaxLength(1000)]
        public string BuyerName { get; set; }
        [MaxLength(255)]
        public string BuyerType { get; set; }

        /*process type*/
        public long ProcessTypeId { get; set; }
        [MaxLength(255)]
        public string ProcessTypeCode { get; set; }
        [MaxLength(1000)]
        public string ProcessTypeName { get; set; }
        [MaxLength(1000)]
        public string ProcessTypeRemark { get; set; }

        /*order type*/
        public long OrderTypeId { get; set; }
        [MaxLength(255)]
        public string OrderTypeCode { get; set; }
        [MaxLength(1000)]
        public string OrderTypeName { get; set; }
        [MaxLength(1000)]
        public string OrderTypeRemark { get; set; }

        /*material from product*/
        public long MaterialId { get; set; }
        [MaxLength(255)]
        public string MaterialCode { get; set; }
        [MaxLength(1000)]
        public string MaterialName { get; set; }
        public double MaterialPrice { get; set; }
        [MaxLength(255)]
        public string MaterialTags { get; set; }

        /* design motive*/
        public int DesignMotiveID { get; set; }
        [MaxLength(25)]
        public string DesignMotiveCode { get; set; }
        [MaxLength(255)]
        public string DesignMotiveName { get; set; }

        /*Uom*/
        public long UomId { get; set; }
        [MaxLength(255)]
        public string UomUnit { get; set; }

        /*material construction*/
        public long MaterialConstructionId { get; set; }
        [MaxLength(1000)]
        public string MaterialConstructionName { get; set; }
        [MaxLength(255)]
        public string MaterialConstructionCode { get; set; }
        public string MaterialConstructionRemark { get; set; }

        /*finish type*/
        public long FinishTypeId { get; set; }
        [MaxLength(255)]
        public string FinishTypeCode { get; set; }
        [MaxLength(1000)]
        public string FinishTypeName { get; set; }
        [MaxLength(1000)]
        public string FinishTypeRemark { get; set; }

        /*standard test*/
        public long StandardTestId { get; set; }
        [MaxLength(255)]
        public string StandardTestCode { get; set; }
        [MaxLength(1000)]
        public string StandardTestName { get; set; }
        [MaxLength(1000)]
        public string StandardTestRemark { get; set; }

        /*Account*/
        public long AccountId { get; set; }
        public string AccountUserName { get; set; }
        [MaxLength(1000)]
        public string ProfileFirstName { get; set; }
        [MaxLength(1000)]
        public string ProfileLastName { get; set; }
        [MaxLength(255)]
        public string ProfileGender { get; set; }

       
    }
}
