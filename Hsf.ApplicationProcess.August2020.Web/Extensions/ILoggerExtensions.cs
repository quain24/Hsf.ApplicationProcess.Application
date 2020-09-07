using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Hsf.ApplicationProcess.August2020.Web.Extensions
{
    public static class ILoggerExtensions
    {
        public static void LogBadCallWithParams(this ILogger logger, string nameOfActionCausingErrors, ModelStateDictionary modelState)
        {
            var logEntry = $"ERROR - Controller action \"{nameOfActionCausingErrors}\" was called with bad parameters:";
            var parameterList = modelState.Keys.ToList();
            parameterList.Remove("");

            //logEntry = parameterList.Aggregate(logEntry, (current, errorType) => current + $" {errorType},").TrimEnd(',');
            logger.LogError(logEntry + "{parameterList}", parameterList);
        }
    }
}