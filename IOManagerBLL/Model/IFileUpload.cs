using System.IO;

namespace IOManagerBLL.Model
{
    /// <summary>
    /// 파일 업로드를 위한 인터페이스
    /// </summary>
    public interface IFileUpload
    {
        string FileName { get; }
        string ContentType { get; }
        long ContentLength { get; }

        /// <summary>
        /// 파일을 읽어오는 방법을 구현
        /// </summary>
        /// <returns></returns>
        Stream OpenReadStream();

        /// <summary>
        /// 파일을 저장하는 방법을 구현
        /// </summary>
        /// <param name="path">저장할 경로</param>
        void SaveAs(string path);
    }
}