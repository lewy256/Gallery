using Azure.Storage.Blobs;
using Gallery.Services;

namespace Gallery.Configuration;

public class InitializeStorage {

    public static async Task<BlobService> InitializeStorageClientInstanceAsync(IConfigurationSection configurationSection) {
        BlobServiceClient blobServiceClient = new BlobServiceClient(configurationSection["ConnectionString"]);

        var containerName = configurationSection["Container"];

        var container = blobServiceClient.GetBlobContainerClient(containerName);

        if (!container.Exists()) {
            await blobServiceClient.CreateBlobContainerAsync(containerName);
        }

        var blobService = new BlobService(container);

        return blobService;
    }
}