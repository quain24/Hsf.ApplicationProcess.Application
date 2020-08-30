using Hsf.ApplicationProcess.August2020.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hsf.ApplicationProcess.August2020.Data.Repositories
{
    public class InMemoryRepository : IApplicantRepository
    {
        private readonly ApplicantsDbContext _context;

        public InMemoryRepository(ApplicantsDbContext context)
        {
            _context = context;

            // Ensures proper initial seeding in ApplicantsDbContext
            _context.Database.EnsureCreated();
        }

        public Task<Applicant> GetApplicantByIdAsync(int id)
        {
            return _context.Applicants.FirstOrDefaultAsync(a => a.ID == id);
        }

        public async Task<bool> AddNewApplicantAsync(Applicant applicant)
        {
            if (await _context.Applicants.AnyAsync(a => a.ID == applicant.ID))
                return false;
            await _context.AddAsync(applicant);
            return true;
        }

        public async Task<bool> UpdateApplicantAsync(int id, Applicant applicant)
        {
            if (!await _context.Applicants.AnyAsync(a => a.ID == id))
                return false;

            _context.Entry(applicant).State = EntityState.Modified;
            return true;
        }

        public async Task<bool> DeleteApplicantAsync(int id)
        {
            var toBeRemoved = await _context.Applicants.FirstOrDefaultAsync(a => a.ID == id);
            if (toBeRemoved is null)
                return false;
            _context.Remove(toBeRemoved);
            return true;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}