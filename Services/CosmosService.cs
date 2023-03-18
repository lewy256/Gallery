using Gallery.Models;
using Microsoft.Azure.Cosmos;

namespace Gallery.Services;

public class CosmosService {
    private readonly Container _container;

    public CosmosService(CosmosClient cosmosClient, string databaseName, string containerName) {
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task AddAsync(Image image) {
        await _container.CreateItemAsync(image, new PartitionKey(image.Id));
    }

    public async Task DeleteAsync(Guid id) {
        var itemId = id.ToString();
        await _container.DeleteItemAsync<Image>(itemId, new PartitionKey(itemId));
    }

    public async Task<IEnumerable<Image>> GetMultipleAsync(string queryString) {
        var query = _container.GetItemQueryIterator<Image>(new QueryDefinition(queryString));
        var results = new List<Image>();
        while (query.HasMoreResults) {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }
}