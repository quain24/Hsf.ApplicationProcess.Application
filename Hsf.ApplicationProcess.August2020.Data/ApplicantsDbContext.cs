using Hsf.ApplicationProcess.August2020.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hsf.ApplicationProcess.August2020.Data
{
    public class ApplicantsDbContext : DbContext
    {
        public ApplicantsDbContext(DbContextOptions<ApplicantsDbContext> options) : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }
    }
}