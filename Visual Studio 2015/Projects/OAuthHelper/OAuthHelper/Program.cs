using Navipro.OAuthHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Trying...");


            //AuthenticationService authService = new AuthenticationService();
            //string result = authService.GetAccessToken("fb7c0a22-829d-4556-85e0-dcfcd9af449b", "iJe8Q~4~RMhy7A35sDUV_.GBKygxeLKbvlONAbMh", "wackes.com");

            string token = "";
            Navipro.OAuthHelper.OAuthHelper.GetToken("hakannavipro.onmicrosoft.com", "9c7d11c9-205d-4bc3-9f79-ebcfae56ea3e", "Vfb8Q~L51t~uFEpPu1aq_-lAnq~Njt~4KgVH-dkB");

            Console.WriteLine("Success: " + token);

            Console.ReadLine();
        }
    }
}
