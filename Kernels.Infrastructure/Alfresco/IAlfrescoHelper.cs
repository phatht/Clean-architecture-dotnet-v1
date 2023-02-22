using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernels.Infrastructure.Alfresco
{
    public interface IAlfrescoHelper
    {
        bool UploadFileToAlfrescoServer(IFormFile fileUpload);
        bool UploadFileToAlfrescoServer(Stream fileUpload, string fileid); 
        Dictionary<string, bool> UploadFileScanToAlfrescoServer(Stream streamFile, string fileName, string userid, long maxFileSize, ref string strErrorFile);
        Task<bool> DownloadFileFromAlfrescoServer(HttpResponse respone, string fileName, string downloadName);
        Task<bool> DownloadFileFromAlfrescoServer(HttpResponse respone, string fileName);
        byte[] DownloadFileForEdit(string fileName, string downloadName); 
        bool DeleteFileFromAlfresco(string filename);

    }
}
