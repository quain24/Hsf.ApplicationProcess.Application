﻿using System;
using System.Collections.Generic;
using System.Text;
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
        public Task<Applicant> GetApplicantById(int id)
        {
            return _context.Applicants.FirstOrDefaultAsync(a => a.ID == id);
        }

        public bool AddNewApplicant(Applicant applicant)
        {
            _context.Applicants.Add(applicant);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateApplicant(int id, Applicant applicant)
        {
            throw new NotImplementedException();
        }

        public bool DeleteApplicant(int id)
        {
            throw new NotImplementedException();
        }
    }
}
