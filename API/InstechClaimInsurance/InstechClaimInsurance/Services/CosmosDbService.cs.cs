using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using ClaimHandlingAPI.Models;
using ClaimHandlingAPI.Services.Services;
using System.Linq;
using System;
using System.Collections.Generic;

namespace ClaimHandlingAPI.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Claim>(id, new PartitionKey(id));
        }

        public async Task AddItemAsync(Claim claim)
        {
            await this._container.CreateItemAsync<Claim>(claim, new PartitionKey(claim.Id));
        }

        public async Task<List<Claim>> GetItemsAsync()
        {
            string sqlQueryText = "SELECT * FROM c";

            QueryDefinition definition = new QueryDefinition(sqlQueryText);

            var iterator = _container.GetItemQueryIterator<Claim>(definition);

            var tempList = new List<Claim>();

            foreach (Claim claimVal in await iterator.ReadNextAsync())
            {
                tempList.Add(claimVal);
            }

            return tempList;
        }

        public async Task<Claim> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Claim> response = await this._container.ReadItemAsync<Claim>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task UpdateItemAsync(string id, Claim claim)
        {
            await this._container.UpsertItemAsync<Claim>(claim, new PartitionKey(id));
        }
    }
}