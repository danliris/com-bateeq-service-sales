﻿using Com.Bateeq.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Bateeq.Service.Sales.Lib.ViewModels.GarmentROViewModels
{
    public class RO_Garment_SizeBreakdownViewModel : BaseViewModel
    {
        public string Code { get; set; }
        public ArticleColorViewModel Color { get; set; }
        public List<RO_Garment_SizeBreakdown_DetailViewModel> RO_Garment_SizeBreakdown_Details { get; set; }
        public int Total { get; set; }
    }
}