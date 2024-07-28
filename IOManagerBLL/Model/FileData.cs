using System.IO;

namespace IOManagerBLL.Model
{
    /// <summary>
    /// 파일 데이터 클래스
    /// </summary>
    public class FileData
    {
        public string ID { get; set; }
        public string FileName { get; set; }
        public string FileLastName { get; set; }
        public string FilePath { get; set; }
        public string FileExt { get; set; }
        public int FileSize { get; set; }
        public string FileMimeType { get; set; }
        public Stream InputStream { get; set; }
    }
}