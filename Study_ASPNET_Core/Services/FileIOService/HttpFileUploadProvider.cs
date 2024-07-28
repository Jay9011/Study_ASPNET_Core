using IOManagerBLL.Model;

namespace Study_ASPNET_Core.Services.FileIOService;

/// <summary>
/// Http의 FormFile을 저장하는 클래스
/// </summary>
/// <param name="files">Request.Form.Files의 파일을 지정</param>
public class HttpFileUploadProvider(IFormFileCollection files) : IFileUploadProvider
{
    private readonly IFormFileCollection _files = files;

    public IEnumerable<IFileUpload> GetUploadFiles()
    {
        foreach (var file in _files)
        {
            yield return new FormFileWrapper(file);
        }
    }
}