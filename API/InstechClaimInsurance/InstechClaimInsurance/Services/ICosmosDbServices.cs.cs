namespace ClaimHandlingAPI.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ClaimHandlingAPI.Models;

    public interface ICosmosDbService
    {
        Task AddItemAsync(Claim item);
        Task<Claim> GetItemAsync(string id);
        Task DeleteItemAsync(string id);
        Task<List<Claim>> GetItemsAsync();
        Task UpdateItemAsync(string id, Claim claim);
    }
}