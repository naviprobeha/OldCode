using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Navipro.Backoffice.Web.Lib;
using System.Xml;

namespace Navipro.Backoffice.Web.Models
{
    public class TimeReportEntry
    {

        public TimeReportEntry()
        {
        }

        public TimeReportEntry(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            resourceNo = dataReader.GetValue(0).ToString();
            date = dataReader.GetDateTime(1);
            entryNo = dataReader.GetInt32(2);
            no = dataReader.GetValue(3).ToString();
            description = dataReader.GetValue(4).ToString();
            quantity = dataReader.GetDecimal(5);
            realQuantity = dataReader.GetDecimal(6);
            unitOfMeasureCode = dataReader.GetValue(7).ToString();

            jobNo = dataReader.GetValue(8).ToString();
            unitPrice = dataReader.GetDecimal(9);
            amount = dataReader.GetDecimal(10);
            commisionTypeCode = dataReader.GetValue(11).ToString();
            activityTypeCode = dataReader.GetValue(12).ToString();

        }

        [Display(Name = "Löpnr")]
        public int entryNo { get; set; }
        [Display(Name = "Resurs")]
        public string resourceNo { get; set; }
        [Display(Name = "Datum")]
        public DateTime date { get; set; }

        [Display(Name = "Nr")]
        public string no { get; set; }
        [Display(Name = "Beskrivning")]
        public string description { get; set; }
        [Display(Name = "Antal")]
        public decimal quantity { get; set; }
        [Display(Name = "Verk. antal")]
        public decimal realQuantity { get; set; }

        [Display(Name = "Enhet")]
        public string unitOfMeasureCode { get; set; }
        [Display(Name = "Projekt")]
        public string jobNo { get; set; }

        [Display(Name = "A-pris")]
        public decimal unitPrice { get; set; }
        [Display(Name = "Belopp")]
        public decimal amount { get; set; }

        [Display(Name = "Uppdragstyp")]
        public string commisionTypeCode { get; set; }
        [Display(Name = "Aktivitetstyp")]
        public string activityTypeCode { get; set; }


        public static List<TimeReportEntry> getList(Database database, string resourceNo, DateTime date)
        {
            List<TimeReportEntry> timeReportList = new List<TimeReportEntry>();

            try
            {
                DatabaseQuery query = database.prepare("SELECT [Resource No_], [Date], [Entry No_], [No_], [Description], [Quantity], [Real Quantity], [Unit of Measure Code], [Job No_], [Unit Price], [Amount], [Commision Type Code], [Activity Type Code] FROM [" + database.getTableName("Time Report") + "] WITH (NOLOCK) WHERE [Resource No_] = @resourceNo AND [Date] = @date");

                query.addStringParameter("resourceNo", resourceNo, 20);
                query.addDateTimeParameter("date", date);

                SqlDataReader dataReader = query.executeQuery();
                while (dataReader.Read())
                {
                    TimeReportEntry timeReportEntry = new TimeReportEntry(dataReader);
                    timeReportList.Add(timeReportEntry);

                }

                dataReader.Close();
            }
            catch (Exception e)
            {
                TimeReportEntry timeReportEntry = new TimeReportEntry();
                timeReportEntry.description = e.Message;
                timeReportList.Add(timeReportEntry);

            }
            return timeReportList;
        }

        public static TimeReportEntry getEntry(Database database, string resourceNo, DateTime date, int entryNo)
        {
            TimeReportEntry timeReportEntry = null;


            DatabaseQuery query = database.prepare("SELECT [Resource No_], [Date], [Entry No_], [No_], [Description], [Quantity], [Real Quantity], [Unit of Measure Code], [Job No_], [Unit Price], [Amount], [Commision Type Code], [Activity Type Code] FROM [" + database.getTableName("Time Report") + "] WITH (NOLOCK) WHERE [Resource No_] = @resourceNo AND [Date] = @date AND [Entry No_] = @entryNo");

            query.addStringParameter("resourceNo", resourceNo, 20);
            query.addDateTimeParameter("date", date);
            query.addIntParameter("entryNo", entryNo);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                timeReportEntry = new TimeReportEntry(dataReader);

            }

            dataReader.Close();


            return timeReportEntry;
        }

        public static string getVerifyMessage(Database database, string resourceNo, DateTime date)
        {
            decimal quantity = 0;
            decimal realQuantity = 0;
            decimal extendedTime = 0;
            decimal reducedTime = 0;

            DatabaseQuery query = database.prepare("SELECT SUM([Quantity]), SUM([Real Quantity]) FROM [" + database.getTableName("Time Report Ledger Entry") + "] WITH (NOLOCK) WHERE [Resource No_] = @resourceNo AND [Date] = @date AND [Include In Time Sum] = 1");

            query.addStringParameter("resourceNo", resourceNo, 20);
            query.addDateTimeParameter("date", date);

 
            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) quantity = dataReader.GetDecimal(0);
                if (!dataReader.IsDBNull(1)) realQuantity = dataReader.GetDecimal(1);
            }
            dataReader.Close();


            query = database.prepare("SELECT SUM([Real Quantity]) FROM [" + database.getTableName("Time Report Ledger Entry") + "] WITH (NOLOCK) WHERE [Resource No_] = @resourceNo AND [Date] = @date AND [Activity Type Code] = 'MERTID'");
            query.addStringParameter("resourceNo", resourceNo, 20);
            query.addDateTimeParameter("date", date);

            dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) extendedTime = dataReader.GetDecimal(0);
            }
            dataReader.Close();

            query = database.prepare("SELECT SUM([Real Quantity]) FROM [" + database.getTableName("Time Report Ledger Entry") + "] WITH (NOLOCK) WHERE [Resource No_] = @resourceNo AND [Date] = @date AND [Activity Type Code] = 'UNDERTID'");
            query.addStringParameter("resourceNo", resourceNo, 20);
            query.addDateTimeParameter("date", date);

            dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) reducedTime = dataReader.GetDecimal(0);
            }
            dataReader.Close();

            string message = "";
            message = "<b>"+realQuantity.ToString("0.#") + " timmar är nu rapporterade.</b><br/><br/>";
            if (realQuantity != quantity) message = message + "Fakturerat antal: " + quantity.ToString("0.#")+" timmar.<br/>";
            if (extendedTime > 0) message = message + "Mertid: " + extendedTime.ToString("0.#") + " timmar.<br/>";
            if (reducedTime > 0) message = message + "Undertid: " + reducedTime.ToString("0.#")+" timmar.";
            return message;
        }

        public static string getReportedTotals(Database database, string resourceNo, DateTime date)
        {
            decimal quantity = 0;
            decimal realQuantity = 0;

            DatabaseQuery query = database.prepare("SELECT SUM([Quantity]), SUM([Real Quantity]) FROM [" + database.getTableName("Time Report") + "] WITH (NOLOCK) WHERE [Resource No_] = @resourceNo AND [Date] = @date");

            query.addStringParameter("resourceNo", resourceNo, 20);
            query.addDateTimeParameter("date", date);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) quantity = dataReader.GetDecimal(0);
                if (!dataReader.IsDBNull(1)) realQuantity = dataReader.GetDecimal(1);
            }
            dataReader.Close();

            return quantity.ToString("0.#") + " - " + realQuantity.ToString("0.#");

        }

        public static string getVerifiedTotals(Database database, string resourceNo, DateTime date)
        {
            decimal quantity = 0;
            decimal realQuantity = 0;

            DatabaseQuery query = database.prepare("SELECT SUM([Quantity]), SUM([Real Quantity]) FROM [" + database.getTableName("Time Report Ledger Entry") + "] WITH (NOLOCK) WHERE [Resource No_] = @resourceNo AND [Date] = @date AND [Include In Time Sum] = 1");

            query.addStringParameter("resourceNo", resourceNo, 20);
            query.addDateTimeParameter("date", date);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) quantity = dataReader.GetDecimal(0);
                if (!dataReader.IsDBNull(1)) realQuantity = dataReader.GetDecimal(1);
            }
            dataReader.Close();

            return quantity.ToString("0.#") + " - " + realQuantity.ToString("0.#");

        }

        public static string getPostedTotals(Database database, string resourceNo, DateTime date)
        {
            decimal quantity = 0;
            decimal realQuantity = 0;

            DatabaseQuery query = database.prepare("SELECT SUM([Quantity]), SUM([Real Quantity]) FROM [" + database.getTableName("Time Report Ledger Entry") + "] WITH (NOLOCK) WHERE [Resource No_] = @resourceNo AND [Date] = @date AND [Include In Time Sum] = 1 AND [Posted] = 1");

            query.addStringParameter("resourceNo", resourceNo, 20);
            query.addDateTimeParameter("date", date);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) quantity = dataReader.GetDecimal(0);
                if (!dataReader.IsDBNull(1)) realQuantity = dataReader.GetDecimal(1);
            }
            dataReader.Close();

            return quantity.ToString("0.#") + " - " + realQuantity.ToString("0.#");

        }

        public XmlDocument toDOM()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><timeReportEntry/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            NAVConnection.addElement(docElement, "resourceNo", resourceNo, "");
            NAVConnection.addElement(docElement, "date", date.ToString("yyyy-MM-dd"), "");
            NAVConnection.addElement(docElement, "entryNo", entryNo.ToString(), "");
            NAVConnection.addElement(docElement, "no", no, "");
            NAVConnection.addElement(docElement, "quantity", quantity.ToString(), "");
            NAVConnection.addElement(docElement, "realQuantity", realQuantity.ToString(), "");
            NAVConnection.addElement(docElement, "unitPrice", unitPrice.ToString(), "");


            return xmlDoc;
        }

        public void submitQuanties(Database database)
        {
            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "submitTimeReport", toDOM(), out responseElement);

        }

        public void submitTemplate(Database database)
        {
            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "submitTimeReportTemplate", toDOM(), out responseElement);

        }

        public void submitVerify(Database database)
        {
            XmlElement responseElement = null;
            NAVConnection.performService(database.configuration, "submitTimeReportVerify", toDOM(), out responseElement);

        }
    }
}