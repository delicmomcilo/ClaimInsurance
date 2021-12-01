using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClaimHandlingAPI.Models;
using ClaimHandlingAPI.Services.Services;
using System.Collections.Generic;

namespace ClaimHandlingAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ClaimController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;
        private ServiceBusSender _serviceBusSender;
        public ClaimController(ICosmosDbService cosmosDbService, ServiceBusSender serviceBusSender)
        {
            _cosmosDbService = cosmosDbService;
            _serviceBusSender = serviceBusSender;
        }

        [HttpPut]
        public async Task<ActionResult> EditAsync(Claim claim)
        {
            if(claim == null || claim.Id == null)
            {
                return BadRequest();
            } else
            {
                if (claim.DamageCost > 100.000m)
                {
                    return BadRequest();
                }

                var currentYear = DateTime.Now.Year;
                if (claim.Year > currentYear || claim.Year < (currentYear - 10))
                    return BadRequest();
            }
            var findClaim = await _cosmosDbService.GetItemAsync(claim.Id);

            if (findClaim == null)
            {
                return NotFound();
            }

            await _cosmosDbService.UpdateItemAsync(claim.Id, claim);

            await _serviceBusSender.SendMessage(new ClaimAudit
            {
                ClaimId = claim.Id,
                TimeStamp = DateTime.Now.ToString(),
                Operation = System.Net.Http.HttpMethod.Put.Method
            });

            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteConfirmedAsync(string id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var claim = await _cosmosDbService.GetItemAsync(id);

            if (claim == null)
            {
                return NotFound();
            }

            await _cosmosDbService.DeleteItemAsync(id);

            await _serviceBusSender.SendMessage(new ClaimAudit
            {
                ClaimId = claim.Id,
                TimeStamp = DateTime.Now.ToString(),
                Operation = System.Net.Http.HttpMethod.Delete.Method
            });

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            var claim = await _cosmosDbService.GetItemAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            return Ok(claim);
        }


        [HttpGet]
        public async Task<ActionResult<List<Claim>>> GetItemsAsync()
        {
            var claims = await _cosmosDbService.GetItemsAsync();
            return Ok(claims);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Claim claim)
        {
            if(claim == null)
            {
                return BadRequest();
            }
            if (claim != null)
            {
                if (claim.DamageCost > 100.000m)
                {
                    return BadRequest();
                }

                var currentYear = DateTime.Now.Year;
                if (claim.Year > currentYear || claim.Year < (currentYear - 10))
                    return BadRequest();
            }

            claim.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddItemAsync(claim);

            await _serviceBusSender.SendMessage(new ClaimAudit
            {
                ClaimId = claim.Id,
                TimeStamp = DateTime.Now.ToString(),
                Operation = System.Net.Http.HttpMethod.Post.Method
            });

            return new OkObjectResult(claim);
        }
    }
}