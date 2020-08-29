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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicantsDbContext).Assembly);
            modelBuilder.Entity<Applicant>().HasData(InitialSeedContent());


            base.OnModelCreating(modelBuilder);
        }

        private Applicant[] InitialSeedContent()
        {
            return new[]
            {
                new Applicant
                {
                    Address = "Adress 1",
                    FamilyName = "Adamson",
                    Age = 32,
                    CountryOfOrigin = "Zambia",
                    EmailAddress = "adamson@o2.pl",
                    Hired = false,
                    Name = "Adam",
                    ID = 1
                },
                new Applicant
                {
                    Address = "Adress 2",
                    FamilyName = "Bern",
                    Age = 18,
                    CountryOfOrigin = "Poland",
                    EmailAddress = "adberm@wp.com",
                    Hired = false,
                    Name = "Bernard",
                    ID = 2
                },
                new Applicant
                {
                    Address = "Adress 3",
                    FamilyName = "Colins",
                    Age = 22,
                    CountryOfOrigin = "Germany",
                    EmailAddress = "colins@google.eu",
                    Hired = false,
                    Name = "Cyryl",
                    ID = 3
                },
                new Applicant
                {
                    Address = "Adress 4",
                    FamilyName = "Davids",
                    Age = 56,
                    CountryOfOrigin = "Poland",
                    EmailAddress = "derv@gmail.com",
                    Hired = false,
                    Name = "Dominic",
                    ID = 4
                }
            };
        }
    }
}