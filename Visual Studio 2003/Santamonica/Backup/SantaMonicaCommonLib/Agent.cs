using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class Agent
	{
		public string code;
		public string description;
		public bool enabled;
		public DateTime lastUpdated;
		public int status;
		public int type;
		public string agentStorageGroup;
		public string organizationNo;
		public bool autoPlanEnabled;

		public int positionX;
		public int positionY;
		public decimal speed;
		public decimal heading;
		public decimal height;
		public string userName;
		public string cloneToAgentCode;

		private string updateMethod;

		public Agent(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			code = dataReader.GetValue(0).ToString();
			description = dataReader.GetValue(1).ToString();

			enabled = false;
			if (dataReader.GetValue(2).ToString() == "1") enabled = true;
			
			DateTime lastUpdatedDate = dataReader.GetDateTime(3);
			DateTime lastUpdatedTime = dataReader.GetDateTime(4);
			this.lastUpdated = new DateTime(lastUpdatedDate.Year, lastUpdatedDate.Month, lastUpdatedDate.Day, lastUpdatedTime.Hour, lastUpdatedTime.Minute, lastUpdatedTime.Second);

			status = dataReader.GetInt32(5);
			type = dataReader.GetInt32(6);
			organizationNo = dataReader.GetValue(7).ToString();
			//noOfContainers = dataReader.GetInt32(8);
			
			positionX = dataReader.GetInt32(9);
			positionY = dataReader.GetInt32(10);
			speed = dataReader.GetDecimal(11);
			height = dataReader.GetDecimal(12);
			heading = dataReader.GetDecimal(13);

			userName = dataReader.GetValue(14).ToString();

			this.autoPlanEnabled = false;
			if (dataReader.GetValue(15).ToString() == "1") this.autoPlanEnabled = true;

			//this.volumeStorage = dataReader.GetInt32(16);

			this.agentStorageGroup = dataReader.GetValue(17).ToString();
			this.cloneToAgentCode = dataReader.GetValue(18).ToString();

			updateMethod = "";
		}

		public Agent(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			code = dataRow.ItemArray.GetValue(0).ToString();
			description = dataRow.ItemArray.GetValue(1).ToString();

			enabled = false;
			if (dataRow.ItemArray.GetValue(2).ToString() == "1") enabled = true;
			
			DateTime lastUpdatedDate = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());
			DateTime lastUpdatedTime = DateTime.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.lastUpdated = new DateTime(lastUpdatedDate.Year, lastUpdatedDate.Month, lastUpdatedDate.Day, lastUpdatedTime.Hour, lastUpdatedTime.Minute, lastUpdatedTime.Second);

			status = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
			type = int.Parse(dataRow.ItemArray.GetValue(6).ToString());
			organizationNo = dataRow.ItemArray.GetValue(7).ToString();
			//noOfContainers = int.Parse(dataRow.ItemArray.GetValue(8).ToString());
			
			positionX = int.Parse(dataRow.ItemArray.GetValue(9).ToString());
			positionY = int.Parse(dataRow.ItemArray.GetValue(10).ToString());
			speed = decimal.Parse(dataRow.ItemArray.GetValue(11).ToString());
			height = decimal.Parse(dataRow.ItemArray.GetValue(12).ToString());
			heading = decimal.Parse(dataRow.ItemArray.GetValue(13).ToString());

			userName = dataRow.ItemArray.GetValue(14).ToString();

			this.autoPlanEnabled = false;
			if (dataRow.ItemArray.GetValue(15).ToString() == "1") this.autoPlanEnabled = true;

			//this.volumeStorage = int.Parse(dataRow.ItemArray.GetValue(16).ToString());
			this.agentStorageGroup = dataRow.ItemArray.GetValue(17).ToString();
			this.cloneToAgentCode = dataRow.ItemArray.GetValue(18).ToString();

			updateMethod = "";
		}


		public string getOrganization(Database database)
		{		
			return organizationNo;
			
		}

		/*
		public int countContainers(Database database, DateTime shipDate)
		{
			LineJournals lineJournals = new LineJournals();
			return lineJournals.countContainers(database, this.code, shipDate);

		}
		*/

		public int countContainers(Database database, DateTime shipDate, int calculationType)
		{
			LineJournals lineJournals = new LineJournals();
			return lineJournals.countContainers(database, this.code, shipDate, calculationType);

		}

		public void save(Database database)
		{
			int enabledVal = 0;
			if (this.enabled) enabledVal = 1;

			int autoPlanEnabledValue = 0;
			if (this.autoPlanEnabled) autoPlanEnabledValue = 1;

			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Agent] WHERE [Code] = '"+code+"'");

				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Code] FROM [Agent] WHERE [Code] = '"+code+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Agent] SET [Description] = '"+description+"', [Enabled] = '"+enabledVal+"', [Last Updated Date] = '"+lastUpdated.ToString("yyyy-MM-dd")+"', [Last Updated Timestamp] = '"+lastUpdated.ToString("1754-01-01 HH:mm:ss")+"', [Status] = '"+status+"', [Type] = '"+type+"', [Organization No] = '"+organizationNo+"', [Position X] = '"+this.positionX+"', [Position Y] = '"+this.positionY+"', [Speed] = '"+speed+"', [Height] = '"+height+"', [Heading] = '"+heading+"', [User Name] = '"+this.userName+"', [Auto Plan Enabled] = '"+autoPlanEnabledValue+"', [Agent Storage Group] = '"+this.agentStorageGroup+"', [Clone To Agent Code] = '"+cloneToAgentCode+"' WHERE [Code] = '"+code+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Agent] ([Code], [Description], [Enabled], [Last Updated Date], [Last Updated Timestamp], [Status], [Type], [Organization No], [Position X], [Position Y], [Speed], [Height], [Heading], [User Name], [Auto Plan Enabled], [Agent Storage Group], [Clone To Agent Code]) VALUES ('"+code+"','"+description+"','"+enabledVal+"','"+lastUpdated.ToString("yyyy-MM-dd 00:00:00")+"', '"+lastUpdated.ToString("1754-01-01 HH:mm:ss")+"', '"+this.status+"', '"+this.type+"', '"+this.organizationNo+"', '"+positionX+"', '"+positionY+"', '"+speed+"', '"+height+"', '"+heading+"', '"+userName+"', '"+autoPlanEnabledValue+"', '"+this.agentStorageGroup+"', '"+this.cloneToAgentCode+"')");
					}

				}
			}
			catch(Exception e)
			{
				throw new Exception("Error on agent update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}

		public void delete(Database database)
		{
			updateMethod = "D";
			save(database);
		}

		public string getStatus()
		{
			if (this.status == 0) return "Utstämplad";
			if (this.status == 1) return "Instämplad";
			if (this.status == 2) return "Rast";

			return "";
		}

		public string getType()
		{
			if (type == 0) return "Uppsamlingsbil";
			if (type == 1) return "Linjebil";
			if (type == 2) return "Multibil";
			if (type == 3) return "Biomalbil";
			return "";
		}

		public AgentTransaction getCurrentAgentTransaction(Database database)
		{
			AgentTransactions agentTransactions = new AgentTransactions();
			return agentTransactions.getCurrentAgentTransaction(database, this.code);
		}

		public AgentTransaction getAgentTransaction(Database database, DateTime date)
		{
			AgentTransactions agentTransactions = new AgentTransactions();
			return agentTransactions.getAgentTransaction(database, this.code, date);
		}

		public int countShipments(Database database, DateTime startDate, DateTime endDate)
		{
			ShipmentHeaders shipmentHeaders = new ShipmentHeaders();
			return shipmentHeaders.countAgentShipments(database, this.code, startDate, endDate);

		}

		public int calcTripMeter(Database database, DateTime startDate, DateTime endDate)
		{
			int startTrip = 0;
			int endTrip = 0;

			AgentTransactions agentTransactions = new AgentTransactions();
			AgentTransaction firstAgentTransaction = agentTransactions.getLastAgentTransactions(database, this.code, startDate.AddDays(-1));
			
			if (firstAgentTransaction != null)
			{
				startTrip = firstAgentTransaction.tripMeter;
			}
			
			AgentTransaction lastAgentTransaction = agentTransactions.getLastAgentTransactions(database, this.code, endDate);

			if (lastAgentTransaction != null)
			{
				endTrip = lastAgentTransaction.tripMeter;
			}

		
			return endTrip - startTrip;

		}


		public int getNoOfContainers(Database database)
		{
			AgentStorageGroups agentStorageGroups = new AgentStorageGroups();
			AgentStorageGroup agentStorageGroup = agentStorageGroups.getEntry(database, this.agentStorageGroup);
			if (agentStorageGroup != null)
			{
				return agentStorageGroup.noOfContainers;
			}

			return 0;

		}

		public int getVolumeStorage(Database database)
		{
			AgentStorageGroups agentStorageGroups = new AgentStorageGroups();
			AgentStorageGroup agentStorageGroup = agentStorageGroups.getEntry(database, this.agentStorageGroup);
			if (agentStorageGroup != null)
			{
				return agentStorageGroup.volumeStorage;
			}

			return 0;

		}

		public int countFactoryOrders(Database database, DateTime shipDate)
		{
			FactoryOrders factoryOrders = new FactoryOrders();
			return factoryOrders.countAgentOrders(database, shipDate, this.code);
		}

		public bool containsMoreStorageThan(Database database, string storageGroup)
		{
			AgentStorageGroups agentStorageGroups = new AgentStorageGroups();
			AgentStorageGroup agentStorageGroup = agentStorageGroups.getEntry(database, storageGroup);
			if (agentStorageGroup != null)
			{
				if ((agentStorageGroup.noOfContainers <= this.getNoOfContainers(database)) && (agentStorageGroup.volumeStorage <= this.getVolumeStorage(database))) return true;
			}
			return false;

		}

		public string getAgentStorageGroupDescription(Database database)
		{
			AgentStorageGroups agentStorageGroups = new AgentStorageGroups();
			AgentStorageGroup agentStorageGroup = agentStorageGroups.getEntry(database, this.agentStorageGroup);
			if (agentStorageGroup != null)
			{
				return agentStorageGroup.description;
			}

			return "";
		}

		public int countSynchEntries(Database database)
		{
			SynchronizationQueueEntries synchEntries = new SynchronizationQueueEntries(this.code);
			return synchEntries.getCount(database);
		}
	}
}
