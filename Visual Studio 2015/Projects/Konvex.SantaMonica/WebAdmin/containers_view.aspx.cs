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
	/// Summary description for customers_view.
	/// </summary>
	public class containers_view : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet containerEntryDataSet;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Navipro.SantaMonica.Common.Container currentContainer;
		protected Navipro.SantaMonica.Common.ContainerType currentContainerType;
		protected MapServer mapServer;

		protected DateTime containerDate;
		protected int noOfContainerRecords;


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


			if (!currentOperator.checkSecurity(database, currentOrganization, "containers.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			this.containerDate = DateTime.Today;
			this.noOfContainerRecords = 20;

			if ((Request["containerDateYear"] != null) && (Request["containerDateYear"] != ""))
			{
				this.containerDate = DateTime.Parse(Request["containerDateYear"]+"-"+Request["containerDateMonth"]+"-"+Request["containerDateDay"]);
				this.noOfContainerRecords = int.Parse(Request["noOfContainerRecords"]);
			}


			Containers containers = new Containers();

			if (Request["containerNo"] != null)
			{
				currentContainer = containers.getEntry(database, Request["containerNo"]);
			}
			else
			{
				currentContainer = new Navipro.SantaMonica.Common.Container();
			}
		

			mapServer = new MapServer(database.getConfiguration());
			mapServer.setSession(Session.SessionID, currentOperator.userId, currentOrganization.no);
 
			if (currentContainer.currentLocationCode != "")
			{
				if (currentContainer.currentLocationType == 0)
				{
					Agents agentsClass = new Agents();
					Agent agent = agentsClass.getAgent(database, currentContainer.currentLocationCode);
					if (agent != null)
					{
						mapServer.setPosition(agent.positionY, agent.positionX);
						mapServer.setPointMode(agent.code);
					}
				}
				if (currentContainer.currentLocationType == 1)
				{
					ShippingCustomers shippingCustomers = new ShippingCustomers();
					ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, currentContainer.currentLocationCode);
					if (shippingCustomer != null)
					{
						mapServer.setPosition(shippingCustomer.positionY, shippingCustomer.positionX);
						mapServer.setPointMode(shippingCustomer.name+", "+shippingCustomer.city);
					}
				}
				if (currentContainer.currentLocationType == 2)
				{
					Factories factories = new Factories();
					Factory factory = factories.getEntry(database, currentContainer.currentLocationCode);
					if (factory != null)
					{
						mapServer.setPosition(factory.positionY, factory.positionX);
						mapServer.setPointMode(factory.name);
					}
				}

			}

			ContainerTypes containerTypes = new ContainerTypes();
			if (currentContainer.containerTypeCode != "")
			{
				currentContainerType = containerTypes.getEntry(database, currentContainer.containerTypeCode);
			}
			else
			{
				currentContainerType = new ContainerType();
			}


			ContainerEntries containerEntries = new ContainerEntries();
			this.containerEntryDataSet = containerEntries.getContainerDataSet(database, currentContainer.no, containerDate, noOfContainerRecords);

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
