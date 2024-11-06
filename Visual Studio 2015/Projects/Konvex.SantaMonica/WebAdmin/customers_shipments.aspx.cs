using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.WebAdmin
{
	/// <summary>
	/// Summary description for shipments.
	/// </summary>
	public class customers_shipments : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet activeShipments;
		protected ShipmentHeaders shipmentsClass;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected bool showInfo;


		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (Session["current.user.operator"] == null)
			{
				Response.Redirect("default.htm");				
			}

			database = (Database)Session["database"];

			currentOperator = (UserOperator)Session["current.user.operator"];
			currentOrganization = (Organization)Session["current.user.organization"];

			if (!currentOperator.checkSecurity(database, currentOrganization, "customers.aspx"))
			{
				Response.Redirect("default.htm");				
			}



			if (Request["command"] == "toggleInfo")
			{
				if (showInfo)
				{
					showInfo = false;
					Session["showInfo"] = showInfo;
				}
				else
				{
					showInfo = true;
					Session["showInfo"] = showInfo;

				}

			}


			shipmentsClass = new ShipmentHeaders();
			activeShipments = shipmentsClass.getCustomerShipmentDataSet(database, Request["organizationNo"], Request["customerNo"]);

		}

		public void createDatePicker(string name, DateTime selectedDate)
		{
			Response.Write("<select name='"+name+"Year' class='Textfield' onchange='changeShipDate()'>");

			int year = DateTime.Now.Year-2;
			while (year < DateTime.Now.Year+1)
			{
				year++;
				if (year == selectedDate.Year)
				{
					Response.Write("<option value='"+year.ToString()+"' selected>"+year.ToString()+"</option>");
				}
				else
				{
					Response.Write("<option value='"+year.ToString()+"'>"+year.ToString()+"</option>");
				}
			}
			Response.Write("</select> - <select name='"+name+"Month' class='Textfield' onchange='changeShipDate()'>");

			int month = 0;
			while (month < 12)
			{
				month++;
				string monthStr = ""+month;
				if (monthStr.Length == 1) monthStr = "0"+month;

				if (month == selectedDate.Month)
				{
					Response.Write("<option value='"+monthStr+"' selected>"+monthStr+"</option>");
				}
				else
				{
					Response.Write("<option value='"+monthStr+"'>"+monthStr+"</option>");
				}
			}
			
			Response.Write("</select> - <select name='"+name+"Day' class='Textfield' onchange='changeShipDate()'>");
			
			int day = 0;
			while (day < 31)
			{
				day++;
				string dayStr = ""+day;
				if (dayStr.Length == 1) dayStr = "0"+day;

				if (day == selectedDate.Day)
				{
					Response.Write("<option value='"+dayStr+"' selected>"+dayStr+"</option>");
				}
				else
				{
					Response.Write("<option value='"+dayStr+"'>"+dayStr+"</option>");
				}
			}

			Response.Write("</select>");
		}											

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
