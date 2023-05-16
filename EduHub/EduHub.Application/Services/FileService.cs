using System.Net.Http.Headers;
using EduHub.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using EduHub.Domain.Settings;

namespace EduHub.Application.Services;

public class FileService : IFileService
{
    private readonly BlobContainerClient _blobContainerClient;
    private readonly BlobStorageSettings _blobStorageSettings;

    public FileService(BlobStorageSettings blobStorageSettings)
    {
        _blobStorageSettings = blobStorageSettings;
        _blobContainerClient = new BlobContainerClient(_blobStorageSettings.ConectionString, 
            _blobStorageSettings.Container);
    }
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
        await using var memory = new MemoryStream();
        await file.CopyToAsync(memory);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var blob = _blobContainerClient.GetBlobClient(fileName);
        memory.Position = 0;

        await blob.UploadAsync(memory, true);

        var fileUrl = GetFileUrl(fileName);

        return fileUrl;
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
        //File.Delete(file);
    }
    private string GetFileUrl(string fileName) =>
       $"{_blobStorageSettings.BlobUrl}/{fileName}";
}