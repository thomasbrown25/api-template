using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using personal_trainer_api.Services.BlobStorageService;

namespace personal_trainer_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BlobStorageController(
        IBlobStorageService blobStorageService
    ) : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService = blobStorageService;

        [HttpPost("upload-image")]
        public async Task<ActionResult<ServiceResponse<string>>> UploadImage()
        {
            string file = "Testfile.txt";
            var response = await _blobStorageService.UploadImage(file);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}