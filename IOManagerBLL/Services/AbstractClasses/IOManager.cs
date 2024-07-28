using System.Collections.Generic;
using IOManagerBLL.Model;

namespace IOManagerBLL.AbstractClasses
{
    public abstract class IOManager : IIOManager
    {
        protected readonly string _basePath;
        protected readonly IFileUploadProvider _fileUploadProvider;

        /// <summary>
        /// 의존 주입을 할 때, basePath에 기본 경로를 설정합니다.
        /// </summary>
        /// <param name="basePath">기본 경로</param>
        /// <param name="fileUploadProvider">파일 업로드용 제공자 클래스</param>
        public IOManager(string basePath, IFileUploadProvider fileUploadProvider)
        {
            _basePath = basePath;
        }

        /// <summary>
        /// 일반 파일 저장 메서드
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <param name="NoFileChk">파일이 없는 경우 빈 FileData를 반환할지 여부</param>
        /// <param name="Ext">파일 확장자</param>
        /// <param name="useDate">파일 이름에 날짜를 사용할지 여부</param>
        /// <param name="TargetFileName">파일 이름을 지정할 경우 사용</param>
        /// <returns>업로드 결과</returns>
        public abstract List<FileData> FileUploads(string path, bool NoFileChk = false, string Ext = "", bool useDate = false, string TargetFileName = "");

        /// <summary>
        /// 해당 경로의 파일 지우기
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <param name="FileName">파일명</param>
        public abstract void FileRemove(string path, string FileName);

        /// <summary>
        /// 임시 파일 지우기
        /// </summary>
        public abstract void TempFileRemove();

        /// <summary>
        /// 파일 확장자 체크
        /// </summary>
        /// <param name="FileType">파일 타입</param>
        /// <param name="FileExt">등록 가능 확장자</param>
        /// <returns>
        /// 0: 모든 파일이 유효한 경우
        /// 양수: 유효하지 않은 파일의 인덱스 (1부터 시작)
        /// -1: 예외 발생
        /// </returns>
        public abstract int FileTypeChk(string FileType, string FileExt);
        public abstract string SetupFileInfo(string Type);

        /// <summary>
        /// 파일명 중복 방지를 위한 메서드
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <param name="fileName">파일명</param>
        /// <param name="overwrite">덮어쓰기 여부</param>
        /// <returns>중복 방지된 파일명</returns>
        protected abstract string GetUniqueFileName(string path, string fileName, bool overwrite = false);
    }

    public interface ISetupInfo
    {
        string DB { get; }
    }
}