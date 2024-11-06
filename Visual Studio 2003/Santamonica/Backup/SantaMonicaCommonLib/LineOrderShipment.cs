using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLine.
	/// </summary>
	public class LineOrderShipment
	{

		public int entryNo;
		public int lineOrderEntryNo;
		public string containerNo;
		public string shipmentNo;

		private string updateMethod;

		public LineOrderShipment(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.lineOrderEntryNo = dataReader.GetInt32(1);
			this.containerNo = dataReader.GetValue(2).ToString();
			this.shipmentNo = dataReader.GetValue(3).ToString();
		}

		public LineOrderShipment(LineOrder lineOrder)
		{

			this.lineOrderEntryNo = lineOrder.entryNo;
		}

		public LineOrderShipment(Database database, DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//

			entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			lineOrderEntryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
			containerNo = dataRow.ItemArray.GetValue(2).ToString();
			shipmentNo = dataRow.ItemArray.GetValue(3).ToString();
		}


		public void save(Database database)
		{
			LineOrders lineOrders = new LineOrders();
			LineOrder lineOrder = lineOrders.getEntry(database, lineOrderEntryNo.ToString());

			SqlDataReader dataReader = database.query("SELECT * FROM [Line Order Shipment] WHERE [Entry No] = '"+entryNo+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					database.nonQuery("DELETE FROM [Line Order Shipment] WHERE [Entry No] = '"+entryNo+"'");
				}

				else
				{

					database.nonQuery("UPDATE [Line Order Shipment] SET [Container No] = '"+this.containerNo+"', [Shipment No] = '"+this.shipmentNo+"' WHERE [Entry No] = '"+this.entryNo+"' AND [Line Order Entry No] = '"+this.lineOrderEntryNo+"'");

				}
			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Line Order Shipment] ([Line Order Entry No], [Container No], [Shipment No]) VALUES ('"+this.lineOrderEntryNo+"','"+this.containerNo+"','"+this.shipmentNo+"')");
				entryNo = (int)database.getInsertedSeqNo();

			}

			lineOrder.updateDetails(database);
		}

		public void delete(Database database)
		{
			updateMethod = "D";
			save(database);
		}
	}
}
