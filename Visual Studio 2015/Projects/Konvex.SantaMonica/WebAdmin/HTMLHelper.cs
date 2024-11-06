using System;
using System.Web;
using System.Collections;

namespace WebAdmin
{
	/// <summary>
	/// Summary description for HTMLHelper.
	/// </summary>
	public class HTMLHelper
	{
		public HTMLHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void createDatePicker(string name, DateTime selectedDate)
		{
			
			HttpContext.Current.Response.Write("<select name='"+name+"Year' class='Textfield' onchange='changeShipDate()'>");

			int year = DateTime.Now.Year-4;
			while (year < DateTime.Now.Year+1)
			{
				year++;
				if (year == selectedDate.Year)
				{
					HttpContext.Current.Response.Write("<option value='"+year.ToString()+"' selected>"+year.ToString()+"</option>");
				}
				else
				{
					HttpContext.Current.Response.Write("<option value='"+year.ToString()+"'>"+year.ToString()+"</option>");
				}
			}
			HttpContext.Current.Response.Write("</select> - <select name='"+name+"Month' class='Textfield' onchange='changeShipDate()'>");

			int month = 0;
			while (month < 12)
			{
				month++;
				string monthStr = ""+month;
				if (monthStr.Length == 1) monthStr = "0"+month;

				if (month == selectedDate.Month)
				{
					HttpContext.Current.Response.Write("<option value='"+monthStr+"' selected>"+monthStr+"</option>");
				}
				else
				{
					HttpContext.Current.Response.Write("<option value='"+monthStr+"'>"+monthStr+"</option>");
				}
			}
			
			HttpContext.Current.Response.Write("</select> - <select name='"+name+"Day' class='Textfield' onchange='changeShipDate()'>");
			
			int day = 0;
			while (day < 31)
			{
				day++;
				string dayStr = ""+day;
				if (dayStr.Length == 1) dayStr = "0"+day;

				if (day == selectedDate.Day)
				{
					HttpContext.Current.Response.Write("<option value='"+dayStr+"' selected>"+dayStr+"</option>");
				}
				else
				{
					HttpContext.Current.Response.Write("<option value='"+dayStr+"'>"+dayStr+"</option>");
				}
			}

			HttpContext.Current.Response.Write("</select>");
		}											

		public static void createYearPicker(string name, int selectedYear)
		{
			
			HttpContext.Current.Response.Write("<select name='"+name+"' class='Textfield' onchange='changeYear()'>");

			int year = DateTime.Now.Year-4;
			while (year < DateTime.Now.Year+2)
			{
				year++;
				if (year == selectedYear)
				{
					HttpContext.Current.Response.Write("<option value='"+year.ToString()+"' selected>"+year.ToString()+"</option>");
				}
				else
				{
					HttpContext.Current.Response.Write("<option value='"+year.ToString()+"'>"+year.ToString()+"</option>");
				}
			}
			HttpContext.Current.Response.Write("</select>");
		}

		public static void createWeekPicker(string name, int selectedYear, int selectedWeek)
		{
			
			HttpContext.Current.Response.Write("<select name='"+name+"' class='Textfield' onchange='changeWeek()'>");

			int noOfWeeks = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(new DateTime(selectedYear, 12, 31), System.Globalization.CalendarWeekRule.FirstFourDayWeek, System.DayOfWeek.Monday);


			int week = 0;
			while (week < noOfWeeks)
			{
				week++;
				if (week == selectedWeek)
				{
					HttpContext.Current.Response.Write("<option value='"+week.ToString()+"' selected>"+week.ToString()+"</option>");
				}
				else
				{
					HttpContext.Current.Response.Write("<option value='"+week.ToString()+"'>"+week.ToString()+"</option>");
				}
			}
			HttpContext.Current.Response.Write("</select>");
		}

		public static ArrayList combineLists(ArrayList list1, ArrayList list2)
		{
			ArrayList newList = null;
			
			if (list1 != null)
			{
				if (newList == null) newList = new ArrayList();
				int i = 0;
				while (i < list1.Count)
				{
					newList.Add(list1[i]);
					i++;
				}
			}
			if (list2 != null)
			{
				if (newList == null) newList = new ArrayList();
				int i = 0;
				while (i < list2.Count)
				{
					newList.Add(list2[i]);
					i++;
				}
			}

			return newList;
		}
	}
}
