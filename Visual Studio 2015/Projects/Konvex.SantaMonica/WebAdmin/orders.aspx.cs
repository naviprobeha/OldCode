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
	/// Summary description for _default.
	/// </summary>
	public class _default : System.Web.UI.Page
	{
	
		protected Database database;
		protected DataSet activeShipOrders;
		protected ShipOrders shipOrders;
		protected DateTime shipDate;
		protected Agents agentsClass;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected DataSet activeAgents;
		protected DataSet organizationDataSet;

		protected DateTime fromDate;
		protected DateTime toDate;

		protected bool showInfo;
		protected bool showLoadedOrders;
		protected string currentOrganizationNo;

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


			if (Session["showInfo"] != null) showInfo = (bool)Session["showInfo"];
			if (Session["showLoadedOrders"] != null) showLoadedOrders = (bool)Session["showLoadedOrders"];

			shipDate = DateTime.Now;

			if (Session["toDate"] == null) Session["toDate"] = DateTime.Now;
			if (Session["noOfDaysBack"] == null) Session["noOfDaysBack"] = "0";
			fromDate = (DateTime)Session["toDate"];
			toDate = (DateTime)Session["toDate"];
			
			if (Session["selectedOrganizationNo"] != null) currentOrganizationNo = (string)Session["selectedOrganizationNo"];
			if ((Request["organizationNo"] != "") && (Request["organizationNo"] != null)) currentOrganizationNo = Request["organizationNo"];
			if (currentOrganizationNo == "-") currentOrganizationNo = "";

			Session["selectedOrganizationNo"] = currentOrganizationNo;

			if (Request["workDateYear"] != null)
			{
				try
				{
					toDate = new DateTime(int.Parse(Request["workDateYear"]), int.Parse(Request["workDateMonth"]), int.Parse(Request["workDateDay"]));
				}
				catch(Exception g)
				{
					try
					{
						toDate = new DateTime(int.Parse(Request["workDateYear"]), int.Parse(Request["workDateMonth"]), 1);
					}
					catch(Exception f)
					{
						toDate = DateTime.Now;

						if (f.Message != "") {}

					}

					if (g.Message != "") {}

				}
			}

			Session["toDate"] = toDate;


			if (Request["noOfDaysBack"] != null)
			{
				Session["noOfDaysBack"] = Request["noOfDaysBack"];
			}

			fromDate = toDate.AddDays(int.Parse(Session["noOfDaysBack"].ToString())*-1);

			Organizations organizations = new Organizations();
			organizationDataSet = organizations.getCallCenterMemberDataSet(database);
			currentOrganization = organizations.getOrganization(database, currentOrganization.no);

			shipOrders = new ShipOrders();

			if (Request["command"] == "retrieveNotLoadedOrders")
			{
				shipOrders.changeShipDateOnNotLoadedOrders(database, currentOrganization.no, toDate);
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

			if (Request["command"] == "toggleLoadedOrders")
			{
				if (showLoadedOrders)
				{
					showLoadedOrders = false;
					Session["showLoadedOrders"] = showLoadedOrders;
				}
				else
				{
					showLoadedOrders = true;
					Session["showLoadedOrders"] = showLoadedOrders;

				}

			}

			if (Request["command"] == "changeOfficeMode")
			{
				currentOrganization = organizations.getOrganization(database, currentOrganization.no);

				if (currentOrganization.officeMode == 0)
				{
					currentOrganization.officeMode = 1;
					currentOrganization.save(database);
				}
				else
				{
					currentOrganization.officeMode = 0;
					currentOrganization.save(database);
				}

			}

			if (currentOrganization.callCenterMaster)
			{
				activeShipOrders = shipOrders.getActiveCallCenterDataSet(database, currentOrganizationNo, Request["agent"], fromDate, toDate, this.showLoadedOrders);
			}
			else
			{
				activeShipOrders = shipOrders.getActiveDataSet(database, currentOrganization.no, Request["agent"], fromDate, toDate, this.showLoadedOrders);
			}
			

			agentsClass = new Agents();
			activeAgents = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_SINGLE);
			currentOrganization = organizations.getOrganization(database, currentOrganization.no);

		}

		protected string getStatusText(int status)
		{
			if (status == 0) 
			{
				return "";
			}
			if (status == 1)
			{
				return "Avb�jd";
			}
			if (status == 2)
			{
				return "Os�ker";
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
				return "Bekr�ftad";
			}
			if (status == 6) 
			{
				return "Lastad";
			}
			if (status == 7) 
			{
				return "Markulerad";
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
				return "ind_orange.gif";
			}
			if (status == 3) 
			{
				return "ind_yellow.gif";
			}
			if (status == 4) 
			{
				return "ind_purple.gif";
			}
			if (status == 5) 
			{
				return "ind_green.gif";
			}
			if (status == 6) 
			{
				return "ind_black.gif";
			}
			if (status == 7) 
			{
				return "ind_blue.gif";
			}

			return "ind_white.gif";
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
