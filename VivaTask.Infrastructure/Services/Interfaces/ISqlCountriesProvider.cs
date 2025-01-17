using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivaTask.Domain.Models;

namespace VivaTask.Infrastructure.Services.Interfaces
{
    public interface ISqlCountriesProvider : ICountriesProvider
    {
        Task TryInsertAsync(List<CountriesPersistenceModel> country,CancellationToken cancellationToken);
    }
}
