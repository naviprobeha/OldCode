using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NaviPro
{
    public class OAuthHelper
    {

        public static string GetBCAccessToken(string tenantId, string clientId, string clientSecret)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var data = new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("scope", "https://api.businesscentral.dynamics.com/.default"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
            };

            HttpClient client = new HttpClient();
            HttpResponseMessage msg = client.PostAsync("https://login.microsoftonline.com/" + tenantId + "/oauth2/v2.0/token", new FormUrlEncodedContent(data)).GetAwaiter().GetResult();
            string response = msg.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            JObject jObject = JObject.Parse(response);
            string accessToken = jObject["access_token"].ToString();

            return accessToken;
        }

        public static string MakeBCPostJsonRequest(string url, string accessToken, string body)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var content = new StringContent(body, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage msg = client.PostAsync(url, content).GetAwaiter().GetResult();

            string response = msg.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return response;
        }
    }
}
