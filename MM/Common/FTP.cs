using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Dart.PowerTCP.Ftp;

namespace MM.Common
{
    public class FTP
    {
        #region Members
        private static Ftp _ftp = new Ftp();
        private static int _maxRetry = 10;
        #endregion

        #region Public Methods
        public static Result Connect(string server, string username, string password)
        {
            Result result = new Result();

            try
            {
                _ftp.Abort();
                _ftp.Close();
                _ftp.Server = server;
                _ftp.Username = username;
                _ftp.Password = password;
                string dir = _ftp.GetDirectory();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.CONNECT_FTP_FAIL;
                result.Error.Description = e.Message;
            }

            return result;
        }

        public static Result UploadFile(FTPConnectionInfo connectionInfo, string localFileName, string remoteFileName)
        {
            Result result = new Result();

            int retry = 0;
            while (retry < _maxRetry)
            {
                try
                {
                    //Connnect
                    _ftp.Server = connectionInfo.ServerName;
                    _ftp.Username = connectionInfo.Username;
                    _ftp.Password = connectionInfo.Password;
                    string dir = _ftp.GetDirectory();

                    //Upload
                    FtpFile ftpFile = _ftp.Put(localFileName, string.Format("{0}{1}", dir, remoteFileName));
                    if (ftpFile.Status == FtpFileStatus.TransferCompleted)
                    {
                        result.Error.Code = ErrorCode.OK;
                        result.Error.Description = string.Empty;
                        break;
                    }
                }
                catch (Exception e)
                {
                    result.Error.Code = ErrorCode.UPLOAD_FTP_FAIL;
                    result.Error.Description = e.Message;
                }

                retry++;
            }

            _ftp.Abort();
            _ftp.Close();

            return result;
        }
        #endregion
    }
}
