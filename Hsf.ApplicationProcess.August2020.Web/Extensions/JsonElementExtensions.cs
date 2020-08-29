using System.Text.Json;

namespace Hsf.ApplicationProcess.August2020.Web.Extensions
{
    public static class JsonElementExtensions
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = false, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        public static Tobject Deserialize<Tobject>(this JsonElement element) where Tobject : class
        {
            var rawData = element.GetRawText();
            return JsonSerializer.Deserialize<Tobject>(rawData, _options);
        }
    }
}