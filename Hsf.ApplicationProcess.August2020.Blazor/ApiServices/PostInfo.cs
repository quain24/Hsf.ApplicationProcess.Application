namespace Hsf.ApplicationProcess.August2020.Blazor.ApiServices
{
    public class PostInfo
    {
        public bool IsSuccess { get; private set; } = false;
        public bool IsConnectionError { get; private set; }
        public bool IsParameterError { get; private set; }
        public bool IsUnknown { get; private set; }
        public ResponseCodes ResponseCodes { get; }

        public PostInfo(PostStatus status, ResponseCodes responseCodes)
        {
            SetStatusFromEnum(status);
            ResponseCodes = responseCodes;
        }

        private void SetStatusFromEnum(PostStatus status)
        {
            switch (status)
            {
                case PostStatus.Success:
                    IsSuccess = true;
                    break;

                case PostStatus.ConnectionError:
                    IsConnectionError = true;
                    break;

                case PostStatus.ParameterError:
                    IsParameterError = true;
                    break;

                default:
                    IsUnknown = true;
                    break;
            }
        }
    }

    public enum PostStatus
    {
        Success,
        ConnectionError,
        ParameterError,
        Unknown
    }
}