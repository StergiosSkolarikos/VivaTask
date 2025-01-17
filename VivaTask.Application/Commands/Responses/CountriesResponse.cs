using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivaTask.Domain.Models;
using VivaTask.Domain.Models.Dto;

namespace VivaTask.Application.Commands.Responses
{
    public class CountriesResponse : Response
    {
        public List<CountriesDto> Countries { get; set; }
        public CountriesResponse(string? status,List<CountriesDto> countries) : base(status)
        {
            Countries = countries;
        }
        public CountriesResponse(string status, string message) : base(status, message)
        {
        }

        public static CountriesResponse CreateSuccessResponse(List<CountriesDto> countries)
        {
            return new CountriesResponse(nameof(Enums.ResponseStatus.Success), countries);
        }
        public static CountriesResponse CreateErrorResponse(string message)
        {
            return new CountriesResponse(nameof(Enums.ResponseStatus.Failed), message);
        }

    }
}
