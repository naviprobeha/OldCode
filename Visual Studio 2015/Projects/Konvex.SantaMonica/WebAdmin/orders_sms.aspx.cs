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
	public class orders_sms : System.Web.UI.Page
	{
	
		protected Database database;
		protected DataSet smsDataSet;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "orders.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			SMSMessages smsMessages = new SMSMessages();

			if (Request["command"] == "createOrder")
			{
				SMSMessage smsMessage = smsMessages.getEntry(database, int.Parse(Request["entryNo"]));
				if (smsMessage != null)
				{
					Navipro.SantaMonica.Common.Customers customersClass = new Navipro.SantaMonica.Common.Customers();
					Customer customer = customersClass.findFirstCustomer(database, Request["customerNo"]);
					if (customer != null)
					{

						ShipOrder shipOrder = new ShipOrder(customer);		

						shipOrder.comments = "SMS> "+smsMessage.message;
						shipOrder.createdBy = 2;
						shipOrder.save(database);
						shipOrder.log(database, currentOperator.userId, "Körordern skapad");
						
						smsMessage.handled = 1;
						smsMessage.save(database);

						Response.Redirect("orders_modify_full.aspx?shipOrderNo="+shipOrder.entryNo, true);
					}
				}
			}

			smsDataSet = smsMessages.getFailedDataSet(database, 0);
			

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
