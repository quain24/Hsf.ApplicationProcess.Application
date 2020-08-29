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
        Task<Applicant> GetApplicantByIdAsync(int id);
        Task<int> AddNewApplicantAsync(Applicant applicant);
        Task<int> UpdateApplicantAsync(int id, Applicant applicant);
        Task<int> DeleteApplicantAsync(int id);
    }
}
