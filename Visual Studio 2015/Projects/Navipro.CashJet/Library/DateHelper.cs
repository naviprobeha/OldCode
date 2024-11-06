using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Navipro.Cashjet.Library
{
    public class DateHelper
    {

        public static void fillYearBox(ref System.Web.UI.WebControls.DropDownList yearBox)
        {
            int year = DateTime.Now.Year;
            int i = 0;
            while (i < 10)
            {
                yearBox.Items.Add((year - i).ToString());
                i++;
            }

        }

        public static void fillMonthBox(ref System.Web.UI.WebControls.DropDownList monthBox)
        {
            int currentMonth = DateTime.Now.Month;
            int i = 0;
            while (i < 12)
            {
                i++;
                monthBox.Items.Add(i.ToString().PadLeft(2, '0'));
            }
            monthBox.SelectedValue = currentMonth.ToString().PadLeft(2, '0');
        }

        public static void fillDayBox(ref System.Web.UI.WebControls.DropDownList dayBox)
        {
            int currentDay = DateTime.Now.Day;
            int i = 0;
            while (i < 31)
            {
                i++;
                dayBox.Items.Add(i.ToString().PadLeft(2, '0'));
            }
            dayBox.SelectedValue = currentDay.ToString().PadLeft(2, '0');
        }

        public static void fillIntervals(ref System.Web.UI.WebControls.DropDownList intervalBox, Configuration configuration)
        {
            intervalBox.Items.Add(new System.Web.UI.WebControls.ListItem(Translation.getTranslation("Dag", configuration.language), "0"));
            intervalBox.Items.Add(new System.Web.UI.WebControls.ListItem(Translation.getTranslation("Vecka", configuration.language), "1"));
            intervalBox.Items.Add(new System.Web.UI.WebControls.ListItem(Translation.getTranslation("Månad", configuration.language), "2"));
            intervalBox.Items.Add(new System.Web.UI.WebControls.ListItem(Translation.getTranslation("År", configuration.language), "3"));
            intervalBox.Items.Add(new System.Web.UI.WebControls.ListItem(Translation.getTranslation("Datumintervall", configuration.language), "4"));
        }

    }
}
