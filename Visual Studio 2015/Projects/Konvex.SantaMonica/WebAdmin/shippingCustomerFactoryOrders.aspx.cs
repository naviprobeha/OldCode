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
	public class shippingCustomerFactoryOrders : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet activeFactoryOrders;
		protected FactoryOrders factoryOrders;
		protected DateTime shipDate;
		protected ShippingCustomerUser currentShippingCustomerUser;

		protected ShippingCustomer currentShippingCustomer;

		protected DateTime fromDate;
		protected DateTime toDate;


		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (Session["current.customer"] == null)
			{
				Response.Redirect("default.htm");				
			}

			database = (Database)Session["database"];

			currentShippingCustomer = (ShippingCustomer)Session["current.customer"];
			this.currentShippingCustomerUser = (ShippingCustomerUser)Session["current.user.shippingCustomerUser"];


			shipDate = DateTime.Now;

			if (Session["toDate"] == null) Session["toDate"] = DateTime.Now;
			if (Session["noOfDaysBack"] == null) Session["noOfDaysBack"] = "0";
			if (Session["forward"] == null) Session["forward"] = "0";
			fromDate = (DateTime)Session["toDate"];
			toDate = (DateTime)Session["toDate"];			

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

			if (Request["command"] == "changeShipDate")
			{
				if (Request["forward"] != "on") Session["forward"] = "0";
				if (Request["forward"] == "on") Session["forward"] = "1";
			}

			if (Request["command"] == "createOrder")
			{
				FactoryOrder factoryOrder = new FactoryOrder();

				factoryOrder.type = 1;
				factoryOrder.creationDate = DateTime.Today;
				factoryOrder.shipDate = toDate;

				factoryOrder.applyFactory(currentShippingCustomer);
												  				
				factoryOrder.createdByType = 2;
				factoryOrder.createdByCode = currentShippingCustomerUser.userId;

				ConsumerRelations consumerRelations = new ConsumerRelations();
				DataSet consumerRelationDataSet = consumerRelations.getDataSet(database, 0, currentShippingCustomer.no);
				if (consumerRelationDataSet.Tables[0].Rows.Count > 0)
				{
					ConsumerRelation consumerRelation = new ConsumerRelation(consumerRelationDataSet.Tables[0].Rows[0]);
					
					Categories categories = new Categories();
					Category category = categories.getEntry(database, consumerRelation.categoryCode);
					if (category != null)
					{
						factoryOrder.categoryCode = category.code;
						factoryOrder.categoryDescription = category.description;
					}
					factoryOrder.quantity = consumerRelation.quantity;
				}

		

				factoryOrder.shipTime = new DateTime(1754, 01, 01, 16, 0, 0);

				factoryOrder.save(database, false);

				Response.Redirect("shippingForm_factory_modify.aspx?factoryOrderEntryNo="+factoryOrder.entryNo.ToString());

			}


			fromDate = toDate.AddDays(int.Parse(Session["noOfDaysBack"].ToString())*-1);

			factoryOrders = new FactoryOrders();

			DateTime fixedToDate = toDate;
			if (Session["forward"].ToString() == "1") fixedToDate = DateTime.Today.AddMonths(6);


			activeFactoryOrders = factoryOrders.getActiveCustomerDataSet(database, this.currentShippingCustomer.no, fromDate, fixedToDate);

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
