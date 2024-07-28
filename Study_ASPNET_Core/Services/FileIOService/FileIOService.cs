using IOManagerBLL.Classes;
using IOManagerBLL.Model;

namespace Study_ASPNET_Core.Services.FileIOService;

public class FileIOService(IWebHostEnvironment environment)
{
    private readonly IWebHostEnvironment _environment = environment;

    public List<FileData> UploadFiles(IFormFileCollection files, string subDirectory = "Upload")
    {
        var fileUploadProvider = new HttpFileUploadProvider(files);
        var ioManager = new S1_IOManager(_environment.ContentRootPath, fileUploadProvider);

        var uploadPath = Path.Combine(_environment.WebRootPath, subDirectory);
        return ioManager.FileUploads(uploadPath);
    }
}