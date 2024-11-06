using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class PhoneLogEntries
	{

		public PhoneLogEntries()
		{

		}

		public PhoneLogEntry getEntry(Database database, string userId, string callId)
		{
			PhoneLogEntry phoneLogEntry = null;
			
			SqlDataReader dataReader = database.query("SELECT [User ID], [Call ID], [Remote Number], [Received Date], [Received Time], [Picked Up Date], [Picked Up Time], [Finished Date], [Finished Time], [Status], [Transfered To Number], [Origin Type], [Origin No], [Name], [City], [Organization No], [Direction] FROM [Phone Log Entry] WHERE [User ID] = '"+userId+"' AND [Call ID] = '"+callId+"'");
			if (dataReader.Read())
			{
				phoneLogEntry = new PhoneLogEntry(dataReader);
			}
			
			dataReader.Close();
			
			return phoneLogEntry;
		}

		public PhoneLogEntry getEntry(Database database, string remoteNumber, DateTime receivedDate, string excludeUserId)
		{
			PhoneLogEntry phoneLogEntry = null;
			
			SqlDataReader dataReader = database.query("SELECT [User ID], [Call ID], [Remote Number], [Received Date], [Received Time], [Picked Up Date], [Picked Up Time], [Finished Date], [Finished Time], [Status], [Transfered To Number], [Origin Type], [Origin No], [Name], [City], [Organization No], [Direction] FROM [Phone Log Entry] WHERE [Remote Number] = '"+remoteNumber+"' AND [Picked Up Date] = '"+receivedDate.ToString("yyyy-MM-dd")+"' AND [User ID] <> '"+excludeUserId+"' AND [Direction] = 0");
			if (dataReader.Read())
			{
				phoneLogEntry = new PhoneLogEntry(dataReader);
			}
			
			dataReader.Close();
			
			return phoneLogEntry;
		}

		public DataSet getDataSet(Database database, string userId, DateTime fromDate, DateTime toDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [User ID], [Call ID], [Remote Number], [Received Date], [Received Time], [Picked Up Date], [Picked Up Time], [Finished Date], [Finished Time], [Status], [Transfered To Number], [Origin Type], [Origin No], [Name], [City], [Organization No], [Direction] FROM [Phone Log Entry] WHERE [User ID] = '"+userId+"' AND [Received Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Received Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' ORDER BY [Received Date] DESC, [Received Time] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "phoneLogEntry");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, string userId, DateTime fromDate, DateTime toDate, int status)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [User ID], [Call ID], [Remote Number], [Received Date], [Received Time], [Picked Up Date], [Picked Up Time], [Finished Date], [Finished Time], [Status], [Transfered To Number], [Origin Type], [Origin No], [Name], [City], [Organization No], [Direction] FROM [Phone Log Entry] WHERE [User ID] = '"+userId+"' AND [Received Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Received Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND [Status] = '"+status+"' ORDER BY [Received Date] DESC, [Received Time] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "phoneLogEntry");
			adapter.Dispose();

			return dataSet;

		}

	}
}
