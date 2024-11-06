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
	/// Summary description for customers.
	/// </summary>
	public class items_view : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet itemPriceDataSet;
		protected DataSet itemPriceExtendedDataSet;
		protected Navipro.SantaMonica.Common.Items items;
		protected Navipro.SantaMonica.Common.Item currentItem;
		protected ItemPrices itemPrices;
		protected ItemPricesExtended itemPricesExtended;

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


			if (!currentOperator.checkSecurity(database, currentOrganization, "items.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			items = new Navipro.SantaMonica.Common.Items();
			currentItem = items.getEntry(database, Request["itemNo"]);

			itemPrices = new ItemPrices();
			itemPriceDataSet = itemPrices.getDataSet(database, currentItem.no);

			itemPricesExtended = new ItemPricesExtended();
			itemPriceExtendedDataSet = itemPricesExtended.getDataSet(database, currentItem.no);

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
