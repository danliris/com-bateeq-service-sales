﻿using Com.Bateeq.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Bateeq.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Bateeq.Service.Sales.Lib.Models.GarmentBookingOrderModel
{
    public class GarmentBookingOrderItem : BaseModel
    {
        public virtual long BookingOrderId { get; set; }
        [ForeignKey("BookingOrderId")]
        public virtual GarmentBookingOrder GarmentBookingOrder { get; set; }

        public long ComodityId { get; set; }
        public string ComodityCode { get; set; }
        public string ComodityName { get; set; }
        public double ConfirmQuantity { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public DateTimeOffset ConfirmDate { get; set; }
        public string Remark { get; set; }
        public bool IsCanceled { get; set; }
        public DateTimeOffset CanceledDate { get; set; }

    }
}
