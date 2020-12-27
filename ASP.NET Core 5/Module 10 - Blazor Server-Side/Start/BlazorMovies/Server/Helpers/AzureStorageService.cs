using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Helpers
{
    public class AzureStorageService : IFileStorageService
    {
        private string connectionString;

        public AzureStorageService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorageConnection");
        }

        public async Task DeleteFile(string fileRoute, string containerName)
        {
            if (string.IsNullOrEmpty(fileRoute))
            {
                return;
            }

            var client = new BlobContainerClient(connectionString, containerName);
            await client.CreateIfNotExistsAsync();
            var fileName = Path.GetFileName(fileRoute);
            var blob = client.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditFile(byte[] content, string extension, 
            string containerName, string fileRoute)
        {
            await DeleteFile(fileRoute, containerName);
            return await SaveFile(content, extension, containerName);
        }

        public async Task<string> SaveFile(byte[] content, string extension, string containerName)
        {
            var client = new BlobContainerClient(connectionString, containerName);
            await client.CreateIfNotExistsAsync();
            client.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var blob = client.GetBlobClient(fileName);
            using (var ms = new MemoryStream(content))
            {
                await blob.UploadAsync(ms);
            }
            return blob.Uri.ToString();
        }
    }
}
