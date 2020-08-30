using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hsf.ApplicationProcess.August2020.Web.Extensions
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
