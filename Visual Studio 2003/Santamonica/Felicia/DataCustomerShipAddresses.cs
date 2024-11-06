using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataCustomers.
	/// </summary>
	public class DataCustomerShipAddresses
	{
		private SmartDatabase smartDatabase;

		public DataCustomerShipAddresses(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
		}

		public DataSet getDataSet(string customerNo)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, organizationNo, customerNo, name, address, address2, postCode, city, countryCode, contactName, positionX, positionY, directionComment, directionComment2, phoneNo, productionSite FROM customerShipAddress WHERE customerNo = '"+customerNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "customerShipAddress");
			adapter.Dispose();

			return dataSet;
		}

	}
}
