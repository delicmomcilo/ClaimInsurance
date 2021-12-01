﻿// <auto-generated />
using ClaimAuditFunction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClaimAuditFunction.Migrations
{
    [DbContext(typeof(ClaimAuditContext))]
    partial class ClaimAuditContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ClaimAuditFunction.ClaimAudit", b =>
                {
                    b.Property<string>("ClaimId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Operation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeStamp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClaimId");

                    b.HasIndex("ClaimId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClaimId"), false);

                    b.ToTable("ClaimAudit");

                    b.HasData(
                        new
                        {
                            ClaimId = "359c6bec-0563-4002-88a8-5c2e3489618d",
                            Operation = "Create",
                            TimeStamp = "11/27/2021 3:13:05 PM"
                        },
                        new
                        {
                            ClaimId = "b1000827-e885-4ad0-9fa8-05e3d31dffbb",
                            Operation = "Create",
                            TimeStamp = "11/27/2021 3:13:05 PM"
                        },
                        new
                        {
                            ClaimId = "b09e007a-3cd3-4bf4-b840-a1d3f69637b6",
                            Operation = "Create",
                            TimeStamp = "11/27/2021 3:13:05 PM"
                        },
                        new
                        {
                            ClaimId = "e1aed24c-ce76-41a5-a075-dc2733697b60",
                            Operation = "Create",
                            TimeStamp = "11/27/2021 3:13:05 PM"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
