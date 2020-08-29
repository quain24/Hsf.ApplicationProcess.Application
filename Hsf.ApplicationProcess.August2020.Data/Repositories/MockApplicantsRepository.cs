﻿using Hsf.ApplicationProcess.August2020.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hsf.ApplicationProcess.August2020.Data.Repositories
{
    public class MockApplicantsRepository : IApplicantRepository
    {
        private readonly List<Applicant> _applicants = new List<Applicant>();

        public MockApplicantsRepository()
        {
            _applicants.AddRange(new Applicant[]
            {
                new Applicant{Address = "Adress 1", FamilyName = "Adamson", Age = 32, CountryOfOrigin = "Zambia", EmailAddress = "adamson@o2.pl", Hired = false, Name = "Adam", ID = 1},
                new Applicant{Address = "Adress 2", FamilyName = "Bern", Age = 18, CountryOfOrigin = "Poland", EmailAddress = "adberm@wp.com", Hired = false, Name = "Bernard", ID = 2},
                new Applicant{Address = "Adress 3", FamilyName = "Colins", Age = 22, CountryOfOrigin = "Germany", EmailAddress = "colins@google.eu", Hired = false, Name = "Cyryl", ID = 3},
                new Applicant{Address = "Adress 4", FamilyName = "Davids", Age = 56, CountryOfOrigin = "Poland", EmailAddress = "derv@gmail.com", Hired = false, Name = "Dominic", ID = 4}
            });
        }

        public Task<Applicant> GetApplicantById(int id)
        {
            return Task.FromResult(_applicants.FirstOrDefault(a => a.ID == id));
        }

        public bool AddNewApplicant(Applicant applicant)
        {
            _applicants.Add(applicant);
            return true;
        }

        public bool UpdateApplicant(int id, Applicant applicant)
        {
            var applicantToUpdate = _applicants.FirstOrDefault(a => a.ID == id);
            if (applicantToUpdate is null)
                return false;
            _applicants.Remove(applicantToUpdate);
            _applicants.Add(applicant);
            return true;
        }

        public bool DeleteApplicant(int id)
        {
            var applicantToRemoval = _applicants.FirstOrDefault(a => a.ID == id);
            if (applicantToRemoval?.ID == id)
            {
                _applicants.Remove(applicantToRemoval);
                return true;
            }

            return false;
        }
    }
}