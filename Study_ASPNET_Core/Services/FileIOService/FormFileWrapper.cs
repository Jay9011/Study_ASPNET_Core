using IOManagerBLL.Model;

namespace Study_ASPNET_Core.Services.FileIOService;

/// <summary>
/// Http의 FormFile을 IFileUpload로 변환하기 위한 Wrapper 클래스
/// </summary>
public class FormFileWrapper : IFileUpload
{
    private readonly IFormFile _file;

    public FormFileWrapper(IFormFile file)
    {
        _file = file;
    }

    public string FileName => _file.FileName;
    public string ContentType => _file.ContentType;
    public long ContentLength => _file.Length;

    public Stream OpenReadStream() => _file.OpenReadStream();

    /// <summary>
    /// 파일을 저장하는 구현체
    /// </summary>
    /// <param name="path">저장할 경로</param>
    public void SaveAs(string path)
    {
        using (var stream = new FileStream(path, FileMode.Create))
        {
            _file.CopyTo(stream);
        }
    }
}