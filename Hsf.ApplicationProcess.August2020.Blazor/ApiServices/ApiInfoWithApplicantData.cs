using Hsf.ApplicationProcess.August2020.Domain.Models;

namespace Hsf.ApplicationProcess.August2020.Blazor.ApiServices
{
    public class ApiInfoWithApplicantData : ApiInfo
    {
        private readonly Applicant _returnedApplicant;

        public ApiInfoWithApplicantData(Status status, ResponseCodes responseCodes, Applicant returnedApplicant = null) : base(status, responseCodes)
        {
            _returnedApplicant = returnedApplicant;
            if (status == Status.NotFound)
                IsNotFound = true;
        }

        public bool IsNotFound { get; private set; }

        public Applicant GetRetrievedData => _returnedApplicant ?? new Applicant();
    }
}