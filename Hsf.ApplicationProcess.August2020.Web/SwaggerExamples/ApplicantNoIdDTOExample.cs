using Hsf.ApplicationProcess.August2020.Web.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace Hsf.ApplicationProcess.August2020.Web.SwaggerExamples
{
    public class ApplicantNoIdDTOExample : IExamplesProvider<ApplicantNoIdDTO>
    {
        public ApplicantNoIdDTO GetExamples()
        {
            return new ApplicantNoIdDTO()
            {
                name = "Gregor",
                familyName = "Novak",
                age = 56,
                address = "Avenue 5 Boston",
                countryOfOrigin = "Germany",
                emailAddress = "Greg@google.com",
                hired = false
            };
        }
    }
}