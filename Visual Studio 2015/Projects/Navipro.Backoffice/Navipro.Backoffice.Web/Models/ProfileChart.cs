using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Lib;

namespace Navipro.Backoffice.Web.Models
{
    public class ProfileChart
    {
        public string caption1 { set; get; }
        public string caption2 { set; get; }

        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public DataView dataView1 { set; get; }
        public DataView dataView2 { set; get; }

        public List<ProfileChartEntry> label { set; get; }

        public List<ProfileChartEntry> value1 { set; get; }
        public List<ProfileChartEntry> value2 { set; get; }

        public ProfileChart()
        {
            label = new List<ProfileChartEntry>();
            value1 = new List<ProfileChartEntry>();
            value2 = new List<ProfileChartEntry>();
        }


        public void updateChart(Database database)
        {
            value1 = Case.getChartCount(database, dataView1, value1, fromDate, toDate);
            value2 = Case.getChartCount(database, dataView2, value2, fromDate, toDate);
        }

        public string[] labelArray 
        {
            get
            {
                int i = 0;
                string[] array = new string[label.Count];

                foreach (ProfileChartEntry profileEntry in label)
                {
                    array[i] = profileEntry.label;
                    i++;
                }
                return array;
            }
        }

        public decimal[] value1Array
        {
            get
            {


                int i = 0;
                decimal[] array = new decimal[label.Count];

                foreach (ProfileChartEntry profileEntry in value1)
                {
                    array[i] = profileEntry.value;
                    i++;
                }
                return array;
            }
        }

        public decimal[] value2Array
        {
            get
            {
                int i = 0;
                decimal[] array = new decimal[label.Count];

                foreach (ProfileChartEntry profileEntry in value2)
                {
                    array[i] = profileEntry.value;
                    i++;
                }
                return array;
            }
        }
        public static ProfileChart createMonthChart(DateTime fromDate, DateTime toDate)
        {
            ProfileChart profileChart = new ProfileChart();
            
            DateTime date = new DateTime(fromDate.Year, fromDate.Month, 1);

            profileChart.fromDate = date;
            profileChart.toDate = toDate;

            while (date < toDate)
            {
                string label = "";
                if (date.Month == 1) label = "Januari";
                if (date.Month == 2) label = "Februari";
                if (date.Month == 3) label = "Mars";
                if (date.Month == 4) label = "April";
                if (date.Month == 5) label = "Maj";
                if (date.Month == 6) label = "Juni";
                if (date.Month == 7) label = "Juli";
                if (date.Month == 8) label = "Augusti";
                if (date.Month == 9) label = "September";
                if (date.Month == 10) label = "Oktober";
                if (date.Month == 11) label = "November";
                if (date.Month == 12) label = "December";

                ProfileChartEntry monthEntry1 = new ProfileChartEntry();
                monthEntry1.label = label;
                monthEntry1.fromDate = date;
                monthEntry1.toDate = date.AddMonths(1).AddDays(-1);

                ProfileChartEntry monthEntry2 = new ProfileChartEntry();
                monthEntry2.label = label;
                monthEntry2.fromDate = date;
                monthEntry2.toDate = date.AddMonths(1).AddDays(-1);

                profileChart.label.Add(monthEntry1);
                profileChart.value1.Add(monthEntry1);
                profileChart.value2.Add(monthEntry2);

                date = date.AddMonths(1);
            }

            return profileChart;
        }
    }

}