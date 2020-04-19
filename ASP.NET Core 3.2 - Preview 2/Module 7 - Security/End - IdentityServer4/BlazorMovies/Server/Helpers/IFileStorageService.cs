using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Helpers
{
    public interface IFileStorageService
    {
        Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute);
        Task DeleteFile(string fileRoute, string containerName);
        Task<string> SaveFile(byte[] content, string extension, string containerName);
    }
}
