using Hsf.ApplicationProcess.August2020.Domain.Models;
using System.Threading.Tasks;

namespace Hsf.ApplicationProcess.August2020.Data.Repositories
{
    public interface IApplicantRepository
    {
        Task<Applicant> GetApplicantByIdAsync(int id);

        Task<bool> AddNewApplicantAsync(Applicant applicant);

        Task<bool> UpdateApplicantAsync(int id, Applicant applicant);

        Task<bool> DeleteApplicantAsync(int id);

        Task<int> SaveChangesAsync();
    }
}