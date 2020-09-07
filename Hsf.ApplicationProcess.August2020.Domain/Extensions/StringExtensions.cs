namespace Hsf.ApplicationProcess.August2020.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string Urlify(this string source)
        {
            var countryName = source.ToLower();
            return countryName.Replace(" ", "%20");
        }
    }
}