using System;
using ClaimAuditFunction;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace ClaimAuditFunction
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString");
            builder.Services.AddDbContext<ClaimAuditContext>(options => options.UseSqlServer(connectionString));
        }
    }
}