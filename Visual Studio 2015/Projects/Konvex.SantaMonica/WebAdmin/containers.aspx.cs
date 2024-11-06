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
	public class containers : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet containerDataSet;
		protected DataSet containerTypeDataSet;
		protected DataSet agentDataSet;
		protected DataSet shippingCustomerDataSet;
		protected DataSet factoryDataSet;
		protected Navipro.SantaMonica.Common.Containers containersClass;
		protected Navipro.SantaMonica.Common.ContainerTypes containerTypesClass;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "containers.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			containersClass = new Navipro.SantaMonica.Common.Containers();
			containerTypesClass = new Navipro.SantaMonica.Common.ContainerTypes();

			if (Request["command"] == "searchContainer")
			{
				containersClass.setSearchCriteria(Request["searchContainerNo"], Request["searchContainerTypeCode"], Request["searchLocationType"], Request["searchLocationCode"]);
			}


			containerDataSet = containersClass.getFullDataSet(database);
			containerTypeDataSet = containerTypesClass.getDataSet(database);

			Agents agents = new Agents();
			agentDataSet = agents.getAllDataSet(database);

			Navipro.SantaMonica.Common.ShippingCustomers shippingCustomers = new Navipro.SantaMonica.Common.ShippingCustomers();
			shippingCustomerDataSet = shippingCustomers.getDataSet(database);

			Navipro.SantaMonica.Common.Factories factories = new Navipro.SantaMonica.Common.Factories();
			factoryDataSet = factories.getDataSet(database);

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
