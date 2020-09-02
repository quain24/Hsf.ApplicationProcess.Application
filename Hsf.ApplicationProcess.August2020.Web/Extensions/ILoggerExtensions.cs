﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Hsf.ApplicationProcess.August2020.Web.Extensions
{
    public static class ILoggerExtensions
    {
        public static void LogAllErrorsFrom(this ILogger logger, string nameOfActionCausingErrors, ModelStateDictionary modelState)
        {
            var logEntry = $"ERROR - Controller action \"{nameOfActionCausingErrors}\" was called with bad parameters:";
            var parameterList = modelState.Keys.ToList();

            logEntry = parameterList.Aggregate(logEntry, (current, errorType) => current + $" {errorType},").TrimEnd(',');
            logger.LogError(logEntry);
        }
    }
}
