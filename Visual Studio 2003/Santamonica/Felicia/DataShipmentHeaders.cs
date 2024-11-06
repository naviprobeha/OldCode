using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataShipmentHeaders.
	/// </summary>
	public class DataShipmentHeaders
	{
		private SmartDatabase smartDatabase;

		public DataShipmentHeaders(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
		}


		public DataSet getEntryDataSet(int entryNo)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, organizationNo, shipDate, customerNo, customerName, address, address2, postCode, city, countryCode, phoneNo, cellPhoneNo, productionSite, status, positionX, positionY, payment, dairyCode, dairyNo, reference, mobileUserName, containerNo, shipOrderEntryNo, customerShipAddressNo, shipName, shipAddress, shipAddress2, shipPostCode, shipCity, invoiceNo FROM shipmentHeader WHERE entryNo = '"+entryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipment");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(int status)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, '' as no, customerName, city, shipDate FROM shipmentHeader WHERE status = '"+status+"' ORDER BY entryNo DESC");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentHeader");
			adapter.Dispose();

			return dataSet;
		}


		public void deleteEntry(int entryNo)
		{
			smartDatabase.nonQuery("DELETE FROM shipmentHeader WHERE entryNo = '"+entryNo+"'");
			smartDatabase.nonQuery("DELETE FROM shipmentLine WHERE shipmentEntryNo = '"+entryNo+"'");
			smartDatabase.nonQuery("DELETE FROM shipmentLineId WHERE shipmentEntryNo = '"+entryNo+"'");

		}

		public void cleanUp()
		{
			DateTime fromDateTime = DateTime.Now.AddDays(-7);
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo FROM shipmentHeader WHERE status = '2' AND shipDate < '"+fromDateTime.ToString("yyyy-MM-dd")+"'");
			while(dataReader.Read())
			{
				deleteEntry(dataReader.GetInt32(0));	
			}
			dataReader.Close();
		}

		public void setStatus(int entryNo, int status)
		{
			smartDatabase.nonQuery("UPDATE shipmentHeader SET status = '"+status+"' WHERE entryNo = '"+entryNo+"'");

		}

	}
}
