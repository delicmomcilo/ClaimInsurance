using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ClaimHandlingAPI.Models;
using System.Text;
using System.Threading.Tasks;

namespace ClaimHandlingAPI
{
    public class ServiceBusSender
    {
        private readonly TopicClient _topicClient;
        private readonly IConfiguration _configuration;
        private const string TopicName = "insetch-dev-weu-sb-topic";

        public ServiceBusSender(IConfiguration configuration)
        {
            _configuration = configuration;
            var serviceBusConnectionString = _configuration.GetSection("CosmosDb:ServiceBusConnectionString").Value;
            _topicClient = new TopicClient(
              serviceBusConnectionString,
              TopicName);
        }

        public async Task SendMessage(ClaimAudit payload)
        {
            string data = JsonConvert.SerializeObject(payload);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            await _topicClient.SendAsync(message);
        }
    }
}