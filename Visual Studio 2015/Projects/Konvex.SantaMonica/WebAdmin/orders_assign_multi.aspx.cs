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
	/// Summary description for orders_assign_multi.
	/// </summary>
	public class orders_assign_multi : System.Web.UI.Page
	{

		protected Database database;
		protected DataSet activeShipOrders;
		protected ShipOrders shipOrders;
		protected DateTime shipDate;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Agents agentsClass;
		protected DataSet activeAgents;
		protected DataSet organizationDataSet;
		protected string organizationNo;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "orders.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			Organizations organizations = new Organizations();
			organizationDataSet = organizations.getCallCenterMemberDataSet(database);

			if (Session["showInfo"] != null) showInfo = (bool)Session["showInfo"];

			shipDate = DateTime.Now;

			shipOrders = new ShipOrders();

			if (currentOrganization.callCenterMaster)
			{
				organizationNo = Request["organizationNo"];
				if ((organizationNo == "") || (organizationNo == null)) organizationNo = organizationDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

			}
			else
			{
				organizationNo = currentOrganization.no;
				
			}

			activeShipOrders = shipOrders.getActiveCallCenterDataSet(database, organizationNo, "", DateTime.Parse(Request["fromDate"]), DateTime.Parse(Request["toDate"]));


			if (Request["command"] == "assignMulti")
			{
				int k = 0;
				while (k < activeShipOrders.Tables[0].Rows.Count)
				{
					if (Request["order"+activeShipOrders.Tables[0].Rows[k].ItemArray.GetValue(1).ToString()] == "on")
					{
						ShipOrder currentShipOrder = shipOrders.getEntry(database, activeShipOrders.Tables[0].Rows[k].ItemArray.GetValue(1).ToString());
						currentShipOrder.assignOrder(database, Request["agent"], currentOperator.userId);
						currentShipOrder.save(database);
					}

					k++;
				}

				Response.Redirect("orders.aspx");
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


			agentsClass = new Agents();
			activeAgents = agentsClass.getDataSet(database, organizationNo, Agents.TYPE_SINGLE);

		}

		protected string getStatusText(int status)
		{
			if (status == 0) 
			{
				return "";
			}
			if (status == 1)
			{
				return "Avböjd";
			}
			if (status == 2)
			{
				return "Osäker";
			}
			if (status == 3) 
			{
				return "Tilldelad";
			}
			if (status == 4) 
			{
				return "Skickad";
			}
			if (status == 5) 
			{
				return "Bekräftad";
			}
			if (status == 6) 
			{
				return "Lastad";
			}

			return "";


		}

		protected string getStatusIcon(int status)
		{
			if (status == 0) 
			{
				return "ind_white.gif";
			}
			if (status == 1)
			{
				return "ind_red.gif";
			}
			if (status == 2) 
			{
				return "ind_yellow.gif";
			}
			if (status == 3) 
			{
				return "ind_yellow.gif";
			}
			if (status == 4) 
			{
				return "ind_green.gif";
			}
			if (status == 5) 
			{
				return "ind_green.gif";
			}
			if (status == 6) 
			{
				return "int_black.gif";
			}

			return "ind_white.gif";
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
