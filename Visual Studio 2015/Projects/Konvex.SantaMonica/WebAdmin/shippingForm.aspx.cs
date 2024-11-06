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
	/// Summary description for shippingForm.
	/// </summary>
	public class shippingForm : System.Web.UI.Page
	{
		protected ShippingCustomer currentShippingCustomer;
		protected ShippingCustomerUser currentShippingCustomerUser;
		protected DataSet categoryDataSet;
		protected DataSet containerDataSet;
		protected Database database;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			this.currentShippingCustomer = (ShippingCustomer)Session["current.customer"];
			this.currentShippingCustomerUser = (ShippingCustomerUser)Session["current.user.shippingCustomerUser"];
			if (currentShippingCustomer == null) Response.Redirect("default.htm");
			if (currentShippingCustomerUser == null) Response.Redirect("default.htm");

			database = (Database)Session["database"];


			Categories categories = new Categories();
			categoryDataSet = categories.getDataSet(database);

			Containers containers = new Containers();
			containerDataSet = containers.getLocationDataSet(database, 1, currentShippingCustomer.no);

			if (Request["command"] == "deleteLineOrder")
			{
				LineOrders lineOrders = new LineOrders();
				LineOrder lineOrder = lineOrders.getEntry(database, Request["lineOrderEntryNo"]);

				lineOrder.deletedByType = 2;
				lineOrder.deletedByCode = currentShippingCustomerUser.userId;

				lineOrder.delete(database);

				Response.Redirect("shippingCustomerLineOrders.aspx");
			}

			if (Request["command"] == "createLineOrder")
			{
				LineOrder lineOrder = new LineOrder();

				if (Request["shipDateYear"] != null) lineOrder.shipDate = new DateTime(int.Parse(Request["shipDateYear"]), 1, 1, 0,0,0);
				if (Request["shipDateMonth"] != null) lineOrder.shipDate = lineOrder.shipDate.AddMonths(int.Parse(Request["shipDateMonth"])-1);
				if (Request["shipDateDay"] != null) lineOrder.shipDate = lineOrder.shipDate.AddDays(int.Parse(Request["shipDateDay"])-1);

				lineOrder.applyShippingCustomer(currentShippingCustomer);	
				lineOrder.shipTime = new DateTime(1754, 1, 1, int.Parse(Request["shipTimeHour"]), int.Parse(Request["shipTimeMinute"]), 0);
				lineOrder.confirmedToDateTime = new DateTime(lineOrder.shipDate.Year, lineOrder.shipDate.Month, lineOrder.shipDate.Day, lineOrder.shipTime.Hour, lineOrder.shipTime.Minute, 0);
				
				lineOrder.comments = Request["comments"];
				if (lineOrder.comments.Length > 200) lineOrder.comments = lineOrder.comments.Substring(1, 200);

				lineOrder.type = 1;
				lineOrder.enableAutoPlan = true;

				lineOrder.createdByType = 2;
				lineOrder.createdByCode = currentShippingCustomerUser.userId;

				lineOrder.save(database, false);

				int z = 0;
				while (z < containerDataSet.Tables[0].Rows.Count)
				{
					if (Request["category_"+containerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()] != "")
					{
						LineOrderContainer lineOrderContainer = new LineOrderContainer(lineOrder);
						lineOrderContainer.containerNo = containerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString();
						lineOrderContainer.categoryCode = Request["category_"+containerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()];
						lineOrderContainer.weight = float.Parse(Request["weight_"+containerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()]);
						lineOrderContainer.save(database);
					}
							
					z++;
				}

				Response.Redirect("shippingCustomerLineOrders.aspx");
			}
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
