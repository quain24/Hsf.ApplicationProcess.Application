using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hsf.ApplicationProcess.August2020.Domain.Models;
using Hsf.ApplicationProcess.August2020.Web.DTO;

namespace Hsf.ApplicationProcess.August2020.Web.AutomapperProfiles
{
    public class ApplicantProfile : Profile
    {
        public ApplicantProfile()
        {
            CreateMap<Applicant, ApplicantNoIdDTO>();
            CreateMap<ApplicantNoIdDTO, Applicant>();
        }
    }
}
