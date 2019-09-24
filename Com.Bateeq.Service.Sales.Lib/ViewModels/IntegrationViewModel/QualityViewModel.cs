﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Bateeq.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class QualityViewModel
    {
        public long Id { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(1000)]
        public string Name { get; set; }
    }
}
