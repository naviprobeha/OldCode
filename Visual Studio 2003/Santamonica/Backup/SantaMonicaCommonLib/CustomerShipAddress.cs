using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for CustomerShipAddress.
	/// </summary>
	public class CustomerShipAddress
	{

		public string organizationNo;
		public string customerNo;
		public string entryNo;

		public string name;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;
		public string contactName;
		public string phoneNo;
		public string productionSite;

		public int positionX;
		public int positionY;

		public string directionComment;
		public string directionComment2;

		private string updateMethod;

		public CustomerShipAddress(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.organizationNo = dataReader.GetValue(0).ToString();
			this.customerNo = dataReader.GetValue(1).ToString();
			this.entryNo = dataReader.GetInt32(2).ToString();
			this.name = dataReader.GetValue(3).ToString();
			this.address = dataReader.GetValue(4).ToString();
			this.address2 = dataReader.GetValue(5).ToString();
			this.postCode = dataReader.GetValue(6).ToString();
			this.city = dataReader.GetValue(7).ToString();
			this.countryCode = dataReader.GetValue(8).ToString();
			this.contactName = dataReader.GetValue(9).ToString();

			this.positionX = dataReader.GetInt32(10);
			this.positionY = dataReader.GetInt32(11);
	
			this.directionComment = dataReader.GetValue(12).ToString();
			this.directionComment2 = dataReader.GetValue(13).ToString();
			
			this.phoneNo = dataReader.GetValue(14).ToString();
			this.productionSite = dataReader.GetValue(15).ToString();
		}

		public CustomerShipAddress()
		{
		}

		public void save(Database database)
		{
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();


			if (updateMethod == "D")
			{
				database.nonQuery("DELETE FROM [Customer Ship Address] WHERE [Organization No] = '"+organizationNo+"' AND [Customer No] = '"+customerNo+"' AND [Entry No] = '"+entryNo+"'");
				synchQueue.enqueueAgentsInOrganization(database, organizationNo, Agents.TYPE_SINGLE, SynchronizationQueueEntries.SYNC_CUSTOMER_SHIP_ADDRESS, this.entryNo, 2);
			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Customer Ship Address] WHERE [Organization No] = '"+organizationNo+"' AND [Customer No] = '"+customerNo+"' AND [Entry No] = '"+entryNo+"'");

				if (dataReader.Read())
				{
					dataReader.Close();
					database.nonQuery("UPDATE [Customer Ship Address] SET [Name] = '"+name+"', [Address] = '"+address+"', [Address 2] = '"+address2+"', [Post Code] = '"+postCode+"', [City] = '"+city+"', [Country Code] = '"+countryCode+"', [Contact Name] = '"+contactName+"', [Position X] = '"+positionX+"', [Position Y] = '"+positionY+"', [Direction Comment] = '"+directionComment+"', [Direction Comment 2] = '"+directionComment2+"', [Phone No] = '"+this.phoneNo+"', [Production Site] = '"+this.productionSite+"'  WHERE [Organization No] = '"+organizationNo+"' AND [Customer No] = '"+customerNo+"' AND [Entry No] = '"+entryNo+"'");
				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Customer Ship Address] ([Organization No], [Customer No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Contact Name], [Position X], [Position Y], [Direction Comment], [Direction Comment 2], [Phone No], [Production Site]) VALUES ('"+organizationNo+"','"+customerNo+"','"+name+"','"+address+"','"+address2+"','"+postCode+"','"+city+"','"+countryCode+"','"+contactName+"','"+positionX+"','"+positionY+"','"+this.directionComment+"','"+this.directionComment2+"', '"+this.phoneNo+"', '"+this.productionSite+"')");

					this.entryNo = database.getInsertedSeqNo().ToString();
				}

				//synchQueue.enqueueAgentsInOrganization(database, organizationNo, Agents.TYPE_SINGLE, SynchronizationQueueEntries.SYNC_CUSTOMER_SHIP_ADDRESS, this.entryNo, 0);

			}
		}

		public void delete(Database database)
		{
			updateMethod = "D";
			save(database);
		}

	}
}
