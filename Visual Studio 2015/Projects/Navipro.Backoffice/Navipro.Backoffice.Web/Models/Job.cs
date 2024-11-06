using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Lib;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Navipro.Backoffice.Web.Models
{
    public class Job
    {
        public Job()
        {


        }

        public Job(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            no = dataReader.GetValue(0).ToString();
            description = dataReader.GetValue(1).ToString();
            billToCustomerNo = dataReader.GetValue(2).ToString();
            billToName = dataReader.GetValue(3).ToString();
            status = int.Parse(dataReader.GetValue(4).ToString());
            startingDate = dataReader.GetDateTime(5);
            endingDate = dataReader.GetDateTime(6);
        }

        [Required]
        [Display(Name = "Nr")]
        public String no { get; set; }

        [Required]
        [Display(Name = "Beskrivning")]
        public String description { get; set; }

        [Display(Name = "Kundnr")]
        public String billToCustomerNo { get; set; }

        [Display(Name = "Namn")]
        public String billToName { get; set; }

        [Display(Name = "Status")]
        public int status { get; set; }

        [Display(Name = "Startdatum")]
        public DateTime startingDate { get; set; }

        [Display(Name = "Slutdatum")]
        public DateTime endingDate { get; set; }

        public string startingDateText { get { if (startingDate.Year > 1754) return startingDate.ToString("yyyy-MM-dd"); return ""; } }
        public string endingDateText { get { if (endingDate.Year > 1754) return endingDate.ToString("yyyy-MM-dd"); return ""; } }

        public string statusText { get { if (status == 0) return "Planering"; if (status == 1) return "Offert"; if (status == 2) return "Pågår";  return "Avslutat"; } }
        public string statusIcon { get { if (status == 0) return "label-plain"; if (status == 1) return "label-warning"; if (status == 2) return "label-primary"; return "label-error"; } }

        public static Job getEntry(Database database, string no)
        {
            Job job = null;

            DatabaseQuery query = database.prepare("SELECT [No_], [Description], [Bill-to Customer No_], [Bill-to Name], [Status], [Starting Date], [Ending Date] FROM [" + database.getTableName("Job") + "] WITH (NOLOCK) WHERE [No_] = @no");
            query.addStringParameter("no", no, 20);

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                job = new Job(dataReader);
            }

            dataReader.Close();

            return job;
        }

        public static List<Job> getList(Database database)
        {
            List<Job> jobList = new List<Job>();

            DatabaseQuery query = database.prepare("SELECT [No_], [Description], [Bill-to Customer No_], [Bill-to Name], [Status], [Starting Date], [Ending Date] FROM [" + database.getTableName("Job") + "] WITH (NOLOCK) WHERE [Blocked] = 0 ORDER BY [Description]");

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                Job job = new Job(dataReader);
                jobList.Add(job);

            }

            dataReader.Close();

            return jobList;
        }

        public static List<Job> getList(Database database, DataView dataView)
        {
            List<Job> jobList = new List<Job>();

            string filterString = "";
            string noOfRecords = "";
            string orderByString = "[Description]";
            if (dataView != null)
            {
                if ((dataView.query != "") && (filterString != "")) filterString = filterString + " AND ";
                filterString = filterString + dataView.query;
                if (dataView.noOfRecords > 0) noOfRecords = "TOP " + dataView.noOfRecords;
                if (dataView.orderBy != "") orderByString = dataView.orderBy;
            }

            DatabaseQuery query = database.prepare("SELECT "+ noOfRecords+ " [No_], [Description], [Bill-to Customer No_], [Bill-to Name], [Status], [Starting Date], [Ending Date] FROM [" + database.getTableName("Job") + "] WITH (NOLOCK) WHERE [Blocked] = 0 " + filterString+" ORDER BY "+orderByString);


            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                Job job = new Job(dataReader);
                jobList.Add(job);

            }

            dataReader.Close();

            return jobList;
        }

        public static List<Job> getList(Database database, string customerNo)
        {
            List<Job> jobList = new List<Job>();

            DatabaseQuery query = database.prepare("SELECT [No_], [Description], [Bill-to Customer No_], [Bill-to Name], [Status], [Starting Date], [Ending Date] FROM [" + database.getTableName("Job") + "] WITH (NOLOCK) WHERE [Bill-to Customer No_] = @customerNo AND [Blocked] = 0 ORDER BY [Description]");
            query.addStringParameter("customerNo", customerNo, 20);

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                Job job = new Job(dataReader);
                jobList.Add(job);

            }

            dataReader.Close();

            return jobList;
        }
        public static List<SelectListItem> getSelectList(Database database)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            List<Job> jobList = getList(database);

            foreach(Job item in jobList)
            {
                SelectListItem selectItem = new SelectListItem();
                selectItem.Value = item.no;
                selectItem.Text = item.no + " - " + item.description;
                selectList.Add(selectItem);

            }

            return selectList;
        }

        public static void search(Database database, string searchQuery, ref List<SearchResult> searchResults)
        {
            DatabaseQuery query = database.prepare("SELECT [No_], [Description] FROM [" + database.getTableName("Job") + "] WITH (NOLOCK) WHERE [Description] LIKE @searchQuery");
            query.addStringParameter("searchQuery", "%" + searchQuery + "%", 100);



            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                SearchResult searchResult = new SearchResult();
                searchResult.caption = "Projekt - " + dataReader.GetValue(0).ToString() + " - " + dataReader.GetValue(1).ToString();
                searchResult.url = "/Job/Details/" + dataReader.GetValue(0).ToString();

                searchResults.Add(searchResult);
            }
            dataReader.Close();

        }
    }



}