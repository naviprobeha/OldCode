using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for SMSMessages.
	/// </summary>
	public class SMSMessages
	{
		public SMSMessages()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getUnhandledDataSet(Database database, int type)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Type], [Phone No], [Message], [Handled], [Received Date], [Received Time] FROM [SMS Message] WHERE [Type] = '" + type + "' AND [Handled] = 0");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "smsMessage");
			adapter.Dispose();

			return dataSet;

		}
	}
}
