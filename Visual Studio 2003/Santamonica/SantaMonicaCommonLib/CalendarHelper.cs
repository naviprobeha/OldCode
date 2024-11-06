using System;

namespace Navipro.SantaMonica.Common
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

			while(calendar.GetWeekOfYear(targetDay, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) != weekNumber)
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

		

	}
}
