
namespace personal_trainer_api.Services.BlobStorageService;

public interface IBlobStorageService
{
    Task<ServiceResponse<string>> UploadImage(string file);
}