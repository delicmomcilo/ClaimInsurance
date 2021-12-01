using ClaimHandlingAPI;
using ClaimHandlingAPI.Controllers;
using ClaimHandlingAPI.Models;
using ClaimHandlingAPI.Services.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InstechClaimInsurance.Tests
{
    public class DeleteClaimTests
    {

        ServiceBusSender serviceBusSender;
        ClaimController claimController;
        Mock<ICosmosDbService> cosmosDbService;


        public DeleteClaimTests()
        {
            cosmosDbService = new Mock<ICosmosDbService>();

            // Arrange ServiceBusSender
            // Setup configurationbuilder with CosmosDB string connection that is set in the appsettings.json
            var builder = new ConfigurationBuilder();
            var configuration = builder.SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .Build();

            // Set the config to the ServiceBusSender class
            serviceBusSender = new ServiceBusSender(configuration);
            claimController = new ClaimController(cosmosDbService.Object, serviceBusSender);

        }

        [Fact]
        public async void DeleteClaim_CannotFindClaim_ReturnsNoContent()
        {
            cosmosDbService.Setup(m => m.GetItemAsync(It.IsAny<string>())).ReturnsAsync((Claim)null);

            var response = await claimController.DeleteConfirmedAsync("123");

            response.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void DeleteClaim_Success_ReturnsNoContent()
        {
            var claim = new Claim
            {
                DamageCost = 10,
                Type = TypeEnum.Fire,
                Year = 2020,
                Name = "Ship"
            };

            cosmosDbService.Setup(m => m.GetItemAsync(It.IsAny<string>())).ReturnsAsync(claim);

            var response = await claimController.DeleteConfirmedAsync("123");

            response.Should().BeOfType<NoContentResult>();
        }

    }
}
