using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Container.
	/// </summary>
	public class Container
	{

		public string no;
		public string description;
		public string containerTypeCode;
		public int currentLocationType;
		public string currentLocationCode;

		public string updateMethod;

		public Container(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.no = dataReader.GetValue(0).ToString();
			this.description = dataReader.GetValue(1).ToString();
			this.containerTypeCode = dataReader.GetValue(2).ToString();
			this.currentLocationType = dataReader.GetInt32(3);
			this.currentLocationCode = dataReader.GetValue(4).ToString();
		}

		public Container()
		{
			this.no = "";
			this.containerTypeCode = "";
		}

		public void save(Database database)
		{
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();

			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Container] WHERE [No] = '"+no+"'");
					synchQueue.enqueueContainerAgents(database, 12, this.no, 2);
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [No] FROM [Container] WHERE [No] = '"+no+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Container] SET [Description] = '"+description+"', [Container Type Code] = '"+containerTypeCode+"', [Current Location Type] = '"+this.currentLocationType+"', [Current Location Code] = '"+this.currentLocationCode+"' WHERE [No] = '"+no+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Container] ([No], [Description], [Container Type Code], [Current Location Type], [Current Location Code]) VALUES ('"+no+"','"+this.description+"','"+this.containerTypeCode+"','"+this.currentLocationType+"', '"+this.currentLocationCode+"')");
					}
					synchQueue.enqueueContainerAgents(database, 12, this.no, 0);

				}
			}
			catch(Exception e)
			{
					
				throw new Exception("Error on container update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}

		public string getLocationType()
		{
			if (this.currentLocationType == 0) return "Bil";
			if (this.currentLocationType == 1) return "Kund";
			if (this.currentLocationType == 2) return "Fabrik";
			return "";
		}

		public string getLocationName(Database database)
		{
			if (this.currentLocationType == 0)
			{
				return this.currentLocationCode;	
			}
			if (this.currentLocationType == 1)
			{
				ShippingCustomers shippingCustomers = new ShippingCustomers();
				ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, this.currentLocationCode);
				if (shippingCustomer != null) return shippingCustomer.name+", "+shippingCustomer.city;
				return "";
			}
			if (this.currentLocationType == 2)
			{
				return this.currentLocationCode;
			}
			return "";
		}

		public void delete(Database database)
		{
			this.updateMethod = "D";
			this.save(database);
		}
	}
}
