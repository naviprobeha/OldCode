using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataMessages
	{
		private SmartDatabase smartDatabase;

		public DataMessages(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}

		public DataMessage getFirstMessage()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, organizationNo, fromName, message FROM message ORDER BY entryNo");

			if (dataReader.Read())
			{
				try
				{
					DataMessage dataMessage = new DataMessage(smartDatabase, dataReader.GetInt32(0), false);
					
					dataMessage.organizationNo = dataReader.GetValue(1).ToString();
					dataMessage.fromName = dataReader.GetValue(2).ToString();
					dataMessage.message = dataReader.GetValue(3).ToString();
					
					dataReader.Dispose();

					return dataMessage;
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();
			return null;
			
		}


	}
}
