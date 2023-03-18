using Azure.Storage.Blobs;

namespace Gallery.Services;

public class BlobService {
    private readonly BlobContainerClient blobContainer;

    public BlobService(BlobContainerClient blobContainerClient) {
        blobContainer = blobContainerClient;
    }

    public async Task<Stream> GetImage(Guid id) {
        var blobClient = blobContainer.GetBlobClient(id.ToString());
        var blob = await blobClient.DownloadAsync();
        return blob.Value.Content;
    }

    public async Task Upload(IFormFile file, Guid id) {
        BlobClient blobClient = blobContainer.GetBlobClient(id.ToString());
        await blobClient.UploadAsync(file.OpenReadStream(), true);
    }

    public async Task Delete(Guid id) {
        var blob = blobContainer.GetBlobClient(id.ToString());
        await blob.DeleteIfExistsAsync();
    }
}