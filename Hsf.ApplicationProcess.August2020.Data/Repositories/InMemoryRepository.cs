using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hsf.ApplicationProcess.August2020.Domain.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<int> AddNewApplicantAsync(Applicant applicant)
        {
            await _context.AddAsync(applicant);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateApplicantAsync(int id, Applicant applicant)
        {
            _context.Entry(await _context.Applicants.FirstOrDefaultAsync(x => x.ID == applicant.ID)).CurrentValues.SetValues(applicant);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteApplicantAsync(int id)
        {
            var toBeRemoved = await _context.Applicants.FirstOrDefaultAsync(a => a.ID == id);
            if (toBeRemoved is null)
                return 0;
            _context.Remove(toBeRemoved);
            return await _context.SaveChangesAsync();
        }
    }
}
