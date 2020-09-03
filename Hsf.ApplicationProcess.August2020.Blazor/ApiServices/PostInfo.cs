using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Hsf.ApplicationProcess.August2020.Blazor.ApiServices
{
    public class PostInfo
    {
        public bool Success { get; }
        public ResponseCodes ResponseCodes { get; }

        public PostInfo(bool success, ResponseCodes responseCodes)
        {
            Success = success;
            ResponseCodes = responseCodes;
        }
    }
}
