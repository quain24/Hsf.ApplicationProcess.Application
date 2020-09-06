using Hsf.ApplicationProcess.August2020.Blazor.Models;
using Hsf.ApplicationProcess.August2020.Domain.Models;

namespace Hsf.ApplicationProcess.August2020.Blazor.Extensions
{
    public static class ApplicantModelExtensions
    {
        public static ApplicantInsertModel ToApplicantInsertModel(this Applicant applicantModel)
        {
            return new ApplicantInsertModel
            {
                address = applicantModel.Address,
                age = applicantModel.Age,
                countryOfOrigin = applicantModel.CountryOfOrigin,
                emailAddress = applicantModel.EmailAddress,
                familyName = applicantModel.FamilyName,
                hired = applicantModel.Hired ?? false,
                name = applicantModel.Name
            };
        }
    }
}