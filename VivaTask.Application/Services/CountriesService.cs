using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivaTask.Application.Commands.Responses;
using VivaTask.Domain.Models;
using VivaTask.Domain.Models.Dto;
using VivaTask.Infrastructure.Services.Interfaces;

namespace VivaTask.Application.Services
{
    public interface ICountriesService 
    {
        Task<IEnumerable<CountriesDto>> GetAllCountries(CancellationToken cancellationToken);
    }
    public class CountriesService : ICountriesService
    {
        private readonly IMapper _mapper;
        private readonly IHttpCountriesProvider _httpCountriesProvider;
        private readonly ISqlCountriesProvider _sqlCountriesProvider;
        private readonly ICacheCountriesProvider _cacheCountriesProvider;
        private readonly ILogger<CountriesService> _logger;
        public CountriesService(
            IHttpCountriesProvider httpCountriesProvider, 
            ISqlCountriesProvider sqlCountriesProvider,
            IMapper mapper,
            ICacheCountriesProvider cacheCountriesProvider, 
            ILogger<CountriesService> logger
        )
        {
            _httpCountriesProvider = httpCountriesProvider;
            _sqlCountriesProvider = sqlCountriesProvider;
            _mapper = mapper;
            _cacheCountriesProvider = cacheCountriesProvider;
            _logger = logger;
        }

        public async Task<IEnumerable<CountriesDto>> GetAllCountries(CancellationToken cancellationToken)
        {
            var countries = await _cacheCountriesProvider.TryGetCountriesAsync(cancellationToken);

            if (countries.Any())
            {
                return countries;
            }

            countries = await FetchAndCacheCountriesAsync(_sqlCountriesProvider, cancellationToken);

            if (countries.Any())
            {
                return countries;
            }

            return await FetchAndPersistCountriesAsync(_httpCountriesProvider, cancellationToken);
        }

        private async Task<IEnumerable<CountriesDto>> FetchAndCacheCountriesAsync(ICountriesProvider provider, CancellationToken cancellationToken)
        {
            var countries = await provider.TryGetCountriesAsync(cancellationToken);

            if (countries.Any())
            {
                var countriesToInsert = _mapper.Map<List<CountriesPersistenceModel>>(countries);
                _cacheCountriesProvider.TryInsertCountries(countriesToInsert);
            }

            return countries;
        }

        private async Task<IEnumerable<CountriesDto>> FetchAndPersistCountriesAsync(ICountriesProvider provider, CancellationToken cancellationToken)
        {
            var countries = await provider.TryGetCountriesAsync(cancellationToken);

            if (countries.Any())
            {
                var countriesToInsert = _mapper.Map<List<CountriesPersistenceModel>>(countries);
                _cacheCountriesProvider.TryInsertCountries(countriesToInsert);
                await _sqlCountriesProvider.TryInsertAsync(countriesToInsert, cancellationToken);
            }

            return countries;
        }
    }
}
