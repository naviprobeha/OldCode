using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Api.Library
{
    public class OAuthHelper
    {


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
                if (response.IsSuccessStatusCode)
                {
                    JObject jObject = JObject.Parse(responseString);
                    return jObject["access_token"].ToString();
                }
                else
                {

                }
            }

            return "";
        }
    }
}