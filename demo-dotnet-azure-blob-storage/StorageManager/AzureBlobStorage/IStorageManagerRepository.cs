using Microsoft.AspNetCore.Http;
using StorageManager.Dtos;

namespace StorageManager.AzureBlobStorage
{
    public interface IStorageManagerRepository
    {
        Task<bool> DeleteFileByName(string fileName);
        Task<DownloadFileDto?> DownloadFileByNameAsync(string fileName);
        Task<bool> UploadFile(IFormFile file);
    }
}