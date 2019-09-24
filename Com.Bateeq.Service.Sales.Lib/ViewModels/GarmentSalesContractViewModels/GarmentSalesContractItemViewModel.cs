﻿using Com.Bateeq.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Bateeq.Service.Sales.Lib.ViewModels.GarmentSalesContractViewModels
{
    public class GarmentSalesContractItemViewModel : BaseViewModel
    {
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
    }
}
