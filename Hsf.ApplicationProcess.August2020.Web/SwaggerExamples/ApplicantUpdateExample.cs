using Hsf.ApplicationProcess.August2020.Domain.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Hsf.ApplicationProcess.August2020.Web.SwaggerExamples
{
    public class ApplicantUpdateExample : IExamplesProvider<Applicant>
    {
        public Applicant GetExamples()
        {
            return new Applicant
            {
                ID = 1,
                Name = "Gregory",
                FamilyName = "Novak",
                Age = 56,
                Address = "Avenue 5",
                CountryOfOrigin = "England",
                EmailAddress = "Greg@google.com",
                Hired = false
            };
        }
    }
}