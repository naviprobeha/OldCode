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
	/// Summary description for linejournals.
	/// </summary>
	public class consumerInventory_generate : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Consumer currentConsumer;
		protected DateTime currentDateTime;
		protected ConsumerInventory currentConsumerInventory;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "consumerInventory.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			Consumers consumers = new Consumers();
			currentConsumer = consumers.getEntry(database, Request["consumerNo"]);

			currentDateTime = DateTime.Parse(Request["date"]+" "+Request["hour"]+":00:00");

			ConsumerInventories consumerInventories = new ConsumerInventories();
			
			currentConsumerInventory = consumerInventories.getEntry(database, currentConsumer.no, currentDateTime);
			if (currentConsumerInventory == null)
			{
				currentConsumerInventory = new ConsumerInventory();
			}

			if (Request["command"] == "calcInventory")
			{
				this.recalculateInventories(database, currentConsumer.no, currentDateTime);

			}

			if (Request["command"] == "modify")
			{
				float inventoryValue = 0;
				try
				{
					inventoryValue = float.Parse(Request["inventory"]);
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
				}

			
				currentConsumerInventory.consumerNo = currentConsumer.no;
				currentConsumerInventory.date = currentDateTime;
				currentConsumerInventory.timeOfDay = new DateTime(1754, 1, 1, currentDateTime.Hour, 0, 0);
				currentConsumerInventory.inventory = inventoryValue;
				currentConsumerInventory.type = 0;
				currentConsumerInventory.save(database);

				consumerInventories.recalculateInventories(database, currentConsumer.no, currentDateTime);

				Response.Redirect("consumerInventory.aspx?consumerNo="+currentConsumer.no);
			}


		}

		public void recalculateInventories(Database database, string consumerNo, DateTime dateTime)
		{
			ConsumerInventories consumerInventories = new ConsumerInventories();
			DateTime currentDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0);
			ConsumerCapacities consumerCapacities = new ConsumerCapacities();
			ConsumerInventory consumerInventory = consumerInventories.findLastActualEntry(database, consumerNo, dateTime);

			Response.Write("Recalcing...");

			if (consumerInventory != null)
			{
				DateTime endingDateTime = currentDateTime;
				currentDateTime = new DateTime(consumerInventory.date.Year, consumerInventory.date.Month, consumerInventory.date.Day, consumerInventory.timeOfDay.Hour, 0, 0);
				Response.Write("Last inv date: "+consumerInventory.date.ToString("yyyy-MM-dd")+" "+consumerInventory.timeOfDay.ToString("HH:mm"));

				ConsumerInventory nextConsumerInventory = consumerInventories.findNextActualEntry(database, consumerNo, dateTime.AddHours(1));
				if (nextConsumerInventory != null)
				{
					Response.Write("Deleting some entries...");
					endingDateTime = new DateTime(nextConsumerInventory.date.Year, nextConsumerInventory.date.Month, nextConsumerInventory.date.Day, nextConsumerInventory.timeOfDay.Hour, 0, 0);
					consumerInventories.deleteInventory(database, consumerNo, currentDateTime, endingDateTime);
				}
				else
				{
					Response.Write("Deleting all entries...");
					consumerInventories.deleteInventory(database, consumerNo, currentDateTime, new DateTime(1753, 1, 1));

					FactoryOrders factoryOrders = new FactoryOrders();
					FactoryOrder factoryOrder = factoryOrders.findLastConsumerEntry(database, consumerNo);
					if (factoryOrder != null)
					{
						endingDateTime = factoryOrder.arrivalDateTime;
					}
					
				}

				Response.Write("Startdate: "+currentDateTime.ToString("yyyy-MM-dd HH:mm:ss")+", Endingdate: "+endingDateTime.ToString("yyyy-MM-dd HH:mm:ss")+"<br>");

				

				bool positiveInventory = false;
				if (consumerInventory.inventory > 0) positiveInventory = true;
				float inventory = consumerInventory.inventory;

				//throw new Exception("CurrDate: "+currentDateTime.ToString("yyyy-MM-dd HH:mm")+", EndingDate: "+endingDateTime.ToString("yyyy-MM-dd HH:mm")+", "+positiveInventory.ToString());
				while ((positiveInventory) || (currentDateTime < endingDateTime))
				{

					currentDateTime = currentDateTime.AddHours(1);

					if (!consumerCapacities.capacityExists(database, consumerNo, currentDateTime)) return;
					inventory = calcInventory(database, consumerNo, currentDateTime, inventory);

					if (inventory == 0)
					{
						positiveInventory = false;	
					}
					else
					{
						positiveInventory = true;
					}

					Response.Write(consumerNo+" "+currentDateTime.ToString("yyyy-MM-dd HH:mm:ss")+": "+inventory+"<br>");
				}
				
			
			}

		

		}

		private float calcInventory(Database database, string consumerNo, DateTime currentDateTime, float inventory)
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
				dateInventory.consumerNo = consumerNo;
				dateInventory.date = currentDateTime;
				dateInventory.timeOfDay = new DateTime(1754, 1, 1, currentDateTime.Hour, 0, 0);						
							
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

					if (inventory < 0) inventory = 0;
				}

				dateInventory.inventory = inventory;
				if (prevInventory != inventory)
				{
					dateInventory.save(database);
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
