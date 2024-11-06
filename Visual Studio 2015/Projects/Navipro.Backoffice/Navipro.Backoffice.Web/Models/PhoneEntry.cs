using Navipro.Backoffice.Web.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Globalization;

namespace Navipro.Backoffice.Web.Models
{
    public class PhoneEntry
    {
        [Display(Name = "Datum och klockslag")]
        public DateTime alertDateTime { get; set; }

        public string alertDateTimeText { get { if (alertDateTime.Year > 2001) return alertDateTime.ToString("yyyy-MM-dd HH:mm:ss"); return ""; } }

        [Display(Name = "Svarat")]
        public DateTime answeredDateTime { get; set; }

        public string answeredTimeText { get { if (answeredDateTime.Year > 2001) return answeredDateTime.ToString("HH:mm:ss"); return ""; } }

        [Display(Name = "Avslutat")]
        public DateTime hangupDateTime { get; set; }
        public string hangupTimeText { get { if (hangupDateTime.Year > 2001) return hangupDateTime.ToString("HH:mm:ss"); return ""; } }

        [Display(Name = "Samtalstyp")]
        public int direction { get; set; }

        [Display(Name = "Avsändare")]
        public string fromUri { get; set; }
        public string answeredByUri { get; set; }
        public string answeredByName { get; set; }

        public int status { get; set; }

        [Display(Name = "Status")]
        public string statusText
        {
            get
            {
                if (status == 1) return "Missat";
                if (status == 2) return "Hanterat";
                return "";
            }
        }


        [Display(Name = "Kundnr")]    
        public string customerNo { get; set; }

        [Display(Name = "Kund")]
        public string customerName { get; set; }

        [Display(Name = "Namn")]
        public string fromName { get; set; }

        [Display(Name = "E-post")]
        public string fromEmail { get; set; }

        [Display(Name = "Svarat av")]
        public string answeredBy { get { if ((answeredByName != "") && (answeredByName != null)) return answeredByName; return answeredByUri; } }

        public string icon
        {
            get
            {
                if (status == 1) return "label-danger";
                if (status == 2) return "label-success";
                return "";
            }
        }

        public string responseGroupUri { get; set; }

        public PhoneEntry()
        { }


        public PhoneEntry(SqlDataReader dataReader)
        {
            fromUri = dataReader.GetValue(0).ToString();
            if (!dataReader.IsDBNull(2)) alertDateTime = dataReader.GetDateTime(2);
            if (!dataReader.IsDBNull(3)) answeredByUri = dataReader.GetValue(3).ToString();
            if (!dataReader.IsDBNull(4)) answeredDateTime = dataReader.GetDateTime(4);
            if (!dataReader.IsDBNull(5)) hangupDateTime = dataReader.GetDateTime(5);
            fromName = "";
            fromEmail = "";
            customerName = "";
            customerNo = "";

            CultureInfo ci = new CultureInfo("sv-SE");
            string alertDateTimeLocalString = alertDateTime.ToString("R", ci);
            DateTime convertedDateTime = DateTime.Parse(alertDateTimeLocalString);
            alertDateTime = TimeZone.CurrentTimeZone.ToLocalTime(convertedDateTime);
        }


        public XmlDocument toDOM()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><phoneLog/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            NAVConnection.addElement(docElement, "alertDateTime", alertDateTime.ToString("yyyy-MM-dd HH:mm:ss"), "");
            NAVConnection.addElement(docElement, "fromUri", fromUri, "");
            NAVConnection.addElement(docElement, "responseGroupUri", responseGroupUri, "");
            NAVConnection.addElement(docElement, "handledBy", answeredByUri, "");

            return xmlDoc;
        }


        public void submitToggleHandled(Database database)
        {

            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "phoneLogToggleHandled", toDOM(), out responseElement);

        }

        public static List<PhoneEntry> getPhoneCalls(Database database, Database cdrDatabase, string responseGroupUri, DateTime fromDate, DateTime toDate)
        {
            List<PhoneEntry> phoneEntryList = new List<PhoneEntry>();
            List<PhoneEntry> handledCallsList = getHandledCalls(database, responseGroupUri, fromDate, toDate);
            //List<PhoneEntry> handledCallsList = new List<PhoneEntry>();

            Dictionary<string, User> userTable = User.getUsersWithSipAddress(database);

            string query = "SELECT fromNumber.PhoneUri as fromNumber, referres.UserUri as responseGroup, s.InviteTime, agentUser.UserUri as agentUser, agent.ResponseTime, agent.SessionEndTime as hangupTime, s.SessionEndTime as responseGroupEndTime FROM VoipDetails v, Phones fromNumber, Users referres, SessionDetails s LEFT JOIN SessionDetails agent ON agent.ReplacesDialogIdTime = s.SessionIdTime AND agent.ReplacesDialogIdSeq = s.SessionIdSeq LEFT JOIN Users agentUser ON agent.User1Id = agentUser.UserId WHERE s.SessionIdTime = v.SessionIdTime AND s.SessionIdSeq = v.SessionIdSeq AND v.ToGatewayId IS NULL AND fromNumber.PhoneId = v.FromNumberId AND s.User2Id = referres.UserId AND referres.UserUri = @responseGroupUri AND (agent.User1Id IS NOT NULL OR s.SessionEndTime IS NOT NULL) AND s.InviteTime >= @fromDate AND s.InviteTime <= @toDate ORDER BY s.InviteTime DESC";

            DatabaseQuery databaseQuery = cdrDatabase.prepare(query);
            databaseQuery.addStringParameter("responseGroupUri", responseGroupUri, 30);
            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", toDate.AddDays(1));

            System.Data.SqlClient.SqlDataReader dataReader = databaseQuery.executeQuery();

            while (dataReader.Read())
            {
                PhoneEntry phoneEntry = new PhoneEntry(dataReader);
                phoneEntry.responseGroupUri = responseGroupUri;
                if (phoneEntry.answeredByUri != null)
                {
                    if (userTable.ContainsKey(phoneEntry.answeredByUri))
                    {
                        phoneEntry.answeredByName = userTable[phoneEntry.answeredByUri].name;
                    }
                }
                phoneEntryList.Add(phoneEntry);

            }

            dataReader.Close();

            Dictionary<string, Customer> customerTable = Customer.getDictionary(database);

            int i = 0;
            while (i < phoneEntryList.Count)
            {
                
                CaseMember caseMember = CaseMember.getEntryByPhoneNo(database, phoneEntryList[i].fromUri);
                if (caseMember != null)
                {
                    phoneEntryList[i].fromName = caseMember.name;
                    phoneEntryList[i].fromEmail = caseMember.email;
                    if (caseMember.customerNo != "")
                    {
                        phoneEntryList[i].customerNo = caseMember.customerNo;
                        Customer customer = customerTable[caseMember.customerNo];
                        phoneEntryList[i].customerName = customer.name;
                    }
                }

                if (phoneEntryList[i].answeredTimeText == "") phoneEntryList[i].status = 1;

                foreach (PhoneEntry handledCall in handledCallsList)
                {
                    //throw new Exception(handledCall.alertDateTime.ToString("yyyy-MM-dd HH:mm:ss") + " <-> " + phoneEntryList[i].alertDateTimeText + ", " + handledCall.fromUri + " <-> " + phoneEntryList[i].fromUri + ", " + handledCall.responseGroupUri + " <-> " + phoneEntryList[i].responseGroupUri);
                    if ( (handledCall.alertDateTime.ToString("yyyy-MM-dd HH:mm:ss") == phoneEntryList[i].alertDateTimeText) && (handledCall.fromUri == phoneEntryList[i].fromUri) && (handledCall.responseGroupUri == phoneEntryList[i].responseGroupUri))
                    {
                        phoneEntryList[i].status = 2;
                        phoneEntryList[i].answeredByUri = handledCall.answeredByUri;

                        if (userTable.ContainsKey(handledCall.answeredByUri))
                        {
                            phoneEntryList[i].answeredByName = userTable[handledCall.answeredByUri].name;
                        }
                    }
                }

                i++;
            }

            return phoneEntryList;
        }

        private static List<PhoneEntry> getHandledCalls(Database database, string responseGroupUri, DateTime fromDate, DateTime toDate)
        {
            List<PhoneEntry> phoneEntryList = new List<PhoneEntry>();            

            DatabaseQuery databaseQuery = database.prepare("SELECT [Alert Date], [Alert Time], [From Uri], [Response Group Uri], [Handled By] FROM [" + database.getTableName("Phone Log Handled Entry")+"] WHERE [Response Group Uri] = @responseGroupUri AND [Alert Date] >= @fromDate AND [Alert Date] < @toDate");
            databaseQuery.addStringParameter("responseGroupUri", responseGroupUri, 100);
            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", toDate.AddDays(1));

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                PhoneEntry phoneEntry = new PhoneEntry();
                phoneEntry.alertDateTime = DateTime.Parse(dataReader.GetDateTime(0).ToString("yyyy-MM-dd")+" "+ dataReader.GetDateTime(1).ToString("HH:mm:ss"));
                phoneEntry.fromUri = dataReader.GetValue(2).ToString();
                phoneEntry.responseGroupUri = dataReader.GetValue(3).ToString();
                phoneEntry.answeredByUri = dataReader.GetValue(4).ToString();

                if (phoneEntry.fromUri[0] == ' ') phoneEntry.fromUri = "+" + phoneEntry.fromUri.Substring(1);

                phoneEntryList.Add(phoneEntry);
            }

            dataReader.Close();
            return phoneEntryList;
        }

        public static int countMissedCalls(Database database, Database cdrDatabase, string responseGroupUri, DateTime fromDate, DateTime toDate)
        {
            List<PhoneEntry> phoneLogList = getPhoneCalls(database, cdrDatabase, responseGroupUri, fromDate, toDate);

            int count = 0;
            foreach (PhoneEntry phoneEntry in phoneLogList)
            {
                if (phoneEntry.status == 1) count++;
            }

            return count;
        }
    }
}