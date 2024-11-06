using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chilkat;

namespace Navipro.SFTPWrapper
{
    public class SFTPWrapper
    {
        private string licenseKey;
        
        public SFTPWrapper(string licenseKey)
        {
            this.licenseKey = licenseKey;
        }

        public void uploadFile(string hostName, int port, string userName, string password, string localFile, string remoteFile)
        {
            Chilkat.SFtp sftp = new SFtp();
            sftp.UnlockComponent(licenseKey);
            
            sftp.IdleTimeoutMs = 10000;
            sftp.ConnectTimeoutMs = 10000;

            sftp.Connect(hostName, port);

            sftp.AuthenticatePw(userName, password);

            sftp.InitializeSftp();

            if (!sftp.UploadFileByName(remoteFile, localFile))
            {
                throw new Exception("Could not upload file: " + localFile);
            }

            sftp.Disconnect();
        }

        public string downloadFirstFile(string hostName, int port, string userName, string password, string folder, string downloadFolder)
        {

            Chilkat.SFtp sftp = new SFtp();
            sftp.UnlockComponent(licenseKey);

            sftp.IdleTimeoutMs = 10000;
            sftp.ConnectTimeoutMs = 10000;

            sftp.Connect(hostName, port);

            sftp.AuthenticatePw(userName, password);

            sftp.InitializeSftp();

            string dirHandle = sftp.OpenDir(folder);
            SFtpDir sftpDir = sftp.ReadDir(dirHandle);

            int noOfFiles = sftpDir.NumFilesAndDirs;
            int i = 0;
            while(i < noOfFiles)
            {
                SFtpFile sftpFile = sftpDir.GetFileObject(i);
                if (sftpFile.FileType == "regular")
                {
                    string remoteFileName = folder + "/" + sftpFile.Filename;
                    string localFileName = downloadFolder + "\\" + sftpFile.Filename;

                    if (!sftp.DownloadFileByName(remoteFileName, localFileName))
                    {
                        throw new Exception("Unable to download file: " + remoteFileName);
                    }

                    sftp.Disconnect();

                    return sftpFile.Filename;
                }
            }

            sftp.Disconnect();

            return "";
        }

        public void deleteFile(string hostName, int port, string userName, string password, string remoteFile)
        {
            Chilkat.SFtp sftp = new SFtp();
            sftp.UnlockComponent(licenseKey);

            sftp.IdleTimeoutMs = 10000;
            sftp.ConnectTimeoutMs = 10000;

            sftp.Connect(hostName, port);

            sftp.AuthenticatePw(userName, password);

            sftp.InitializeSftp();

            if (!sftp.RemoveFile(remoteFile))
            {
                throw new Exception("Could not delete file: " + remoteFile);
            }

            sftp.Disconnect();
        }

    }
}
