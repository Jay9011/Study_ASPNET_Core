using System.Collections.Generic;
using IOManagerBLL.Model;

namespace IOManagerBLL
{
    /// <summary>
    /// 파일 입출력을 관리하는 인터페이스
    /// </summary>
    public interface IIOManager
    {
        /// <summary>
        /// 일반 파일 저장 메서드
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <param name="NoFileChk">파일이 없는 경우 빈 FileData를 반환할지 여부</param>
        /// <param name="Ext">파일 확장자</param>
        /// <param name="useDate">파일 이름에 날짜를 사용할지 여부</param>
        /// <param name="TargetFileName">파일 이름을 지정할 경우 사용</param>
        /// <returns>업로드 결과</returns>
        List<FileData> FileUploads(string path, bool NoFileChk = false, string Ext = "", bool useDate = false, string TargetFileName = "");
        /// <summary>
        /// 해당 경로의 파일 지우기
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <param name="FileName">파일명</param>
        void FileRemove(string path, string FileName);
        /// <summary>
        /// 임시 파일 지우기
        /// </summary>
        void TempFileRemove();
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
        int FileTypeChk(string FileType, string FileExt);
        string SetupFileInfo(string Type);
    }
}