using Microsoft.AspNetCore.Hosting;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _environment;
    public ImageService(IWebHostEnvironment env) => _environment = env;

    public string Save(IFormFile file)
    {
        var uploadsFolder = Path.Combine(_environment.WebRootPath, FileSettings.ImagePath.TrimStart('/'));
        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var path = Path.Combine(uploadsFolder, fileName);

        using var fs = new FileStream(path, FileMode.Create);
        file.CopyTo(fs);

        return $"{FileSettings.ImagePath}/{fileName}";
    }

    public void Delete(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return;
        var relative = url.TrimStart('/');
        var full = Path.Combine(_environment.WebRootPath, relative.Replace('/', Path.DirectorySeparatorChar));
        if (File.Exists(full)) 
            File.Delete(full);
    }
}
