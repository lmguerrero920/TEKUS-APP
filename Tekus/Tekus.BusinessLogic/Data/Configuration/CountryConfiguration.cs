using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Core.Entities;

namespace Tekus.BusinessLogic.Data.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
           // builder.Property(s => s.).IsRequired().HasColumnType("decimal(18,2)");

          //builder.HasOne(co => co.Country).WithMany().HasForeignKey(s => s.CountryId);


        }
    }
}
