namespace Hsf.ApplicationProcess.August2020.Blazor.ApiServices
{
    public class ApiInfo
    {
        public bool IsSuccess { get; private set; } = false;
        public bool IsConnectionError { get; private set; }
        public bool IsParameterError { get; private set; }
        public bool IsInputFormatError { get; private set; }
        public bool IsUnknown { get; private set; }
        public ResponseCodes ResponseCodes { get; }

        public ApiInfo(Status status, ResponseCodes responseCodes)
        {
            SetStatusFromEnum(status);
            ResponseCodes = responseCodes;
        }

        private void SetStatusFromEnum(Status status)
        {
            switch (status)
            {
                case Status.Success:
                    IsSuccess = true;
                    break;

                case Status.ConnectionError:
                    IsConnectionError = true;
                    break;

                case Status.ParameterError:
                    IsParameterError = true;
                    break;

                case Status.InputFormatError:
                    IsInputFormatError = true;
                    break;

                default:
                    IsUnknown = true;
                    break;
            }
        }
    }

    public enum Status
    {
        Success,
        ConnectionError,
        ParameterError,
        InputFormatError,
        NotFound,
        Unknown
    }
}