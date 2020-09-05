namespace Hsf.ApplicationProcess.August2020.Blazor.Models
{
    public class ApplicantInsertModel
    {
        public string name { get; set; }
        public string familyName { get; set; }
        public string address { get; set; }
        public string countryOfOrigin { get; set; }
        public string emailAddress { get; set; }
        public int age { get; set; }
        public bool hired { get; set; }

        public string fullName => string.Concat(name, " ", familyName);
    }
}
