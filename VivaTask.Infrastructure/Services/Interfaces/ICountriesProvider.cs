using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivaTask.Domain.Models;
using VivaTask.Domain.Models.Dto;

namespace VivaTask.Infrastructure.Services.Interfaces
{
    public interface ICountriesProvider
    {
        Task<IEnumerable<CountriesDto>> TryGetCountriesAsync(CancellationToken cancellationToken);
    }
}
