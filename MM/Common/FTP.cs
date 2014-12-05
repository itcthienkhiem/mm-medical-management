using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace MM.Common
{
    public class FTP
    {
        #region Public Methods
        public static Result Connect(string server, string username, string password)
        {
            Result result = new Result();

            try
            {
                string ftpFileName = string.Format("ftp://{0}/{1}", server, "/results/test.txt");
                System.Net.FtpWebRequest ftpWebRequest = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(ftpFileName));
                ftpWebRequest.Credentials = new System.Net.NetworkCredential(username, password);
                ftpWebRequest.KeepAlive = false;
                ftpWebRequest.Timeout = 20000;
                ftpWebRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                ftpWebRequest.UseBinary = true;
                System.IO.Stream stream = ftpWebRequest.GetRequestStream();
                stream.Close();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.CONNECT_FTP_FAIL;
                result.Error.Description = e.Message;
            }

            return result;
        }

        //public static Result UploadFile(FTPConnectionInfo connectionInfo, string localFileName, string remoteFileName)
        //{
        //    Result result = new Result();

        //    try
        //    {
        //        UploadFile(localFileName, string.Format("ftp://{0}/{1}", connectionInfo.ServerName, remoteFileName),
        //                connectionInfo.Username, connectionInfo.Password);
        //    }
        //    catch (Exception e)
        //    {
        //        result.Error.Code = ErrorCode.UPLOAD_FTP_FAIL;
        //        result.Error.Description = e.Message;
        //    }
            

        //    //int retry = 0;
        //    //while (retry < _maxRetry)
        //    //{
        //    //    try
        //    //    {
        //    //        //Connnect
        //    //        //_ftp.Server = connectionInfo.ServerName;
        //    //        //_ftp.Username = connectionInfo.Username;
        //    //        //_ftp.Password = connectionInfo.Password;
        //    //        //string dir = _ftp.GetDirectory();

        //    //        ////Upload
        //    //        //FtpFile ftpFile = _ftp.Put(localFileName, string.Format("{0}{1}", dir, remoteFileName));
        //    //        //if (ftpFile.Status == FtpFileStatus.TransferCompleted)
        //    //        //{
        //    //        //    result.Error.Code = ErrorCode.OK;
        //    //        //    result.Error.Description = string.Empty;
        //    //        //    break;
        //    //        //}

        //    //        UploadFile(localFileName, string.Format("ftp://{0}/{1}", connectionInfo.ServerName, remoteFileName), 
        //    //            connectionInfo.Username, connectionInfo.Password);
        //    //    }
        //    //    catch (Exception e)
        //    //    {
        //    //        result.Error.Code = ErrorCode.UPLOAD_FTP_FAIL;
        //    //        result.Error.Description = e.Message;
        //    //    }

        //    //    retry++;
        //    //}

        //    //_ftp.Abort();
        //    //_ftp.Close();

        //    return result;
        //}

        public static Result UploadFile(FTPConnectionInfo connectionInfo, string localFileName, string remoteFileName)
        {
            Result result = new Result();
            string ftpFileName = string.Format("ftp://{0}/{1}", connectionInfo.ServerName, remoteFileName);

            FileInfo fileInfo = new FileInfo(localFileName);
            System.Net.FtpWebRequest ftpWebRequest = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(ftpFileName));
            ftpWebRequest.Credentials = new System.Net.NetworkCredential(connectionInfo.Username, connectionInfo.Password);
            ftpWebRequest.KeepAlive = false;
            ftpWebRequest.Timeout = 20000;
            ftpWebRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.ContentLength = fileInfo.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            System.IO.FileStream fileStream = fileInfo.OpenRead();
            try
            {
                System.IO.Stream stream = ftpWebRequest.GetRequestStream();
                int contentLen = fileStream.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    stream.Write(buff, 0, contentLen);
                    contentLen = fileStream.Read(buff, 0, buffLength);
                }

                stream.Close();
                stream.Dispose();
                fileStream.Close();
                fileStream.Dispose();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UPLOAD_FTP_FAIL;
                result.Error.Description = e.Message;
            }

            return result;
        }
        #endregion
    }
}
