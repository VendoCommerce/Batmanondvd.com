using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Quigley_Report
{
    class FTPUploader
    {
        public void UploadFtpFile(string ftpAddress, string fileFullName)
        {
            FtpWebRequest request;
            try
            {
                string absoluteFileName = Path.GetFileName(fileFullName);

                request = WebRequest.Create(new Uri(string.Format(@"ftp://{0}/{1}", ftpAddress, absoluteFileName))) as FtpWebRequest;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UseBinary = true;
                request.UsePassive = true;
                request.KeepAlive = true;
                request.Credentials = new NetworkCredential("TestUser", "TestPass");
                //request.ConnectionGroupName = "group";

                using (FileStream fs = File.OpenRead(fileFullName))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Close();
                    requestStream.Flush();
                }
            }
            catch (Exception ex)
            {
                Logger.LogToFile(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
