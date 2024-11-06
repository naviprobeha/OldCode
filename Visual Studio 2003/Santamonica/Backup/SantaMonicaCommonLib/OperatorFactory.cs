using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class OperatorFactory
	{
		public string userId;
		public string factoryNo;

		public string updateMethod;

		public OperatorFactory(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.userId = dataReader.GetValue(0).ToString();
			this.factoryNo = dataReader.GetValue(1).ToString();

		}

		public void delete(Database database)
		{

			updateMethod = "D";
			save(database);
		}

		public void save(Database database)
		{
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();


			if (updateMethod == "D")
			{
				database.nonQuery("DELETE FROM [Operator Factory] WHERE [User ID] = '"+this.userId+"' AND [Factory No] = '"+this.factoryNo+"'");

			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [Factory No] FROM [Operator Factory] WHERE [User ID] = '"+this.userId+"' AND [Factory No] = '"+this.factoryNo+"'");

				if (dataReader.Read())
				{
					dataReader.Close();
					//database.nonQuery("UPDATE [Operator Factory] SET [] = '"+name+"' WHERE [Organization No] = '"+organizationNo+"' AND [Shipping Customer No] = '"+this.shippingCustomerNo+"'");
				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Operator Factory] ([User ID], [Factory No]) VALUES ('"+userId+"','"+factoryNo+"')");
				}


			}
		}

	}
}
