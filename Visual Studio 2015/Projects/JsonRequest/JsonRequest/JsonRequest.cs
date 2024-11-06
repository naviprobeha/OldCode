using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace JsonRequest
{
    public class JsonRequest
    {        

        public string DoRequest(string url, string bearer, string secret, string postJson, string method)
        {

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", bearer+" " + secret);
            webRequest.ContentType = "application/json";
            webRequest.Method = method;


            if (postJson != "")
            {
                StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
                streamWriter.WriteLine(postJson);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                WebResponse webResponse = webRequest.GetResponse();


                StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
                string responseJson = streamReader.ReadToEnd();
                streamReader.Close();
                return responseJson;
            }
            catch (WebException e)
            {
                HttpWebResponse response = (HttpWebResponse)e.Response;
                if (response != null) return "Status: " + (int)response.StatusCode;
                return "Unknown error!";
            }

            return "";
        }

 
    }
}
