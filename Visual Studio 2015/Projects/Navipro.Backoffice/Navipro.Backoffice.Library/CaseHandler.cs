using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Navipro.Backoffice.Library
{
    public class CaseHandler
    {
        public CaseHandler()
        {

        }

        public static void CreateCase(Configuration configuration, Chilkat.Email email, Logger logger)
        {
            string resource = "";
            string body = "";
            body = email.GetPlainTextBody();
            if (email.HasHtmlBody()) body = email.GetHtmlBody();

            
            int i = 0;
            while (i < email.NumRelatedItems)
            {
                string cid = email.GetRelatedContentID(i);
                if (body.IndexOf(cid) > 0)
                {
                    byte[] byteArray = email.GetRelatedData(i);
                    string data = System.Convert.ToBase64String(byteArray);

                    string imgSrc = String.Format("data:image/gif;base64,{0}", data);

                    body = body.Replace("cid:" + cid, imgSrc);
                }
                i++;
            }



            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><case/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            NAVConnection.addElement(docElement, "subject", email.Subject, "");
            NAVConnection.addElement(docElement, "ordererEmail", email.FromAddress, "");
            NAVConnection.addElement(docElement, "ordererName", email.FromName, "");
            NAVConnection.addElement(docElement, "emailPool", configuration.emailAddress, "");
            NAVConnection.addElement(docElement, "responsibleResource", resource, "");
            NAVConnection.addElement(docElement, "receivedDate", email.EmailDate.ToString("yyyy-MM-dd"), "");

            NAVConnection.addElement(docElement, "description", body, "");
            NAVConnection.addElement(docElement, "messageId", email.GetHeaderField("Message-ID"), "");

            XmlElement membersElement = NAVConnection.addElement(docElement, "members", "", "");

            XmlElement memberElement = NAVConnection.addElement(membersElement, "member", "", "");
            memberElement.SetAttribute("name", email.FromName);
            memberElement.SetAttribute("email", email.FromAddress);
            memberElement.SetAttribute("type", "from");


            i = 0;
            while (i < email.NumTo)
            {
                memberElement = NAVConnection.addElement(membersElement, "member", "", "");
                memberElement.SetAttribute("name", email.GetToName(i));
                memberElement.SetAttribute("email", email.GetToAddr(i));
                memberElement.SetAttribute("type", "to");
                i++;
            }

            i = 0;
            while (i < email.NumCC)
            {
                memberElement = NAVConnection.addElement(membersElement, "member", "", "");
                memberElement.SetAttribute("name", email.GetCcName(i));
                memberElement.SetAttribute("email", email.GetCcAddr(i));
                memberElement.SetAttribute("type", "cc");
                i++;
            }

            XmlElement attachmentsElement = NAVConnection.addElement(docElement, "attachments", "", "");
            i = 0;
            while(i < email.NumAttachments)
            {
                byte[] byteArray = email.GetAttachmentData(i);
                string fileName = email.GetAttachmentFilename(i);
                string data = System.Convert.ToBase64String(byteArray);

                XmlElement attachmentElement = NAVConnection.addElement(attachmentsElement, "attachment", data, "");
                attachmentElement.SetAttribute("fileName", fileName);

                i++;
            }



            XmlElement responseElement = null;
            NAVConnection.performService(configuration, "createCaseFromEmail", xmlDoc, out responseElement);

        }


        public static void CreateCaseReply(Configuration configuration, Chilkat.Email email, Logger logger, string caseNo)
        {
            string resource = "";
            byte[] bodyArray = null;
            bodyArray = email.GetMbPlainTextBody("utf-8");
            
            if (email.HasHtmlBody()) bodyArray = email.GetMbHtmlBody("utf-8");


 
            string body = System.Text.Encoding.UTF8.GetString(bodyArray);                


            int i = 0;
            while (i < email.NumRelatedItems)
            {
                string cid = email.GetRelatedContentID(i);
                if (body.IndexOf(cid) > 0)
                {
                    byte[] byteArray = email.GetRelatedData(i);
                    string data = System.Convert.ToBase64String(byteArray);

                    string imgSrc = String.Format("data:image/gif;base64,{0}", data);

                    body = body.Replace("cid:" + cid, imgSrc);
                }
                i++;
            }



            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><case/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            NAVConnection.addElement(docElement, "no", caseNo, "");
            NAVConnection.addElement(docElement, "subject", email.Subject, "");
            NAVConnection.addElement(docElement, "ordererEmail", email.FromAddress, "");
            NAVConnection.addElement(docElement, "ordererName", email.FromName, "");
            NAVConnection.addElement(docElement, "emailPool", configuration.emailAddress, "");
            NAVConnection.addElement(docElement, "responsibleResource", resource, "");
            NAVConnection.addElement(docElement, "receivedDate", email.EmailDate.ToString("yyyy-MM-dd"), "");

            NAVConnection.addElement(docElement, "description", body, "");
            NAVConnection.addElement(docElement, "messageId", email.GetHeaderField("Message-ID"), "");

            XmlElement membersElement = NAVConnection.addElement(docElement, "members", "", "");

            XmlElement memberElement = NAVConnection.addElement(membersElement, "member", "", "");
            memberElement.SetAttribute("name", email.FromName);
            memberElement.SetAttribute("email", email.FromAddress);
            memberElement.SetAttribute("type", "from");


            i = 0;
            while (i < email.NumTo)
            {
                memberElement = NAVConnection.addElement(membersElement, "member", "", "");
                memberElement.SetAttribute("name", email.GetToName(i));
                memberElement.SetAttribute("email", email.GetToAddr(i));
                memberElement.SetAttribute("type", "to");
                i++;
            }

            i = 0;
            while (i < email.NumCC)
            {
                memberElement = NAVConnection.addElement(membersElement, "member", "", "");
                memberElement.SetAttribute("name", email.GetCcName(i));
                memberElement.SetAttribute("email", email.GetCcAddr(i));
                memberElement.SetAttribute("type", "cc");
                i++;
            }

            XmlElement attachmentsElement = NAVConnection.addElement(docElement, "attachments", "", "");
            i = 0;
            while (i < email.NumAttachments)
            {
                int size = email.GetAttachmentSize(i);
                byte[] byteArray = email.GetAttachmentData(i);
                string fileName = email.GetAttachmentFilename(i);
                string data = System.Convert.ToBase64String(byteArray);
                string errorMessage = "";
                if (size > 10000000)
                {
                    data = "";
                    errorMessage = "Bilagan är för stor. (" + size + " MB)";
                }

                XmlElement attachmentElement = NAVConnection.addElement(attachmentsElement, "attachment", data, "");
                attachmentElement.SetAttribute("fileName", fileName);
                attachmentElement.SetAttribute("errorMessage", errorMessage);

                i++;
            }



            XmlElement responseElement = null;
            NAVConnection.performService(configuration, "createCaseReplyFromEmail", xmlDoc, out responseElement);


        }

    }
}
