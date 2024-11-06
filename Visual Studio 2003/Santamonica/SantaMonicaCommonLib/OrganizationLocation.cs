using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class OrganizationLocation
	{
		public string organizationNo;
		public string shippingCustomerNo;
		public string name;

		public string updateMethod;

		public OrganizationLocation(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.organizationNo = dataReader.GetValue(0).ToString();
			this.shippingCustomerNo = dataReader.GetValue(1).ToString();
			this.name = dataReader.GetValue(2).ToString();

		}

		public OrganizationLocation(string organizationNo)
		{
			this.organizationNo = organizationNo;
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
				database.nonQuery("DELETE FROM [Organization Location] WHERE [Organization No] = '"+this.organizationNo+"' AND [Shipping Customer No] = '"+this.shippingCustomerNo+"'");
				synchQueue.enqueueAgentsInOrganization(database, organizationNo, Agents.TYPE_SINGLE, SynchronizationQueueEntries.SYNC_ORGANIZATION_LOCATION, this.shippingCustomerNo, 2);

			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [Name] FROM [Organization Location] WHERE [Organization No] = '"+this.organizationNo+"' AND [Shipping Customer No] = '"+this.shippingCustomerNo+"'");

				if (dataReader.Read())
				{
					dataReader.Close();
					database.nonQuery("UPDATE [Organization Location] SET [Name] = '"+name+"' WHERE [Organization No] = '"+organizationNo+"' AND [Shipping Customer No] = '"+this.shippingCustomerNo+"'");
				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Organization Location] ([Organization No], [Shipping Customer No], [Name]) VALUES ('"+organizationNo+"','"+shippingCustomerNo+"','"+name+"')");
				}

				synchQueue.enqueueAgentsInOrganization(database, organizationNo, Agents.TYPE_SINGLE, SynchronizationQueueEntries.SYNC_ORGANIZATION_LOCATION, this.shippingCustomerNo, 0);

			}
		}

	}
}
