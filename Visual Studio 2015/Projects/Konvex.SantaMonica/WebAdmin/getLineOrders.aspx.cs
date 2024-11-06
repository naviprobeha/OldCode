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
	public class getLineOrders : System.Web.UI.Page
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

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml("<objects/>");

			XmlElement docElement = xmlDoc.DocumentElement;

			DataSet lineOrderDataSet;
			LineOrders lineOrders = new LineOrders();

			if (currentOrganization.allowLineOrderSupervision)
			{
				lineOrderDataSet = lineOrders.getMapDataSet(database);
			}
			else
			{
				lineOrderDataSet = lineOrders.getMapDataSet(database, currentOrganization.no);
			}

			if (lineOrderDataSet.Tables[0].Rows.Count > 0)
			{
				XmlElement groupElement = xmlDoc.CreateElement("objectGroup");
				groupElement.SetAttribute("name", "Linjeorder");

				int i = 0;
				while (i < lineOrderDataSet.Tables[0].Rows.Count)
				{
					LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);

					if ((lineOrder.positionX > 0) && (lineOrder.positionY > 0))
					{
									
						XmlElement objectElement = xmlDoc.CreateElement("object");
						objectElement.SetAttribute("type", "3");
						objectElement.SetAttribute("name", lineOrder.shippingCustomerName);
						objectElement.SetAttribute("heading", "0");
						objectElement.SetAttribute("speed", "0");
						objectElement.SetAttribute("positionX", lineOrder.positionY.ToString());
						objectElement.SetAttribute("positionY", lineOrder.positionX.ToString());
						objectElement.SetAttribute("user", "");
						objectElement.SetAttribute("status", "");
				
						groupElement.AppendChild(objectElement);
					}

					i++;
				}
				docElement.AppendChild(groupElement);
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
