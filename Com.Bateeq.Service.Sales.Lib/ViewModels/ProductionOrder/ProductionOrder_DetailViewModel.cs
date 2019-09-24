using Com.Bateeq.Service.Sales.Lib.Utilities;
using Com.Bateeq.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Bateeq.Service.Sales.Lib.ViewModels.ProductionOrder
{
    public class ProductionOrder_DetailViewModel : BaseViewModel, IValidatableObject
    {
        [MaxLength(255)]
        public string ColorRequest { get; set; }
        [MaxLength(255)]
        public string ColorTemplate { get; set; }
        [MaxLength(255)]
        public ColorTypeViewModel ColorType { get; set; }
        public double? Quantity { get; set; }

        public UomViewModel Uom { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
