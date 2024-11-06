using System;
using System.Web;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for CalendarHelper.
    /// </summary>
    public class CalendarHelper
    {
        public CalendarHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static DateTime GetFirstDayOfWeek(int year, int weekNumber)
        {
            return GetFirstDayOfWeek(year, weekNumber, System.Threading.Thread.CurrentThread.CurrentCulture);
        }

        public static int GetWeek(DateTime day)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(day, System.Globalization.CalendarWeekRule.FirstFourDayWeek, System.DayOfWeek.Monday);
        }


        public static DateTime GetFirstDayOfWeek(int year, int weekNumber, System.Globalization.CultureInfo culture)
        {

            System.Globalization.Calendar calendar = culture.Calendar;

            if (weekNumber > calendar.GetWeekOfYear(new DateTime(year, 12, 31), System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
            {
                weekNumber = calendar.GetWeekOfYear(new DateTime(year, 12, 31), System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            }


            DateTime firstOfYear = new DateTime(year, 1, 1, calendar);
            DateTime targetDay = calendar.AddWeeks(firstOfYear, weekNumber);
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            //throw new Exception("Year: "+year+", Week: "+weekNumber+", targetDay: "+targetDay.ToString("yyyy-MM-dd"));

            while (calendar.GetWeekOfYear(targetDay, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) != weekNumber)
            {
                if (((calendar.GetWeekOfYear(targetDay, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) - weekNumber) == -1) || ((calendar.GetWeekOfYear(targetDay, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) - weekNumber) == 1))
                {
                    if (calendar.GetWeekOfYear(targetDay, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) < weekNumber)
                    {
                        targetDay = targetDay.AddDays(7);
                    }
                    if (calendar.GetWeekOfYear(targetDay, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) > weekNumber)
                    {
                        targetDay = targetDay.AddDays(-7);
                    }
                }
                else
                {
                    if (calendar.GetWeekOfYear(targetDay, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) > weekNumber)
                    {
                        targetDay = targetDay.AddDays(1);
                    }
                    if (calendar.GetWeekOfYear(targetDay, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) < weekNumber)
                    {
                        targetDay = targetDay.AddDays(-1);
                    }

                }
            }


            while (targetDay.DayOfWeek != firstDayOfWeek)
            {
                targetDay = targetDay.AddDays(-1);
            }


            return targetDay;

        }

        public static string GetDayOfWeek(DateTime dateTime)
        {

            if (dateTime.DayOfWeek == System.DayOfWeek.Monday) return "Måndag";
            if (dateTime.DayOfWeek == System.DayOfWeek.Tuesday) return "Tisdag";
            if (dateTime.DayOfWeek == System.DayOfWeek.Wednesday) return "Onsdag";
            if (dateTime.DayOfWeek == System.DayOfWeek.Thursday) return "Torsdag";
            if (dateTime.DayOfWeek == System.DayOfWeek.Friday) return "Fredag";
            if (dateTime.DayOfWeek == System.DayOfWeek.Saturday) return "Lördag";
            if (dateTime.DayOfWeek == System.DayOfWeek.Sunday) return "Söndag";

            return "";
        }


        public static void createDatePicker(string name, DateTime selectedDate)
        {

            HttpContext.Current.Response.Write("<select name='" + name + "Year' class='Textfield' onchange='changeDate()'>");

            int year = DateTime.Now.Year - 2;
            while (year < DateTime.Now.Year + 1)
            {
                year++;
                if (year == selectedDate.Year)
                {
                    HttpContext.Current.Response.Write("<option value='" + year.ToString() + "' selected>" + year.ToString() + "</option>");
                }
                else
                {
                    HttpContext.Current.Response.Write("<option value='" + year.ToString() + "'>" + year.ToString() + "</option>");
                }
            }
            HttpContext.Current.Response.Write("</select> - <select name='" + name + "Month' class='Textfield' onchange='changeDate()'>");

            int month = 0;
            while (month < 12)
            {
                month++;
                string monthStr = "" + month;
                if (monthStr.Length == 1) monthStr = "0" + month;

                if (month == selectedDate.Month)
                {
                    HttpContext.Current.Response.Write("<option value='" + monthStr + "' selected>" + monthStr + "</option>");
                }
                else
                {
                    HttpContext.Current.Response.Write("<option value='" + monthStr + "'>" + monthStr + "</option>");
                }
            }

            HttpContext.Current.Response.Write("</select> - <select name='" + name + "Day' class='Textfield' onchange='changeDate()'>");

            int day = 0;
            while (day < 31)
            {
                day++;
                string dayStr = "" + day;
                if (dayStr.Length == 1) dayStr = "0" + day;

                if (day == selectedDate.Day)
                {
                    HttpContext.Current.Response.Write("<option value='" + dayStr + "' selected>" + dayStr + "</option>");
                }
                else
                {
                    HttpContext.Current.Response.Write("<option value='" + dayStr + "'>" + dayStr + "</option>");
                }
            }

            HttpContext.Current.Response.Write("</select>");
        }

        public static void createYearPicker(string name, int selectedYear)
        {

            HttpContext.Current.Response.Write("<select name='" + name + "' class='Textfield' onchange='changeYear()'>");

            int year = DateTime.Now.Year - 2;
            while (year < DateTime.Now.Year + 2)
            {
                year++;
                if (year == selectedYear)
                {
                    HttpContext.Current.Response.Write("<option value='" + year.ToString() + "' selected>" + year.ToString() + "</option>");
                }
                else
                {
                    HttpContext.Current.Response.Write("<option value='" + year.ToString() + "'>" + year.ToString() + "</option>");
                }
            }
            HttpContext.Current.Response.Write("</select>");
        }

        public static void createWeekPicker(string name, int selectedYear, int selectedWeek)
        {

            HttpContext.Current.Response.Write("<select name='" + name + "' class='Textfield' onchange='changeWeek()'>");

            int noOfWeeks = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(new DateTime(selectedYear, 12, 31), System.Globalization.CalendarWeekRule.FirstFourDayWeek, System.DayOfWeek.Monday);


            int week = 0;
            while (week < noOfWeeks)
            {
                week++;
                if (week == selectedWeek)
                {
                    HttpContext.Current.Response.Write("<option value='" + week.ToString() + "' selected>" + week.ToString() + "</option>");
                }
                else
                {
                    HttpContext.Current.Response.Write("<option value='" + week.ToString() + "'>" + week.ToString() + "</option>");
                }
            }
            HttpContext.Current.Response.Write("</select>");
        }


    }
}
