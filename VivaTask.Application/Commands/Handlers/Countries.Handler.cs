using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivaTask.Application.Commands.Requests;
using VivaTask.Application.Commands.Responses;
using VivaTask.Application.Services;

namespace VivaTask.Application.Commands.Handlers
{
    public class CountriesHandler : IRequestHandler<CountriesRequest, Response>
    {
        private readonly ICountriesService _countriesService;
        private readonly ILogger<CountriesHandler> _logger;
        public CountriesHandler(ICountriesService countriesService,ILogger<CountriesHandler> logger)
        {
            _countriesService = countriesService;
            _logger = logger;
        }

        public async Task<Response> Handle(CountriesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start Fetching Countries at {DateTime}", DateTime.Now);
                var countries = await _countriesService.GetAllCountries(cancellationToken);
                _logger.LogInformation("Complete Fetching at {DateTime} and found {Count} Countries",DateTime.Now,countries.Count());
                return CountriesResponse.CreateSuccessResponse(countries.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to retrive Countries");
                return CountriesResponse.CreateErrorResponse(ex.Message);
            }
        }
    }
}
