using Hsf.ApplicationProcess.August2020.Blazor.Models;
using Hsf.ApplicationProcess.August2020.Domain.Models;

namespace Hsf.ApplicationProcess.August2020.Blazor.Extensions
{
    public static class ApplicantInsertModelExtensions
    {
        public static bool IsDefault(this ApplicantInsertModel model)
        {
            return string.IsNullOrEmpty(model.address) &&
                string.IsNullOrEmpty(model.countryOfOrigin) &&
                string.IsNullOrEmpty(model.emailAddress) &&
                string.IsNullOrEmpty(model.familyName) &&
                string.IsNullOrEmpty(model.name) &&
                model.age == 0;
        }

        public static Applicant ToApplicantModel(this ApplicantInsertModel applicantInsertModel, int id)
        {
            return new Applicant
            {
                Address = applicantInsertModel.address,
                FamilyName = applicantInsertModel.familyName,
                EmailAddress = applicantInsertModel.emailAddress,
                Age = applicantInsertModel.age,
                CountryOfOrigin = applicantInsertModel.countryOfOrigin,
                Hired = applicantInsertModel.hired,
                Name = applicantInsertModel.name,
                ID = id
            };
        }
    }
}