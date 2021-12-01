using Microsoft.EntityFrameworkCore;
using System;

namespace ClaimAuditFunction
{
    public class ClaimAuditContext : DbContext
    {

        public DbSet<ClaimAudit> ClaimAudit { get; set; }

        public ClaimAuditContext(DbContextOptions<ClaimAuditContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ClaimAudit>(entity =>
            {
                entity.HasIndex(e => e.ClaimId).IsClustered(false);
            });

            builder.Entity<ClaimAudit>().HasData(
              new
              {
                  ClaimId = Guid.NewGuid().ToString(),
                  TimeStamp = "11/27/2021 3:13:05 PM",
                  Operation = "Create",
              }, new
              {
                  ClaimId = Guid.NewGuid().ToString(),
                  TimeStamp = "11/27/2021 3:13:05 PM",
                  Operation = "Create",
              }, new
              {
                  ClaimId = Guid.NewGuid().ToString(),
                  TimeStamp = "11/27/2021 3:13:05 PM",
                  Operation = "Create",
              }, new
              {
                  ClaimId = Guid.NewGuid().ToString(),
                  TimeStamp = "11/27/2021 3:13:05 PM",
                  Operation = "Create",
              }
            );
        }
    }
}
