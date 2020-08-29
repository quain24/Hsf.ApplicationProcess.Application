using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hsf.ApplicationProcess.August2020.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Hsf.ApplicationProcess.August2020.Data.Repositories
{
    public interface IApplicantRepository
    {
        Task<Applicant> GetApplicantById(int id);
        bool AddNewApplicant(Applicant applicant);
        bool UpdateApplicant(int id, Applicant applicant);
        bool DeleteApplicant(int id);
    }
}
