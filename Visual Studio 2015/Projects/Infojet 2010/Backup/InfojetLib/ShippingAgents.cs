using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ShippingAgents.
	/// </summary>
	public class ShippingAgents
	{
		public ShippingAgents()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getShippingAgents(Database database)
		{
			SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Code], [Name], [Internet Address] FROM ["+database.getTableName("Shipping Agent")+"] ORDER BY [Name]");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}
	}
}
