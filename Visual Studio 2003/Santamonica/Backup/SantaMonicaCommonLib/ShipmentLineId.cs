using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLine.
	/// </summary>
	public class ShipmentLineId
	{

		private int entryNo;
		private int originalEntryNo;
		private string shipmentNo;
		private int shipmentLineEntryNo;
		private string unitId;
		private int type;
		private string reMarkUnitId;
		private bool bseTesting;
		private bool postMortem;

		private string agentCode;
		private string updateMethod;

		public ShipmentLineId(Database database, string agentCode, int shipmentLineEntryNo, DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			
			this.agentCode = agentCode;
			this.shipmentLineEntryNo = shipmentLineEntryNo;

			fromDataRow(dataRow);
			updateMethod = "";
			
			save(database);

			updateMethod = "";

		}

		public ShipmentLineId(DataRow dataRow)
		{
			fromDataRow(dataRow);
		}

		private void fromDataRow(DataRow dataRow)
		{
			entryNo = 0;
			type = 0;
			this.reMarkUnitId = "";
			this.bseTesting = false;
			this.postMortem = false;
			
			originalEntryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			shipmentNo = agentCode+"-"+dataRow.ItemArray.GetValue(1).ToString();
			unitId = dataRow.ItemArray.GetValue(3).ToString();

			if (dataRow.ItemArray.Length > 4)
			{
				type = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			}

			if (dataRow.ItemArray.Length > 5)
			{
				this.reMarkUnitId = dataRow.ItemArray.GetValue(5).ToString();
				if (dataRow.ItemArray.GetValue(6).ToString() == "1") this.bseTesting = true;
				if (dataRow.ItemArray.GetValue(7).ToString() == "1") this.postMortem = true;
			}

		}

		public XmlElement toDOM(XmlDocument xmlDoc)
		{
			
			XmlElement shipmentLineIdElement = xmlDoc.CreateElement("SHIPMENT_LINE_ID");

			XmlElement unitIdElement = xmlDoc.CreateElement("UNIT_ID");
			unitIdElement.AppendChild(xmlDoc.CreateTextNode(this.unitId));
			shipmentLineIdElement.AppendChild(unitIdElement);

			XmlElement typeElement = xmlDoc.CreateElement("TYPE");
			typeElement.AppendChild(xmlDoc.CreateTextNode(this.type.ToString()));
			shipmentLineIdElement.AppendChild(typeElement);


			return shipmentLineIdElement;
		}

		public void save(Database database)
		{
			int bseTestingVal = 0;
			if (this.bseTesting) bseTestingVal = 1;

			int postMortemVal = 0;
			if (this.postMortem) postMortemVal = 1;


			SqlDataReader dataReader = database.query("SELECT * FROM [Shipment Line ID] WHERE [Entry No] = '"+entryNo+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					database.nonQuery("DELETE FROM [Shipment Line ID] WHERE [Entry No] = '"+entryNo+"'");
				}

				else
				{

					database.nonQuery("UPDATE [Shipment Line ID] SET [Shipment No] = '"+this.shipmentNo+"', [Shipment Line Entry No] = '"+this.shipmentLineEntryNo+"', [Unit ID] = '"+this.unitId+"', [Type] = '"+this.type+"', [ReMark Unit ID] = '"+this.reMarkUnitId+"', [BSE Testing] = '"+bseTestingVal+"', [Post Mortem] = '"+postMortemVal+"', [Original Entry No] = '"+originalEntryNo+"' WHERE [Entry No] = '"+this.entryNo+"'");
				}
			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Shipment Line ID] ([Shipment No], [Shipment Line Entry No], [Unit ID], [Type], [ReMark Unit ID], [BSE Testing], [Post Mortem], [Original Entry No]) VALUES ('"+this.shipmentNo+"','"+this.shipmentLineEntryNo+"','"+this.unitId+"', '"+this.type+"', '"+this.reMarkUnitId+"', '"+bseTestingVal+"', '"+postMortemVal+"', '"+originalEntryNo+"')");
			}

		}
	}
}
