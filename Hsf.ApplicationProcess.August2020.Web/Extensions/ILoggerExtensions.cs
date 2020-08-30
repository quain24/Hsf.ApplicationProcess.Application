using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Hsf.ApplicationProcess.August2020.Web.Extensions
{
    public static class ILoggerExtensions
    {
        public static void LogAllErrorsFrom(this ILogger logger, string nameOfActionCausingErrors, ModelStateDictionary modelState)
        {
            var logEntry = $"Controller action \"{nameOfActionCausingErrors}\" was called and executed with errors:";

            logEntry = modelState.Values.SelectMany(state => state.Errors)
                .Aggregate(logEntry, (current, error) => current + $"\n{error.ErrorMessage}");

            logger.LogError(logEntry);
        }
    }
}
