using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Lib;
using System.Data.SqlClient;
using System.Xml;
using System.Web.Mvc;

namespace Navipro.Backoffice.Web.Models
{
    public class CaseLine
    {
        public CaseLine()
        {


        }

        public CaseLine(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            caseNo = dataReader.GetValue(0).ToString();
            lineNo = dataReader.GetInt32(1);
            date = dataReader.GetDateTime(2);
            time = dataReader.GetDateTime(3);
            type = dataReader.GetValue(4).ToString();
            subject = dataReader.GetValue(5).ToString();
            fromName = dataReader.GetValue(6).ToString();
            fromEmail = dataReader.GetValue(7).ToString();

            if (dataReader.GetValue(8).ToString() == "1") sendAsEmail = true;
            if (dataReader.GetValue(9).ToString() == "1") emailSent = true;
            emailSentDateTime = DateTime.Parse(dataReader.GetDateTime(10).ToString("yyyy-MM-dd") + " " + dataReader.GetDateTime(11).ToString("HH:mm:ss"));

            border = "border: 1px solid #e5e5e5;";
        }

        [Required]
        [Display(Name = "Ärendenr")]
        public String caseNo { get; set; }

        [Required]
        [Display(Name = "Radnr")]
        public int lineNo { get; set; }

        [Display(Name = "Datum")]
        public DateTime date { get; set; }

        [Display(Name = "Klockslag")]
        public DateTime time { get; set; }

        [Display(Name = "Typ")]
        public String type { get; set; }

        [Display(Name = "Rubrik")]
        public String subject { get; set; }

        [Display(Name = "Avsändare")]
        public String fromName { get; set; }

        [Display(Name = "Avsändare e-post")]
        public String fromEmail { get; set; }

        public String externalComment { get; set; }
        public String internalComment { get; set; }

        public String messageId { get; set; }

        [AllowHtml]
        public String htmlComment { get; set; }

        public string[] receiverList { get; set; }
        public string caseStatusCode { get; set; }

        public bool sendAsEmail { get; set; }
        public bool closeCase { get; set; }
        public bool emailSent { get; set; }
        public DateTime emailSentDateTime { get; set; }

        public string emailStatusIcon { get { if ((sendAsEmail) && (!emailSent)) return "label-danger"; if ((sendAsEmail) && (emailSent)) return "label-primary"; return ""; } }
        public string emailStatusText { get { if ((sendAsEmail) && (!emailSent)) return "Ej skickat!"; if ((sendAsEmail) && (emailSent)) return "Skickat "+emailSentDateTime.ToString("yyyy-MM-dd HH:mm"); return ""; } }
        public String whileAgo
        {
            get
            {
                TimeSpan diff = DateTime.Now - dateTime;
                if (diff.Days > 0) return diff.Days + " dagar sedan";
                if (diff.Hours > 0) return diff.Hours + " timmar sedan";
                if (diff.Minutes > 0) return diff.Minutes + " minuter sedan";
                return "";
            }
        }

        public string border { get; set; }

        public bool includeInTimeReport { get; set; }

        public DateTime dateTime
        {
            get
            {
                return DateTime.Parse(date.ToString("yyyy-MM-dd") + " " + time.ToString("HH:mm:ss"));
            }
        }


        public static List<CaseLine> getCaseLines(Database database, string caseNo)
        {
            List<CaseLine> caseLineList = new List<CaseLine>();

            try
            {
                DatabaseQuery query = database.prepare("SELECT [Case No_], [Line No_], [Log Date], [Log Time], [Type], [Subject], [From Name], [From E-mail], [Send As E-mail], [E-mail Sent], [E-mail Sent Date], [E-mail Sent Time] FROM [" + database.getTableName("Case Log") + "] WITH (NOLOCK) WHERE [Case No_] = @caseNo AND [Type] = '' ORDER BY [Log Date] DESC, [Log Time] DESC");
                query.addStringParameter("caseNo", caseNo, 20);

                SqlDataReader dataReader = query.executeQuery();

                while (dataReader.Read())
                {
                    CaseLine caseLine = new CaseLine(dataReader);
                    caseLineList.Add(caseLine);

                }

                dataReader.Close();
            }
            catch (Exception e)
            {
                CaseLine caseLine = new CaseLine();
                caseLine.subject = e.Message;
                caseLineList.Add(caseLine);

            }

            caseLineList = getCaseLineComments(database, caseNo, caseLineList);

            return caseLineList;
        }

        public static List<CaseLine> getCaseLineComments(Database database, string caseNo, List<CaseLine> caseLineList)
        {
            Dictionary<int, string> externalComments = new Dictionary<int, string>();
            Dictionary<int, string> internalComments = new Dictionary<int, string>();


            DatabaseQuery query = database.prepare("SELECT [Case Log Line], [Line No_], [Internal], [Comment] FROM [" + database.getTableName("Case Log Comment") + "] WITH (NOLOCK) WHERE [Case No_] = @caseNo ORDER BY [Case Log Line], [Internal], [Line No_]");
            query.addStringParameter("caseNo", caseNo, 20);

            SqlDataReader dataReader = query.executeQuery();

            while (dataReader.Read())
            {
                if (dataReader.GetValue(2).ToString() == "1")
                {
                    if (!internalComments.ContainsKey(dataReader.GetInt32(0)))
                    {
                        internalComments.Add(dataReader.GetInt32(0), "");
                    }
                    if (internalComments[dataReader.GetInt32(0)] != "") internalComments[dataReader.GetInt32(0)] = internalComments[dataReader.GetInt32(0)] + " ";
                    internalComments[dataReader.GetInt32(0)] = internalComments[dataReader.GetInt32(0)] + dataReader.GetValue(3).ToString();
                }
                else
                {
                    if (!externalComments.ContainsKey(dataReader.GetInt32(0)))
                    {
                        externalComments.Add(dataReader.GetInt32(0), "");
                    }
                    if (externalComments[dataReader.GetInt32(0)] != "") externalComments[dataReader.GetInt32(0)] = externalComments[dataReader.GetInt32(0)] + " ";
                    externalComments[dataReader.GetInt32(0)] = externalComments[dataReader.GetInt32(0)] + dataReader.GetValue(3).ToString();
                }
            }

            dataReader.Close();

            int i = 0;
            while (i < caseLineList.Count)
            {
                if (internalComments.ContainsKey(caseLineList[i].lineNo)) caseLineList[i].internalComment = internalComments[caseLineList[i].lineNo];
                if (externalComments.ContainsKey(caseLineList[i].lineNo)) caseLineList[i].externalComment = externalComments[caseLineList[i].lineNo];

                //checkHtmlComment
                string internalComment = caseLineList[i].getInternalComment(database);
                if (internalComment != null) caseLineList[i].internalComment = caseLineList[i].internalComment + internalComment;

                string externalComment = caseLineList[i].getExternalComment(database);
                if (externalComment != null) caseLineList[i].externalComment = caseLineList[i].externalComment + HttpUtility.HtmlDecode(externalComment);
                i++;
            }

            return caseLineList;
        }

        public XmlDocument toDOM()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><caseLine/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            string receivers = "";
            if (receiverList != null)
            {
                
                foreach (string receiver in receiverList)
                {
                    if (receivers != "") receivers = receivers + "|";
                    receivers = receivers + receiver;
                }
            }
            string includeInTimeReportStr = "false";
            if (includeInTimeReport) includeInTimeReportStr = "true";

            NAVConnection.addElement(docElement, "caseNo", caseNo, "");
            NAVConnection.addElement(docElement, "lineNo", lineNo.ToString(), "");
            NAVConnection.addElement(docElement, "fromEmail", this.fromEmail, "");
            NAVConnection.addElement(docElement, "fromName", this.fromName, "");
            NAVConnection.addElement(docElement, "date", date.ToString("yyyy-MM-dd"), "");
            NAVConnection.addElement(docElement, "time", time.ToString("HH:mm:ss"), "");
            NAVConnection.addElement(docElement, "type",type, "");
            NAVConnection.addElement(docElement, "subject", subject, "");
            NAVConnection.addElement(docElement, "internalComment", System.Net.WebUtility.HtmlEncode(internalComment), "");
            NAVConnection.addElement(docElement, "htmlComment", htmlComment, "");
            NAVConnection.addElement(docElement, "receiverList", receivers, "");
            NAVConnection.addElement(docElement, "sendAsEmail", sendAsEmail.ToString(), "");
            NAVConnection.addElement(docElement, "closeCase", closeCase.ToString(), "");
            NAVConnection.addElement(docElement, "messageId", messageId, "");
            NAVConnection.addElement(docElement, "caseStatusCode", caseStatusCode, "");
            NAVConnection.addElement(docElement, "includeInTimeReport", includeInTimeReportStr, "");

            return xmlDoc;
        }

        protected string getInternalComment(Database database)
        {
            DatabaseQuery query = database.prepare("SELECT [Internal Comments] FROM [" + database.getTableName("Case Log") + "] WHERE [Case No_] = @caseNo AND [Line No_] = @lineNo");
            query.addStringParameter("caseNo", caseNo, 20);
            query.addIntParameter("lineNo", lineNo);

            object byteArrayObj = query.executeScalar();
            if (byteArrayObj.GetType() != typeof(DBNull))
            {
                byte[] byteArray = (byte[])byteArrayObj;
                return Navipro.Backoffice.Lib.HtmlSanitizer.SanitizeHtml(System.Net.WebUtility.HtmlDecode(System.Text.Encoding.Default.GetString(byteArray)), null);

            }
            return null;
        }

        protected string getExternalComment(Database database)
        {
            DatabaseQuery query = database.prepare("SELECT [External Comments] FROM [" + database.getTableName("Case Log") + "] WHERE [Case No_] = @caseNo AND [Line No_] = @lineNo");
            query.addStringParameter("caseNo", caseNo, 20);
            query.addIntParameter("lineNo", lineNo);

            object byteArrayObj = query.executeScalar();
            if (byteArrayObj.GetType() != typeof(DBNull))
            {
                byte[] byteArray = (byte[])byteArrayObj;
                return Navipro.Backoffice.Lib.HtmlSanitizer.SanitizeHtml(System.Net.WebUtility.HtmlDecode(System.Text.Encoding.Default.GetString(byteArray)), null);
                
            }

            return null;
        }



        public CaseLine getPrevCaseLine(Database database)
        {
            CaseLine caseLine = null;

            DatabaseQuery query = database.prepare("SELECT TOP 1 [Case No_], [Line No_], [Log Date], [Log Time], [Type], [Subject], [From Name], [From E-mail], [Send As E-mail], [E-mail Sent], [E-mail Sent Date], [E-mail Sent Time] FROM [" + database.getTableName("Case Log") + "] WITH (NOLOCK) WHERE [Case No_] = @caseNo AND ([Type] = '' OR [Type] = 'DESC') AND [Line No_] < @lineNo ORDER BY [Log Date] DESC, [Log Time] DESC");
            query.addStringParameter("caseNo", caseNo, 20);
            query.addIntParameter("lineNo", lineNo);
            SqlDataReader dataReader = query.executeQuery();

            if (dataReader.Read())
            {
                caseLine = new CaseLine(dataReader);


            }

            dataReader.Close();

            if (caseLine != null)
            {
                caseLine.externalComment = caseLine.getExternalComment(database);
            }

            return caseLine;
        }
    }



}