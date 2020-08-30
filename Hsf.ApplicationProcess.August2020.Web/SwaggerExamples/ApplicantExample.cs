using Hsf.ApplicationProcess.August2020.Domain.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Hsf.ApplicationProcess.August2020.Web.SwaggerExamples
{
    public class ApplicantExample : IExamplesProvider<Applicant>
    {
        public Applicant GetExamples()
        {
            return new Applicant
            {
                Name = "Henryk",
                FamilyName = "Wiśniewski",
                Age = 35,
                Address = "Dąbrowskiego 107/1",
                CountryOfOrigin = "Poland",
                EmailAddress = "henryk.wisniewski@gmail.com",
                Hired = true
            };
        }
    }
}