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
	public class containers_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Navipro.SantaMonica.Common.Container currentContainer;
		protected Navipro.SantaMonica.Common.ContainerType currentContainerType;

		protected DataSet agentDataSet;
		protected DataSet shippingCustomerDataSet;
		protected DataSet factoryDataSet;
		protected DataSet containerTypeDataSet;
		protected MapServer mapServer;

		protected int currentLocationType;
		protected string errorMessage = "";


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


			Containers containers = new Containers();
			ShippingCustomers shippingCustomers = new ShippingCustomers();
			Factories factories = new Factories();
			Agents agents = new Agents();
			ContainerTypes containerTypes = new ContainerTypes();

			if ((Request["containerNo"] != null) && (Request["containerNo"] != ""))
			{
				currentContainer = containers.getEntry(database, Request["containerNo"]);
			}
			else
			{
				currentContainer = new Navipro.SantaMonica.Common.Container();
				if (Request["no"] != null) currentContainer.no = Request["no"];
				if (Request["description"] != null) currentContainer.description = Request["description"];
				if (Request["containerTypeCode"] != null) currentContainer.containerTypeCode = Request["containerTypeCode"];
			}

			if (Request["command"] == "saveContainer")
			{
				bool saveContainer = true;

				if (Request["containerNo"] == "") 
				{
					Navipro.SantaMonica.Common.Container container = containers.getEntry(database, Request["no"]);
					if (container != null)
					{
						errorMessage = "Container "+Request["no"]+" finns redan.";
						saveContainer = false;
					}
					else
					{
						currentContainer.no = Request["no"];
					}
				}

				if (saveContainer)
				{
					currentContainer.description = Request["description"];
					currentContainer.containerTypeCode = Request["containerTypeCode"];

					currentContainer.save(database);

					if (currentContainer.currentLocationCode != Request["currentLocationCode"])
					{
						ContainerEntry containerEntry = new ContainerEntry();
						containerEntry.containerNo = currentContainer.no;
						containerEntry.entryDateTime = DateTime.Now;
						containerEntry.estimatedArrivalDateTime = DateTime.Now;
						containerEntry.locationType = int.Parse(Request["currentLocationType"]);
						containerEntry.locationCode = Request["currentLocationCode"];
						containerEntry.receivedDateTime = DateTime.Now;
						containerEntry.sourceType = 2;
						containerEntry.sourceCode = currentOperator.userId;
						containerEntry.type = 4;
						containerEntry.creatorType = 1;
						containerEntry.creatorNo = currentOperator.userId;
						containerEntry.save(database);
					}

					Response.Redirect("containers_view.aspx?containerNo="+currentContainer.no);
				}
			}

			if (Request["command"] == "deleteContainer")
			{
				currentContainer.delete(database);

				Response.Redirect("containers.aspx");
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
					ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, currentContainer.currentLocationCode);
					if (shippingCustomer != null)
					{
						mapServer.setPosition(shippingCustomer.positionY, shippingCustomer.positionX);
						mapServer.setPointMode(shippingCustomer.name+", "+shippingCustomer.city);
					}
				}
				if (currentContainer.currentLocationType == 2)
				{
					Factory factory = factories.getEntry(database, currentContainer.currentLocationCode);
					if (factory != null)
					{
						mapServer.setPosition(factory.positionY, factory.positionX);
						mapServer.setPointMode(factory.name);
					}
				}

			}

			if (currentContainer.containerTypeCode != "")
			{
				currentContainerType = containerTypes.getEntry(database, currentContainer.containerTypeCode);
			}
			else
			{
				currentContainerType = new ContainerType();
			}


			currentLocationType = currentContainer.currentLocationType;
			if ((Request["currentLocationType"] != null) && (Request["currentLocationType"] != null)) currentLocationType = int.Parse(Request["currentLocationType"]);

			agentDataSet = agents.getAllDataSet(database);
			shippingCustomerDataSet = shippingCustomers.getDataSet(database, 0);
			factoryDataSet = factories.getDataSet(database);
			containerTypeDataSet = containerTypes.getDataSet(database);
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
