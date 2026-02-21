using System;
using ComprasVentas.Services.spec;

namespace ComprasVentas.Services.impl;

public class FileService : IFileService
{
    private readonly IConfiguration _configuration;

    private readonly IWebHostEnvironment _environment;

    public FileService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        _environment = webHostEnvironment;
    }

    public Task<Stream> GetFileAsync(string filePath)
    {
        var fullpath = Path.Combine(_environment.ContentRootPath, filePath);
        if (!File.Exists(fullpath))
        {
            throw new FileNotFoundException("Archivo no encontrado", fullpath);
        }

        return Task.FromResult<Stream>(new FileStream(fullpath, FileMode.Open, FileAccess.Read));
    }

    public async Task<string> SaveFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is null or empty");
        }

        var storagePath = _configuration.GetValue<string>("Storage:ImageDirectory");

        var uploadPath = Path.Combine(_environment.ContentRootPath, storagePath);

        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath); // crea la carpeta images
        }

        var fileName = $"{Guid.NewGuid()} {file.FileName}"; //asda22432d_image.png
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var relative = Path.Combine(storagePath, fileName);

        return relative.Replace("\\", "/");
    }
}
