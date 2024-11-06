using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataCustomer.
	/// </summary>
	public class DataContainerEntry
	{

		public int entryNo;
		public string containerNo;
		public int type;
		public DateTime entryDateTime;
		public int positionX;
		public int positionY;
		public DateTime estimatedArrivalTime;
		public string locationCode;
		public int locationType;
		public int documentType;
		public string documentNo;

		private string updateMethod;
		private SmartDatabase smartDatabase;


		public DataContainerEntry(SmartDatabase smartDatabase, int entryNo)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			this.entryDateTime = DateTime.Now;
			this.estimatedArrivalTime = DateTime.Parse("2001-01-01 00:00:00");
			this.documentType = 0;
			this.documentNo = "";
			this.entryNo = entryNo;
			getFromDb();
		}


		public DataContainerEntry(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			this.entryNo = 0;

			this.entryDateTime = DateTime.Now;
			this.estimatedArrivalTime = DateTime.Parse("2001-01-01 00:00:00");

		}
		

		public void commit()
		{
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM containerEntry WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM containerEntry WHERE entryNo = '"+entryNo+"'");
				}
				else
				{
					
					try
					{
						smartDatabase.nonQuery("UPDATE containerEntry SET containerNo = '"+containerNo+"', type = '"+type+"', entryDateTime = '"+entryDateTime.ToString("yyyy-MM-dd HH:mm:ss")+"', positionX = '"+this.positionX+"', positionY = '"+this.positionY+"', estimatedArrivalTime = '"+this.estimatedArrivalTime.ToString("yyyy-MM-dd HH:mm:ss")+"', locationCode = '"+this.locationCode+"', locationType = '"+locationType+"', documentType = '"+this.documentType+"', documentNo = '"+this.documentNo+"' WHERE entryNo = '"+entryNo+"'");
					}
					catch (SqlCeException e) 
					{
						smartDatabase.ShowErrors(e);
					}

					DataSyncActions dataSyncActions = new DataSyncActions(smartDatabase);
					dataSyncActions.addSyncAction(6, 0, this.entryNo.ToString());
					
				}
			}
			else
			{
				try
				{
					smartDatabase.nonQuery("INSERT INTO containerEntry (containerNo, type, entryDateTime, positionX, positionY, estimatedArrivalTime, locationCode, locationType, documentType, documentNo) VALUES ('"+containerNo+"', '"+type+"', '"+entryDateTime.ToString("yyyy-MM-dd HH:mm:ss")+"', '"+positionX+"', '"+positionY+"', '"+estimatedArrivalTime.ToString("yyyy-MM-dd HH:mm:ss")+"', '"+this.locationCode+"', '"+locationType+"', '"+documentType+"', '"+documentNo+"')");
					dataReader = smartDatabase.query("SELECT entryNo FROM containerEntry WHERE entryNo = @@IDENTITY");
					if (dataReader.Read())
					{
						this.entryNo = dataReader.GetInt32(0);

					}
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}

				DataSyncActions dataSyncActions = new DataSyncActions(smartDatabase);
				dataSyncActions.addSyncAction(6, 0, this.entryNo.ToString());

			}
			dataReader.Dispose();	
		}


		public bool getFromDb()
		{
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, containerNo, type, entryDateTime, positionX, positionY, estimatedArrivalTime, locationCode, locationType, documentType, documentNo FROM containerEntry WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.containerNo = dataReader.GetValue(1).ToString();
					this.entryDateTime = dataReader.GetDateTime(2);				
					this.type = dataReader.GetInt32(3);
					this.positionX = dataReader.GetInt32(4);
					this.positionY = dataReader.GetInt32(5);
					this.estimatedArrivalTime = dataReader.GetDateTime(6);
					this.locationCode = dataReader.GetValue(7).ToString();
					this.locationType = dataReader.GetInt32(8);
					this.documentType = dataReader.GetInt32(9);
					this.documentNo = dataReader.GetValue(10).ToString();

					dataReader.Dispose();
					return true;
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}

			dataReader.Dispose();
			return false;
			
		}

		public void delete()
		{
			this.updateMethod = "D";
			commit();
		}

	}
}
