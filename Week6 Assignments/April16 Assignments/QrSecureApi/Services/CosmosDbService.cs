using Microsoft.Azure.Cosmos;
using QrSecureApi.Models;

public class CosmosDbService
{
    private readonly Container _container;

    public CosmosDbService(CosmosClient client, string db, string container)
    {
        _container = client.GetContainer(db, container);
    }

    public async Task SaveAsync(QrItem item)
    {
        await _container.CreateItemAsync(item);
    }

    public async Task<QrItem?> GetAsync(string id)
    {
        var response = await _container.ReadItemAsync<QrItem>(id, new PartitionKey(id));
        return response.Resource;
    }

    public async Task<Product?> GetItemAsync(string id)
    {
        var response = await _container.ReadItemAsync<Product>(id, new PartitionKey(id));
        return response.Resource;
    }

    public async Task SaveSessionAsync(LoginSession session)
    {
        await _container.CreateItemAsync(session);
    }

    public async Task<LoginSession?> GetSessionAsync(string id)
    {
        var response = await _container.ReadItemAsync<LoginSession>(id, new PartitionKey(id));
        return response.Resource;
    }
    public async Task CreateItemAsync(Product item)
    {
        await _container.CreateItemAsync(item, new PartitionKey(item.id));
    }

    public async Task UpdateItemAsync(Product item)
    {
        await _container.UpsertItemAsync(item, new PartitionKey(item.id));
    }

    public async Task DeleteItemAsync(string id)
    {
        await _container.DeleteItemAsync<Product>(id, new PartitionKey(id));
    }

    public async Task UpdateSessionAsync(LoginSession session)
    {
        await _container.UpsertItemAsync(session);
    }
    public async Task<List<Product>> GetAllItemsAsync()
    {
        var query = new QueryDefinition("SELECT * FROM c");

        var iterator = _container.GetItemQueryIterator<Product>(query);

        var results = new List<Product>();

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }


    public async Task<LoginSession?> GetSessionByTokenAsync(string token)
    {
        var query = new QueryDefinition(
            "SELECT * FROM c WHERE c.Token = @token")
            .WithParameter("@token", token);

        var iterator = _container.GetItemQueryIterator<LoginSession>(query);

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            return response.FirstOrDefault();
        }


        return null;
    }
}