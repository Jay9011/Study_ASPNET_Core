using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using Study_ASPNET_Core.Services.FileIOService;

namespace TestProject.MVC.Services;

[TestSubject(typeof(Study_ASPNET_Core.Services.FileIOService.FileIOService))]
public class FileIOServiceTest
{
    private readonly Mock<IWebHostEnvironment> _mockEnvironment;
    private readonly FileIOService _fileIOService;

    public FileIOServiceTest()
    {
        _mockEnvironment = new Mock<IWebHostEnvironment>();
        _mockEnvironment.Setup(env => env.ContentRootPath).Returns("C:\\fake_root_path");
        _mockEnvironment.Setup(env => env.WebRootPath).Returns("C:\\fake_webroot_path");

        _fileIOService = new FileIOService(_mockEnvironment.Object);
    }

    [Fact]
    public void UploadFiles_ShouldReturnListOfFileData()
    {
        // Arrange
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.FileName).Returns("test.txt");
        mockFile.Setup(f => f.ContentType).Returns("text/plain");
        mockFile.Setup(f => f.Length).Returns(100);

        var files = new FormFileCollection { mockFile.Object };

        // Act
        var result = _fileIOService.UploadFiles(files);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

    }
}