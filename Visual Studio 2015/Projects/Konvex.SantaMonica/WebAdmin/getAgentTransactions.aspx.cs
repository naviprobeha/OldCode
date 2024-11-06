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
using System.Xml;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.WebAdmin
{
	/// <summary>
	/// Summary description for getAgents.
	/// </summary>
	public class getAgentTransactions : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here


			Database database = (Database)Session["database"];


			Organizations organizations = new Organizations();
			Organization currentOrganization = organizations.getOrganization(database, Request["parameter2"], false);

			if (currentOrganization == null)
			{
				database.close();
				Response.End();
			}

			UserOperators operators = new UserOperators();
			UserOperator currentOperator = operators.getOperator(database, Request["parameter1"]);

			if (currentOperator == null)
			{
				database.close();
				Response.End();
			}

			Agents agentsClass = new Agents();
			AgentTransactions agentTransactions = new AgentTransactions();
			DataSet agentTransactionDataSet = null;

			if (currentOrganization.allowLineOrderSupervision)
			{
				if (currentOperator.systemRoleCode == "FABRIK")
				{
				}
				else
				{
					agentTransactionDataSet = agentTransactions.getAgentTransactions(database, Request["agentCode"], DateTime.Parse(Request["fromDate"]));
				}
			}
			else
			{
				Agent agent = agentsClass.getAgent(database, Request["agentCode"]);
				if (agent.organizationNo == currentOrganization.no)
				{
					agentTransactionDataSet = agentTransactions.getAgentTransactions(database, Request["agentCode"], DateTime.Parse(Request["fromDate"]));
				}
			}

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml("<objects/>");

			XmlElement docElement = xmlDoc.DocumentElement;

			if (agentTransactionDataSet != null)
			{
				if (agentTransactionDataSet.Tables[0].Rows.Count > 0)
				{
					XmlElement groupElement = xmlDoc.CreateElement("objectGroup");
					groupElement.SetAttribute("name", "Historik");

					int i = 0;
					Decimal lastSpeed = 9999;
					DateTime lastTransactionDateTime = DateTime.Now;

					while (i < agentTransactionDataSet.Tables[0].Rows.Count)
					{
						AgentTransaction agentTransaction = new AgentTransaction(agentTransactionDataSet.Tables[0].Rows[i]);

						if (i == 0) lastTransactionDateTime = agentTransaction.updatedDateTime;

						if ((agentTransaction.positionX > 0) && (agentTransaction.positionY > 0))
						{
							if (lastSpeed != agentTransaction.speed)
							{
								string type = "8";
								if (lastTransactionDateTime.AddMinutes(15) < agentTransaction.updatedDateTime) type = "9";

								XmlElement objectElement = xmlDoc.CreateElement("object");
								objectElement.SetAttribute("type", type);
								objectElement.SetAttribute("name", agentTransaction.agentCode);
								objectElement.SetAttribute("heading", ((int)agentTransaction.heading).ToString());
								objectElement.SetAttribute("speed", ((int)agentTransaction.speed).ToString());
								objectElement.SetAttribute("positionX", agentTransaction.positionY.ToString());
								objectElement.SetAttribute("positionY", agentTransaction.positionX.ToString());
								objectElement.SetAttribute("user", agentTransaction.userName);
								objectElement.SetAttribute("status", "1");
								objectElement.SetAttribute("timestamp", agentTransaction.updatedDateTime.ToString("yyyy-MM-dd HH:mm"));
							
								groupElement.AppendChild(objectElement);

								lastSpeed = agentTransaction.speed;
								lastTransactionDateTime = agentTransaction.updatedDateTime;
							}
						}

						i++;
					}
					docElement.AppendChild(groupElement);
				}
			}

			database.close();

			xmlDoc.Save(Response.OutputStream);
			Response.End();
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
