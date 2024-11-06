using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Messages.
	/// </summary>
	public class Messages
	{
		public Messages()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getUnReadDataSet(Database database, string organizationNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT m.[Organization No], m.[Entry No], m.[From Name], m.[Message] FROM [Message] m LEFT JOIN [Message Agent] ma ON m.[Organization No] = ma.[Organization No] AND m.[Entry No] = ma.[Message Entry No] WHERE m.[Organization No] = '"+organizationNo+"' AND ma.[Status] < 2 ORDER BY m.[Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "message");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getUnAssignedDataSet(Database database, string organizationNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT m.[Organization No], m.[Entry No], m.[From Name], m.[Message] FROM [Message] m LEFT JOIN [Message Agent] ma ON m.[Organization No] = ma.[Organization No] AND m.[Entry No] = ma.[Message Entry No] WHERE m.[Organization No] = '"+organizationNo+"' AND ma.[Status] is null ORDER BY m.[Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "message");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getReadDataSet(Database database, string organizationNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT m.[Organization No], m.[Entry No], m.[From Name], m.[Message] FROM [Message] m LEFT JOIN [Message Agent] ma ON m.[Organization No] = ma.[Organization No] AND m.[Entry No] = ma.[Message Entry No] WHERE m.[Organization No] = '"+organizationNo+"' AND ma.[Status] = 2 ORDER BY m.[Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "message");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getActiveDataSet(Database database, string organizationNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Entry No], [From Name], [Message] FROM [Message] WHERE [Organization No] = '"+organizationNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "message");
			adapter.Dispose();

			return dataSet;

		}

		public Message getEntry(Database database, string organizationNo, string entryNo)
		{
			SqlDataReader dataReader = database.query("SELECT [Organization No], [Entry No], [From Name], [Message] FROM [Message] WHERE [Organization No] = '"+organizationNo+"' AND [Entry No] = '"+entryNo+"'");
			
			Message message = null;

			if (dataReader.Read())
			{
				message = new Message(dataReader);
			}
			dataReader.Close();

			return message;


		}

		public DataSet getDataSetEntry(Database database, string entryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Entry No], [From Name], [Message] FROM [Message] WHERE [Entry No] = '"+entryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "message");
			adapter.Dispose();

			return dataSet;

		}

		public void cleanUp(Database database)
		{
			database.nonQuery("DELETE FROM [Message] WHERE [Entry No] IN (SELECT [Message Entry No] FROM [Message Agent] WHERE [Acknowledged Date] < '"+DateTime.Today.AddMonths(-1)+"')");
			database.nonQuery("DELETE FROM [Message Agent] WHERE [Acknowledged Date] < '"+DateTime.Today.AddMonths(-1)+"')");

		}

	}
}
