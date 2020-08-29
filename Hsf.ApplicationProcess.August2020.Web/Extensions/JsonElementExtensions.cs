using System.Text.Json;

namespace Hsf.ApplicationProcess.August2020.Web.Extensions
{
    public static class JsonElementExtensions
    {
        public static Tobject Deserialize<Tobject>(this JsonElement element) where Tobject : class
        {
            var rawData = element.GetRawText();
            return JsonSerializer.Deserialize<Tobject>(rawData);
        }
    }
}