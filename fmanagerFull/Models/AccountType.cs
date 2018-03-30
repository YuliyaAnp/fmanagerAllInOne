using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace fmanagerFull.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AccountType
    {
        Income = 0,
        Expenses = 1,
        Liabilities = 2,
        Assets = 3
    }
}