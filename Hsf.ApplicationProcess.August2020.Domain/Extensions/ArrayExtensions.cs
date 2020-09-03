using System;
using System.Collections.Generic;
using System.Text;

namespace Hsf.ApplicationProcess.August2020.Domain.Extensions
{
    public static class ArrayExtensions
    {
        public static bool IsNullOrEmpty<T>(this T[] array) =>
            array is null || array.Length == 0;
    }
}
