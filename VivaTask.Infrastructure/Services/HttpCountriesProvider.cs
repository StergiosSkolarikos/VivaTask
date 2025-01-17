using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using VivaTask.Domain.Models;
using VivaTask.Domain.Models.Dto;
using VivaTask.Infrastructure.Configurations;
using VivaTask.Infrastructure.Services.Interfaces;

namespace VivaTask.Infrastructure.Services
{
    public class HttpCountriesProvider : IHttpCountriesProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpCountriesProvider> _logger;
        public HttpCountriesProvider(IHttpClientFactory httpClientFactory,ILogger<HttpCountriesProvider> logger) 
        {
            _httpClient = httpClientFactory.CreateClient(nameof(RestCountriesConfiguration));
            _logger = logger;
        }
        public async Task<IEnumerable<CountriesDto>> TryGetCountriesAsync(CancellationToken cancellationToken)
        {
            var url = $"{_httpClient.BaseAddress}?fields=name,capital,borders";
            try
            {
                var response = await _httpClient.GetAsync(url,cancellationToken);

                response.EnsureSuccessStatusCode();

                var countries = await response.Content.ReadAsStringAsync(cancellationToken);
                var deserializedCountries= JsonSerializer.Deserialize<List<Countries>>(countries);
                var countriesToReturn = deserializedCountries.ConvertAll(country => new CountriesDto
                {
                    CommonName = country.Name.Common ?? string.Empty,
                    Capital = country.Capital.Any() ? country.Capital.FirstOrDefault() : string.Empty,
                    Borders = country.Borders.Any() ? country.Borders : []
                });

                _logger.LogInformation("{Count} Countries retrieved from {Endpoint}", countriesToReturn.Count, url);

                return countriesToReturn;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Failed to retrieve Countries from {Endpoint} at {Datetime} with error: {Error}", url, DateTime.Now, ex.Message);
                return new List<CountriesDto>();
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to retrieve Countries from {Endpoint} at {Datetime} with error: {Error}", url, DateTime.Now, ex.Message);
                return new List<CountriesDto>();
            }
            finally
            {
                _httpClient.Dispose();
            }
        }
    }
}
