using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class ShippingCustomerUser
	{
		public string userId;
		public string shippingCustomerNo;
		public string password;
		public string name;

		public string updateMethod;

		public ShippingCustomerUser()
		{
			this.userId = "";

		}

		public ShippingCustomerUser(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.userId = dataReader.GetValue(0).ToString();
			this.shippingCustomerNo = dataReader.GetValue(1).ToString();
			this.password = dataReader.GetValue(2).ToString();
			this.name = dataReader.GetValue(3).ToString();

		}


		public void save(Database database)
		{

			if (updateMethod == "D")
			{
				database.nonQuery("DELETE FROM [Shipping Customer User] WHERE [User ID] = '"+this.userId+"'");
			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [User ID] FROM [Shipping Customer User] WHERE [User ID] = '"+this.userId+"'");

				if (dataReader.Read())
				{
					dataReader.Close();
					database.nonQuery("UPDATE [Shipping Customer User] SET [Shipping Customer No] = '"+shippingCustomerNo+"', [Password] = '"+password+"', [Name] = '"+name+"' WHERE [User ID] = '"+this.userId+"'");

				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Shipping Customer User] ([User ID], [Shipping Customer No], [Password], [Name]) VALUES ('"+userId+"','"+shippingCustomerNo+"','"+password+"','"+name+"')");
				}

			}
		}

		public void delete(Database database)
		{
			this.updateMethod = "D";
			save(database);
		}
	
	}
}
