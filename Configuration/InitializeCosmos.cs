using Gallery.Services;
using Microsoft.Azure.Cosmos;

namespace Gallery.Configuration;

public class InitializeCosmos {

    public static async Task<CosmosService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection) {
        CosmosClientOptions cosmosClientOptions = new CosmosClientOptions() {
            HttpClientFactory = () => {
                HttpMessageHandler httpMessageHandler = new HttpClientHandler() {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                return new HttpClient(httpMessageHandler);
            },
            ConnectionMode = ConnectionMode.Gateway
        };

        var databaseName = configurationSection["DatabaseName"];
        var containerName = configurationSection["ContainerName"];
        var account = configurationSection["Account"];
        var key = configurationSection["Key"];
        var client = new CosmosClient(account, key, cosmosClientOptions);
        var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
        await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
        var cosmosDbService = new CosmosService(client, databaseName, containerName);

        return cosmosDbService;
    }
}