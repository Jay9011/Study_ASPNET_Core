using System.Collections.Generic;

namespace IOManagerBLL.Model
{
    /// <summary>
    /// 파일 업로드 제공자
    /// </summary>
    public interface IFileUploadProvider
    {
        /// <summary>
        /// 업로드된 파일을 가져온다.
        /// </summary>
        /// <returns>FileUpload 인터페이스를 구현한 업로드된 파일</returns>
        IEnumerable<IFileUpload> GetUploadFiles();
    }
}