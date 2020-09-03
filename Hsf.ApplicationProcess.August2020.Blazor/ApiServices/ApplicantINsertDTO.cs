using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hsf.ApplicationProcess.August2020.Blazor.ApiServices
{
    public class ApplicantInsertDTO
    {
        public string name { get; set; }
        public string familyName { get; set; }
        public string address { get; set; }
        public string countryOfOrigin { get; set; }
        public string emailAddress { get; set; }
        public int age { get; set; }
        public bool? hired { get; set; }
    }
}
