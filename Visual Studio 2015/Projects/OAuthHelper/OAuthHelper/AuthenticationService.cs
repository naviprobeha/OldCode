using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Navipro.OAuthHelper
{
    public class AuthenticationService
    {

        public AuthenticationService()
        {

        }

        public string GetAccessToken(string clientId, string clientSecret, string tenantId)
        {

            var authorityUri = $"https://login.microsoftonline.com/{tenantId}";

            var redirectUri = "https://businesscentral.dynamics.com/OAuthLanding.htm";

            var scopes = new List<string> { "https://api.businesscentral.dynamics.com/.default" };



            var publicClient = PublicClientApplicationBuilder

                          .Create(clientId)

                          .WithAuthority(new Uri(authorityUri))

                          .WithRedirectUri(redirectUri)

                          .Build();

            var accessTokenRequest = publicClient.AcquireTokenInteractive(scopes);

            var accessToken = accessTokenRequest.ExecuteAsync().Result.AccessToken;



            return accessToken;

        }
    }
}