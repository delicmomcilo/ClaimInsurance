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
    public class CreateClaimTests
    {

        ServiceBusSender serviceBusSender;
        ClaimController claimController;
        Mock<ICosmosDbService> cosmosDbService;


        public CreateClaimTests()
        {
            cosmosDbService = new Mock<ICosmosDbService>();

            // Arrange ServiceBusSender
            // Setup configurationbuilder with CosmosDB string connection that is set in the appsettings.json
            var builder = new ConfigurationBuilder();
            var configuration = builder.SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .Build();

            // Set the config to the ServiceBusSender 

            serviceBusSender = new ServiceBusSender(configuration);

            claimController = new ClaimController(cosmosDbService.Object, serviceBusSender);

        }

        [Fact]
        public async void CreateClaim_ReturnsOK()
        {
            var claim = new Claim
            {
                DamageCost = 10,
                Type = TypeEnum.Fire,
                Year = 2020,
                Name = "Ship"
            };

            var response = await claimController.CreateAsync(claim);

            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void CreateClaim_ClaimNull_ReturnsBadRequest()
        {

            var response = await claimController.CreateAsync(null);

            response.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async void CreateClaim_BadDamageCostInput_ReturnsBadRequest()
        {
            var claim = new Claim
            {
                DamageCost = 1000.000m,
                Type = TypeEnum.Fire,
                Year = 2020,
                Name = "Ship"
            };

            var response = await claimController.CreateAsync(claim);

            response.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async void CreateClaim_BadYear_ReturnsBadRequest()
        {
            var claim = new Claim
            {
                DamageCost = 10.000m,
                Year = 2010
            };

            var response = await claimController.CreateAsync(claim);

            response.Should().BeOfType<BadRequestResult>();
        }

    }
}
