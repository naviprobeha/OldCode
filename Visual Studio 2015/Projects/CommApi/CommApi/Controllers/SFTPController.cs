using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommApi.Controllers
{
    [AllowAnonymous]
    public class SFTPController : ApiController
    {
        // GET api/values
        public List<string> List(string hostname, string username, string password, int port, string folder)
        {
            Chilkat.SFtp sftp = new Chilkat.SFtp();
            sftp.UnlockComponent("QNAVPRSSH_uuuxMXhR2Bny");

            sftp.ConnectTimeoutMs = 5000;
            sftp.IdleTimeoutMs = 10000;
            
            sftp.Connect(hostname, port);
            sftp.AuthenticatePw(username, password);

            sftp.InitializeSftp();

            Chilkat.SFtpDir dir = sftp.ReadDir(sftp.OpenDir(folder));

            List<string> fileNames = new List<string>();

            int i = 0;
            while(i < dir.NumFilesAndDirs)
            {
                Chilkat.SFtpFile file = dir.GetFileObject(i);

                fileNames.Add(file.Filename);

                i++;
            }

            sftp.Disconnect();

            return fileNames;


        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
