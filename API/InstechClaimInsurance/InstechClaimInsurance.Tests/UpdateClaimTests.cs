using ClaimHandlingAPI;
using ClaimHandlingAPI.Controllers;
using ClaimHandlingAPI.Models;
using ClaimHandlingAPI.Services.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace InstechClaimInsurance.Tests
{
    public class UpdateClaimTests
    {

        ServiceBusSender serviceBusSender;
        ClaimController claimController;
        Mock<ICosmosDbService> cosmosDbService;


        public UpdateClaimTests()
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
        public async void UpdateClaim_ReturnsOK()
        {
            cosmosDbService.Setup(m => m.GetItemAsync(It.IsAny<string>())).ReturnsAsync((Claim)new Claim());

            var claim = new Claim
            {
                Id = "123",
                DamageCost = 10,
                Type = TypeEnum.Fire,
                Year = 2020,
                Name = "Ship"
            };

            var response = await claimController.EditAsync(claim);

            response.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async void UpdateClaim_FindItem_ReturnsNotFound()
        {
            cosmosDbService.Setup(m => m.GetItemAsync(It.IsAny<string>())).ReturnsAsync((Claim)null);

            var claim = new Claim
            {
                Id = "123",
                DamageCost = 10,
                Type = TypeEnum.Fire,
                Year = 2020,
                Name = "Ship"
            };

            var response = await claimController.EditAsync(claim);

            response.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void UpdateClaim_NoID_ReturnsBadRequest()
        {
            var claim = new Claim
            {
                DamageCost = 10,
                Type = TypeEnum.Fire,
                Year = 2020,
                Name = "Ship"
            };

            var response = await claimController.EditAsync(claim);

            response.Should().BeOfType<BadRequestResult>();
        }


        [Fact]
        public async void UpdateClaim_ClaimNull_ReturnsBadRequest()
        {

            var response = await claimController.EditAsync(null);

            response.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async void UpdateClaim_BadDamageCostInput_ReturnsBadRequest()
        {
            var claim = new Claim
            {
                DamageCost = 1000.000m,
                Type = TypeEnum.Fire,
                Year = 2020,
                Name = "Ship"
            };

            var response = await claimController.EditAsync(claim);

            response.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async void UpdateClaim_BadYear_ReturnsBadRequest()
        {
            var claim = new Claim
            {
                DamageCost = 10.000m,
                Year = 2010
            };

            var response = await claimController.EditAsync(claim);

            response.Should().BeOfType<BadRequestResult>();
        }

    }
}
