using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ClaimAuditFunction
{
    public class ClaimsAuditFunction
    {
        private readonly ILogger<ClaimsAuditFunction> _logger;
        private readonly ClaimAuditContext _claimAuditContext;

        public ClaimsAuditFunction(ClaimAuditContext claimAuditContext, ILogger<ClaimsAuditFunction> log)
        {
            _logger = log;
            _claimAuditContext = claimAuditContext;
        }

        [FunctionName("ClaimsAuditFunction")]
        public void Run([ServiceBusTrigger("insetch-dev-weu-sb-topic", "SBT1", Connection = "QueueConnectionString")] string mySbMsg)
        {
            var updateCustomerFullNameModel = JsonConvert.DeserializeObject<ClaimAudit>(mySbMsg);

            _claimAuditContext.Add(updateCustomerFullNameModel);
            _claimAuditContext.SaveChangesAsync();


        }
    }
}
