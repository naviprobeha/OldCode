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
	/// Summary description for map.
	/// </summary>
	public class map_consumer : System.Web.UI.Page
	{
		protected Database database;

		protected MapServer mapServer;
		protected ShippingCustomer currentShippingCustomer;


		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			if (Session["current.customer"] == null)
			{
				Response.Redirect("default.htm");				
			}

			database = (Database)Session["database"];

			currentShippingCustomer = (ShippingCustomer)Session["current.customer"];
			ShippingCustomerUser currentShippingCustomerUser = (ShippingCustomerUser)Session["current.user.shippingCustomerUser"];



			Agents agentsClass = new Agents();

			mapServer = new MapServer(database.getConfiguration());
			mapServer.setSession(Session.SessionID, currentShippingCustomerUser.userId, currentShippingCustomer.no);

		
			DataSet lineAgents = agentsClass.getDataSet(database, Agents.TYPE_LINE);

			if (lineAgents.Tables[0].Rows.Count > 0)
			{
				Agent agent = agentsClass.getAgent(database, lineAgents.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

				mapServer.setPosition(agent.positionY, agent.positionX);
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
