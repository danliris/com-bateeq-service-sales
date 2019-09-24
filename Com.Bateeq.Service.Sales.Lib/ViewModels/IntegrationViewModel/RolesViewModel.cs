﻿using Com.Bateeq.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Bateeq.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class RolesViewModel : BaseViewModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(1000)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }

    }
}
