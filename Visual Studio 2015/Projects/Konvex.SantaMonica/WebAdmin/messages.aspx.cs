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
	/// Summary description for messages.
	/// </summary>
	public class messages : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet unreadMessages;
		protected DataSet unassignedMessages;
		protected DataSet readMessages;
		protected MessageAgents messageAgents;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "messages.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			Messages messages = new Messages();
			unreadMessages = messages.getUnReadDataSet(database, currentOrganization.no);
			unassignedMessages = messages.getUnAssignedDataSet(database, currentOrganization.no);
			readMessages = messages.getReadDataSet(database, currentOrganization.no);

			messageAgents = new MessageAgents();


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
