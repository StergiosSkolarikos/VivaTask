using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivaTask.Domain.Models;
using VivaTask.Domain.Models.Dto;
using VivaTask.Infrastructure.DbContexts;
using VivaTask.Infrastructure.Services.Interfaces;

namespace VivaTask.Infrastructure.Services
{
    public class CacheCountriesProvider : ICacheCountriesProvider
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        private readonly ILogger<CacheCountriesProvider> _logger;

        private const string CountriesCacheKey = "CountriesCache";

        public CacheCountriesProvider(IMemoryCache memoryCache, IMapper mapper,ILogger<CacheCountriesProvider> logger)
        {
            _memoryCache = memoryCache;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CountriesDto>> TryGetCountriesAsync(CancellationToken cancellationToken)
        {
            var countriesToReturn = new List<CountriesDto>();

            if (_memoryCache.TryGetValue(CountriesCacheKey, out List<CountriesPersistenceModel> cachedCountries))
            {
                countriesToReturn = _mapper.Map<List<CountriesDto>>(cachedCountries);
            }

            _logger.LogInformation("{Count} Countries found in Cache", countriesToReturn.Count);
            return countriesToReturn;
        }

        public void TryInsertCountries(List<CountriesPersistenceModel> countries)
        {
            try
            {
                _memoryCache.Set(CountriesCacheKey, countries, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                });
                _logger.LogInformation("{Count} Coutries inserted in Cache",countries.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to insert Countries in Cache at {DateTime} with error : {Error} ",DateTime.Now,ex.Message);
            }
        }
    }
}
