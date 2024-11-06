using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;

namespace Navipro.OAuthHelper
{
    public class OAuthHelper
    {

        public static string RefreshToken(string AzureADTenantID, string ClientID, string ClientSecret)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://login.microsoftonline.com/" + AzureADTenantID + "/oauth2/v2.0/token");

            string scope = "https://api.businesscentral.dynamics.com/.default";


            request.Method = "POST";
            request.Headers.Add("cache-control", "no-cache");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "application/json";

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            keyValues.Add(new KeyValuePair<string, string>("client_id", ClientID));
            keyValues.Add(new KeyValuePair<string, string>("client_secret", ClientSecret));
            keyValues.Add(new KeyValuePair<string, string>("scope", scope));
            //keyValues.Add(new KeyValuePair<string, string>("redirect_uri", "https://localhost"));

            var content = new FormUrlEncodedContent(new Dictionary<string, string> {
              { "client_id", ClientID },
              { "client_secret", ClientSecret },
              { "grant_type", "client_credentials" },
              { "scope", "https://api.businesscentral.dynamics.com/.default" },
            });

            
            StreamWriter streamWriter = new StreamWriter(request.GetRequestStream());
            streamWriter.WriteLine(keyValues.ToString());
            streamWriter.Flush();
            streamWriter.Close();
            
            Console.WriteLine(keyValues.ToString());
            WebResponse response = request.GetResponse();

            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string content2 = streamReader.ReadToEnd();

            Console.WriteLine(content2);

            return content2;
        }

        public static string GetToken(string AzureADTenantID, string ClientID, string ClientSecret)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string result = "";

            var _httpClient = new HttpClient();

            var content = new FormUrlEncodedContent(new Dictionary<string, string> {
              { "client_id", ClientID },
              { "client_secret", ClientSecret },
              { "grant_type", "client_credentials" },
              { "scope", "https://api.businesscentral.dynamics.com/.default" },
            });

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://login.microsoftonline.com/" + AzureADTenantID + "/oauth2/v2.0/token"))
            {
                Content = content
            };

            using (var response = _httpClient.SendAsync(httpRequestMessage).Result)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseString);
                if (response.IsSuccessStatusCode)
                {
                    string tokenString = responseString.Substring(responseString.IndexOf("access_token") + 15);
                    tokenString = tokenString.Substring(0, tokenString.IndexOf("\""));
                    Console.WriteLine(tokenString);
                }
                else
                {
                   
                }
            }

            return "";
        }


    }
}
