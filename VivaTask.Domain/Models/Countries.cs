using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VivaTask.Domain.Models
{
    public class Countries
    {
        [JsonPropertyName("name")]
        public Name Name { get; set; }
        [JsonPropertyName("capital")]
        public List<string> Capital { get; set; }
        [JsonPropertyName("borders")]
        public List<string> Borders { get; set; }
    }
    public class Name
    {
        [JsonPropertyName("common")]
        public string Common { get; set; }
    }
}
