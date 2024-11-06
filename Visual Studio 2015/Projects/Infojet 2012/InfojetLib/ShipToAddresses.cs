using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ExtendedTextLines.
	/// </summary>
	public class ShipToAddresses
	{
		private Database database;

		public ShipToAddresses(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public DataSet getShipToAddresses(string customerNo)
		{
			SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Customer No_], [Code], [Name], [Name 2], [Address], [Address 2], [Post Code], [City], [Country_Region Code], [Contact], [Phone No_], [E-Mail], [Shipment Method Code], [Shipping Agent Code], [Shipping Agent Service Code] FROM ["+database.getTableName("Ship-to Address")+"] WHERE [Customer No_] = '"+customerNo+"' ORDER BY [Name]");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}

	}
}