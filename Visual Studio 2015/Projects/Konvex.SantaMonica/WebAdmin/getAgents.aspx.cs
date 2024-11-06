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
	public class getAgents : System.Web.UI.Page
	{
		private Database database;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here


			database = (Database)Session["database"];


			
			Navipro.SantaMonica.Common.ShippingCustomers shippingCustomers = new Navipro.SantaMonica.Common.ShippingCustomers();
			Organizations organizations = new Organizations();
			Organization currentOrganization = organizations.getOrganization(database, Request["parameter2"], false);

			if (currentOrganization == null)
			{
				Navipro.SantaMonica.Common.ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, Request["parameter2"]);
				if (shippingCustomer == null)
				{
					database.close();
					Response.End();
				}
			}

			UserOperators operators = new UserOperators();
			UserOperator currentOperator = operators.getOperator(database, Request["parameter1"]);

			if (currentOperator != null)
			{
				generateOperatorAgents(currentOrganization, currentOperator);
			}
			else
			{
				Navipro.SantaMonica.Common.ShippingCustomer currentShippingCustomer = shippingCustomers.getEntry(database, Request["parameter2"]);
				ShippingCustomerUsers shippingCustomerUsers = new ShippingCustomerUsers();
				ShippingCustomerUser currentShippingCustomerUser = shippingCustomerUsers.getEntry(database, Request["parameter1"]);
				if (currentShippingCustomerUser != null)
				{		
					generateShippingCustomerAgents(currentShippingCustomer, currentShippingCustomerUser);
				}
				database.close();
				Response.End();
			}

		}


		private void generateOperatorAgents(Organization currentOrganization, UserOperator currentOperator)
		{
			Agents agentsClass = new Agents();
			DataSet singleAgents = null;
			DataSet lineAgents = null;
			DataSet tankAgents = null;
			DataSet factories;

			if (currentOrganization.allowLineOrderSupervision)
			{
				if (currentOperator.systemRoleCode == "FABRIK")
				{
					LineJournals lineJournals = new LineJournals();
					lineAgents = lineJournals.getLineJournalAgentsForOperator(database, currentOperator.userId);
					
					FactoryOrders factoryOrders = new FactoryOrders();
					tankAgents = factoryOrders.getFactoryOrderAgentsForOperator(database, currentOperator.userId);
				}
				else
				{
					singleAgents = agentsClass.getDataSet(database, Agents.TYPE_SINGLE);
					lineAgents = agentsClass.getDataSet(database, Agents.TYPE_LINE);
					tankAgents = agentsClass.getDataSet(database, Agents.TYPE_TANK);
				}
			}
			else
			{
				singleAgents = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_SINGLE);
				lineAgents = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_LINE);
				tankAgents = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_TANK);
			}

			Factories factoriesClass = new Factories();
			factories = factoriesClass.getDataSet(database); 

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml("<objects/>");

			XmlElement docElement = xmlDoc.DocumentElement;

			int i = 0;
			if (factories.Tables[0].Rows.Count > 0)
			{
				XmlElement groupElement = xmlDoc.CreateElement("objectGroup");
				groupElement.SetAttribute("name", "Fabriker");

				while (i < factories.Tables[0].Rows.Count)
				{
					Factory factory = new Factory(factories.Tables[0].Rows[i]);

					if ((factory.positionX > 0) && (factory.positionY > 0))
					{

						XmlElement objectElement = xmlDoc.CreateElement("object");
						objectElement.SetAttribute("type", "0");
						objectElement.SetAttribute("name", factory.name);
						objectElement.SetAttribute("heading", "0");
						objectElement.SetAttribute("speed", "0");
						objectElement.SetAttribute("positionX", factory.positionY.ToString());
						objectElement.SetAttribute("positionY", factory.positionX.ToString());
						objectElement.SetAttribute("user", "");
						objectElement.SetAttribute("status", "0");
						objectElement.SetAttribute("timestamp", "");
	
				

						groupElement.AppendChild(objectElement);
					}

					i++;
				}
				docElement.AppendChild(groupElement);
			}

			if (singleAgents != null)
			{
				if (singleAgents.Tables[0].Rows.Count > 0)
				{
					XmlElement groupElement = xmlDoc.CreateElement("objectGroup");
					groupElement.SetAttribute("name", "Uppsamling");

					i = 0;
					while (i < singleAgents.Tables[0].Rows.Count)
					{
						Agent agent = agentsClass.getAgent(database, singleAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

						if ((agent.positionX > 0) && (agent.positionY > 0))
						{

							int status = 0;									
							if (agent.lastUpdated.AddMinutes(30) < DateTime.Now) status = 1;
							if (agent.lastUpdated.AddMinutes(60) < DateTime.Now) status = 2;
											
							string timeStamp = "";
							if (agent.lastUpdated < DateTime.Today) 
								timeStamp = agent.lastUpdated.ToString("yyyy-MM-dd");
							else
								timeStamp = agent.lastUpdated.ToString("HH:mm:ss");

							XmlElement objectElement = xmlDoc.CreateElement("object");
							objectElement.SetAttribute("type", "1");
							objectElement.SetAttribute("name", agent.code);
							objectElement.SetAttribute("heading", ((int)agent.heading).ToString());
							objectElement.SetAttribute("speed", ((int)agent.speed).ToString());
							objectElement.SetAttribute("positionX", agent.positionY.ToString());
							objectElement.SetAttribute("positionY", agent.positionX.ToString());
							objectElement.SetAttribute("user", agent.userName);
							objectElement.SetAttribute("status", status.ToString());
							objectElement.SetAttribute("timestamp", timeStamp);
							
							groupElement.AppendChild(objectElement);
						}

						i++;
					}
					docElement.AppendChild(groupElement);
				}
			}
			

			if (lineAgents != null)
			{
				if (lineAgents.Tables[0].Rows.Count > 0)
				{
					XmlElement groupElement = xmlDoc.CreateElement("objectGroup");
					groupElement.SetAttribute("name", "Linjetrafik");

					i = 0;
					while (i < lineAgents.Tables[0].Rows.Count)
					{
						Agent agent = agentsClass.getAgent(database, lineAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

						if ((agent.positionX > 0) && (agent.positionY > 0))
						{
							int status = 0;									
							if (agent.lastUpdated.AddMinutes(30) < DateTime.Now) status = 1;
							if (agent.lastUpdated.AddMinutes(60) < DateTime.Now) status = 2;
											
							string timeStamp = "";
							if (agent.lastUpdated < DateTime.Today) 
								timeStamp = agent.lastUpdated.ToString("yyyy-MM-dd");
							else
								timeStamp = agent.lastUpdated.ToString("HH:mm:ss");

							XmlElement objectElement = xmlDoc.CreateElement("object");
							objectElement.SetAttribute("type", "2");
							objectElement.SetAttribute("name", agent.code);
							objectElement.SetAttribute("heading", ((int)agent.heading).ToString());
							objectElement.SetAttribute("speed", ((int)agent.speed).ToString());
							objectElement.SetAttribute("positionX", agent.positionY.ToString());
							objectElement.SetAttribute("positionY", agent.positionX.ToString());
							objectElement.SetAttribute("user", agent.userName);
							objectElement.SetAttribute("status", status.ToString());
							objectElement.SetAttribute("timestamp", timeStamp);
				
							groupElement.AppendChild(objectElement);
						}
						i++;
					}
					docElement.AppendChild(groupElement);
				}
			}

			if (tankAgents != null)
			{
				if (tankAgents.Tables[0].Rows.Count > 0)
				{
					XmlElement groupElement = xmlDoc.CreateElement("objectGroup");
					groupElement.SetAttribute("name", "Biomal");

					i = 0;
					while (i < tankAgents.Tables[0].Rows.Count)
					{
						try
						{
							Agent agent = agentsClass.getAgent(database, tankAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

							if ((agent.positionX > 0) && (agent.positionY > 0))
							{
								
								int status = 0;									
								if (agent.lastUpdated.AddMinutes(30) < DateTime.Now) status = 1;
								if (agent.lastUpdated.AddMinutes(60) < DateTime.Now) status = 2;
							
							
								string timeStamp = "";
								if (agent.lastUpdated < DateTime.Today) 
									timeStamp = agent.lastUpdated.ToString("yyyy-MM-dd");
								else
									timeStamp = agent.lastUpdated.ToString("HH:mm:ss");

							
								XmlElement objectElement = xmlDoc.CreateElement("object");
								objectElement.SetAttribute("type", "3");
								objectElement.SetAttribute("name", agent.code);
								objectElement.SetAttribute("heading", ((int)agent.heading).ToString());
								objectElement.SetAttribute("speed", ((int)agent.speed).ToString());
								objectElement.SetAttribute("positionX", agent.positionY.ToString());
								objectElement.SetAttribute("positionY", agent.positionX.ToString());
								objectElement.SetAttribute("user", agent.userName);
								objectElement.SetAttribute("status", status.ToString());
								objectElement.SetAttribute("timestamp", timeStamp);
				
							
								groupElement.AppendChild(objectElement);
						
								
							}
						
						}
						catch(Exception e)
						{
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

		private void generateShippingCustomerAgents(ShippingCustomer currentShippingCustomer, ShippingCustomerUser currentShippingCustomerUser)
		{
			Agents agentsClass = new Agents();
			DataSet singleAgents = null;
			DataSet lineAgents = null;
			DataSet tankAgents = null;
			DataSet factories;

			Consumers consumers = new Consumers();
			Consumer consumer = consumers.getShippingCustomerEntry(database, currentShippingCustomer.no);
			if (consumer != null)
			{
				FactoryOrders factoryOrders = new FactoryOrders();
				tankAgents = factoryOrders.getFactoryOrderAgentsForConsumer(database, consumer.no);

			}

			Factories factoriesClass = new Factories();
			factories = factoriesClass.getDataSet(database); 

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml("<objects/>");

			XmlElement docElement = xmlDoc.DocumentElement;

			int i = 0;
			if (factories.Tables[0].Rows.Count > 0)
			{
				XmlElement groupElement = xmlDoc.CreateElement("objectGroup");
				groupElement.SetAttribute("name", "Fabriker");

				while (i < factories.Tables[0].Rows.Count)
				{
					Factory factory = new Factory(factories.Tables[0].Rows[i]);

					if ((factory.positionX > 0) && (factory.positionY > 0))
					{

						XmlElement objectElement = xmlDoc.CreateElement("object");
						objectElement.SetAttribute("type", "0");
						objectElement.SetAttribute("name", factory.name);
						objectElement.SetAttribute("heading", "0");
						objectElement.SetAttribute("speed", "0");
						objectElement.SetAttribute("positionX", factory.positionY.ToString());
						objectElement.SetAttribute("positionY", factory.positionX.ToString());
						objectElement.SetAttribute("user", "");
						objectElement.SetAttribute("status", "0");
						objectElement.SetAttribute("timestamp", "");
	
				

						groupElement.AppendChild(objectElement);
					}

					i++;
				}
				docElement.AppendChild(groupElement);
			}


			
			if (singleAgents != null)
			{
				if (singleAgents.Tables[0].Rows.Count > 0)
				{
					XmlElement groupElement = xmlDoc.CreateElement("objectGroup");
					groupElement.SetAttribute("name", "Uppsamling");

					i = 0;
					while (i < singleAgents.Tables[0].Rows.Count)
					{
						Agent agent = agentsClass.getAgent(database, singleAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

						if ((agent.positionX > 0) && (agent.positionY > 0))
						{

							int status = 0;									
							if (agent.lastUpdated.AddMinutes(30) < DateTime.Now) status = 1;
							if (agent.lastUpdated.AddMinutes(60) < DateTime.Now) status = 2;
											
							string timeStamp = "";
							if (agent.lastUpdated < DateTime.Today) 
								timeStamp = agent.lastUpdated.ToString("yyyy-MM-dd");
							else
								timeStamp = agent.lastUpdated.ToString("HH:mm:ss");

							XmlElement objectElement = xmlDoc.CreateElement("object");
							objectElement.SetAttribute("type", "1");
							objectElement.SetAttribute("name", agent.code);
							objectElement.SetAttribute("heading", ((int)agent.heading).ToString());
							objectElement.SetAttribute("speed", ((int)agent.speed).ToString());
							objectElement.SetAttribute("positionX", agent.positionY.ToString());
							objectElement.SetAttribute("positionY", agent.positionX.ToString());
							objectElement.SetAttribute("user", agent.userName);
							objectElement.SetAttribute("status", status.ToString());
							objectElement.SetAttribute("timestamp", timeStamp);
							
							groupElement.AppendChild(objectElement);
						}

						i++;
					}
					docElement.AppendChild(groupElement);
				}
			}


			if (lineAgents != null)
			{
				if (lineAgents.Tables[0].Rows.Count > 0)
				{
					XmlElement groupElement = xmlDoc.CreateElement("objectGroup");
					groupElement.SetAttribute("name", "Linjetrafik");

					i = 0;
					while (i < lineAgents.Tables[0].Rows.Count)
					{
						Agent agent = agentsClass.getAgent(database, lineAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

						if ((agent.positionX > 0) && (agent.positionY > 0))
						{
							int status = 0;									
							if (agent.lastUpdated.AddMinutes(30) < DateTime.Now) status = 1;
							if (agent.lastUpdated.AddMinutes(60) < DateTime.Now) status = 2;
											
							string timeStamp = "";
							if (agent.lastUpdated < DateTime.Today) 
								timeStamp = agent.lastUpdated.ToString("yyyy-MM-dd");
							else
								timeStamp = agent.lastUpdated.ToString("HH:mm:ss");

							XmlElement objectElement = xmlDoc.CreateElement("object");
							objectElement.SetAttribute("type", "2");
							objectElement.SetAttribute("name", agent.code);
							objectElement.SetAttribute("heading", ((int)agent.heading).ToString());
							objectElement.SetAttribute("speed", ((int)agent.speed).ToString());
							objectElement.SetAttribute("positionX", agent.positionY.ToString());
							objectElement.SetAttribute("positionY", agent.positionX.ToString());
							objectElement.SetAttribute("user", agent.userName);
							objectElement.SetAttribute("status", status.ToString());
							objectElement.SetAttribute("timestamp", timeStamp);
				
							groupElement.AppendChild(objectElement);
						}

						i++;
					}
					docElement.AppendChild(groupElement);
				}
			}

			

			if (tankAgents != null)
			{
				if (tankAgents.Tables[0].Rows.Count > 0)
				{
					XmlElement groupElement = xmlDoc.CreateElement("objectGroup");
					groupElement.SetAttribute("name", "Biomal");

					i = 0;
					while (i < tankAgents.Tables[0].Rows.Count)
					{
						Agent agent = agentsClass.getAgent(database, tankAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

					
						if ((agent.positionX > 0) && (agent.positionY > 0))
						{
							int status = 0;									
							if (agent.lastUpdated.AddMinutes(30) < DateTime.Now) status = 1;
							if (agent.lastUpdated.AddMinutes(60) < DateTime.Now) status = 2;
											
							string timeStamp = "";
							if (agent.lastUpdated < DateTime.Today) 
								timeStamp = agent.lastUpdated.ToString("yyyy-MM-dd");
							else
								timeStamp = agent.lastUpdated.ToString("HH:mm:ss");

							XmlElement objectElement = xmlDoc.CreateElement("object");
							objectElement.SetAttribute("type", "3");
							objectElement.SetAttribute("name", agent.code);
							objectElement.SetAttribute("heading", ((int)agent.heading).ToString());
							objectElement.SetAttribute("speed", ((int)agent.speed).ToString());
							objectElement.SetAttribute("positionX", agent.positionY.ToString());
							objectElement.SetAttribute("positionY", agent.positionX.ToString());
							objectElement.SetAttribute("user", agent.userName);
							objectElement.SetAttribute("status", status.ToString());
							objectElement.SetAttribute("timestamp", timeStamp);
				
							groupElement.AppendChild(objectElement);
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
