using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Xml;
using System.IO;

namespace Navipro.NAV
{
    class Guids
    {
        public const string coclsguid = "E03D715B-A13F-4cff-92F1-1319ADB3EF5F";
        public const string intfguid = "D030D214-C984-496a-87E7-41732C114F1E";
        public const string eventguid = "D030D214-C984-496a-87E7-41732C114F1F";

        public static readonly System.Guid idcoclass;
        public static readonly System.Guid idintf;
        public static readonly System.Guid idevent;

        static Guids()
        {
            idcoclass = new System.Guid(coclsguid);
            idintf = new System.Guid(intfguid);
            idevent = new System.Guid(eventguid);
        }
    }

    [Guid(Guids.intfguid), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IHTTPRequest
    {
        [DispId(1)]
        void performService(string url, string method, string fileName, string outFileName, string soapAction);
    }

    [Guid(Guids.coclsguid), ProgId("Navipro.Nav.HTTPRequest"), ClassInterface(ClassInterfaceType.None)]
    public class HTTPRequest : IHTTPRequest 
    {
        

        public void performService(string url, string method, string fileName, string outFileName, string soapAction)
        {

            System.Net.WebRequest httpWebRequest = System.Net.HttpWebRequest.Create(url);
            httpWebRequest.ContentType = "text/xml; charset=utf-8";
            if (soapAction != "") httpWebRequest.Headers.Add("SOAPAction", soapAction);
            httpWebRequest.Method = method;

            StreamReader fileReader = new StreamReader(File.OpenRead(fileName));
            string postContent = fileReader.ReadToEnd();
            fileReader.Close();

            System.Text.Encoding utf8 = new System.Text.UTF8Encoding();
            byte[] postData = utf8.GetBytes(postContent);
            byte[] crlf = new byte[2];

            crlf[0] = 13;
            crlf[1] = 10;

            System.IO.Stream outStream = httpWebRequest.GetRequestStream();
            outStream.Write(postData, 0, postData.Length);
            outStream.Write(crlf, 0, 2);
            outStream.Flush();
            outStream.Close();


            System.Net.WebResponse webResponse = httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
            string content = streamReader.ReadToEnd();
            streamReader.Close();

            StreamWriter streamWriter = new StreamWriter(outFileName);
            streamWriter.WriteLine(content);
            streamWriter.Close();
        }

    }
}
