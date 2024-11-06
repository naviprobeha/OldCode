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
	/// Summary description for lineorders.
	/// </summary>
	public class factoryOrders_shipReport : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet organizationDataSet;
		protected DataSet factoryDataSet;
		protected DataSet shippingCustomerDataSet;
		protected DataSet consumerDataSet;
		protected DataSet factoryOrderLedgerDataSet;
		protected FactoryOrders factoryOrders;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected DateTime fromDate;
		protected DateTime toDate;
		protected int dayDuration;


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

			if (!currentOperator.checkSecurity(database, currentOrganization, "factoryOrders_shipReport.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			if (Session["toDate"] == null) Session["toDate"] = DateTime.Now;
			if (Session["fromDate"] == null) Session["fromDate"] = DateTime.Now.AddDays(-7);
			fromDate = (DateTime)Session["fromDate"];
			toDate = (DateTime)Session["toDate"];
			
			if (Request["fromDateYear"] != null)
			{
				try
				{
					fromDate = new DateTime(int.Parse(Request["fromDateYear"]), int.Parse(Request["fromDateMonth"]), int.Parse(Request["fromDateDay"]));
				}
				catch(Exception g)
				{
					try
					{
						fromDate = new DateTime(int.Parse(Request["fromDateYear"]), int.Parse(Request["fromDateMonth"]), 1);
					}
					catch(Exception f)
					{
						fromDate = DateTime.Now;

						if (f.Message != "") {}

					}

					if (g.Message != "") {}

				}
			}


			if (Request["toDateYear"] != null)
			{
				try
				{
					toDate = new DateTime(int.Parse(Request["toDateYear"]), int.Parse(Request["toDateMonth"]), int.Parse(Request["toDateDay"]));
				}
				catch(Exception g)
				{
					try
					{
						toDate = new DateTime(int.Parse(Request["toDateYear"]), int.Parse(Request["toDateMonth"]), 1);
					}
					catch(Exception f)
					{
						toDate = DateTime.Now;

						if (f.Message != "") {}

					}

					if (g.Message != "") {}

				}
			}

			Session["fromDate"] = fromDate;
			Session["toDate"] = toDate;

			this.dayDuration = DateTime.Now.Subtract(fromDate).Days;
			if (DateTime.Now > toDate) this.dayDuration = toDate.Subtract(fromDate).Days;

			FactoryOrderLedgerEntries factoryOrderLedgerEntries = new FactoryOrderLedgerEntries();

			if (Request["command"] == "addInvoice")
			{
				FactoryOrderLedgerEntry factoryOrderLedgerEntry = new FactoryOrderLedgerEntry();
				factoryOrderLedgerEntry.consumerNo = Request["consumerNo"];
				factoryOrderLedgerEntry.documentNo = Request["documentNo"];
				factoryOrderLedgerEntry.invoiceDate = DateTime.Parse(Request["invoiceDateYear"]+"-"+Request["invoiceDateMonth"]+"-"+Request["invoiceDateDay"]);
				
				string quantityStr = Request["quantity"];
				factoryOrderLedgerEntry.quantity = float.Parse(quantityStr.Replace(".", ","));

				string amountStr = Request["amount"];
				factoryOrderLedgerEntry.amount = float.Parse(amountStr.Replace(".", ","));

				factoryOrderLedgerEntry.save(database);

			}

			if (Request["command"] == "deleteInvoice")
			{
				FactoryOrderLedgerEntry factoryOrderLedgerEntry = factoryOrderLedgerEntries.getEntry(database, int.Parse(Request["argument"]));
				if (factoryOrderLedgerEntry != null) 
				{
					factoryOrderLedgerEntry.delete(database);
					
				}

			}

			factoryOrders = new FactoryOrders();

			Organizations organizationsClass = new Organizations();
			organizationDataSet = organizationsClass.getDataSet(database);

			Factories factories = new Factories();
			factoryDataSet = factories.getDataSet(database, "KONVEX");

			Consumers consumers = new Consumers();
			consumerDataSet = consumers.getDataSet(database);

			ShippingCustomers shippingCustomers = new ShippingCustomers();
			shippingCustomerDataSet = shippingCustomers.getDataSet(database, 1);

			factoryOrderLedgerDataSet = factoryOrderLedgerEntries.getDataSet(database, fromDate, toDate);
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
