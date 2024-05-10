namespace demo_dotnet_azure_blob_storage.Extensions
{
    public static class ObjectExtension
    {
        public static bool IsNull<T>(this T value)
        {
            return value == null;
        }
    }
}