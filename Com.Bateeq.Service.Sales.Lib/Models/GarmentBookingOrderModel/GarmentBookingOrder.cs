﻿using Com.Bateeq.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Bateeq.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Bateeq.Service.Sales.Lib.Models.GarmentBookingOrderModel
{
    public class GarmentBookingOrder : BaseModel
    {
        public string BookingOrderNo { get; set; }
        public DateTimeOffset BookingOrderDate { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public long BuyerId { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public long SectionId { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public double OrderQuantity { get; set; }
        public string Remark { get; set; }
        public bool IsBlockingPlan { get; set; }
        public bool IsCanceled { get; set; }
        public DateTimeOffset? CanceledDate { get; set; }
        public double CanceledQuantity { get; set; }
        public DateTimeOffset? ExpiredBookingDate { get; set; }
        public double ExpiredBookingQuantity { get; set; }
        public double ConfirmedQuantity { get; set; }
        public bool HadConfirmed { get; set; }
        public virtual ICollection<GarmentBookingOrderItem> Items { get; set; }

    }
}