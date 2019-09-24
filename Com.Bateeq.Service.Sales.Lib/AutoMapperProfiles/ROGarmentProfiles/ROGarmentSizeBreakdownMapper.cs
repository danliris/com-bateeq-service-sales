using AutoMapper;
using Com.Bateeq.Service.Sales.Lib.Models.ROGarments;
using Com.Bateeq.Service.Sales.Lib.ViewModels.GarmentROViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Bateeq.Service.Sales.Lib.AutoMapperProfiles.ROGarmentProfiles
{
    public class ROGarmentSizeBreakdownMapper : Profile
    {
        public ROGarmentSizeBreakdownMapper()
        {
            CreateMap<RO_Garment_SizeBreakdown, RO_Garment_SizeBreakdownViewModel>()
              .ForPath(d => d.Color.Id, opt => opt.MapFrom(s => s.ColorId))
              .ForPath(d => d.Color.Name, opt => opt.MapFrom(s => s.ColorName))
              .ReverseMap();
        }
    }
}