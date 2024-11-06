using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Navipro.Infojet.Lib;

namespace WebInterface._taglib
{
    public partial class UserDefined_STARTORDER : System.Web.UI.UserControl, InfojetUserControl
    {
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected WebPageLine webPageLine;
        protected string prevWebPageUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();
            
        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion

        public string createDatePicker(string name, DateTime selectedDate)
        {
            string html = "";
            html = "<select name='" + name + "Year' class='Textfield' onchange='changeShipDate()'>";

            int year = DateTime.Now.Year - 2;
            while (year < DateTime.Now.Year + 1)
            {
                year++;
                if (year == selectedDate.Year)
                {
                    html = html + "<option value='" + year.ToString() + "' selected>" + year.ToString() + "</option>";
                }
                else
                {
                    html = html + "<option value='" + year.ToString() + "'>" + year.ToString() + "</option>";
                }
            }
            html = html + "</select> - <select name='" + name + "Month' class='Textfield' onchange='changeShipDate()'>";

            int month = 0;
            while (month < 12)
            {
                month++;
                string monthStr = "" + month;
                if (monthStr.Length == 1) monthStr = "0" + month;

                if (month == selectedDate.Month)
                {
                    html = html + "<option value='" + monthStr + "' selected>" + monthStr + "</option>";
                }
                else
                {
                    html = html + "<option value='" + monthStr + "'>" + monthStr + "</option>";
                }
            }

            html = html + "</select> - <select name='" + name + "Day' class='Textfield' onchange='changeShipDate()'>";

            int day = 0;
            while (day < 31)
            {
                day++;
                string dayStr = "" + day;
                if (dayStr.Length == 1) dayStr = "0" + day;

                if (day == selectedDate.Day)
                {
                    html = html + "<option value='" + dayStr + "' selected>" + dayStr + "</option>";
                }
                else
                {
                    html = html + "<option value='" + dayStr + "'>" + dayStr + "</option>";
                }
            }

            html = html + "</select>";

            return html;
        }											

    }
}