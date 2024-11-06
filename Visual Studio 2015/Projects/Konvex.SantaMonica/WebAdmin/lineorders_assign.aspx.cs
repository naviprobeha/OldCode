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

namespace WebAdmin
{
	/// <summary>
	/// Summary description for lineorders_assign.
	/// </summary>
	public class lineorders_assign : System.Web.UI.Page
	{

		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Agents agentsClass;
		protected DataSet activeAgents;
		protected DataSet lineJournalDataSet;

		protected LineOrder currentLineOrder;
		protected LineJournal currentLineOrderJournal;
		protected ShippingCustomer shippingCustomer;

		protected MapServer mapServer;


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

			if (!currentOperator.checkSecurity(database, currentOrganization, "lineorders.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			agentsClass = new Agents();
			activeAgents = agentsClass.getDataSet(database, Agents.TYPE_LINE);

			LineJournals lineJournals = new LineJournals();
			lineJournalDataSet = lineJournals.getAssignableDataSet(database);

			if (Request["lineOrderNo"] != null)
			{
				LineOrders lineOrders = new LineOrders();
				
				currentLineOrder = lineOrders.getEntry(database, Request["lineOrderNo"]);
				if (currentLineOrder == null)
				{
					currentLineOrder = new LineOrder();
				}
				
			}
			else
			{
				currentLineOrder = new LineOrder();
			}

			currentLineOrderJournal = currentLineOrder.getJournal(database);


			if (Request["command"] == "assignOrder")
			{

				DateTime shipDate = DateTime.Today;

				if (Request["shipDateYear"] != null)
				{
					try
					{
						shipDate = new DateTime(int.Parse(Request["shipDateYear"]), int.Parse(Request["shipDateMonth"]), int.Parse(Request["shipDateDay"]));
					}
					catch(Exception g)
					{
						try
						{
							shipDate = new DateTime(int.Parse(Request["shipDateYear"]), int.Parse(Request["shipDateMonth"]), 1);
						}
						catch(Exception f)
						{
							shipDate = DateTime.Now;

							if (f.Message != "") {}

						}

						if (g.Message != "") {}

					}
				}

				if (Request["type"] == "assign")
				{
					currentLineOrder.enableAutoPlan = false;
					if (Request["autoPlan"] == "on") currentLineOrder.enableAutoPlan = true;
					currentLineOrder.save(database, false);

					if (Request["agentCode"] != "")
					{
						if ((Request["createNewJournal"] != null) && (Request["createNewJournal"] == "on"))
						{
							currentLineOrder.assignToRoute(database, Request["agentCode"], shipDate, true);
						}
						else
						{
							currentLineOrder.assignToRoute(database, Request["agentCode"], shipDate);
						}
						
					}
					else
					{
						currentLineOrder.unassignToRoute(database);
					}
				}
				else
				{
					currentLineOrder.assignToRoute(database, int.Parse(Request["lineJournalEntryNo"]));
				}

				Response.Redirect("lineorders.aspx");
			}

			ShippingCustomers shippingCustomers = new ShippingCustomers();
			shippingCustomer = shippingCustomers.getEntry(database, currentLineOrder.shippingCustomerNo);

			
			mapServer = new MapServer(database.getConfiguration());
			mapServer.setSession(Session.SessionID, currentOperator.userId, currentOrganization.no);
			mapServer.setPosition(currentLineOrder.positionY, currentLineOrder.positionX);
			mapServer.setPointMode(currentLineOrder.shippingCustomerName);
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
