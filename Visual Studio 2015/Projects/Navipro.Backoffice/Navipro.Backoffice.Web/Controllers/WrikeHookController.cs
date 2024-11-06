using Navipro.Backoffice.Web.Lib;
using Navipro.Backoffice.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Navipro.Backoffice.Web.Controllers
{
    public class WrikeHookController : BaseController
    {
        private string secret = "eyJ0dCI6InAiLCJhbGciOiJIUzI1NiIsInR2IjoiMSJ9.eyJkIjoie1wiYVwiOjIyMzE0NzEsXCJpXCI6MTMwMzQsXCJjXCI6NDU5NTIwNSxcInZcIjpudWxsLFwidVwiOjQ4MTQwMDQsXCJyXCI6XCJFVVwiLFwic1wiOltcIldcIixcIkZcIixcIklcIixcIlVcIixcIktcIixcIkNcIl0sXCJ6XCI6W10sXCJ0XCI6MH0iLCJpYXQiOjE1NDEwNTc0MzN9.IJIpleav1c_B-vPcQKn6jYDs31pmsrIDmMehg8dSpiI";

        private Dictionary<string, string> customFieldsTable;
        private Dictionary<string, string> userTable;

        // GET: WrikeHook
        [ValidateInput(false)]
        public ActionResult Index()
        {

            if (Request["mode"]=="register")
            {
                updateWebHooks();
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);  // OK = 200
            }

            StreamReader stream = new StreamReader(Request.InputStream);
            string payload = stream.ReadToEnd();
            logDebug("------------------------------");
            logDebug("Incoming payload: " + payload);

            try
            {

                if (payload.StartsWith("["))
                {
                    payload = payload.Substring(1);
                    if (payload.EndsWith("]")) payload = payload.Substring(0, payload.Length - 1);
                }
            }
            catch(Exception e)
            {
                logDebug("Parse error: " + e.Message);
            }
            

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(payload, "Wrike");
            if (xmlDoc != null)
            {

                XmlElement docElement = xmlDoc.DocumentElement;


                System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\temp\\wrikehook_debug.log");
                sw.WriteLine(xmlDoc.OuterXml);
                sw.Flush();
                sw.Close();

                string eventType = getNodeValue(docElement, "eventType");
                logDebug("Event: " + eventType);
                try
                {
                    if (eventType == "TaskStatusChanged") changeCaseStatusEvent(docElement);
                    if (eventType == "TaskCreated") taskCreationEvent(docElement);
                    if (eventType == "CommentAdded") commentAddedEvent(docElement);
                    if (eventType == "TaskCustomFieldChanged") customFieldChangedEvent(docElement);
                    if (eventType == "TaskResponsiblesAdded") assignedResourceEvent(docElement);
                    if (eventType == "TaskResponsiblesRemoved") assignedResourceEvent(docElement);
                    if (eventType == "TaskDatesChanged") assignedResourceEvent(docElement);

                }
                catch (Exception e)
                {
                    logDebug("Exception: " + e.Message+" ("+e.StackTrace+")");
                }
            }
            else
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\temp\\wrikehook_debug.log");
                sw.WriteLine("Illegal payload: "+payload);
                sw.Flush();
                sw.Close();
            }


            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);  // OK = 200
        }


        private void changeCaseStatusEvent(XmlElement eventElement)
        {
            string wrikeTaskId = getNodeValue(eventElement, "taskId");
            string status = getNodeValue(eventElement, "status");
            string contactId = getNodeValue(eventElement, "eventAuthorId");

            status = convertStatus(status);

            logDebug("TaskID: " + wrikeTaskId + ", status:" + status);

            XmlDocument parameterDoc = new XmlDocument();

            XmlDocument taskDocument = getTaskById(wrikeTaskId);
            string caseNo = getCustomFieldValueFromDocument(taskDocument, "Case No.");
            string description = getNodeValue(taskDocument.DocumentElement, "data/title");
            string jobNo = "";
            string budget = getCustomFieldValueFromDocument(taskDocument, "Budget (h)");

            if (caseNo == "")
            {
                if ((status.ToUpper() == "ORDER") || (status.ToUpper() == "PÅGÅR") || (status.ToUpper() == "IN PROGRESS") || (status.ToUpper() == "TESTKLAR") || (status.ToUpper() == "GODKÄND") || (status.ToUpper() == "AVSLUTAT"))
                {
                    if (description.ToUpper() == "PROJEKTSTATUS")
                    {
                        createUpdateProject(taskDocument);
                        return;
                    }
                    jobNo = findJobFolder(taskDocument);
                    
                }
            }

            //Update Case Status in NAV.
            parameterDoc.LoadXml("<case/>");
            XmlElement docElement = parameterDoc.DocumentElement;
            addElement(docElement, "no", caseNo);
            addElement(docElement, "taskId", wrikeTaskId);
            addElement(docElement, "jobNo", jobNo);
            addElement(docElement, "title", description);
            addElement(docElement, "status", status);
            addElement(docElement, "contactEmail", getCustomFieldValueFromDocument(taskDocument, "Contact E-mail"));
            addElement(docElement, "contactName", getCustomFieldValueFromDocument(taskDocument, "Contact Name"));
            addElement(docElement, "budget", budget);

            logDebug("Case Update: " + parameterDoc.OuterXml);

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "wrike_updateCase", parameterDoc, out responseElement);
            logDebug("Updated successfully");

            if (caseNo == "") return;

            //Include in time report
            getUsers();
            if (!userTable.ContainsKey(contactId)) return;

            parameterDoc = new XmlDocument();
            parameterDoc.LoadXml("<case><no>" + caseNo + "</no><resourceNo>" + userTable[contactId] + "</resourceNo></case>");

            logDebug("Include in time report: " + parameterDoc.OuterXml);
            NAVConnection.performService(database.configuration, "wrike_includeInTimeReport", parameterDoc, out responseElement);

        }

        private void commentAddedEvent(XmlElement eventElement)
        {
            string wrikeTaskId = getNodeValue(eventElement, "taskId");
            string status = getNodeValue(eventElement, "status");
            string contactId = getNodeValue(eventElement, "eventAuthorId");

            string comment = getNodeValue(eventElement, "comment/text");

            logDebug("TaskID: " + wrikeTaskId + ", status:" + status+", author: "+contactId+", comment: "+comment);

            if (checkCommentCommand(comment, wrikeTaskId)) return;

            XmlDocument taskDocument = getTaskById(wrikeTaskId);
            string caseNo = getCustomFieldValueFromDocument(taskDocument, "Case No.");
            if (caseNo == "") return;

            getUsers();
            if (!userTable.ContainsKey(contactId)) return;

            
            //Include in time report.
            XmlDocument parameterDoc = new XmlDocument();
            parameterDoc.LoadXml("<case><no>" + caseNo + "</no><resourceNo>" + userTable[contactId] + "</resourceNo></case>");

            logDebug("Include in time report: " + parameterDoc.OuterXml);

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "wrike_includeInTimeReport", parameterDoc, out responseElement);

        }

        private bool checkCommentCommand(string commentCommand, string taskId)
        {
            if (commentCommand.Substring(0, 9).ToUpper() == "#TILLDELA") return assignTaskToResource(commentCommand, taskId);
            if (commentCommand.Substring(0, 9).ToUpper() == "#ASSIGN") return assignTaskToResource(commentCommand, taskId);
            if (commentCommand.Substring(0, 8).ToUpper() == "#SUBTASK") return createSubTask(taskId, commentCommand);
            if (commentCommand.Substring(0, 11).ToUpper() == "#DELSUBTASK") return deleteSubTask(taskId);
            if (commentCommand.Substring(0, 7).ToUpper() == "GUDRUN:") return doBotCommand(commentCommand, taskId);
            return false;
        }

        private bool assignTaskToResource(string commentCommand, string taskId)
        {
            string resourceNo = commentCommand.Substring(commentCommand.IndexOf(" ") + 1, 4).ToUpper();
            getUsers();
            string resourceId = getResourceIdFromUserTable(resourceNo);
            if (resourceId == "") return true;

            logDebug("Assign task " + taskId + " to " + resourceNo);
            updateTaskAssignResource(taskId, resourceId);
            

            return true;
        }

        private bool doBotCommand(string commentCommand, string taskId)
        {
            string command = commentCommand.Substring(8);
            if (command.Substring(0, 7).ToUpper() == "VAD ÄR ")
            {
                string question = command.Substring(7);
                string message = question + " E du goo eller?";
                if (question.Substring(question.Length - 3, 2) == "??") message = "2";
                createTaskComment(taskId, message);

            }
            if (command.Substring(0, 8).ToUpper() == "TILLDELA")
            {
                getUsers();
                string resourceNo = command.Substring(9, 4).ToUpper();
                logDebug("Assign task " + taskId + " to [" + resourceNo+"]");

                string resourceId = getResourceIdFromUserTable(resourceNo);
                if (resourceId == "") return true;

                createTaskComment(taskId, "Självklart!");
                updateTaskAssignResource(taskId, resourceId);
            }
            return true;
        }


        private void taskCreationEvent(XmlElement eventElement)
        {
            string wrikeTaskId = getNodeValue(eventElement, "taskId");
            string status = getNodeValue(eventElement, "status");

            logDebug("TaskID: " + wrikeTaskId + ", status:" + status);
            XmlDocument taskDocument = getTaskById(wrikeTaskId);
            if (taskIsRequestCase(taskDocument))
            {
                convertTaskWithTemplate(taskDocument);
            }
            logDebug("Task Creation event Done");
        }

        private void customFieldChangedEvent(XmlElement eventElement)
        {
            string wrikeTaskId = getNodeValue(eventElement, "taskId");

            logDebug("TaskID: " + wrikeTaskId);
            XmlDocument taskDocument = getTaskById(wrikeTaskId);

            updateCasePlan(wrikeTaskId, taskDocument);

        }

        private void assignedResourceEvent(XmlElement eventElement)
        {
            string wrikeTaskId = getNodeValue(eventElement, "taskId");

            logDebug("TaskID: " + wrikeTaskId);
            XmlDocument taskDocument = getTaskById(wrikeTaskId);

            updateCasePlan(wrikeTaskId, taskDocument);

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

        private void createUpdateProject(XmlDocument taskDocument)
        {
            logDebug("Create / Update Project");
            string jobNo = findJobFolder(taskDocument);
            logDebug("Job No: " + jobNo);
            if (jobNo == "") return;

            string contactEmail = getCustomFieldValueFromDocument(taskDocument, "Contact E-mail");
            string contactName = getCustomFieldValueFromDocument(taskDocument, "Contact Name");
            if ((contactEmail == "") || (contactName == "")) return;

            string folderId = getFolderIdByJobNo(jobNo);
            XmlDocument tasksDocument = getFolderTasks(folderId);
            XmlElement docElement = tasksDocument.DocumentElement;

            XmlNodeList taskNodeList = docElement.SelectNodes("data");
            logDebug("Task count: " + taskNodeList.Count);

            int i = 0;
            while (i < taskNodeList.Count)
            {
                XmlElement projectTaskElement = (XmlElement)taskNodeList.Item(i);

                string wrikeTaskId = getNodeValue(projectTaskElement, "id");
                string description = getNodeValue(projectTaskElement, "title");
                string reasonCode = getCustomFieldValueFromElement(projectTaskElement, "Reason Code");
                string activityType = getActivityTypeFromTask(projectTaskElement);
                string caseNo = getCustomFieldValueFromElement(projectTaskElement, "Case No.");
                string budget = getCustomFieldValueFromElement(projectTaskElement, "Budget (h)");


                logDebug("Project Task: ID:" + wrikeTaskId + ", Desc: " + description + ", Reason: " + reasonCode + ", Activity:" + activityType+", Case No: "+caseNo);
                if ((reasonCode != "INGEN") && (caseNo == ""))
                {
                    //Update Case Status in NAV.
                    XmlDocument parameterDoc = new XmlDocument();
                    parameterDoc.LoadXml("<case/>");
                    XmlElement paramDocElement = parameterDoc.DocumentElement;
                    addElement(paramDocElement, "no", caseNo);
                    addElement(paramDocElement, "taskId", wrikeTaskId);
                    addElement(paramDocElement, "jobNo", jobNo);
                    addElement(paramDocElement, "title", description);
                    addElement(paramDocElement, "status", "NY");
                    addElement(paramDocElement, "reasonCode", reasonCode);
                    addElement(paramDocElement, "activityType", activityType);
                    addElement(paramDocElement, "contactEmail", contactEmail);
                    addElement(paramDocElement, "contactName", contactName);
                    addElement(paramDocElement, "budget", budget);

                    logDebug("Budget: " + budget);
                    logDebug("Case Update: " + parameterDoc.OuterXml);

                    XmlElement responseElement = null;
                    NAVConnection.performService(database.configuration, "wrike_updateCase", parameterDoc, out responseElement);
                    logDebug("Updated successfully");

                }


                i++;
            }
        }

        private void updateCasePlan(string taskId, XmlDocument taskDocument)
        {
            logDebug("Update Case Plan");

            string reasonCode = getCustomFieldValueFromDocument(taskDocument, "Reason Code");
            string caseNo = getCustomFieldValueFromDocument(taskDocument, "Case No.");
            string budget = getCustomFieldValueFromDocument(taskDocument, "Budget (h)");
            string startDate = getNodeValue(taskDocument.DocumentElement, "data/dates/start");
            string endDate = getNodeValue(taskDocument.DocumentElement, "data/dates/due");



            if ((reasonCode != "INGEN") && (caseNo != ""))
            {
                //Update Case Status in NAV.
                XmlDocument parameterDoc = new XmlDocument();
                parameterDoc.LoadXml("<case/>");
                XmlElement paramDocElement = parameterDoc.DocumentElement;
                addElement(paramDocElement, "no", caseNo);
                addElement(paramDocElement, "taskId", taskId);
                addElement(paramDocElement, "budget", budget);
                if (startDate != "")
                {
                    addElement(paramDocElement, "startDate", startDate.Substring(0, 10));
                    addElement(paramDocElement, "endDate", endDate.Substring(0, 10));
                }




                XmlElement resourceElement = addElement(paramDocElement, "assignedResources", "");

                getUsers();

                XmlNodeList resourceNodeList = taskDocument.GetElementsByTagName("responsibleIds");
                foreach (XmlNode resourceNode in resourceNodeList)
                {
                    XmlElement resElement = (XmlElement)resourceNode;

                    if (userTable.ContainsKey(resElement.InnerText))
                    {
                        addElement(resourceElement, "id", userTable[resElement.InnerText]);
                    }

                }


                logDebug("Update doc: " + parameterDoc.OuterXml);

                XmlElement responseElement = null;
                NAVConnection.performService(database.configuration, "wrike_updateCasePlan", parameterDoc, out responseElement);
                logDebug("Updated successfully");

            }
        }

        private string getActivityTypeFromTask(XmlElement taskElement)
        {
            XmlNodeList nodeList = taskElement.SelectNodes("parentIds");

            foreach (XmlNode xmlNode in nodeList)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;
                string folderId = xmlElement.FirstChild.Value;

                XmlDocument folderDocument = getFolderById(folderId);
                string activityType = getCustomFieldValueFromDocument(folderDocument, "Activity Type");
                

                if (activityType != "") return activityType;

            }

            return "";
        }

        private void logDebug(string message)
        {
            try
            {
                if (System.IO.File.Exists("C:\\temp\\wrikehook.log"))
                {
                    StreamWriter streamWriter = new StreamWriter("C:\\temp\\wrikehook.log", true);
                    streamWriter.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + message);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                else
                {
                    StreamWriter streamWriter = new StreamWriter("C:\\temp\\wrikehook.log");
                    streamWriter.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + message);
                    streamWriter.Flush();
                    streamWriter.Close();

                }
            }
            catch (Exception)
            { }

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

        public string DoRequest(string url, string secret, string postJson, string method)
        {
            url = "https://app-eu.wrike.com/api/v4/" + url;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebRequest webRequest = WebRequest.Create(url);

            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", "bearer " + secret);
            webRequest.Method = method;


            if (postJson != "")
            {
                StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
                streamWriter.Write(postJson);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                WebResponse webResponse = webRequest.GetResponse();


                StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
                string responseJson = streamReader.ReadToEnd();
                streamReader.Close();
                return responseJson;
            }
            catch (WebException e)
            {
                HttpWebResponse response = (HttpWebResponse)e.Response;
                return "Status: " + (int)response.StatusCode;
            }

            return "";
        }

        private void getCustomFields()
        {
            if (customFieldsTable != null) return;
            customFieldsTable = new Dictionary<string, string>();


            string jsonData = DoRequest("customfields", secret, "", "GET");

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonData, "Wrike");

            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("Wrike/data");

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;
                customFieldsTable.Add(getNodeValue(xmlElement, "id"), getNodeValue(xmlElement, "title"));
            }

        }

        private void getUsers()
        {
            if (userTable != null) return;
            userTable = new Dictionary<string, string>();

            List<Navipro.Backoffice.Web.Models.CaseMember> resourceList = Navipro.Backoffice.Web.Models.CaseMember.getResources(database);

            string jsonData = DoRequest("contacts", secret, "", "GET");

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonData, "Wrike");

            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("Wrike/data");

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;

                foreach (CaseMember caseMember in resourceList)
                {
                    string fullName = getNodeValue(xmlElement, "firstName") + " " + getNodeValue(xmlElement, "lastName");
                    if (caseMember.name == fullName)
                    {
                        if (!userTable.ContainsKey(getNodeValue(xmlElement, "id")))
                        {
                            userTable.Add(getNodeValue(xmlElement, "id"), caseMember.resourceNo);
                        }
                    }
                }
            }

        }

        private string getResourceIdFromUserTable(string resourceNo)
        {
            foreach (string id in userTable.Keys)
            {
                if (userTable[id] == resourceNo) return id;
            }
            return "";
        }

        private XmlDocument getTaskById(string taskId)
        {
            string jsonData = DoRequest("tasks/"+taskId, secret, "", "GET");

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonData, "Wrike");

            return xmlDoc;
        }

        private XmlDocument getFolderTasks(string folderId)
        {
            string jsonData = DoRequest("folders/" + folderId+ "/tasks?fields=[\"description\",\"parentIds\",\"responsibleIds\",\"customFields\"]&descendants=true", secret, "", "GET");

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonData, "Wrike");

            return xmlDoc;
        }

        private XmlDocument getFolderById(string folderId)
        {
            string jsonData = DoRequest("folders/" + folderId, secret, "", "GET");
            //logDebug("Folder data: " + jsonData);

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonData, "Wrike");

            return xmlDoc;
        }

        public string getFolderIdByJobNo(string jobNo)
        {
            getCustomFields();

            string jobNoId = getCustomFieldId("Job No.");

            string jsonData = DoRequest("folders?descendants=true&customField={\"id\":\""+jobNoId+"\",\"value\":\""+jobNo+"\"}&fields=[\"customFields\"]", secret, "", "GET");

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonData, "Wrike");

            return getNodeValue(xmlDoc.DocumentElement, "data/id");
        }

        public string createTaskInFolder(string folderId, string caseNo, string title, string description, string resourceName, string zendeskId)
        {
            getCustomFields();
            getUsers();


            title = title.Replace("&", "");

            string responsibles = "";
            if (resourceName != "")
            {
                responsibles = "&responsibles=[\"" + getUserId(resourceName) + "\"]";
            }

            string jsonData = DoRequest("folders/" + folderId + "/tasks?title=" + caseNo + " - " + title + "&description="+description+"&customFields=[{\"id\":\"" + getCustomFieldId("Case No.") + "\", \"value\":\"" + caseNo + "\"},{\"id\":\"" + getCustomFieldId("Zendesk ID") + "\", \"value\":\"" + zendeskId + "\"}]" + responsibles, secret, "", "POST");

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonData, "Wrike");

            return getNodeValue(xmlDoc.DocumentElement, "data/id");

        }

        public void createTaskComment(string taskId, string comment)
        {
            string jsonData = DoRequest("tasks/" + taskId + "/comments?text=" + System.Uri.EscapeDataString(comment) + "&plainText=true", secret, "", "POST");

        }

        public bool createSubTask(string parentTaskId, string comment)
        {
            logDebug("Create subtask: " + comment);
            if (comment.Length > 10)
            {
                string jsonData = DoRequest("tasks?title=" + System.Uri.EscapeDataString("SUB: "+comment.Substring(9)) + "&superTasks=[\"" + parentTaskId + "\"]", secret, "", "POST");
            }

            return true;
        }

        public bool deleteSubTask(string taskId)
        {
            XmlDocument taskDocument = getTaskById(taskId);
            string description = getNodeValue(taskDocument.DocumentElement, "data/title");

            if (description.Substring(0, 4) == "SUB:")
            {
                string jsonData = DoRequest("tasks/"+taskId, secret, "", "DELETE");
            }

            return true;
        }

        private void updateTaskSetDescription(string taskId, string description)
        {

            string jsonData = DoRequest("tasks/" + taskId+"?description="+ System.Uri.EscapeDataString(description), secret, "", "PUT");

        }

        private void updateTaskAssignResource(string taskId, string resourceId)
        {

            string jsonData = DoRequest("tasks/" + taskId + "?addResponsibles=[" + resourceId+"]", secret, "", "PUT");

        }

        public XmlDocument getTaskByCustomFields(string customField, string customValue)
        {
            getCustomFields();

            string jsonData = DoRequest("tasks?descendants=false&customField={\"id\":\""+getCustomFieldId(customField)+"\",\"value\":\""+customValue+"\"}&fields=[\"description\",\"customFields\"]", secret, "", "GET");

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonData, "Wrike");

            return xmlDoc;
        }

        private string getCustomFieldId(string fieldName)
        {
            foreach (string id in customFieldsTable.Keys)
            {
                if (customFieldsTable[id] == fieldName) return id;
            }

            return "";
        }

        private string getUserId(string userName)
        {
            foreach (string id in userTable.Keys)
            {
                if (userTable[id] == userName) return id;
            }

            return "";
        }


        private string getCustomFieldValueFromDocument(XmlDocument document, string customField)
        {

            getCustomFields();
            Dictionary<string, string> taskCustomFields = new Dictionary<string, string>();


            XmlNodeList nodeList = document.SelectNodes("Wrike/data/customFields");

            foreach (XmlNode xmlNode in nodeList)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;
                if (customFieldsTable.ContainsKey(getNodeValue(xmlElement, "id")))
                {
                    if (customFieldsTable[getNodeValue(xmlElement, "id")] == customField) return getNodeValue(xmlElement, "value");
                }
            }

            return "";

        }

        private string getCustomFieldValueFromElement(XmlElement xmlDocElement, string customField)
        {

            getCustomFields();
            Dictionary<string, string> taskCustomFields = new Dictionary<string, string>();


            XmlNodeList nodeList = xmlDocElement.SelectNodes("customFields");

            foreach (XmlNode xmlNode in nodeList)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;
                if (customFieldsTable.ContainsKey(getNodeValue(xmlElement, "id")))
                {
                    if (customFieldsTable[getNodeValue(xmlElement, "id")] == customField) return getNodeValue(xmlElement, "value");
                }
            }

            return "";

        }

        private bool taskIsRequestCase(XmlDocument taskDocument)
        {
            logDebug("Checking request case...");

            getCustomFields();
            Dictionary<string, string> taskCustomFields = new Dictionary<string, string>();

            logDebug("Task document: " + taskDocument.OuterXml);


            XmlNodeList nodeList = taskDocument.SelectNodes("Wrike/data/customFields");

            foreach(XmlNode xmlNode in nodeList)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;
                if (customFieldsTable.ContainsKey(getNodeValue(xmlElement, "id")))
                {
                    taskCustomFields.Add(customFieldsTable[getNodeValue(xmlElement, "id")], getNodeValue(xmlElement, "value"));
                    logDebug("Field: " + customFieldsTable[getNodeValue(xmlElement, "id")] + ", value: " + getNodeValue(xmlElement, "value"));
                }
            }

            //Conditions
            if (!taskCustomFields.ContainsKey("Description")) return false;
            if (!taskCustomFields.ContainsKey("Customer Name")) return false;

            if (taskCustomFields["Description"] == "") return false;
            if (taskCustomFields["Customer Name"] == "") return false;
            
           
            return true;


        }

        private string findJobFolder(XmlDocument taskDocument)
        {
            logDebug("Checking parent folder...");

            getCustomFields();
            Dictionary<string, string> taskCustomFields = new Dictionary<string, string>();


            XmlNodeList nodeList = taskDocument.SelectNodes("Wrike/data/parentIds");

            foreach (XmlNode xmlNode in nodeList)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;
                string folderId = xmlElement.FirstChild.Value;

                XmlDocument folderDocument = getFolderById(folderId);
                string jobNo = getCustomFieldValueFromDocument(folderDocument, "Job No.");
                logDebug("Parent ID: " + folderId + ", job no: " + jobNo);

                if (jobNo != "") return jobNo;

            }


            return "";
        }


        private void convertTaskWithTemplate(XmlDocument taskDocument)
        {
            getCustomFields();

            XmlDocument templateDocument = getTaskByCustomFields("Reason Code", "REQUESTTEMPLATE");
            if (templateDocument == null) return;

            string descriptionText = getNodeValue(templateDocument.DocumentElement, "data/description");

            logDebug("Template: " + descriptionText);

            XmlNodeList nodeList = taskDocument.SelectNodes("Wrike/data/customFields");

            foreach (XmlNode xmlNode in nodeList)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;
                descriptionText = descriptionText.Replace("[#" + customFieldsTable[getNodeValue(xmlElement, "id")] + "]", getNodeValue(xmlElement, "value"));
            }

            updateTaskSetDescription(getNodeValue(taskDocument.DocumentElement, "data/id"), descriptionText);

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

        private void updateWebHooks()
        {
            string jsonData = DoRequest("webhooks", secret, "", "GET");

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonData, "Wrike");

            XmlNodeList nodeList = xmlDoc.SelectNodes("Wrike/data");

            foreach (XmlNode xmlNode in nodeList)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;
                string id = getNodeValue(xmlElement, "id");
                logDebug("Deleting webhook id: " + id);
                DoRequest("webhooks/"+id, secret, "", "DELETE");
            }

            DoRequest("webhooks?hookUrl=http://hd.navipro.se/wrikehook", secret, "", "POST");
        }

        private string convertStatus(string status)
        {
            if (status.ToUpper() == "NEW") return "NY";
            if (status.ToUpper() == "PROPOSAL") return "OFFERT";
            if (status.ToUpper() == "READY FOR START") return "PÅGÅR";
            if (status.ToUpper() == "IN PROGRESS") return "PÅGÅR";
            if (status.ToUpper() == "READY FOR TEST") return "TESTKLAR";
            if (status.ToUpper() == "READY FOR DEPLOY") return "TESTKLAR";
            if (status.ToUpper() == "APPROVED") return "GODKÄND";
            if (status.ToUpper() == "COMPLETED") return "GODKÄND";
            if (status.ToUpper() == "CANCELLED") return "ÅTER";
            if (status.ToUpper() == "PAUSED") return "PARKERAD";
            return status;

        }
    }
}