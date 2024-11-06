using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class ShippingCustomerUsers
	{

		public ShippingCustomerUsers()
		{

		}

		public ShippingCustomerUser getEntry(Database database, string userId)
		{
			ShippingCustomerUser shippingCustomerUser = null;
			
			SqlDataReader dataReader = database.query("SELECT [User ID], [Shipping Customer No], [Password], [Name] FROM [Shipping Customer User] WHERE [User ID] = '"+userId+"'");
			if (dataReader.Read())
			{
				shippingCustomerUser = new ShippingCustomerUser(dataReader);
			}
			
			dataReader.Close();
			
			return shippingCustomerUser;
		}

		public ShippingCustomerUser getEntry(Database database, string userId, string password)
		{
			ShippingCustomerUser shippingCustomerUser = null;
			
			SqlDataReader dataReader = database.query("SELECT [User ID], [Shipping Customer No], [Password], [Name] FROM [Shipping Customer User] WHERE [User ID] = '"+userId+"' AND [Password] = '"+password+"'");
			if (dataReader.Read())
			{
				shippingCustomerUser = new ShippingCustomerUser(dataReader);
			}
			
			dataReader.Close();
			
			return shippingCustomerUser;
		}

		public DataSet getDataSet(Database database, string shippingCustomerNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [User ID], [Shipping Customer No], [Password], [Name] FROM [Shipping Customer User] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shippingCustomerUser");
			adapter.Dispose();

			return dataSet;

		}

		public bool userExists(Database database, string userId)
		{
			bool found = false;

			SqlDataReader dataReader = database.query("SELECT [User ID] FROM [Shipping Customer User] WHERE [User ID] = '"+userId+"'");
			if (dataReader.Read())
			{
				found = true;	
			}

			dataReader.Close();

			if (!found)
			{
				dataReader = database.query("SELECT [User ID] FROM [Operator] WHERE [User ID] = '"+userId+"'");
				if (dataReader.Read())
				{
					found = true;	
				}

				dataReader.Close();
			}

			return found;
		}
	}
}
