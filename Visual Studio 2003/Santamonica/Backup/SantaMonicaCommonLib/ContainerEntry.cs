using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Container.
	/// </summary>
	public class ContainerEntry
	{

		public int entryNo;
		public string containerNo;
		public int sourceType;
		public string sourceCode;
		public int type;
		public DateTime entryDateTime;
		public DateTime receivedDateTime;
		public int positionX;
		public int positionY;
		public DateTime estimatedArrivalDateTime;
		public string locationCode;
		public int locationType;
		public int documentType;
		public string documentNo;
		public int creatorType;
		public string creatorNo;

		private Database database;
		private string updateMethod;

		public ContainerEntry()
		{
			this.entryDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
			this.estimatedArrivalDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
			this.receivedDateTime = new DateTime(1753, 1, 1, 0, 0, 0);


		}

		public ContainerEntry(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.containerNo = dataReader.GetValue(1).ToString();
			this.sourceType = int.Parse(dataReader.GetValue(2).ToString());
			this.sourceCode = dataReader.GetValue(3).ToString();
			this.type = dataReader.GetInt32(4);
			
			DateTime entryDate = dataReader.GetDateTime(5);
			DateTime entryTime = dataReader.GetDateTime(6);
			this.entryDateTime = new DateTime(entryDate.Year, entryDate.Month, entryDate.Day, entryTime.Hour, entryTime.Minute, entryTime.Second);

			this.positionX = dataReader.GetInt32(7);
			this.positionY = dataReader.GetInt32(8);
			
			DateTime estimatedArrivalDate = dataReader.GetDateTime(9);
			DateTime estimatedArrivalTime = dataReader.GetDateTime(10);
			this.estimatedArrivalDateTime = new DateTime(estimatedArrivalDate.Year, estimatedArrivalDate.Month, estimatedArrivalDate.Day, estimatedArrivalTime.Hour, estimatedArrivalTime.Minute, estimatedArrivalTime.Second);

			DateTime receivedDate = dataReader.GetDateTime(11);
			DateTime receivedTime = dataReader.GetDateTime(12);
			this.receivedDateTime = new DateTime(receivedDate.Year, receivedDate.Month, receivedDate.Day, receivedTime.Hour, receivedTime.Minute, receivedTime.Second);

			this.locationCode = dataReader.GetValue(13).ToString();
			this.locationType = dataReader.GetInt32(14);

			this.documentType = dataReader.GetInt32(15);
			this.documentNo = dataReader.GetValue(16).ToString();

			this.creatorType = dataReader.GetInt32(17);
			this.creatorNo = dataReader.GetValue(18).ToString();

			updateMethod = "";

		}

		public ContainerEntry(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			this.containerNo = dataRow.ItemArray.GetValue(1).ToString();
			this.sourceType = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.sourceCode = dataRow.ItemArray.GetValue(3).ToString();
			this.type = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			
			DateTime entryDate = DateTime.Parse(dataRow.ItemArray.GetValue(5).ToString());
			DateTime entryTime = DateTime.Parse(dataRow.ItemArray.GetValue(6).ToString());
			this.entryDateTime = new DateTime(entryDate.Year, entryDate.Month, entryDate.Day, entryTime.Hour, entryTime.Minute, entryTime.Second);

			this.positionX = int.Parse(dataRow.ItemArray.GetValue(7).ToString());
			this.positionY = int.Parse(dataRow.ItemArray.GetValue(8).ToString());
			
			DateTime estimatedArrivalDate = DateTime.Parse(dataRow.ItemArray.GetValue(9).ToString());
			DateTime estimatedArrivalTime = DateTime.Parse(dataRow.ItemArray.GetValue(10).ToString());
			this.estimatedArrivalDateTime = new DateTime(estimatedArrivalDate.Year, estimatedArrivalDate.Month, estimatedArrivalDate.Day, estimatedArrivalTime.Hour, estimatedArrivalTime.Minute, estimatedArrivalTime.Second);

			DateTime receivedDate = DateTime.Parse(dataRow.ItemArray.GetValue(11).ToString());
			DateTime receivedTime = DateTime.Parse(dataRow.ItemArray.GetValue(12).ToString());
			this.receivedDateTime = new DateTime(receivedDate.Year, receivedDate.Month, receivedDate.Day, receivedTime.Hour, receivedTime.Minute, receivedTime.Second);

			this.locationCode = dataRow.ItemArray.GetValue(13).ToString();
			this.locationType = int.Parse(dataRow.ItemArray.GetValue(14).ToString());

			this.documentType = int.Parse(dataRow.ItemArray.GetValue(15).ToString());
			this.documentNo = dataRow.ItemArray.GetValue(16).ToString();

			this.creatorType = int.Parse(dataRow.ItemArray.GetValue(17).ToString());
			this.creatorNo = dataRow.ItemArray.GetValue(18).ToString();

			updateMethod = "";

		}

		public ContainerEntry(Database database, string agentCode, DataSet dataSet)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.sourceType = 0;
			this.sourceCode = agentCode;
			this.receivedDateTime = DateTime.Now;
			this.creatorType = 0;
			this.creatorNo = agentCode;

			fromDataSet(dataSet);
			if (this.estimatedArrivalDateTime.ToString("yyyy-MM-dd") == "2001-01-01") this.estimatedArrivalDateTime = DateTime.Parse("1753-01-01 00:00:00");
			if ((this.locationCode == "") && ((this.locationType == 0) || (this.locationType == 1)))
			{
				Agents agents = new Agents();
				Agent agent = agents.getAgent(database, agentCode);
				Organizations organizations = new Organizations();
				Organization organization = organizations.getOrganization(database, agent.organizationNo);
				this.locationType = 1;
				this.locationCode = organization.shippingCustomerNo;

			}

			save(database);

			updateMethod = "";

		}


		public void fromDataSet(DataSet dataset)
		{
			entryNo = 0;
			containerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			type = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
			entryDateTime = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
			positionX = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString());
			positionY = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString());
			this.estimatedArrivalDateTime = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(6).ToString());
			
			try
			{
				this.locationCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
			}
			catch(Exception e)
			{
				this.locationCode = "";
				if (e.Message != "") {}
			}

			try
			{
				this.locationType = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(8).ToString());
			}
			catch(Exception e)
			{
				if (this.locationCode != "")
					this.locationType = 1;
				else
					this.locationType = 0;

				if (e.Message != "") {}

			}

			try
			{
				this.documentType = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(9).ToString());
				this.documentNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
			}
			catch(Exception e)
			{
				if (e.Message != "") {}
			}


		}

		public void save(Database database)
		{
		
			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Container Entry] WHERE [Entry No] = '"+entryNo+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Container Entry] WHERE [Entry No] = '"+entryNo+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Container Entry] SET [Container No] = '"+containerNo+"', [Source Type] = '"+sourceType+"', [Source Code] = '"+sourceCode+"', [Type] = '"+type+"', [Entry Date] = '"+entryDateTime.ToString("yyyy-MM-dd 00:00:00")+"', [Entry Time] = '"+entryDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Position X] = '"+positionX+"', [Position Y] = '"+positionY+"', [Estimated Arrival Date] = '"+estimatedArrivalDateTime.ToString("yyyy-MM-dd 00:00:00")+"', [Estimated Arrival Time] = '"+estimatedArrivalDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Received Date] = '"+receivedDateTime.ToString("yyyy-MM-dd 00:00:00")+"', [Received Time] = '"+receivedDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Location Code] = '"+this.locationCode+"', [Location Type] = '"+this.locationType+"', [Document Type] = '"+this.documentType+"', [Document No] = '"+this.documentNo+"', [Creator Type] = '"+creatorType+"', [Creator No] = '"+creatorNo+"' WHERE [Entry No] = '"+entryNo+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Container Entry] ([Container No], [Source Type], [Source Code], [Type], [Entry Date], [Entry Time], [Position X], [Position Y], [Estimated Arrival Date], [Estimated Arrival Time], [Received Date], [Received Time], [Location Code], [Location Type], [Document Type], [Document No], [Creator Type], [Creator No]) VALUES ('"+this.containerNo+"','"+this.sourceType+"','"+this.sourceCode+"','"+this.type+"','"+entryDateTime.ToString("yyyy-MM-dd 00:00:00")+"','"+entryDateTime.ToString("1754-01-01 HH:mm:ss")+"','"+positionX+"','"+positionY+"','"+estimatedArrivalDateTime.ToString("yyyy-MM-dd 00:00:00")+"','"+estimatedArrivalDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+receivedDateTime.ToString("yyyy-MM-dd 00:00:00")+"', '"+receivedDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+this.locationCode+"', '"+this.locationType+"', '"+this.documentType+"', '"+this.documentNo+"', '"+creatorType+"', '"+creatorNo+"')");
					}
				}

				updateContainer(database);
			}
			catch(Exception e)
			{					
				throw new Exception("Error on container entry update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}


		private void updateContainer(Database database)
		{
			Containers containers = new Containers();
			Container container = containers.getEntry(database, this.containerNo);
			if (container != null)
			{
				if (type == 0)
				{
					if (sourceType == 0)
					{
						container.currentLocationType = 0;
						container.currentLocationCode = this.sourceCode;
					}
					if (sourceType == 1)
					{
						container.currentLocationType = 2;
						container.currentLocationCode = this.sourceCode;
					}
				}

				if (type == 1)
				{ 
					if (sourceType == 0)
					{
						if ((container.currentLocationType == 0) && (container.currentLocationCode == this.sourceCode))
						{
							container.currentLocationType = this.locationType;
							container.currentLocationCode = this.locationCode;
						}
					}
				}

				if (type == 4)
				{
					container.currentLocationType = this.locationType;
					container.currentLocationCode = this.locationCode;
				}
				container.save(database);

			}
		}

		public string getType()
		{
			if (this.type == 0) return "Lastad";
			if (this.type == 1) return "Lossad";
			if (this.type == 2) return "Ankomstrapporterad";
			if (this.type == 3) return "Service";
			if (this.type == 4) return "Manuell tilldelning";

			return "";
		}

		public string getSourceType()
		{
			if (this.sourceType == 0) return "Bil";
			if (this.sourceType == 1) return "Fabrik";
			if (this.sourceType == 2) return "Användare";

			return "";
		}

		public string getLocationType()
		{
			if (this.locationType == 0) return "Bil";
			if (this.locationType == 1) return "Kund";
			if (this.locationType == 2) return "Fabrik";
			if (this.locationType == 3) return "Grundinst.";

			return "";
		}

		public string getDocumentType()
		{
			if (this.documentType == 0) return "";
			if (this.documentType == 1) return "Linjeorder";
			if (this.documentType == 2) return "Rutt";

			return "";
		}

		public string getCreatorType()
		{
			if (this.documentType == 0) return "Bil";
			if (this.documentType == 1) return "Användare";

			return "";

		}

		public Container getContainer(Database database)
		{
			Containers containers = new Containers();
			return containers.getEntry(database, this.containerNo);

		}

		public string getLocationName(Database database)
		{
			if (locationType == 0)
			{
				return this.locationCode;
			}
			if (locationType == 1)
			{
				ShippingCustomers shippingCustomers = new ShippingCustomers();
				ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, this.locationCode);
				if (shippingCustomer != null)
				{
					return shippingCustomer.name;
				}
			}
			if (locationType == 2)
			{
				Factories factories = new Factories();
				Factory factory = factories.getEntry(database, this.locationCode);
				if (factory != null)
				{
					return factory.name;
				}
			}

			return "";
		}

	}
}
