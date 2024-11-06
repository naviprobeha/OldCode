using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataSyncActions
	{
		private SmartDatabase smartDatabase;

		public DataSyncActions(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}

		public DataSyncAction getFirstSyncAction()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM syncAction ORDER BY type, primaryKey");

			DataSyncAction syncAction = null;

			if (dataReader.Read())
			{
				syncAction = new DataSyncAction(smartDatabase, dataReader);
			}

			dataReader.Dispose();
			return syncAction;
		}

		public bool syncActionExists(int type, int action, string primaryKey)
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM syncAction WHERE type = '"+type+"' AND action = '"+action+"' AND primaryKey = '"+primaryKey+"'");

			bool exists = false;

			if (dataReader.Read())
			{
				exists = true;
			}

			dataReader.Dispose();

			return exists;
		}
		public void addSyncAction(int type, int action, string primaryKey)
		{
			DataSyncAction dataSyncAction = new DataSyncAction(smartDatabase, 0);
			dataSyncAction.type = type;
			dataSyncAction.action = action;
			dataSyncAction.primaryKey = primaryKey;
			dataSyncAction.commit();
		}
	}
}