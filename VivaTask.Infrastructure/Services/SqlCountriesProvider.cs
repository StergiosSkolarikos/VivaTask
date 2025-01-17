using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VivaTask.Domain.Models;
using VivaTask.Domain.Models.Dto;
using VivaTask.Infrastructure.DbContexts;
using VivaTask.Infrastructure.Services.Interfaces;

namespace VivaTask.Infrastructure.Services
{
    public class SqlCountriesProvider : ISqlCountriesProvider
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<SqlCountriesProvider> _logger;

        public SqlCountriesProvider(ApplicationDbContext context,IMapper mapper,ILogger<SqlCountriesProvider> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CountriesDto>> TryGetCountriesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var countries = await _context.Countries.ToListAsync(cancellationToken);
                _logger.LogInformation("{Count} Countries found in Database",countries.Count);
                var countriesDto = _mapper.Map<List<CountriesDto>>(countries);
                return countriesDto;
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to retrieve Countries from Database at {Datetime} with error:{Error}", DateTime.Now, ex.Message);
                return Enumerable.Empty<CountriesDto>();
            } 
        }

        public async Task TryInsertAsync(List<CountriesPersistenceModel> countries,CancellationToken cancellationToken)
        {
            try
            {
                await _context.Countries.AddRangeAsync(countries, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("{Count} Countries inserted in Database", countries.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to insert Countries in Database at {Datetime} with error:{Error}", DateTime.Now, ex.Message);
            }
        }
    }
}
