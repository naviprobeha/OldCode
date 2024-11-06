using Navipro.Backoffice.Web.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Navipro.Backoffice.Web.Controllers
{
    public class ZendeskHookController : BaseController
    {
        // GET: ZendeskHook
        public ActionResult Index()
        {
            StreamReader stream = new StreamReader(Request.InputStream);
            string payload = stream.ReadToEnd();
            logDebug("------------------------------");
            logDebug("Incoming payload: " + payload);


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(payload);
            if (xmlDoc.DocumentElement != null)
            {

                XmlElement docElement = xmlDoc.DocumentElement;

                string eventType = getNodeValue(docElement, "eventType");
                logDebug("Event: " + eventType);
                try
                {
                    if (eventType == "statusChange") changeCaseStatusEvent(docElement);
                }
                catch (Exception e)
                {
                    logDebug("Exception: " + e.Message);
                }
            }
            else
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\temp\\zendeskhook_debug.log");
                sw.WriteLine("Illegal payload: " + payload);
                sw.Flush();
                sw.Close();
            }
            

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);  // OK = 200
        }

        private void changeCaseStatusEvent(XmlElement eventElement)
        {
            publishInWrike(eventElement);

            string zendeskId = getNodeValue(eventElement, "id");
            string caseNo = getNodeValue(eventElement, "caseNo");
            string status = getNodeValue(eventElement, "status");
            string requesterEmail = getNodeValue(eventElement, "requesterEmail");
            string requesterName = getNodeValue(eventElement, "requesterName");
            string subject = getNodeValue(eventElement, "subject");
            string resourceEmail = getNodeValue(eventElement, "resourceEmail");
            string resourceName = getNodeValue(eventElement, "resourceName");
            string customerNo = getNodeValue(eventElement, "customerNo");

            if (customerNo == "") return;

            logDebug("TaskID: " + zendeskId + ", status:" + status);

            XmlDocument parameterDoc = new XmlDocument();

            if (status == "Öppen") status = "PÅGÅR";
            if (status == "Väntar") status = "PÅGÅR";
            if (status == "Löst") status = "AVSLUTAT";


            //Update Case Status in NAV.
            parameterDoc.LoadXml("<case/>");
            XmlElement docElement = parameterDoc.DocumentElement;
            addElement(docElement, "no", caseNo);
            addElement(docElement, "taskId", zendeskId);
            addElement(docElement, "jobNo", customerNo+"-996");
            addElement(docElement, "title", subject);
            addElement(docElement, "status", status);
            addElement(docElement, "contactEmail", requesterEmail);
            addElement(docElement, "contactName", requesterName);
            addElement(docElement, "resourceEmail", resourceEmail);
            addElement(docElement, "resourceName", resourceName);

            logDebug("Case Update: " + parameterDoc.OuterXml);

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "zendesk_updateCase", parameterDoc, out responseElement);
            logDebug("Updated successfully: "+responseElement.OuterXml);

            if (caseNo == "") caseNo = getNodeValue(responseElement, "no");

            parameterDoc = new XmlDocument();
            parameterDoc.LoadXml("<case><no>" + caseNo + "</no><resourceEmail>" + resourceEmail + "</resourceEmail></case>");

            logDebug("Include in time report: " + parameterDoc.OuterXml);
            NAVConnection.performService(database.configuration, "zendesk_includeInTimeReport", parameterDoc, out responseElement);

            
        }

        private void publishInWrike(XmlElement eventElement)
        {
            logDebug("Publish Support Task in Wrike");

            string zendeskId = getNodeValue(eventElement, "id");
            string caseNo = getNodeValue(eventElement, "caseNo");
            string status = getNodeValue(eventElement, "status");
            string requesterEmail = getNodeValue(eventElement, "requesterEmail");
            string requesterName = getNodeValue(eventElement, "requesterName");
            string subject = getNodeValue(eventElement, "subject");
            string resourceEmail = getNodeValue(eventElement, "resourceEmail");
            string resourceName = getNodeValue(eventElement, "resourceName");
            string customerNo = getNodeValue(eventElement, "customerNo");
            string description = getNodeValue(eventElement, "description");
            string latestComment = getNodeValue(eventElement, "comment");

            if (customerNo == "") return;

            try
            {
                WrikeHookController wrikeController = new WrikeHookController();
                XmlDocument wrikeTask = wrikeController.getTaskByCustomFields("Zendesk ID", zendeskId);

                string wrikeId = getNodeValue(wrikeTask.DocumentElement, "data/id");
                if (wrikeId == "")
                {
                    //Create Task
                    string folderId = wrikeController.getFolderIdByJobNo(customerNo + "-996");
                    logDebug("FolderID: " + folderId);
                    if (folderId != "")
                    {
                        wrikeId = wrikeController.createTaskInFolder(folderId, "", subject, description, "", zendeskId);
                    }
                }
                logDebug("Wrike task id: " + wrikeId);

                logDebug("Comment: " + latestComment);
                if (latestComment != "")
                {
                    wrikeController.createTaskComment(wrikeId, latestComment);
                }
            }
            catch (Exception e)
            {
                logDebug("Exception: " + e.Message);
            }
        }


        private void logDebug(string message)
        {
            if (System.IO.File.Exists("C:\\temp\\zendeskhook.log"))
            {
                StreamWriter streamWriter = new StreamWriter("C:\\temp\\zendeskhook.log", true);
                streamWriter.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + message);
                streamWriter.Flush();
                streamWriter.Close();
            }
            else
            {
                StreamWriter streamWriter = new StreamWriter("C:\\temp\\zendeskhook.log");
                streamWriter.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + message);
                streamWriter.Flush();
                streamWriter.Close();

            }

        }

        private string getNodeValue(XmlElement parentElement, string nodePath)
        {
            XmlElement element = (XmlElement)parentElement.SelectSingleNode(nodePath);
            if (element != null)
            {
                XmlText text = (XmlText)element.FirstChild;
                if (text != null)
                {
                    return text.Value;
                }
            }

            return "";
        }

        private XmlElement addElement(XmlElement parentElement, string name, string value)
        {
            XmlElement childElement = parentElement.OwnerDocument.CreateElement(name);
            if (value != "")
            {
                XmlText text = parentElement.OwnerDocument.CreateTextNode(value);
                childElement.AppendChild(text);
            }
            parentElement.AppendChild(childElement);

            return childElement;
        }
    }
}