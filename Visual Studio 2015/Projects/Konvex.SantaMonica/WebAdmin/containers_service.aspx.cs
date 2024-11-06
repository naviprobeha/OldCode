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
	/// Summary description for containers.
	/// </summary>
	public class containers_service : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet containerServiceDataSet;
		protected DataSet activeServiceTypes;
		protected Navipro.SantaMonica.Common.ContainerEntries containerEntries;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "containers_service.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			containerEntries = new Navipro.SantaMonica.Common.ContainerEntries();

			if (Request["command"] == "report")
			{
				ContainerServiceEntry containerServiceEntry = new ContainerServiceEntry();
				containerServiceEntry.containerNo = Request["containerNo"];
				containerServiceEntry.entryDateTime = DateTime.Now;
				containerServiceEntry.userId = currentOperator.userId;
				containerServiceEntry.serviceType = Request["serviceType_"+Request["entryNo"]];
				containerServiceEntry.serviceType2 = Request["serviceType2_"+Request["entryNo"]];
				containerServiceEntry.save(database);

				
				DataSet containerDataSet = containerEntries.getServiceDataSet(database, Request["containerNo"]);
				int i = 0;
				while (i < containerDataSet.Tables[0].Rows.Count)
				{
					ContainerEntry containerEntry = new ContainerEntry(containerDataSet.Tables[0].Rows[i]);

					ContainerLinkEntry containerLinkEntry = new ContainerLinkEntry();
					containerLinkEntry.containerEntryNo = containerEntry.entryNo;
					containerLinkEntry.containerSericeEntryNo = containerServiceEntry.entryNo;
					containerLinkEntry.linkType = 0;
					containerLinkEntry.save(database);

					i++;
				}

			}

			if (Request["command"] == "destruct")
			{
				ContainerServiceEntry containerServiceEntry = new ContainerServiceEntry();
				containerServiceEntry.containerNo = Request["containerNo"];
				containerServiceEntry.entryDateTime = DateTime.Now;
				containerServiceEntry.userId = currentOperator.userId;
				containerServiceEntry.save(database);

				
				DataSet containerDataSet = containerEntries.getServiceDataSet(database, Request["containerNo"]);
				int i = 0;
				while (i < containerDataSet.Tables[0].Rows.Count)
				{
					ContainerEntry containerEntry = new ContainerEntry(containerDataSet.Tables[0].Rows[i]);

					ContainerLinkEntry containerLinkEntry = new ContainerLinkEntry();
					containerLinkEntry.containerEntryNo = containerEntry.entryNo;
					containerLinkEntry.containerSericeEntryNo = containerServiceEntry.entryNo;
					containerLinkEntry.linkType = 1;
					containerLinkEntry.save(database);

					i++;
				}

				Containers containers = new Containers();
				Navipro.SantaMonica.Common.Container container = containers.getEntry(database, containerServiceEntry.containerNo);
				if (container != null)
				{
					container.delete(database);
				}

			}

			if (Request["command"] == "undo")
			{
				containerEntries.undoService(database, Request["containerNo"]);				
				

			}

			ServiceTypes servicetypes = new ServiceTypes();
			activeServiceTypes = servicetypes.getDataSet(database);


			containerServiceDataSet = containerEntries.getServiceDataSet(database);

			
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
