using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Organizations.
	/// </summary>
	public class ShippingCustomerOrganizations
	{
		public static int ORDER_TYPE_LINEORDER = 0;
		public static int ORDER_TYPE_FACTORYORDER = 1;

		public ShippingCustomerOrganizations()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getShippingCustomerDataSet(Database database, string shippingCustomerNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Shipping Customer No], [Type], [Code], [Sort Order], [Order Type] FROM [Shipping Customer Organization] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' ORDER BY [Sort Order]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shippingCustomerOrganization");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getShippingCustomerDataSet(Database database, string shippingCustomerNo, int orderType)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Shipping Customer No], [Type], [Code], [Sort Order], [Order Type] FROM [Shipping Customer Organization] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' AND [Order Type] = '"+orderType+"' ORDER BY [Sort Order]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shippingCustomerOrganization");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, int orderType)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Shipping Customer No], [Type], [Code], [Sort Order], [Order Type] FROM [Shipping Customer Organization] WHERE [Order Type] = '"+orderType+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shippingCustomerOrganization");
			adapter.Dispose();

			return dataSet;

		}

		public ShippingCustomerOrganization getShippingCustomerOrganization(Database database, string shippingCustomerNo, int orderType, int type, string code)
		{
			ShippingCustomerOrganization shippingCustomerOrganization = null;

			SqlDataReader dataReader = database.query("SELECT [Shipping Customer No], [Type], [Code], [Sort Order], [Order Type] FROM [Shipping Customer Organization] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' AND [Type] = '"+type+"' AND [Code] = '"+code+"' AND [Order Type] = '"+orderType+"'");
			if (dataReader.Read())
			{
				shippingCustomerOrganization = new ShippingCustomerOrganization(dataReader);

			}

			dataReader.Close();

			return shippingCustomerOrganization;

		}


		public bool checkType(Database database, string shippingCustomerNo, int orderType)
		{

			SqlDataReader dataReader = database.query("SELECT [Code] FROM [Shipping Customer Organization] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' AND [Order Type] = '"+orderType+"'");
			if (dataReader.Read())
			{
				dataReader.Close();
				return true;

			}
			dataReader.Close();

			return false;
		}
	}
}
