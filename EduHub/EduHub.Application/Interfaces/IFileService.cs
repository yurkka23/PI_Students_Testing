using Microsoft.AspNetCore.Http;

namespace EduHub.Application.Interfaces;

public interface IFileService
{
    Task<List<string>> SaveFiles(IEnumerable<IFormFile> collection);
    Task<string> SaveFile(IFormFile file);
    void DeleteFiles(IEnumerable<string> files);
    void DeleteFile(string file);
}
