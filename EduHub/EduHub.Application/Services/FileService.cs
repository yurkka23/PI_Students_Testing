using System.Net.Http.Headers;
using EduHub.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EduHub.Application.Services
{
    public class FileService : IFileService
    {
        public async Task<List<string>> SaveFiles(IEnumerable<IFormFile> collection)
        {
            var files = new List<string>();
            foreach (var file in collection)
            {
                files.Add(await SaveFile(file));
            }

            return files;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            var folderName = Path.Combine("AppFiles", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').Split(".");

            var newFileName = new string(Guid.NewGuid() + "." + fileName.Last());
            var fullPath = Path.Combine(pathToSave, newFileName);
            var path = Path.Combine(folderName, newFileName);
            await using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return path;
        }

        public void DeleteFiles(IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        public void DeleteFile(string file)
        {
            File.Delete(file);
        }
    }
}