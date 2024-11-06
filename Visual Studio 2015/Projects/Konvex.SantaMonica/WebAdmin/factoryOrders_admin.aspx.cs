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
	public class factoryorders_admin : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet activeFactoryOrders;
		protected DataSet organizationDataSet;
		protected FactoryOrders factoryOrders;
		protected DateTime shipDate;
		protected Agents agentsClass;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected DataSet activeAgents;

		protected DateTime fromDate;
		protected DateTime toDate;


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

			if (!currentOperator.checkSecurity(database, currentOrganization, "factoryOrders.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			shipDate = DateTime.Now;

			if (Session["toDate"] == null) Session["toDate"] = DateTime.Now;
			if (Session["noOfDaysBack"] == null) Session["noOfDaysBack"] = "0";
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

			fromDate = toDate.AddDays(int.Parse(Session["noOfDaysBack"].ToString())*-1);

			factoryOrders = new FactoryOrders();

			agentsClass = new Agents();
			Organizations organizationsClass = new Organizations();
			organizationDataSet = organizationsClass.getDataSet(database);

			if (currentOrganization.allowLineOrderSupervision)
			{
				if ((Request["organizationCode"] != "") && (Request["organizationCode"] != null))
				{
					string agentCode = Request["agent"];
					if (agentCode != "")
					{
						Agent agent = agentsClass.getAgent(database, agentCode);
						if (agent.organizationNo != Request["organizationCode"]) agentCode = "";
					}

					activeFactoryOrders = factoryOrders.getActiveOrganizationDataSet(database, Request["organizationCode"], agentCode, fromDate, toDate);			
					activeAgents = agentsClass.getDataSet(database, Request["organizationCode"], Agents.TYPE_TANK);
				}
				else
				{
					activeFactoryOrders = factoryOrders.getActiveDataSet(database, Request["agent"], fromDate, toDate);			
					activeAgents = agentsClass.getDataSet(database, Agents.TYPE_TANK);
				}
			}
			else
			{
				activeFactoryOrders = factoryOrders.getActiveOrganizationDataSet(database, currentOrganization.no, Request["agent"], fromDate, toDate);
				activeAgents = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_TANK);
			}


			if (Request["command"] == "createOrders") this.produceFactoryOrders();
			if (Request["command"] == "deleteAllOrders") factoryOrders.deleteAll(database);
		}	










		private void produceFactoryOrders()
		{
			ShippingCustomers shippingCustomers = new ShippingCustomers();
			ShippingCustomerOrganizations shippingCustomerOrganizations = new ShippingCustomerOrganizations();
			ConsumerRelations consumerRelations = new ConsumerRelations();	
			ConsumerInventories consumerInventories = new ConsumerInventories();
			ShippingCustomerSchedules shippingCustomerSchedules = new ShippingCustomerSchedules();
			FactoryOrders factoryOrders = new FactoryOrders();
			Categories categories = new Categories();

			DataSet factoryShippingCustomerDataSet = shippingCustomerOrganizations.getDataSet(database, 1);

			int i = 0;
			while (i < factoryShippingCustomerDataSet.Tables[0].Rows.Count)
			{
				ShippingCustomerOrganization shippingCustomerOrganization = new ShippingCustomerOrganization(factoryShippingCustomerDataSet.Tables[0].Rows[i]);
				
				FactoryOrder factoryOrder = factoryOrders.getLastEntry(database, shippingCustomerOrganization.shippingCustomerNo);
				DateTime lastDate = DateTime.Today;
				if (factoryOrder != null)
				{
					lastDate = factoryOrder.shipDate;
				}
				if (lastDate < DateTime.Today) lastDate = DateTime.Today;
				
				DateTime currentDate = lastDate;
				while (currentDate < DateTime.Today.AddDays(14))
				{
					if (shippingCustomerSchedules.checkSchedule(database, shippingCustomerOrganization.shippingCustomerNo, currentDate))
					{

						log("Schedule for customer found...", 0);
						ShippingCustomerSchedule shippingCustomerSchedule = shippingCustomerSchedules.findSchedule(database, shippingCustomerOrganization.shippingCustomerNo, currentDate);
						
						log("Schedule for customer: "+shippingCustomerSchedule.shippingCustomerNo+", Schedule no: "+shippingCustomerSchedule.entryNo, 0);

						if (!factoryOrders.orderExists(database, shippingCustomerSchedule.shippingCustomerNo, currentDate))
						{


							DataSet consumerRelationDataSet = consumerRelations.getDataSet(database, 0, shippingCustomerOrganization.shippingCustomerNo);

							int j = 0;
							bool orderCreated = false;
							while ((j < consumerRelationDataSet.Tables[0].Rows.Count) && (!orderCreated))
							{
								ConsumerRelation consumerRelation = new ConsumerRelation(consumerRelationDataSet.Tables[0].Rows[j]);

								log("ConsumerRelation: "+consumerRelation.consumerNo, 0);

								Consumer consumer = consumerRelation.getConsumer(database);
								if (consumer != null)
								{
									DateTime consumerInventoryDateTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, shippingCustomerSchedule.time.Hour, 0, 0).AddMinutes(consumerRelation.travelTime);

									log("Getting consumer inventory...", 0);
									ConsumerInventory consumerInventory = consumerInventories.getEntry(database, consumerRelation.consumerNo, consumerInventoryDateTime);
									if (consumerInventory == null)
									{
										consumerInventory = new ConsumerInventory();
										consumerInventory.inventory = 0;
									}

									if (consumerInventory.inventory <= consumer.inventoryShipmentPoint)
									{
										log("Inventory below shipment point: "+consumerInventory.inventory, 0);
										ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, consumerRelation.no);

										if (shippingCustomer != null)
										{
											ConsumerCapacities consumerCapacities = new ConsumerCapacities();
											if (consumerCapacities.capacityExists(database, consumer.no, consumerInventoryDateTime)) 
											{

												FactoryOrder newOrder = new FactoryOrder();

												log("Creating new order for consumer: "+consumer.name+", Customer: "+shippingCustomer.name, 0);
									
												if (shippingCustomerOrganization.type == 0)
												{
													newOrder.organizationNo = shippingCustomerOrganization.code;
												}
												else
												{
													newOrder.agentCode = shippingCustomerOrganization.code;
													Agents agents = new Agents();
													Agent agent = agents.getAgent(database, shippingCustomerOrganization.code);
													if (agent != null) newOrder.organizationNo = agent.organizationNo;
										
												}

												Category category = categories.getEntry(database, consumerRelation.categoryCode);
								

										
												newOrder.applyConsumer(consumer);
												newOrder.applyFactory(shippingCustomer);
												newOrder.shipDate = currentDate;
												newOrder.shipTime = shippingCustomerSchedule.time;
												newOrder.categoryCode = consumerRelation.categoryCode;
												if (category != null) newOrder.categoryDescription = category.description;
												newOrder.quantity = shippingCustomerSchedule.quantity;
												newOrder.createdByType = 0;
												newOrder.createdByCode = "AUTO";
												newOrder.type = 2;
												newOrder.save(database);

												orderCreated = true;
											}

										}
									}
								

								}
								j++;
							}
						}

					}
					log("Date finished.", 0);

					currentDate = currentDate.AddDays(1);
				}

				i++;
			}

		}

		private void log(string message, int type)
		{
			
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
