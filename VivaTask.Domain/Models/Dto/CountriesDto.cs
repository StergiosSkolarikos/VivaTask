using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivaTask.Domain.Models.Dto
{
    public class CountriesDto
    {
        public string CommonName { get; set; }
        public string Capital { get; set; }
        public List<string> Borders { get; set; }
    }
}
