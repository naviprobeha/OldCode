using HtmlAgilityPack;
using Navipro.Backoffice.Web.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace Navipro.Backoffice.Web.Lib
{
    public class MailSender
    {
        public static void SendCreate(Database database, Case caseItem)
        {
            string receiver = caseItem.ordererEmail;

            if (!receiver.Contains("mailgun"))
            {
                string templateBody = loadTemplate("EmailTemplate_create_default");

                templateBody = templateBody.Replace("[caseNo]", caseItem.no);
                templateBody = templateBody.Replace("[subject]", HttpUtility.HtmlDecode(caseItem.subject));

                Dictionary<string, byte[]> inlineList = createHtmlCID(ref templateBody);


                RestClient client = new RestClient();
                client.BaseUrl = "https://api.mailgun.net/v2";
                client.Authenticator = new HttpBasicAuthenticator("api", database.configuration.smtpKey);
                RestRequest request = new RestRequest();
                request.AddParameter("domain", database.configuration.smtpDomain, ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", database.configuration.smtpFromName + " <" + database.configuration.smtpFromAddress + ">");
                request.AddParameter("to", receiver);
                request.AddParameter("h:In-Reply-To", caseItem.messageId);
                request.AddParameter("subject", "[" + caseItem.no + "] " + caseItem.subject);
                request.AddParameter("html", templateBody);
                request.AddFile("inline", HttpContext.Current.Server.MapPath("~/Templates/navipro.jpg"));
                request.AddFile("inline", HttpContext.Current.Server.MapPath("~/Templates/navipro_small.jpg"));

                foreach (string key in inlineList.Keys)
                {
                    if (inlineList[key] != null) request.AddFile("inline", inlineList[key], key);
                }


                request.Method = Method.POST;


                IRestResponse response = client.Execute(request);

                Dictionary<string, string> responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);

            }


        }


        public static void SendReply(Database database, Case caseItem, CaseLine caseLine)
        {

            foreach (string receiver in caseLine.receiverList)
            {
                if (!receiver.Contains("mailgun"))
                {
                    string templateBody = loadTemplate("EmailTemplate_reply_default");
                    string historyComment = "";

                    CaseLine prevComment = caseLine.getPrevCaseLine(database);
                    if (prevComment != null) historyComment = prevComment.externalComment;

                    templateBody = templateBody.Replace("[caseNo]", caseItem.no);
                    templateBody = templateBody.Replace("[subject]", HttpUtility.HtmlDecode(caseItem.subject));
                    templateBody = templateBody.Replace("[fromName]", caseLine.fromName);
                    templateBody = templateBody.Replace("[body]", HttpUtility.HtmlDecode(caseLine.htmlComment));

                    templateBody = templateBody.Replace("[history]", HttpUtility.HtmlDecode(historyComment));

                    Dictionary<string, byte[]> inlineList = createHtmlCID(ref templateBody);


                    RestClient client = new RestClient();
                    client.BaseUrl = "https://api.mailgun.net/v2";
                    client.Authenticator = new HttpBasicAuthenticator("api", database.configuration.smtpKey);
                    RestRequest request = new RestRequest();
                    request.AddParameter("domain", database.configuration.smtpDomain, ParameterType.UrlSegment);
                    request.Resource = "{domain}/messages";
                    request.AddParameter("from", database.configuration.smtpFromName + " <" + database.configuration.smtpFromAddress + ">");
                    request.AddParameter("to", receiver);
                    request.AddParameter("h:In-Reply-To", caseItem.messageId);
                    request.AddParameter("subject", "[" + caseItem.no + "] " + caseItem.subject);
                    request.AddParameter("html", templateBody);
                    request.AddFile("inline", HttpContext.Current.Server.MapPath("~/Templates/navipro.jpg"));
                    request.AddFile("inline", HttpContext.Current.Server.MapPath("~/Templates/navipro_small.jpg"));

                    foreach(string key in inlineList.Keys)
                    {
                        request.AddFile("inline", inlineList[key], key);
                    }

                    request.Method = Method.POST;


                    IRestResponse response = client.Execute(request);

                    Dictionary<string, string> responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);

                    caseItem.markCommentAsSent(database, caseLine.lineNo, responseData["id"]);

                }
            }

        }



        private static string loadTemplate(string type)
        {
            string templatePath = HttpContext.Current.Server.MapPath("~/Templates/"+type+".html");
            StreamReader streamReader = new StreamReader(templatePath);
            string body = streamReader.ReadToEnd();
            streamReader.Close();

            return body;
        }

        private static Dictionary<string, byte[]> createHtmlCID(ref string htmlString)
        {
            int attachmentNo = 0;
            Dictionary<string, byte[]> inlineList = new Dictionary<string, byte[]>();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(htmlString);
            document.DocumentNode.Descendants("img")
                                .Where(e =>
                                {
                                    string src = e.GetAttributeValue("src", null) ?? "";
                                    return !string.IsNullOrEmpty(src) && src.StartsWith("data:image");
                                })
                                .ToList()
                                .ForEach(x =>
                                {
                                    attachmentNo++;
                                    
                                    string currentSrcValue = x.GetAttributeValue("src", null);
                                    string currentClipboardFileName = x.GetAttributeValue("data-filename", "clipboard.png");


                                    currentSrcValue = currentSrcValue.Split(',')[1];//Base64 part of string
                                    byte[] imageData = Convert.FromBase64String(currentSrcValue);

                                    string filename = "attachment" + attachmentNo + currentClipboardFileName.Substring(currentClipboardFileName.IndexOf("."));

                                    inlineList.Add(filename, imageData);
                                    
                                    x.SetAttributeValue("src", "cid:" + filename);
                                });

            htmlString = document.DocumentNode.OuterHtml;
            return inlineList;
        }
    }
}