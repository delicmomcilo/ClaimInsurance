using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ClaimAuditFunction
{
    public class ClaimAuditFactory  : IDesignTimeDbContextFactory<ClaimAuditContext>
    {
        public ClaimAuditContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClaimAuditContext>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DatabaseConnectionString"));

            return new ClaimAuditContext(optionsBuilder.Options);
        }
    }
}
