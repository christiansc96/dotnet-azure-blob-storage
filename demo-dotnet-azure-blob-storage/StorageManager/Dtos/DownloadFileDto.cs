namespace StorageManager.Dtos
{
    public class DownloadFileDto
    {
        public required MemoryStream Stream { get; set; }
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
    }
}