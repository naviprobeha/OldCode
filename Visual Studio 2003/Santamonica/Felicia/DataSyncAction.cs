using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for SyncAction.
	/// </summary>
	public class DataSyncAction
	{
		public int entryNo;
		public int type;
		public int action;
		public string primaryKey;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataSyncAction(SmartDatabase smartDatabase, int entryNo)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = entryNo;
			getFromDb();
		}

		public DataSyncAction(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
		{
			this.smartDatabase = smartDatabase;

			this.entryNo = dataReader.GetInt32(0);
			this.type = dataReader.GetInt32(1);
			this.action = dataReader.GetInt32(2);
			this.primaryKey = dataReader.GetValue(3).ToString();
		}

		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM syncAction WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM syncAction WHERE entryNo = '"+entryNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE syncAction SET type = '"+type+"', action = '"+action+"', primaryKey = '"+primaryKey+"' WHERE entryNo = '"+entryNo+"'");
					}
					catch (SqlCeException e) 
					{
						smartDatabase.ShowErrors(e);
					}
				}
			}
			else
			{
				try
				{
					smartDatabase.nonQuery("INSERT INTO syncAction (type, action, primaryKey) VALUES ('"+type+"','"+action+"','"+primaryKey+"')");
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();	
		}

		public bool getFromDb()
		{
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM syncAction WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.type = dataReader.GetInt32(1);
					this.action = dataReader.GetInt32(2);
					this.primaryKey = dataReader.GetValue(3).ToString();

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
