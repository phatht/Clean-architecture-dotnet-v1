 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
 

namespace Kernels.Infrastructure.Alfresco
{
    public class AlfrescoHelper : IAlfrescoHelper
    {

        private readonly string _alfrescoServerPath;
        private readonly string _alfrescoHost;
        private readonly string _alfrescoPort;
        private readonly string _alfrescoHostFtp;

        private string _prefix = string.Empty;
        private string _alfrescoUsername = string.Empty;
        private string _alfrescoPassword = string.Empty;

        private readonly int _maxFileSizeMB;
        private readonly long _maxFileSize;

        public AlfrescoHelper(IConfiguration config)
        {
            _alfrescoServerPath = config["Alfresco:ServerPath"];
            _alfrescoHost = config["Alfresco:Host"];
            _alfrescoPort = config["Alfresco:Port"];
            _alfrescoHostFtp = config["Alfresco:HostFTP"];

            _prefix = config["Alfresco:Prefix"];
            _alfrescoUsername = config["Alfresco:Username"];
            _alfrescoPassword = config["Alfresco:Password"];

            _maxFileSizeMB = Int32.Parse(config["Alfresco:MaxFileSizeMB"]);
            _maxFileSize = 1024 * 1024 * Int32.Parse(config["Alfresco:MaxFileSize"]);
        }
         
        public bool UploadFileToAlfrescoServer(IFormFile fileUpload)
        {
            try
            {
                AlfrescoFTP alfrescoFTP = new AlfrescoFTP(_alfrescoHostFtp, _alfrescoUsername, _alfrescoPassword);
                string remoteFile = _alfrescoServerPath + fileUpload.FileName;
                Stream fileContentStream = fileUpload.OpenReadStream();
                bool result = alfrescoFTP.upload(remoteFile, fileContentStream);
                return result;
            }
            catch (Exception)
            {
                //FIND TO [LOG4NET]
                return false;
            }
        }

        public bool UploadFileToAlfrescoServer(Stream fileUpload, string fileid)
        {
            try
            {
                AlfrescoFTP alfrescoFTP = new AlfrescoFTP(_alfrescoHostFtp, _alfrescoUsername, _alfrescoPassword);
                string remoteFile = _alfrescoServerPath + fileid;
                Stream fileContentStream = fileUpload;
                bool result = alfrescoFTP.upload(remoteFile, fileContentStream);
                return result;
            }
            catch (Exception)
            {
                //FIND TO [LOG4NET]
                return false;
            }
        }

        public Dictionary<string, bool> UploadFileScanToAlfrescoServer(Stream streamFile, string fileName, string userid, long maxFileSize, ref string strErrorFile)
        {
            Dictionary<string, bool> dicResult;
            try
            {
                dicResult = new Dictionary<string, bool>();
                var alfrescoFtp = new AlfrescoFTP(_alfrescoHostFtp, _alfrescoUsername, _alfrescoPassword);
                strErrorFile = "";

                var fileSize = streamFile.Length;
                var fileExtension = fileName.Substring(fileName.LastIndexOf('.'));
                var uploadName = _prefix + DateTime.Now.ToString("ddMMyyhhmmssFF") + "_" + userid + fileExtension;
                var remoteFile = _alfrescoServerPath + uploadName;

                var result = alfrescoFtp.upload(remoteFile, streamFile);
                dicResult.Add(fileName + "|" + uploadName + "|" + fileExtension + "|" + fileSize, result);

            }
            catch (Exception ex)
            {
                dicResult = new Dictionary<string, bool> { { "error upload: " + ex.Message, false } };
            }

            return dicResult;
        }

        public async Task<bool> DownloadFileFromAlfrescoServer(HttpResponse respone, string fileName, string downloadName)
        {
            try
            {
                var alfrescoFtp = new AlfrescoFTP(_alfrescoHostFtp, _alfrescoUsername, _alfrescoPassword);
                var bytes = alfrescoFtp.DirectDownload(_alfrescoServerPath + fileName, downloadName);
                //if (bytes == null) bytes = alfrescoFtp.DirectDownload(_alfrescoServer1CPath + fileName, downloadName);
                if (bytes == null) return false;
                //var alfrescoCmis = new AfrescoCMIS(AlfrescoUsername, AlfrescoPassword, _alfrescoHost, _alfrescoPort, true);
                //var objectId = alfrescoCmis.GetObjectIdByFileName(fileName);
                //var bytes = alfrescoCmis.GetFileByObjectId(objectId);
                string contentType;
                new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType); 
                var mimeType = contentType;

                respone.Clear();
                respone.ContentType = contentType ?? "application/octet-stream";
                respone.Headers.Add("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(downloadName) + "\"");
                await respone.Body.WriteAsync(bytes);
                await respone.StartAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DownloadFileFromAlfrescoServer(HttpResponse respone, string fileName)
        {
            try
            {
                AlfrescoFTP alfrescoFTP = new AlfrescoFTP(_alfrescoHostFtp, _alfrescoUsername, _alfrescoPassword);
                Byte[] bytes = alfrescoFTP.DirectDownload(_alfrescoServerPath + fileName, fileName);

                // NET FW
                //string MimeType = MimeMapping.GetMimeMapping(fileName);
                //respone.ContentType = MimeType;
                //respone.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                //respone.BinaryWrite(bytes);
                //respone.End();


                // NET CORE
                string contentType;
                new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
                var mimeType = contentType; 
                respone.Clear();
                respone.ContentType = contentType ?? "application/octet-stream";
                respone.Headers.Add("Content-Disposition", "attachment; filename=" + fileName);
                await respone.Body.WriteAsync(bytes);
                await respone.StartAsync();
                 
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public byte[] DownloadFileForEdit(string fileName, string downloadName)
        {
            try
            {
                var alfrescoFtp = new AlfrescoFTP(_alfrescoHostFtp, _alfrescoUsername, _alfrescoPassword);
                var bytes = alfrescoFtp.DirectDownload(_alfrescoServerPath + fileName, downloadName);
                //if (bytes == null) return false;
                //var alfrescoCmis = new AfrescoCMIS(AlfrescoUsername, AlfrescoPassword, _alfrescoHost, _alfrescoPort, true);
                //var objectId = alfrescoCmis.GetObjectIdByFileName(fileName);
                //var bytes = alfrescoCmis.GetFileByObjectId(objectId);
                //var mimeType = MimeMapping.GetMimeMapping(fileName);
                //respone.ContentType = mimeType;
                //respone.AddHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(downloadName) + "\"");
                //respone.BinaryWrite(bytes);
                //respone.End();
                return bytes;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteFileFromAlfresco(string filename)
        {
            try
            {
                var alfrescoFtp = new AlfrescoFTP(_alfrescoHostFtp, _alfrescoUsername, _alfrescoPassword);
                return alfrescoFtp.delete(_alfrescoServerPath + filename);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string ConvertStringToASCII(string text)
        { 
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

    }
}
