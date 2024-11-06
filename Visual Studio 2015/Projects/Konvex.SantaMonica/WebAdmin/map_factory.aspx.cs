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
	public class map_factory : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected MapServer mapServer;


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


			if (!currentOperator.checkSecurity(database, currentOrganization, "map_factory.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			Agents agentsClass = new Agents();
			DataSet lineAgents;

			mapServer = new MapServer(database.getConfiguration());
			mapServer.setSession(Session.SessionID, currentOperator.userId, currentOrganization.no);

			if (currentOrganization.allowLineOrderSupervision)
			{
				lineAgents = agentsClass.getDataSet(database, Agents.TYPE_LINE);
			}
			else
			{
				lineAgents = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_LINE);
			}

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
