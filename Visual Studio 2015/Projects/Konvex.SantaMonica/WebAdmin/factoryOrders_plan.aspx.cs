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
	/// Summary description for lineorders_modify.
	/// </summary>
	public class factoryOrders_plan : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected FactoryOrder templateFactoryOrder;
		protected DataSet factoryDataSet;
		protected DataSet consumerDataSet;
		protected DataSet categoryDataSet;
		protected DataSet shippingCustomersDataSet;
		protected DataSet organizationDataSet;
		protected ArrayList planList;

		protected int mode;
		protected int noOfOrders;

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


			Factories factories = new Factories();
			Consumers consumers = new Consumers();
			ShippingCustomers shippingCustomers = new ShippingCustomers();
			Organizations organizations = new Organizations();
			Categories categories = new Categories();

			mode = 0;

			if (Request["command"] == "createPlan")
			{
				mode = 1;
				
				noOfOrders = int.Parse(Request["noOfOrders"]);

				templateFactoryOrder = new FactoryOrder();

				if (Request["creationDateYear"] != null) templateFactoryOrder.creationDate = new DateTime(int.Parse(Request["creationDateYear"]), 1, 1, 0,0,0);
				if (Request["creationDateMonth"] != null) templateFactoryOrder.creationDate = templateFactoryOrder.creationDate.AddMonths(int.Parse(Request["creationDateMonth"])-1);
				if (Request["creationDateDay"] != null) templateFactoryOrder.creationDate = templateFactoryOrder.creationDate.AddDays(int.Parse(Request["creationDateDay"])-1);

				//templateFactoryOrder.shipDate = startDate;

				ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, Request["factoryNo"]);
				if (shippingCustomer != null)
				{
					templateFactoryOrder.applyFactory(shippingCustomer);
				}

				Factory factory = factories.getEntry(database, Request["factoryNo"]);
				if (factory != null)
				{
					templateFactoryOrder.applyFactory(factory);
				}

				Consumer consumer = consumers.getEntry(database, Request["consumerNo"]);
				if (consumer != null)
				{
					templateFactoryOrder.applyConsumer(consumer);
				}

				templateFactoryOrder.organizationNo = Request["organizationNo"];
				
				Category category = categories.getEntry(database, Request["categoryCode"]);
				if (category != null)
				{
					templateFactoryOrder.categoryCode = category.code;
					templateFactoryOrder.categoryDescription = category.description;
				}

				try
				{
					templateFactoryOrder.quantity = float.Parse(Request["quantity"]);
				}
				catch(Exception)
				{}

				try
				{
					templateFactoryOrder.consumerLevel = float.Parse(Request["minimumQuantity"]);
				}
				catch(Exception)
				{}


				templateFactoryOrder.type = 1;
				templateFactoryOrder.createdByType = 1;
				templateFactoryOrder.createdByCode = currentOperator.userId;

				planList = new ArrayList();
				DateTime startDateTime = DateTime.Today;

				int i = 0;
				while (i < noOfOrders)
				{
					FactoryOrder factoryOrder = new FactoryOrder(templateFactoryOrder);
					factoryOrder.plannedArrivalDateTime = findNextArrivalDate(database, factoryOrder.consumerNo, planList, templateFactoryOrder.consumerLevel, startDateTime);
					startDateTime = factoryOrder.plannedArrivalDateTime;
					if (factoryOrder.plannedArrivalDateTime.Year == 1753) factoryOrder.type = 0;
					//Response.Write("Order: "+i+", Date: "+factoryOrder.plannedArrivalDateTime.ToString("yyyy-MM-dd HH:mm")+"<br>");
					
					ConsumerRelations consumerRelations = new ConsumerRelations();
					int factoryType = 0;
					if (factoryOrder.factoryType == 0) factoryType = 1;
					ConsumerRelation consumerRelation = consumerRelations.getEntry(database, factoryOrder.consumerNo, factoryType, factoryOrder.factoryNo);
					if (consumerRelation != null)
					{
						DateTime shipDateTime = factoryOrder.plannedArrivalDateTime.AddMinutes(consumerRelation.travelTime*-1);
						factoryOrder.shipDate = shipDateTime;
						factoryOrder.shipTime = shipDateTime;
					}

					planList.Add(factoryOrder);

					i++;
				}
			}

			if (Request["command"] == "createOrders")
			{
			
				noOfOrders = int.Parse(Request["noOfOrders"]);

				templateFactoryOrder = new FactoryOrder();

				if (Request["creationDate"] != null) templateFactoryOrder.creationDate = DateTime.Parse(Request["creationDate"]);

				Consumer consumer = consumers.getEntry(database, Request["consumerNo"]);
				if (consumer != null)
				{
					templateFactoryOrder.applyConsumer(consumer);
				}
		
				Category category = categories.getEntry(database, Request["categoryCode"]);
				if (category != null)
				{
					templateFactoryOrder.categoryCode = category.code;
					templateFactoryOrder.categoryDescription = category.description;
				}

				templateFactoryOrder.createdByType = 1;
				templateFactoryOrder.createdByCode = currentOperator.userId;


				int i = 0;
				while (i < noOfOrders)
				{
					if (Request["create_"+i] == "on")
					{
						FactoryOrder factoryOrder = new FactoryOrder(templateFactoryOrder);

						ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, Request["factoryNo_"+i]);
						if (shippingCustomer != null)
						{
							factoryOrder.applyFactory(shippingCustomer);
						}

						Factory factory = factories.getEntry(database, Request["factoryNo_"+i]);
						if (factory != null)
						{
							factoryOrder.applyFactory(factory);
						}

						factoryOrder.organizationNo = Request["organizationNo_"+i];
						factoryOrder.agentCode = "";

						if (Request["shipDate_"+i+"_Year"] != null) factoryOrder.shipDate = new DateTime(int.Parse(Request["shipDate_"+i+"_Year"]), 1, 1, 0,0,0);
						if (Request["shipDate_"+i+"_Month"] != null) factoryOrder.shipDate = factoryOrder.shipDate.AddMonths(int.Parse(Request["shipDate_"+i+"_Month"])-1);
						if (Request["shipDate_"+i+"_Day"] != null) factoryOrder.shipDate = factoryOrder.shipDate.AddDays(int.Parse(Request["shipDate_"+i+"_Day"])-1);
						if (Request["shipTimeHour_"+i] != null) factoryOrder.shipTime = new DateTime(1754, 1, 1, int.Parse(Request["shipTimeHour_"+i]), int.Parse(Request["shipTimeMinute_"+i]), 0);

						if (Request["plannedArrivalDate_"+i+"_Year"] != null) factoryOrder.plannedArrivalDateTime = new DateTime(int.Parse(Request["plannedArrivalDate_"+i+"_Year"]), 1, 1, 0,0,0);
						if (Request["plannedArrivalDate_"+i+"_Month"] != null) factoryOrder.plannedArrivalDateTime = factoryOrder.plannedArrivalDateTime.AddMonths(int.Parse(Request["plannedArrivalDate_"+i+"_Month"])-1);
						if (Request["plannedArrivalDate_"+i+"_Day"] != null) factoryOrder.plannedArrivalDateTime = factoryOrder.plannedArrivalDateTime.AddDays(int.Parse(Request["plannedArrivalDate_"+i+"_Day"])-1);
						if (Request["plannedArrivalTimeHour_"+i] != null) factoryOrder.plannedArrivalDateTime = factoryOrder.plannedArrivalDateTime.AddHours(int.Parse(Request["plannedArrivalTimeHour_"+i]));
						if (Request["plannedArrivalTimeMinute_"+i] != null) factoryOrder.plannedArrivalDateTime = factoryOrder.plannedArrivalDateTime.AddMinutes(int.Parse(Request["plannedArrivalTimeMinute_"+i]));

						factoryOrder.planningType = int.Parse(Request["planningType_"+i]);
						factoryOrder.quantity = int.Parse(Request["quantity_"+i]);

						factoryOrder.save(database, false);

						factoryOrder.updateArrivalTime(database);
					}

					i++;
				}

				Response.Redirect("factoryOrders.aspx");
			}


			factoryDataSet = factories.getDataSet(database);
			consumerDataSet = consumers.getDataSet(database);
			shippingCustomersDataSet = shippingCustomers.getDataSet(database, 1);
			organizationDataSet = organizations.getDataSet(database);		
			categoryDataSet = categories.getDataSet(database, true);


		}

		public DateTime findNextArrivalDate(Database database, string consumerNo, ArrayList plannedOrderList, float minimumLevel, DateTime startDateTime)
		{
			//DateTime startDateTime = DateTime.Today;

			ConsumerInventories consumerInventories = new ConsumerInventories();
			//DateTime currentDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, startDateTime.Hour, 0, 0);
			DateTime currentDateTime = startDateTime;
			ConsumerCapacities consumerCapacities = new ConsumerCapacities();
			ConsumerInventory consumerInventory = consumerInventories.findLastActualEntry(database, consumerNo, startDateTime);

			//Response.Write("**** New order ****<br>");
			//Response.Write("DateTime: "+currentDateTime.ToString("yyyy-MM-dd HH:mm")+"<br>");

			if (consumerInventory != null)
			{
				DateTime endingDateTime = currentDateTime;
				currentDateTime = new DateTime(consumerInventory.date.Year, consumerInventory.date.Month, consumerInventory.date.Day, consumerInventory.timeOfDay.Hour, 0, 0);
				

				ConsumerInventory nextConsumerInventory = consumerInventories.findNextActualEntry(database, consumerNo, startDateTime.AddHours(1));
				if (nextConsumerInventory != null)
				{
					endingDateTime = new DateTime(nextConsumerInventory.date.Year, nextConsumerInventory.date.Month, nextConsumerInventory.date.Day, nextConsumerInventory.timeOfDay.Hour, 0, 0);
				}
				else
				{
					FactoryOrders factoryOrders = new FactoryOrders();
					FactoryOrder factoryOrder = factoryOrders.findLastConsumerEntry(database, consumerNo);
					if (factoryOrder != null)
					{
						endingDateTime = factoryOrder.arrivalDateTime;
					}
					
				}

				

				bool positiveInventory = false;
				if (consumerInventory.inventory > 0) positiveInventory = true;
				float inventory = consumerInventory.inventory;

				//Response.Write("Last comsumer inventory: "+consumerInventory.inventory+"<br/>");
				//Response.Write("DateTime: "+currentDateTime.ToString("yyyy-MM-dd HH:mm")+"<br>");

				int i = 0;
				while (i < plannedOrderList.Count)
				{
					FactoryOrder factoryOrder = (FactoryOrder)plannedOrderList[i];
					//Response.Write("Adding factory order: "+factoryOrder.plannedArrivalDateTime.ToString("yyyy-MM-dd HH:mm")+": "+factoryOrder.quantity+"<br/>");

					inventory = inventory + factoryOrder.quantity;

					i++;
				}

				//Response.Write("Inbound inventory: "+inventory+"<br>");

				while ((positiveInventory) || (currentDateTime < endingDateTime))
				{

					currentDateTime = currentDateTime.AddHours(1);
					//Response.Write("Looping DateTime: "+currentDateTime.ToString("yyyy-MM-dd HH:mm")+", "+inventory+"<br>");

					if (!consumerCapacities.capacityExists(database, consumerNo, currentDateTime)) return new DateTime(1753, 1, 1, 0, 0, 0);
					//Response.Write("Capacity checked.<br>");
					inventory = calcInventory(database, consumerNo, currentDateTime, inventory, plannedOrderList);

					//Response.Write("Inventory: "+inventory+".<br>");

					if (inventory <= minimumLevel)
					{
						return currentDateTime;
					}
					else
					{
						positiveInventory = true;
					}

				}
				
			
			}

			return currentDateTime;		


		}

		private float calcInventory(Database database, string consumerNo, DateTime currentDateTime, float inventory, ArrayList plannedOrderList)
		{
			ConsumerInventories consumerInventories = new ConsumerInventories();
			float prevInventory = inventory;
			ConsumerCapacities consumerCapacities = new ConsumerCapacities();
			if (!consumerCapacities.capacityExists(database, consumerNo, currentDateTime)) return 0;

			ConsumerInventory dateInventory = consumerInventories.getEntry(database, consumerNo, currentDateTime);
			if (dateInventory == null)
			{
				dateInventory = new ConsumerInventory();
				dateInventory.type = 1;
						
			}
			if (dateInventory.type == 0)
			{
				return dateInventory.inventory;
			}
			else
			{
							
				ConsumerCapacity consumerCapacity = consumerCapacities.getEntry(database, consumerNo, currentDateTime);
				if (consumerCapacity != null)
				{
					if (consumerCapacity.actualCapacity > 0)
					{
						inventory = inventory - consumerCapacity.actualCapacity;
					}
					else
					{
						inventory = inventory - consumerCapacity.plannedCapacity;
					}

					FactoryOrders factoryOrders = new FactoryOrders();
					
					DataSet factoryOrderDataSet = factoryOrders.getConsumerEntries(database, consumerNo, currentDateTime);
					int i = 0;
					while (i < factoryOrderDataSet.Tables[0].Rows.Count)
					{
						FactoryOrder factoryOrder = new FactoryOrder(factoryOrderDataSet.Tables[0].Rows[i]);
						inventory = inventory + factoryOrder.quantity;
						i++;
					}

					/*
					i = 0;
					while (i < plannedOrderList.Count)
					{
						FactoryOrder factoryOrder = (FactoryOrder)plannedOrderList[i];
						if (factoryOrder.plannedArrivalDateTime.ToString("yyyy-MM-dd HH:mm") == currentDateTime.ToString("yyyy-MM-dd HH:mm"))
						{
							inventory = inventory + factoryOrder.quantity;
						}

						i++;
					}
					*/
					if (inventory < 0) inventory = 0;
				}

			}

			return inventory;
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
