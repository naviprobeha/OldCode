using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Lib;
using System.Data.SqlClient;
using System.Xml;
using System.Web.Mvc;
using System.Globalization;

namespace Navipro.Backoffice.Web.Models
{
    public class Case
    {
        private List<CaseLine> _caseLineList;

        public Case()
        {
        }

        public Case(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            no = dataReader.GetValue(0).ToString();
            caseTypeCode = dataReader.GetValue(1).ToString();

            ordererEmail = dataReader.GetValue(3).ToString();
            ordererName = dataReader.GetValue(4).ToString();
            authorEmail = dataReader.GetValue(5).ToString();
            authorName = dataReader.GetValue(6).ToString();
            activityTypeCode = dataReader.GetValue(7).ToString();

            commisionTypeCode = dataReader.GetValue(8).ToString();
            caseStatusCode = dataReader.GetValue(9).ToString();
            jobNo = dataReader.GetValue(10).ToString();
            jobDescription = dataReader.GetValue(11).ToString();
            customerNo = dataReader.GetValue(12).ToString();
            customerName = dataReader.GetValue(13).ToString();
            priorityCode = dataReader.GetValue(14).ToString();
            reasonCode = dataReader.GetValue(15).ToString();
            responsibleResource = dataReader.GetValue(16).ToString();
            assignedResources = dataReader.GetValue(17).ToString();
            receivedDate = dataReader.GetDateTime(18);
            assignedDate = dataReader.GetDateTime(19);
            estimatedEndingDate = dataReader.GetDateTime(20);
            estimatedTestDate = dataReader.GetDateTime(21);
            actualEndingDate = dataReader.GetDateTime(22);
            lastActivityDateTime = dataReader.GetDateTime(23);
            caseOrigin = int.Parse(dataReader.GetValue(24).ToString());
            messageId = dataReader.GetValue(25).ToString();

            subject = dataReader.GetValue(26).ToString();
            if (subject == "") subject = dataReader.GetValue(2).ToString();


            activityCommisionCode = commisionTypeCode + "|" + activityTypeCode;
            assignedResourcesList = assignedResources.Split(new string[1] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            lastActivity = dataReader.GetInt32(27);

            CultureInfo ci = new CultureInfo("sv-SE");
            string lastActivityDateTimeLocalString = lastActivityDateTime.ToString("R", ci);
            DateTime convertedDateTime = DateTime.Parse(lastActivityDateTimeLocalString);
            lastActivityDateTime = TimeZone.CurrentTimeZone.ToLocalTime(convertedDateTime);

        }

        [Required]
        [Display(Name = "Nr")]
        public String no { get; set; }

        [Required]
        [Display(Name = "Typ")]
        public String caseTypeCode { get; set; }

        [Display(Name = "Rubrik")]
        public String subject { get; set; }

        [Display(Name = "E-post beställare")]
        public String ordererEmail { get; set; }

        [Display(Name = "Beställare")]
        public String ordererName { get; set; }

        [Display(Name = "E-post författare")]
        public String authorEmail { get; set; }

        [Display(Name = "Författare")]
        public String authorName { get; set; }

        [Display(Name = "Aktivitetstyp")]
        public String activityTypeCode { get; set; }

        [Display(Name = "Uppdragstyp")]
        public String commisionTypeCode { get; set; }

        [Display(Name = "Aktivitetstyp")]
        public String activityCommisionCode { get; set; }

        [Display(Name = "Status")]
        public String caseStatusCode { get; set; }

        [Display(Name = "Projektnr")]
        public String jobNo { get; set; }

        [Display(Name = "Projektnamn")]
        public String jobDescription { get; set; }

        [Display(Name = "Kundnr")]
        public String customerNo { get; set; }

        [Display(Name = "Kundnamn")]
        public String customerName { get; set; }

        [Display(Name = "Prioritet")]
        public String priorityCode { get; set; }

        [Display(Name = "Uppföljningskod")]
        public String reasonCode { get; set; }

        [Display(Name = "Ansvarig resurs")]
        public String responsibleResource { get; set; }

        [Display(Name = "Tilldelade resurser")]
        public String assignedResources { get; set; }

        [Display(Name = "Tilldelade resurser")]
        public String[] assignedResourcesList { get; set; }

        [Display(Name = "Mottaget")]
        public DateTime receivedDate { get; set; }

        [Display(Name = "Tilldelat")]
        public DateTime assignedDate { get; set; }

        [Display(Name = "Uppskattat leveransdatum")]
        public DateTime estimatedEndingDate { get; set; }

        [Display(Name = "Faktiskt slutdatum")]
        public DateTime actualEndingDate { get; set; }

        [Display(Name = "Uppskattat datum för test")]
        public DateTime estimatedTestDate { get; set; }

        [Display(Name = "Projekt")]
        public string jobPresentation { get { return customerName + " - " + jobDescription; } }

        [Display(Name = "Mottaget")]
        public string receivedDateText { get { return receivedDate.ToString("yyyy-MM-dd"); } set { receivedDate = DateTime.Parse(value.Substring(0, 10)); } }

        [Display(Name = "Senaste aktivitet")]
        public DateTime lastActivityDateTime { get; set; }

        [Display(Name = "Senaste aktivitet")]
        public string lastActivityDateTimeText { get { if (lastActivityDateTime.Year > 1754) return lastActivityDateTime.ToString("yyyy-MM-dd HH:mm"); return ""; } set { lastActivityDateTime = DateTime.Parse(value.Substring(0, 10)); } }

        public bool validated { get { if (customerNo == "") return false; if (jobNo == "") return false; return true; } }

        
        [Display(Name = "Uppskattat")]
        public string estimatedEndingDateText
        {
            get
            {
                if (estimatedTestDate.Year > 1753) return estimatedTestDate.ToString("yyyy-MM-dd");
                if (estimatedEndingDate.Year > 1753) return estimatedEndingDate.ToString("yyyy-MM-dd");
                return "";
            }
            set { estimatedEndingDate = DateTime.Parse(value.Substring(0, 10)); }
        }

        [Display(Name = "Beskrivning")]
        [AllowHtml]
        public string description { get; set; }


        [Display(Name = "Kommentar")]
        [AllowHtml]
        public string newComment { get; set; }

        public string mode { get; set; }

        public int caseOrigin { get; set; }

        public string messageId { get; set; }

        public int lastActivity { get; set; }

        public string shortSubject { get { if (subject.Length > 20) return subject.Substring(0, 20)+"..."; return subject; } }
        public string icon
        {
            get
            {
                if (caseOrigin == 0) return "fa-pencil";
                if (caseOrigin == 1) return "fa-phone";
                if (caseOrigin == 2) return "fa-envelope";
                if (caseOrigin == 3) return "fa-desktop";
                return "fa-pencil";
            }
        }

        public string lastActivityIcon
        {
            get
            {
                if (lastActivity == 0) return "fa-pencil";
                if (lastActivity == 1) return "fa-envelope";
                if (lastActivity == 2) return "fa-envelope-o";
                return "fa-pencil";
            }
        }

        public List<CaseLine> caseLineList { get { return _caseLineList; } }


        public static List<Case> getList(Database database, string jobNoFilter, string statusFilter, string yearFilter)
        {
            return getList(database, jobNoFilter, statusFilter, yearFilter, null);
        }


        public static List<Case> getList(Database database, string jobNoFilter, string statusFilter, string yearFilter, DataView dataView)
        {
            List<Case> caseList = new List<Case>();

            string filterString = "";
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today;

            if (yearFilter != "")
            {
                startDate = DateTime.Parse(yearFilter + "-01-01");
                endDate = DateTime.Parse(yearFilter + "-12-31");

                filterString = "AND ([Received Date] >= @startDateFilter AND [Received Date] <= @endDateFilter) ";
            }

            if (jobNoFilter != "") filterString = filterString + "AND [Job No_] = @jobNoFilter ";
            if (statusFilter != "") filterString = filterString + "AND [Case Status Code] = @statusFilter ";

            string noOfRecords = "";
            string orderByString = "[Received Date] DESC";
            if (dataView != null)
            {
                if (dataView.query != "") filterString = filterString + " AND " + dataView.query;

                if (dataView.noOfRecords > 0) noOfRecords = "TOP " + dataView.noOfRecords;
                if (dataView.orderBy != "") orderByString = dataView.orderBy;
            }

            

            try
            {
                DatabaseQuery query = database.prepare("SELECT "+noOfRecords+ " [No_], [Case Type Code], [Subject], [Orderer E-mail], [Orderer Name], [Author E-mail], [Author Name], [Activity Type Code], [Commision Type Code], [Case Status Code], [Job No_], [Job Description], [Customer No_], [Customer Name], [Priority Code], [Reason Code], [Responsible Resource], [Assigned Resources], [Received Date], [Assigned Date], [Estimated Ending Date], [Estimated Test Date], [Actual Ending Date], [Last Activity Date Time], [Case Origin], [Message-ID], [Full Subject], [Last Activity Part] FROM [" + database.getTableName("Case") + "] WITH (NOLOCK) WHERE 1=1 " + filterString + " ORDER BY "+orderByString);

                query.addStringParameter("jobNoFilter", jobNoFilter, 20);
                query.addStringParameter("statusFilter", statusFilter, 20);
                query.addDateTimeParameter("startDateFilter", startDate);
                query.addDateTimeParameter("endDateFilter", endDate);

                SqlDataReader dataReader = query.executeQuery();
                while (dataReader.Read())
                {
                    Case caseItem = new Case(dataReader);
                    caseList.Add(caseItem);

                }

                dataReader.Close();
            }
            catch (Exception e)
            {
                Case caseItem = new Case();
                caseItem.subject = e.Message;
                caseList.Add(caseItem);

            }
            return caseList;
        }

        public static int countView(Database database, DataView dataView)
        {
            string filterString = "";
            if (dataView != null) filterString = dataView.query;

            if (filterString == "") filterString = "1=1";
            int count = 0;

            DatabaseQuery query = database.prepare("SELECT COUNT(*) FROM [" + database.getTableName("Case") + "] WHERE " + filterString);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                count = int.Parse(dataReader.GetValue(0).ToString());


            }

            dataReader.Close();


            return count;
        }

        public static List<ProfileChartEntry> getChartCount(Database database, DataView dataView, List<ProfileChartEntry> profileChartEntryList, DateTime fromDate, DateTime toDate)
        {
            string filterString = "";
            if (dataView != null) filterString = dataView.query;

            if (filterString == "") filterString = "1=1";

            //throw new Exception("SELECT " + dataView.select + " FROM [" + database.getTableName("Case") + "] WHERE " + filterString + " " + dataView.groupBy);

            DatabaseQuery query = database.prepare("SELECT "+dataView.select+" FROM [" + database.getTableName("Case") + "] WHERE " + filterString+" "+dataView.groupBy);
            query.addDateTimeParameter("fromDate", fromDate);
            query.addDateTimeParameter("toDate", toDate);

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                DateTime dateTime = dataReader.GetDateTime(0);
                int count = int.Parse(dataReader.GetValue(1).ToString());

                foreach (ProfileChartEntry profileChartEntry in profileChartEntryList)
                {
                    if ((profileChartEntry.fromDate <= dateTime) && (profileChartEntry.toDate > dateTime))
                    {
                        profileChartEntry.value = profileChartEntry.value + count;
                    }

                }
            }

            dataReader.Close();


            return profileChartEntryList;
        }
        public static Case getEntry(Database database, string caseNo)
        {
            Case caseItem = null;

            DatabaseQuery query = database.prepare("SELECT [No_], [Case Type Code], [Subject], [Orderer E-mail], [Orderer Name], [Author E-mail], [Author Name], [Activity Type Code], [Commision Type Code], [Case Status Code], [Job No_], [Job Description], [Customer No_], [Customer Name], [Priority Code], [Reason Code], [Responsible Resource], [Assigned Resources], [Received Date], [Assigned Date], [Estimated Ending Date], [Estimated Test Date], [Actual Ending Date], [Last Activity Date Time], [Case Origin], [Message-ID], [Full Subject], [Last Activity Part] FROM [" + database.getTableName("Case") + "] WITH (NOLOCK) WHERE [No_] = @caseNo");

            query.addStringParameter("caseNo", caseNo, 20);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                caseItem = new Case(dataReader);                
            }

            dataReader.Close();
            
            if (caseItem != null)
            {
                caseItem.loadLineList(database);
                caseItem.loadDescription(database);
            }
            return caseItem;
        }

        protected void loadLineList(Database database)
        {
            _caseLineList = CaseLine.getCaseLines(database, no);
            if (_caseLineList.Count > 0)
            {
                _caseLineList[0].border = "border: 2px solid red;";

            }
        }

        protected void loadDescription(Database database)
        {
            DatabaseQuery query = database.prepare("SELECT [External Comments] FROM [" + database.getTableName("Case Log") + "] WITH (NOLOCK) WHERE [Case No_] = @caseNo AND [Type] = 'DESC'");
            query.addStringParameter("caseNo", no, 20);

            object resultObj = query.executeScalar();
            if (resultObj != System.DBNull.Value)
            {
                byte[] byteArray = (byte[])resultObj;
                if (byteArray != null)
                {
                    this.description = Navipro.Backoffice.Lib.HtmlSanitizer.SanitizeHtml(System.Net.WebUtility.HtmlDecode(System.Text.Encoding.Default.GetString(byteArray)), null);
                    this.description = HttpUtility.HtmlDecode(this.description);
                }
            }
            

        }

 
        public XmlDocument toDOM()
        {
            string[] codes = activityCommisionCode.Split('|');
            commisionTypeCode = codes[0];
            activityTypeCode = codes[1];


            assignedResources = "";
            if (assignedResourcesList != null)
            {
                foreach (string resource in assignedResourcesList)
                {
                    if (assignedResources != "") assignedResources = assignedResources + "|";
                    assignedResources = assignedResources + resource;
 
                }
            }

            

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><case/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            NAVConnection.addElement(docElement, "no", no, "");
            NAVConnection.addElement(docElement, "caseTypeCode", caseTypeCode, "");
            NAVConnection.addElement(docElement, "subject", subject, "");
            NAVConnection.addElement(docElement, "ordererEmail", ordererEmail, "");
            NAVConnection.addElement(docElement, "ordererName", ordererName, "");
            NAVConnection.addElement(docElement, "authorEmail", authorEmail, "");
            NAVConnection.addElement(docElement, "authorName", authorName, "");
            NAVConnection.addElement(docElement, "activityTypeCode", activityTypeCode, "");
            NAVConnection.addElement(docElement, "commisionTypeCode", commisionTypeCode, "");
            NAVConnection.addElement(docElement, "caseStatusCode", caseStatusCode, "");
            NAVConnection.addElement(docElement, "jobNo", jobNo, "");
            NAVConnection.addElement(docElement, "jobDescription", jobDescription, "");
            NAVConnection.addElement(docElement, "customerNo", customerNo, "");
            NAVConnection.addElement(docElement, "customerName", customerName, "");
            NAVConnection.addElement(docElement, "priorityCode", priorityCode, "");
            NAVConnection.addElement(docElement, "reasonCode", reasonCode, "");
            NAVConnection.addElement(docElement, "responsibleResource", responsibleResource, "");
            NAVConnection.addElement(docElement, "assignedResources", assignedResources, "");
            NAVConnection.addElement(docElement, "receivedDate", receivedDate.ToString("yyyy-MM-dd"), "");
            NAVConnection.addElement(docElement, "estimatedEndingDate", estimatedEndingDate.ToString("yyyy-MM-dd"), "");
            NAVConnection.addElement(docElement, "actualEndingDate", actualEndingDate.ToString("yyyy-MM-dd"), "");
            NAVConnection.addElement(docElement, "caseOrigin", caseOrigin.ToString(), "");

            NAVConnection.addElement(docElement, "description", System.Net.WebUtility.HtmlEncode(description), "");

            

            return xmlDoc;
        }

        public void addAttachment(Database database, string fileName, string fromName, string fromEmail, string attachmentBase64, string errorMessage)
        {
 
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><caseAttachment/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            NAVConnection.addElement(docElement, "caseNo", no, "");
            NAVConnection.addElement(docElement, "fileName", fileName, "");
            NAVConnection.addElement(docElement, "data", attachmentBase64, "");
            NAVConnection.addElement(docElement, "fromName", fromName, "");
            NAVConnection.addElement(docElement, "fromEmail", fromEmail, "");
            NAVConnection.addElement(docElement, "errorMessage", errorMessage, "");

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "addAttachment", xmlDoc, out responseElement);


        }


        public void submitCreate(Database database, User user)
        {
            this.receivedDate = DateTime.Today;
            this.authorEmail = user.email;
            this.authorName = user.name;

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "createCase", toDOM(), out responseElement);

            this.no = responseElement.GetAttribute("no");
            
        }

        public void submitUpdate(Database database, User user)
        {

            XmlElement responseElement = null;           
            NAVConnection.performService(database.configuration, "updateCase", toDOM(), out responseElement);
        }

        public void submitDelete(Database database, User user)
        {

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "deleteCase", toDOM(), out responseElement);
        }

        public void submitUpdateDescription(Database database, User user)
        {

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "updateCaseDescription", toDOM(), out responseElement);

        }

        public void submitCreateComment(Database database, User user, string comment, string newStatus, bool includeInTimeReport)
        {
            submitCreateComment(database, user, comment, newStatus, DateTime.Now, includeInTimeReport);
        }
        public void submitCreateComment(Database database, User user, string comment, string newStatus, DateTime dateTime, bool includeInTimeReport)
        {
            CaseLine caseLine = new CaseLine();
            caseLine.caseNo = no;
            caseLine.date = dateTime;
            caseLine.time = dateTime;
            caseLine.fromEmail = user.email;
            caseLine.fromName = user.name;
            caseLine.subject = subject;
            caseLine.internalComment = comment;
            caseLine.caseStatusCode = newStatus;
            caseLine.includeInTimeReport = includeInTimeReport;
            //if (caseLine.subject.Length > 250) caseLine.subject = caseLine.subject.Substring(0, 250);

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "createCaseComment", caseLine.toDOM(), out responseElement);

        }


        public void submitCreateReply(Database database, User user, string comment, string[] receiverList, bool sendAsEmail, bool closeCase)
        {
            CaseLine caseLine = new CaseLine();
            caseLine.caseNo = no;
            caseLine.date = DateTime.Today;
            caseLine.time = DateTime.Now;
            caseLine.fromEmail = user.email;
            caseLine.fromName = user.name;
            caseLine.subject = subject;
            caseLine.htmlComment = HttpUtility.HtmlEncode(comment);
            caseLine.sendAsEmail = sendAsEmail;
            caseLine.closeCase = closeCase;
            caseLine.receiverList = receiverList;
            caseLine.caseStatusCode = this.caseStatusCode;
            

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "createCaseReply", caseLine.toDOM(), out responseElement);

            caseLine.lineNo = int.Parse(responseElement.GetAttribute("lineNo"));

            if (sendAsEmail)
            {
                MailSender.SendReply(database, this, caseLine);
            }
        }

        public void deleteComment(Database database, int lineNo)
        {
            CaseLine caseLine = new CaseLine();
            caseLine.caseNo = no;
            caseLine.lineNo = lineNo;
            caseLine.date = DateTime.Today;
            caseLine.time = DateTime.Now;

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "deleteLine", caseLine.toDOM(), out responseElement);

        }

        public void markCommentAsSent(Database database, int lineNo, string messageId)
        {
            CaseLine caseLine = new CaseLine();
            caseLine.caseNo = no;
            caseLine.lineNo = lineNo;
            caseLine.date = DateTime.Today;
            caseLine.time = DateTime.Now;
            caseLine.messageId = messageId;

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "markCommentAsSent", caseLine.toDOM(), out responseElement);

        }
        public void assignResource(Database database, string resourceNo)
        {
            this.responsibleResource = resourceNo;

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "assignRespResource", toDOM(), out responseElement);

        }

        public void changeStatus(Database database, string newCaseStatusCode)
        {
            this.caseStatusCode = newCaseStatusCode;

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "changeStatus", toDOM(), out responseElement);

        }

        public void setJob(Database database, string newJobNo)
        {
            this.jobNo = newJobNo;

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "setJob", toDOM(), out responseElement);

        }
        public void closeCase(Database database, string newCaseStatusCode)
        {
            this.caseStatusCode = newCaseStatusCode;

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "closeCase", toDOM(), out responseElement);

        }

        public static void search(Database database, string searchQuery, ref List<SearchResult> searchResults)
        {
            DatabaseQuery query = database.prepare("SELECT [No_], [Subject] FROM [" + database.getTableName("Case") + "] WITH (NOLOCK) WHERE [Subject] LIKE @searchQuery OR [No_] LIKE @searchQuery OR [Full Subject] LIKE @searchQuery");
            query.addStringParameter("searchQuery", "%" + searchQuery + "%", 100);



            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {                

                SearchResult searchResult = new SearchResult();
                searchResult.caption = "Ärende - " + dataReader.GetValue(0).ToString() + " - " + dataReader.GetValue(1).ToString();
                searchResult.url = "/Case/Details/" + dataReader.GetValue(0).ToString();

                searchResults.Add(searchResult);
            }
            dataReader.Close();

        }

        public static bool checkIfEmailIsExistingCase(Database database, string messageId, out string caseNo)
        {
            caseNo = "";
            if (messageId == "") return false;

            DatabaseQuery query = database.prepare("SELECT [Case No_] FROM [" + database.getTableName("Case Log") + "] WHERE [Message-ID] = @messageId");
            query.addStringParameter("messageId", messageId, 100);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                caseNo = dataReader.GetValue(0).ToString();
                dataReader.Close();
                return true;
            }
            dataReader.Close();

            return false;
        }

        public static bool checkReferences(Database database, string references, out string caseNo)
        {
            caseNo = "";
            if (references == null) return false;

            string[] referenceArray = references.Split(new string[] { " " }, StringSplitOptions.None);
            if (referenceArray == null) return false;

            foreach (string reference in referenceArray)
            {
              
                if (checkIfEmailIsExistingCase(database, reference, out caseNo)) return true;
            }
            return false;
        }

        public void setLastActivity(Database database, int lastActivity)
        {
            XmlElement responseElement = null;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<case><no>" + this.no + "</no><lastActivity>" + lastActivity + "</lastActivity></case>");
            NAVConnection.performService(database.configuration, "setLastActivity", xmlDoc, out responseElement);

        }
    }



}