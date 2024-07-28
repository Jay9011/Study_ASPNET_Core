using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using IOManagerBLL.AbstractClasses;
using IOManagerBLL.Model;

namespace IOManagerBLL.Classes
{
    public class S1_IOManager : IOManager
    {
        private readonly FilePath _filePath;
        private readonly IFileUploadProvider _fileUploadProvider;

        /// <summary>
        /// 의존 주입을 할 때, basePath에 기본 경로를 설정합니다.
        /// </summary>
        /// <param name="basePath">기본 경로</param>
        /// <param name="fileUploadProvider">파일 업로드용 제공자 클래스</param>
        public S1_IOManager(string basePath, IFileUploadProvider fileUploadProvider) : base(basePath, fileUploadProvider)
        {
            _filePath = new FilePath(basePath);
            _fileUploadProvider = fileUploadProvider;
        }

        /// <summary>
        /// 파일 경로 관련 정보를 담고 있는 클래스
        /// </summary>
        public class FilePath
        {
            private readonly string _basePath;

            public FilePath(string basePath)
            {
                _basePath = basePath;
            }

            public string DefaultiiSPath = "/Upload/";
            public string DataUpload => Path.Combine(_basePath, "Upload");
            public string DataPerson => Path.Combine(_basePath, "Upload", "Person");
            public string DataLog => Path.Combine(_basePath, "Upload", "Log4Query");
            public string DataConn => Path.Combine(_basePath, "Upload", "Data");
            public string DataWave => Path.Combine(_basePath, "Upload", "Sound");
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
        public override List<FileData> FileUploads(string path, bool NoFileChk = false, string Ext = "", bool useDate = false, string TargetFileName = "")
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            var result = new List<FileData>();
            var uploadFiles = _fileUploadProvider.GetUploadFiles();

            // 업로드 준비중인 파일들을 돌아가며 실행
            foreach (var file in uploadFiles)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    if (!string.IsNullOrEmpty(TargetFileName))
                    {
                        fileName = TargetFileName;
                    }

                    if (useDate)
                    {
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                        var extension = Path.GetExtension(fileName);
                        fileName = $"{fileNameWithoutExtension}_{DateTime.Now:yyMMddHHmm}{extension}";
                    }

                    if (!string.IsNullOrEmpty(Ext))
                    {
                        fileName = Path.ChangeExtension(fileName, Ext);
                    }

                    var filePath = Path.Combine(path, fileName);
                    var uniqueFileName = GetUniqueFileName(path, fileName);

                    file.SaveAs(filePath);

                    // 결과를 FileData에 저장
                    result.Add(new FileData
                    {
                        FileName = fileName,
                        FileLastName = uniqueFileName,
                        FilePath = path,
                        FileExt = Path.GetExtension(uniqueFileName).TrimStart('.'),
                        FileSize = (int)file.ContentLength,
                        FileMimeType = file.ContentType
                    });
                }
                // 만약 파일이 없다면
                else if (NoFileChk)
                {
                    result.Add(new FileData
                    {
                        FileName = "",
                        FileLastName = "",
                        FilePath = "",
                        FileExt = "",
                        FileSize = 0,
                        FileMimeType = ""
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// 해당 경로의 파일 지우기
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <param name="FileName">파일명</param>
        public override void FileRemove(string path, string FileName)
        {
            if (File.Exists(path + FileName))
            {
                File.Delete(path + FileName);
            }
        }

        /// <summary>
        /// 임시 파일 지우기
        /// </summary>
        public override void TempFileRemove()
        {
            DirectoryInfo di = new DirectoryInfo(_filePath.DataUpload);

            FileInfo[] files = di.GetFiles();

            foreach (FileInfo file in files)
            {
                try
                {
                    file.Delete();
                }
                catch { }
            }
        }

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
        public override int FileTypeChk(string FileType, string FileExt)
        {
            var uploadFiles = _fileUploadProvider.GetUploadFiles();
            int RtnVlu = 0;
            string[] arrFileType = FileType.Split(',').Select(t => t.Trim()).ToArray();
            string[] arrFileExt = FileExt.Split(',').Select(ext => "." + ext.Trim().ToLower()).ToArray();

            try
            {
                for (int i = 0; i < uploadFiles.Count(); i++)
                {
                    var file = uploadFiles.ElementAt(i);
                    if (file.ContentLength > 0)
                    {
                        bool isFileTypeValid = arrFileType.Contains(file.ContentType);
                        bool isFileExtValid = arrFileExt.Contains(Path.GetExtension(file.FileName).ToLower());

                        if (!isFileTypeValid || !isFileExtValid)
                        {
                            RtnVlu = i + 1; // 유효하지 않은 파일의 인덱스 반환
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                RtnVlu = -1; // 예외 발생 시 -1을 반환
            }

            return RtnVlu;
        }

        public override string SetupFileInfo(string Type)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 파일명 중복 방지를 위한 메서드
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <param name="fileName">파일명</param>
        /// <param name="overwrite">덮어쓰기 여부</param>
        /// <returns>중복 방지된 파일명</returns>
        protected override string GetUniqueFileName(string path, string fileName, bool overwrite = false)
        {
            if (overwrite)
            {
                return fileName;
            }

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            string newFileName = fileName;
            int count = 1;

            while (File.Exists(Path.Combine(path, newFileName)))
            {
                newFileName = $"{fileNameWithoutExtension}[{count}]{extension}";
                count++;
            }

            return newFileName;
        }
    }
}