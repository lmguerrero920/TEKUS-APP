using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tekus.Core.Entities;

namespace Tekus.BusinessLogic.Data
{
    public class TekusDbContext : DbContext
    {


        public TekusDbContext(DbContextOptions
            <TekusDbContext>options ):base (options)
            
        {



        }



        public DbSet<Supplier> Supplier { get; set; }  
        public DbSet<Country> Country { get; set; }  
        public DbSet<Services> Services { get; set; }  



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }


    }
}
