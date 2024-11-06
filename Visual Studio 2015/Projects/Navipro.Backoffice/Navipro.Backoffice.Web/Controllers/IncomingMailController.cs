using Navipro.Backoffice.Web.Lib;
using Navipro.Backoffice.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Navipro.Backoffice.Web.Controllers
{
    public class IncomingMailController : BaseController
    {
        // GET: IncomingMail
        [ValidateInput(false)]
        public ActionResult Index()
        {            
            string messageId = Request["Message-Id"];
            string references = Request["References"];
            string subject = getHeaderField(Request["message-headers"], "Subject");
            
            /*
            StreamReader stream = new StreamReader(Request.InputStream);
            string x = stream.ReadToEnd();

            System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\temp\\incomingmail.txt");
            sw.WriteLine(x);
            sw.Flush();
            sw.Close();
            */

            string caseNo = "";


            if (!Case.checkIfEmailIsExistingCase(database, messageId, out caseNo))
            {
                if (Case.checkReferences(database, references, out caseNo))
                {
                    if (caseNo == "") caseNo = getCaseNoFromSubject(subject);
                    if (caseNo != "")
                    {
                        if (caseStatusIsOpen(caseNo))
                        {
                            createCaseReply(caseNo);
                        }
                        else
                        {
                            createCase();
                        }
                    }
                    else
                    {
                        createCase();
                    }
                }
                else
                {
                    caseNo = getCaseNoFromSubject(subject);
                    if (caseNo == "")
                    {
                        createCase();
                    }
                }

            }


            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);  // OK = 200
        }

        private void createCase()
        {
            string resource = "";
            string body = "";
            string subject = getHeaderField(Request["message-headers"], "Subject");
            
            body = Request["body-html"];
            if (body == "") body = Request["body-plain"];

            StreamReader reader = new StreamReader(Request.InputStream);
            string raw = reader.ReadToEnd();
            StreamWriter writer = new StreamWriter("C:\\temp\\content.raw");
            writer.Write(raw);
            writer.Flush();
            writer.Close();

            

            string messageId = Request["Message-Id"];
            string from = Request["From"];
            string[] fromArray = from.Split(new string[] { ">," }, StringSplitOptions.None);
            string fromName = "";
            string fromAddress = "";
            cleanAddress(fromArray[0], out fromName, out fromAddress);


            Dictionary<string, string> contentMap = new Dictionary<string, string>();
            List<string> contentList = new List<string>();
            List<CaseAttachment> attachmentList = new List<CaseAttachment>();
            Dictionary<string, CaseAttachment> attachmentTable = new Dictionary<string, CaseAttachment>();


            foreach (string key in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[key];

                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                byte[] byteArray = target.ToArray();

                string errorMessage = "";
                if (byteArray.Length >= 100000000)
                {
                    errorMessage = "Bilagan är för stor. Max 100MB.";
                }
                if (byteArray.Length == 0)
                {
                    errorMessage = "Bilagan är tom.";
                }

                CaseAttachment caseAttachment = new CaseAttachment();
                caseAttachment.fileName = file.FileName;
                caseAttachment.data = byteArray;
                caseAttachment.fromEmail = fromAddress;
                caseAttachment.fromName = fromName;
                caseAttachment.errorMessage = errorMessage;

                attachmentList.Add(caseAttachment);
                attachmentTable.Add(key, caseAttachment);
            }

            string contentIdMapJson = Request["content-id-map"];
            if ((contentIdMapJson != "") && (contentIdMapJson != null))
            {
                contentMap = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(contentIdMapJson);
            }
            
            foreach (string key in contentMap.Keys)
            {                
                string cid = key.Substring(1);
                cid = cid.Substring(0, cid.Length - 1);
             
                CaseAttachment attachment = attachmentTable[contentMap[key]];            

                string data = System.Convert.ToBase64String(attachment.data);
                string imgSrc = String.Format("data:image/gif;base64,{0}", data);

                if (body.IndexOf(cid) > -1)
                {
                    body = body.Replace("cid:" + cid, imgSrc);

                    contentList.Add(attachment.fileName);
                }
            }


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><case/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            NAVConnection.addElement(docElement, "subject", subject, "");
            NAVConnection.addElement(docElement, "ordererEmail", fromAddress, "");
            NAVConnection.addElement(docElement, "ordererName", fromName, "");
            NAVConnection.addElement(docElement, "emailPool", "support@navipro.se", "");
            NAVConnection.addElement(docElement, "responsibleResource", resource, "");
            NAVConnection.addElement(docElement, "receivedDate", DateTime.Today.ToString("yyyy-MM-dd"), "");

            NAVConnection.addElement(docElement, "description", HttpUtility.HtmlEncode(body), "");
            NAVConnection.addElement(docElement, "messageId", messageId, "");

            XmlElement membersElement = NAVConnection.addElement(docElement, "members", "", "");

            XmlElement memberElement = NAVConnection.addElement(membersElement, "member", "", "");
            memberElement.SetAttribute("name", fromName);
            memberElement.SetAttribute("email", fromAddress);
            memberElement.SetAttribute("type", "from");

            string to = Request["To"];
            string[] toArray = to.Split(new string[] { ">," }, StringSplitOptions.None);

            foreach (string toItem in toArray)
            {                
                string toName = "";
                string toAddress = "";
                cleanAddress(toItem, out toName, out toAddress);

                memberElement = NAVConnection.addElement(membersElement, "member", "", "");
                memberElement.SetAttribute("name", toName);
                memberElement.SetAttribute("email", toAddress);
                memberElement.SetAttribute("type", "to");
            }

            string cc = Request["Cc"];
            string[] ccArray = to.Split(new string[] { ">," }, StringSplitOptions.None);

            foreach (string ccItem in ccArray)
            {
                string ccName = "";
                string ccAddress = "";
                cleanAddress(ccItem, out ccName, out ccAddress);

                memberElement = NAVConnection.addElement(membersElement, "member", "", "");
                memberElement.SetAttribute("name", ccName);
                memberElement.SetAttribute("email", ccAddress);
                memberElement.SetAttribute("type", "cc");
            }

            
            XmlElement attachmentsElement = NAVConnection.addElement(docElement, "attachments", "", "");

            foreach (CaseAttachment attachment in attachmentList)
            {
                if (!contentList.Contains(attachment.fileName))
                {
                    XmlElement attachmentElement = NAVConnection.addElement(attachmentsElement, "attachment", "", "");
                    attachmentElement.SetAttribute("fileName", attachment.fileName);
                    attachmentElement.SetAttribute("fromEmail", attachment.fromEmail);
                    attachmentElement.SetAttribute("fromName", attachment.fromName);
                    attachmentElement.SetAttribute("errorMessage", attachment.errorMessage);
                }
            }


            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "createCaseFromEmail", xmlDoc, out responseElement);

            try
            {
                string caseNo = responseElement.GetAttribute("no");
                Case caseItem = Case.getEntry(database, caseNo);
                if (caseItem != null)
                {
                    try
                    {
                        MailSender.SendCreate(database, caseItem);
                    }
                    catch (Exception) { };

                    Database resourcesDatabase = (Database)Session["resourcesDatabase"];

                    foreach (CaseAttachment attachment in attachmentList)
                    {
                        if (!contentList.Contains(attachment.fileName))
                        {
                            attachment.create(resourcesDatabase, caseItem.no, 0);
                        }
                    }
                }
            }
            catch (Exception )
            {

            }
        }

        public void createCaseReply(string caseNo)
        {

            logDebug("Step 1");

            string resource = "";
            string body = "";
            string subject = getHeaderField(Request["message-headers"], "Subject");
            body = Request["body-html"];
            if (body == "") body = Request["body-plain"];

            logDebug("Step 2");

            string messageId = Request["Message-Id"];
            string from = Request["From"];
            string[] fromArray = from.Split(new string[] { ">," }, StringSplitOptions.None);
            string fromName = "";
            string fromAddress = "";
            cleanAddress(fromArray[0], out fromName, out fromAddress);

            logDebug("Step 3");

            Dictionary<string, string> contentMap = new Dictionary<string, string>();
            List<string> contentList = new List<string>();
            List<CaseAttachment> attachmentList = new List<CaseAttachment>();
            Dictionary<string, CaseAttachment> attachmentTable = new Dictionary<string, CaseAttachment>();

            logDebug("Step 4");

            try
            {
                foreach (string key in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[key];

                    if (file != null)
                    {
                        MemoryStream target = new MemoryStream();
                        file.InputStream.CopyTo(target);
                        byte[] byteArray = target.ToArray();

                        string errorMessage = "";
                        if (byteArray.Length >= 100000000)
                        {
                            errorMessage = "Bilagan är för stor. Max 100MB.";
                        }
                        if (byteArray.Length == 0)
                        {
                            errorMessage = "Bilagan är tom.";
                        }

                        CaseAttachment caseAttachment = new CaseAttachment();
                        caseAttachment.fileName = file.FileName;
                        caseAttachment.data = byteArray;
                        caseAttachment.fromEmail = fromAddress;
                        caseAttachment.fromName = fromName;
                        caseAttachment.errorMessage = errorMessage;

                        attachmentList.Add(caseAttachment);
                        attachmentTable.Add(key, caseAttachment);
                    }
                }
                string contentIdMapJson = Request["content-id-map"];
                if ((contentIdMapJson != "") && (contentIdMapJson != null))
                {
                    contentMap = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(contentIdMapJson);
                }

                foreach (string key in contentMap.Keys)
                {
                    string cid = key.Substring(1);
                    cid = cid.Substring(0, cid.Length - 1);

                    CaseAttachment attachment = attachmentTable[contentMap[key]];

                    string data = System.Convert.ToBase64String(attachment.data);
                    string imgSrc = String.Format("data:image/gif;base64,{0}", data);

                    if (body.IndexOf(cid) > -1)
                    {
                        body = body.Replace("cid:" + cid, imgSrc);

                        contentList.Add(attachment.fileName);
                    }
                }

            }
            catch (Exception)
            {
                logDebug("Exception 1");

            }


            logDebug("Push 1");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><case/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            NAVConnection.addElement(docElement, "no", caseNo, "");
            NAVConnection.addElement(docElement, "subject", subject, "");
            NAVConnection.addElement(docElement, "ordererEmail", fromAddress, "");
            NAVConnection.addElement(docElement, "ordererName", fromName, "");
            NAVConnection.addElement(docElement, "emailPool", "support@navipro.se", "");
            NAVConnection.addElement(docElement, "responsibleResource", resource, "");
            NAVConnection.addElement(docElement, "receivedDate", DateTime.Today.ToString("yyyy-MM-dd"), "");

            NAVConnection.addElement(docElement, "description", HttpUtility.HtmlEncode(body), "");
            NAVConnection.addElement(docElement, "messageId", messageId, "");

            logDebug("Push 2");

            XmlElement membersElement = NAVConnection.addElement(docElement, "members", "", "");

            XmlElement memberElement = NAVConnection.addElement(membersElement, "member", "", "");
            memberElement.SetAttribute("name", fromName);
            memberElement.SetAttribute("email", fromAddress);
            memberElement.SetAttribute("type", "from");

            logDebug("Push 3");

            try
            {

                string to = Request["To"];
                string[] toArray = to.Split(new string[] { ">," }, StringSplitOptions.None);

                foreach (string toItem in toArray)
                {
                    string toName = "";
                    string toAddress = "";
                    cleanAddress(toItem, out toName, out toAddress);

                    memberElement = NAVConnection.addElement(membersElement, "member", "", "");
                    memberElement.SetAttribute("name", toName);
                    memberElement.SetAttribute("email", toAddress);
                    memberElement.SetAttribute("type", "to");
                }

                logDebug("Push 4");

                string cc = Request["Cc"];
                string[] ccArray = to.Split(new string[] { ">," }, StringSplitOptions.None);

                foreach (string ccItem in ccArray)
                {
                    string ccName = "";
                    string ccAddress = "";
                    cleanAddress(ccItem, out ccName, out ccAddress);

                    memberElement = NAVConnection.addElement(membersElement, "member", "", "");
                    memberElement.SetAttribute("name", ccName);
                    memberElement.SetAttribute("email", ccAddress);
                    memberElement.SetAttribute("type", "cc");
                }

            }
            catch (Exception)
            { }

            logDebug("Push 5");


            try
            {
                XmlElement attachmentsElement = NAVConnection.addElement(docElement, "attachments", "", "");
                Database resourcesDatabase = (Database)Session["resourcesDatabase"];


                foreach (CaseAttachment attachment in attachmentList)
                {
                    if (!contentList.Contains(attachment.fileName))
                    {
                        XmlElement attachmentElement = NAVConnection.addElement(attachmentsElement, "attachment", "", "");
                        attachmentElement.SetAttribute("fileName", attachment.fileName);
                        attachmentElement.SetAttribute("fromEmail", attachment.fromEmail);
                        attachmentElement.SetAttribute("fromName", attachment.fromName);
                        attachmentElement.SetAttribute("errorMessage", attachment.errorMessage);

                        attachment.create(resourcesDatabase, caseNo, 0);

                    }
                }
            }
            catch (Exception)
            {
                logDebug("Exception 2");

            }


            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "createCaseReplyFromEmail", xmlDoc, out responseElement);

            logDebug("Done");

        }

        private void cleanAddress(string address, out string name, out string email)
        {
            address = address.Trim();
            char tab = '\u0009';
            address = address.Replace(tab.ToString(), "");

            if (address.IndexOf("<") > 0)
                name = address.Substring(0, address.IndexOf("<") - 1);
            else
                name = address;

            if (name[0] == '"') name = name.Substring(1);
            if (name[name.Length - 1] == '"') name = name.Substring(0, name.Length - 1);

            if (address.IndexOf("<") > -1)
            {
                email = address.Substring(address.IndexOf("<"));
            }
            else
            {
                email = address;
            }
            if (email[0] == '<') email = email.Substring(1);
            if (email[email.Length - 1] == '>') email = email.Substring(0, email.Length - 1);

        }

        private string getHeaderField(string headers, string fieldName)
        {
            string value = "";
            if (headers.Contains("\""+fieldName+"\""))
            {
                string section = headers.Substring(headers.IndexOf("\"" + fieldName + "\""));
                section = section.Substring(fieldName.Length + 5);
                section = section.Substring(0, section.IndexOf("\"]"));
                value = System.Text.RegularExpressions.Regex.Unescape(section);
                
            }

            return value;

        }
        
        private string getCaseNoFromSubject(string subject)
        {
            if (subject.IndexOf("[CASE") > -1)
            {
                string caseNo = subject.Substring(subject.IndexOf("[CASE") + 1);
                caseNo = caseNo.Substring(0, caseNo.IndexOf("] "));
                return caseNo;
            }
            return "";
        }

        private bool caseStatusIsOpen(string caseNo)
        {
            Case caseItem = Case.getEntry(database, caseNo);
            if (caseItem != null)
            {
                if (caseItem.caseTypeCode == "ÄRENDE") 
                {
                    return true;
                }
                
            }

            return false;
        }

        private void logDebug(string message)
        {
            if (System.IO.File.Exists("C:\\temp\\debug.txt"))
            {
                StreamWriter streamWriter = new StreamWriter("C:\\temp\\debug.txt", true);
                streamWriter.WriteLine("["+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"] "+ message);
                streamWriter.Flush();
                streamWriter.Close();
            }
            else
            {
                StreamWriter streamWriter = new StreamWriter("C:\\temp\\debug.txt");
                streamWriter.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + message);
                streamWriter.Flush();
                streamWriter.Close();

            }

        }
    }
}