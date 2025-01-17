using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivaTask.Domain.Models;
using VivaTask.Domain.Models.Dto;

namespace VivaTask.Infrastructure.Configurations
{
    public class AutomapperConfig :Profile
    {
        public AutomapperConfig() 
        {
            CreateMap<CountriesPersistenceModel, CountriesDto>()
            .ForMember(dest => dest.Borders, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.Borders) ? new List<string>() : src.Borders.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()));

            CreateMap<CountriesDto, CountriesPersistenceModel>()
                .ForMember(dest => dest.Borders, opt => opt.MapFrom(src => string.Join(',', src.Borders)));
        }
    }
}
