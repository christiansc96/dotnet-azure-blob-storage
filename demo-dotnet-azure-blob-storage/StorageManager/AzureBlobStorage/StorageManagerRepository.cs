using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using StorageManager.Dtos;

namespace StorageManager.AzureBlobStorage
{
    public class StorageManagerRepository : IStorageManagerRepository
    {
        private const string ContainerName = "YourContainerNameHere";
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        public StorageManagerRepository(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            _containerClient.CreateIfNotExists();
        }

        public async Task<bool> DeleteFileByName(string fileName)
        {
            bool result = false;
            try
            {
                BlobClient blobClient = _containerClient.GetBlobClient(fileName);
                await blobClient.DeleteIfExistsAsync();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while deleting the file: " + ex.Message);
            }
            return result;
        }

        public async Task<DownloadFileDto?> DownloadFileByNameAsync(string fileName)
        {
            try
            {
                BlobClient blobClient = _containerClient.GetBlobClient(fileName);
                MemoryStream memoryStream = new();
                await blobClient.DownloadToAsync(memoryStream);
                memoryStream.Position = 0;
                string contentType = blobClient.GetProperties().Value.ContentType;
                return new DownloadFileDto()
                {
                    ContentType = contentType,
                    FileName = fileName,
                    Stream = memoryStream
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while download the file: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> UploadFile(IFormFile file)
        {
            bool result = false;
            try
            {
                BlobClient blobClient = _containerClient.GetBlobClient(file.FileName);
                await blobClient.UploadAsync(file.OpenReadStream(), true);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while uploading the file: " + ex.Message);
            }
            return result;
        }
    }
}