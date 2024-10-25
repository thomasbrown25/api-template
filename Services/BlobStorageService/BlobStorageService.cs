

using Azure.Storage.Blobs;
using personal_trainer_api.Services.LoggingService;

namespace personal_trainer_api.Services.BlobStorageService;

public class BlobStorageService(
    IConfiguration configuration,
    ILoggingService loggingService,
    BlobServiceClient blobServiceClient) : IBlobStorageService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly ILoggingService _loggingService = loggingService;
    private readonly BlobServiceClient _blobServiceClient = blobServiceClient;

    public async Task<ServiceResponse<string>> UploadImage(string file)
    {
        ServiceResponse<string> response = new();
        string containerName = "profilepics";
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        try
        {

            // Create a local file in the ./data/ directory for uploading and downloading
            string localFilePath = "C:\\Projects\\personal-trainer-app\\src\\assets\\images\\profile-pic.txt";

            // Write text to the file
            await File.WriteAllTextAsync(localFilePath, "Hello, World!");

            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient("profile-pic.txt");

            // Upload data from the local file, overwrite the blob if it already exists
            await blobClient.UploadAsync(localFilePath, true);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Success = false;
            _loggingService.LogException(ex);
        }

        return response;
    }
}