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
    public class GetClaimTests
    {

        ServiceBusSender serviceBusSender;
        ClaimController claimController;
        Mock<ICosmosDbService> cosmosDbService;


        public GetClaimTests()
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
        public async void GetClaim_ReturnOk()
        {
            cosmosDbService.Setup(m => m.GetItemAsync(It.IsAny<string>())).ReturnsAsync(new Claim());

            var response = await claimController.DetailsAsync("123");

            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetClaim_ReturnNotFound()
        {
            cosmosDbService.Setup(m => m.GetItemAsync(It.IsAny<string>())).ReturnsAsync((Claim)null);

            var response = await claimController.DetailsAsync("123");

            response.Should().BeOfType<NotFoundResult>();
        }


    }

}