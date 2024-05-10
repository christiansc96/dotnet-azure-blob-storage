using demo_dotnet_azure_blob_storage.Extensions;
using Microsoft.AspNetCore.Mvc;
using StorageManager.AzureBlobStorage;
using StorageManager.Dtos;

namespace demo_dotnet_azure_blob_storage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController(IStorageManagerRepository storageManagerRepository) : ControllerBase
    {
        private readonly IStorageManagerRepository _storageManagerRepository = storageManagerRepository;

        [HttpGet]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            DownloadFileDto? fileResponse = await _storageManagerRepository.DownloadFileByNameAsync(fileName);
            return !fileResponse.IsNull() 
                ? File(fileResponse!.Stream, fileResponse.ContentType, fileResponse.FileName) : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            bool fileResponse = await _storageManagerRepository.UploadFile(file);
            return fileResponse ? Ok(fileResponse) : BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            bool fileResponse = await _storageManagerRepository.DeleteFileByName(fileName);
            return fileResponse ? Ok(fileResponse) : BadRequest();
        }
    }
}