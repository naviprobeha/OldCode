using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class ScheduledShipOrders
	{

		public ScheduledShipOrders()
		{
			//
			// TODO: Add constructor logic here
			//			
		}

	
		public ScheduledShipOrder getEntry(Database database, string organizationNo, string no)
		{
			ScheduledShipOrder shipOrder = null;
			
			SqlDataReader dataReader = database.query("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Comments], [Position X], [Position Y], [Bill-to Customer No], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Direction Comment], [Direction Comment 2], [Payment Type], [Monday], [Tuesday], [Wednesday], [Thursday], [Friday], [Saturday], [Sunday], [Week Type] FROM [Scheduled Ship Order] WHERE [Organization No] = '"+organizationNo+"' AND [Entry No] = '"+no+"'");
			if (dataReader.Read())
			{
				shipOrder = new ScheduledShipOrder(dataReader);
			}
			
			dataReader.Close();
			return shipOrder;
		}


		public DataSet getDataSet(Database database, string organizationNo, string sorting)
		{
			string sortQuery = "";
			if ((sorting != null) && (sorting != "")) sortQuery = "ORDER BY ["+sorting+"]";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Phone No], [Cell Phone No], [Comments], [Position X], [Position Y], [Bill-to Customer No], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Direction Comment], [Direction Comment 2], [Payment Type] FROM [Scheduled Ship Order] WHERE [Organization No] = '"+organizationNo+"' "+sortQuery);

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;


		}

		public DataSet getDataSetByWeekday(Database database, string organizationNo, DayOfWeek weekDay, int weekNo)
		{
			string dayFilter = "";

			if (weekDay == DayOfWeek.Monday) dayFilter = " AND [Monday] = 1";
			if (weekDay == DayOfWeek.Tuesday) dayFilter = " AND [Tuesday] = 1";
			if (weekDay == DayOfWeek.Wednesday) dayFilter = " AND [Wednesday] = 1";
			if (weekDay == DayOfWeek.Thursday) dayFilter = " AND [Thursday] = 1";
			if (weekDay == DayOfWeek.Friday) dayFilter = " AND [Friday] = 1";
			if (weekDay == DayOfWeek.Saturday) dayFilter = " AND [Saturday] = 1";
			if (weekDay == DayOfWeek.Sunday) dayFilter = " AND [Sunday] = 1";

			string weekFilter = "";
			if ((weekNo % 2) == 0)
			{
				weekFilter = " AND ([Week Type] = '0' OR [Week Type] = '1')";
			}
			else
			{
				weekFilter = " AND ([Week Type] = '0' OR [Week Type] = '2')";
			}

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Phone No], [Cell Phone No], [Comments], [Position X], [Position Y], [Bill-to Customer No], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Direction Comment], [Direction Comment 2], [Payment Type] FROM [Scheduled Ship Order] WHERE [Organization No] = '"+organizationNo+"'"+dayFilter+weekFilter);

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;


		}
	

		public void createOrders(Database database, string organizationNo, DateTime shipDate)
		{
			System.Globalization.CultureInfo myCI = new System.Globalization.CultureInfo("sv-SE");
			int weekNo = myCI.Calendar.GetWeekOfYear(shipDate, System.Globalization.CalendarWeekRule.FirstFourDayWeek, System.DayOfWeek.Monday);


			DataSet scheduledShipOrderDataSet = getDataSetByWeekday(database, organizationNo, shipDate.DayOfWeek, weekNo);

			int i = 0;
			while(i < scheduledShipOrderDataSet.Tables[0].Rows.Count)
			{
				ScheduledShipOrder scheduledShipOrder = getEntry(database, organizationNo, scheduledShipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
				ShipOrder shipOrder = new ShipOrder(scheduledShipOrder, shipDate);

				Customers customers = new Customers();
				Customer customer = customers.getEntry(database, scheduledShipOrder.organizationNo, scheduledShipOrder.customerNo);
				if (customer != null)
				{
					shipOrder.productionSite = customer.productionSite;
				}

				shipOrder.save(database);

				i++;
			}

		}
	}
}
