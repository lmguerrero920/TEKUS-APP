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
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        /// <summary>
        /// Definición de Data Annotations
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Property(s=>s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s=>s.Email).IsRequired().HasMaxLength(100);
            builder.Property(s=>s.NIT).IsRequired().HasMaxLength(15);
            
            builder.HasOne(se=>se.Services).WithMany().HasForeignKey(s=>s.ServicesId);

        }
    }
}
