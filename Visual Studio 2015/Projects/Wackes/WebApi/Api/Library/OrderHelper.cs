using Api.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace Api.Library
{
    public class OrderHelper
    {
 
        public static void SubmitOrder(string partner, XmlDocument orderXmlDoc)
        {

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            int entryNo = 1;

            System.Data.SqlClient.SqlDataReader dataReader = database.query("SELECT TOP 1 [Entry No_] FROM [WACKES$E-commerce Log$028a6c2e-d842-4c31-871c-7eb52e2bc71d] ORDER BY [Entry No_] DESC");
            if (dataReader.Read())
            {
                entryNo = dataReader.GetInt32(0);
                entryNo = entryNo + 1;
            }
            dataReader.Close();


            byte[] buffer = new byte[orderXmlDoc.OuterXml.Length + 1000];

            buffer = System.Text.Encoding.UTF8.GetBytes(orderXmlDoc.OuterXml);

            DatabaseQuery query = database.prepare("INSERT INTO [WACKES$E-commerce Log$028a6c2e-d842-4c31-871c-7eb52e2bc71d] ([Entry No_], [E-commerce Part Code], [E-commerce Transfer System], [Type], [Date], [Time of day], [Response Received Date], [Response Received Time], [Document Type], [Document No_], [Status], [Message Text 1], [Message Text 2], [Message Text 3], [Document]) VALUES (@entryNo, @partner, @partnerIn, 1, @date, @time, @date, @time, 0, '', 0, '', '', '', @document)");
            query.addIntParameter("@entryNo", entryNo);
            query.addStringParameter("@partner", partner, 20);
            query.addStringParameter("@partnerIn", partner, 20);
            query.addDateTimeParameter("@date", DateTime.Today);
            query.addDateTimeParameter("@time", DateTime.Parse("1754-01-01 "+DateTime.Now.ToString("HH:mm:ss")));
            query.addBlobParameter("@document", buffer, buffer.Length);

            query.execute();


            database.close();


        }

        public static void SubmitOrder2(string partner, XmlDocument orderXmlDoc)
        {

            Configuration configuration = new Configuration();
            configuration.init();

            string token = OAuthHelper.GetToken(configuration.domain, configuration.clientId, configuration.clientSecret);

            JObject obj = new JObject();
            obj.Add("partCode", partner);
            obj.Add("data", orderXmlDoc.OuterXml);


            CallBCAPIPost("wackes.com/production/ODataV4/ecommerceHandler_incomingDocument?company=WACKES", token, obj.ToString());


        }

        public static string CallBCAPIPost(string endpoint, string token, string content)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.businesscentral.dynamics.com/v2.0/" + endpoint);
            request.Headers.Add("Authorization", "Bearer " + token);

            request.ContentType = "application/json";
            request.Method = "POST";
            request.Accept = "application/json";

            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(content);
            writer.Flush();
            writer.Close();


            try
            {
                WebResponse response = request.GetResponse();

                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string content2 = streamReader.ReadToEnd();

                Console.WriteLine(content2);

                return content2;
            }
            catch(WebException e1)
            {
                WebResponse response = e1.Response;

                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string content2 = streamReader.ReadToEnd();

                throw new Exception(content2);
            }
        }

    }
}