using System.Collections.Generic;

namespace Hsf.ApplicationProcess.August2020.Web.Config
{
    public class LanguageConfig
    {
        public string DefaultLanguage { get; set; }
        public string FallbackLanguage { get; set; }
        public string LocalesFolderLocation { get; set; }
        public List<string> SupportedLanguages { get; set; }
    }
}