using System;
using System.Collections.Generic;
using System.Text;
using Hsf.ApplicationProcess.August2020.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsf.ApplicationProcess.August2020.Data.Configuration
{
    class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.Address).IsRequired().HasMaxLength(300);
            builder.Property(a => a.Age).IsRequired();
            builder.Property(a => a.CountryOfOrigin).IsRequired().HasMaxLength(100);
            builder.Property(a => a.EmailAddress).IsRequired().HasMaxLength(300);
            builder.Property(a => a.FamilyName).IsRequired().HasMaxLength(300);
            builder.Property(a => a.Hired).IsRequired();
        }
    }
}
