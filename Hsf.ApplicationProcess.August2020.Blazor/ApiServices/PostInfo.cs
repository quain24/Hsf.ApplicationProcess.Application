using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Hsf.ApplicationProcess.August2020.Blazor.ApiServices
{
    public class PostInfo
    {
        public bool IsSuccess { get; }
        public ResponseCodes ResponseCodes { get; }

        public PostInfo(bool success, ResponseCodes responseCodes)
        {
            IsSuccess = success;
            ResponseCodes = responseCodes;
        }
    }
}
