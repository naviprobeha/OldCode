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
	/// Summary description for messages_modify.
	/// </summary>
	public class messages_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected Message currentMessage;
		protected MessageAgents messageAgents;
		protected DataSet activeAgents;

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


			messageAgents = new MessageAgents();
			Agents agents = new Agents();

			if (currentOrganization.callCenterMaster)
			{
				activeAgents = agents.getDataSet(database, Agents.TYPE_SINGLE);
			}
			else
			{
				activeAgents = agents.getDataSet(database, currentOrganization.no);
			}

			if (Request["messageEntryNo"] != null)
			{
				Messages messages = new Messages();
				
				currentMessage = messages.getEntry(database, currentOrganization.no, Request["messageEntryNo"]);
				if (currentMessage == null)
				{
					currentMessage = new Message(currentOrganization.no);
					currentMessage.fromName = currentOperator.name;
				}
				
			}
			else
			{
				currentMessage = new Message(currentOrganization.no);
				currentMessage.fromName = currentOperator.name;
			}


			if (Request["command"] == "saveMessage")
			{
				currentMessage.fromName = Request["sender"];
				currentMessage.message = Request["message"];

				currentMessage.save(database);

				int i = 0;
				while (i < activeAgents.Tables[0].Rows.Count)
				{
					MessageAgent messageAgent = messageAgents.getEntry(database, currentMessage.entryNo, activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

					if (Request["agent_"+activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()] == "on")
					{
						if (messageAgent == null)
						{
							messageAgent = new MessageAgent(currentOrganization.no);
							messageAgent.messageEntryNo = currentMessage.entryNo;
							messageAgent.agentCode = activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
							messageAgent.ackDateTime = new DateTime(1753,1,1);
							messageAgent.save(database);
						}
					}
					else
					{
						if (messageAgent != null) messageAgent.delete(database);
					}

					i++;
				}

				Response.Redirect("messages.aspx");
			}

			if (Request["command"] == "deleteMessage")
			{
				currentMessage.delete(database);

				Response.Redirect("messages.aspx");
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
