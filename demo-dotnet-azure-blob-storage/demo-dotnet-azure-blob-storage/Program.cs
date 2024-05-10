using Azure.Storage.Blobs;
using StorageManager.AzureBlobStorage;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("StorageAccount")));
builder.Services.AddScoped<IStorageManagerRepository, StorageManagerRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();