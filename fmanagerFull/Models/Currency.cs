using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace fmanagerFull.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        Ruble = 0,
        Pound = 1,
        Dollar = 2,
        Euro = 3
    }
}