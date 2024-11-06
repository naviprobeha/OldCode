using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ContainerTypes.
	/// </summary>
	public class ContainerEntries
	{

		public ContainerEntries()
		{

		}


		public DataSet getDocumentDataSet(Database database, int documentType, string documentNo)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Container No], [Source Type], [Source Code], [Type], [Entry Date], [Entry Time], [Position X], [Position Y], [Estimated Arrival Date], [Estimated Arrival Time], [Received Date], [Received Time], [Location Code], [Location Type], [Document Type], [Document No], [Creator Type], [Creator No] FROM [Container Entry] WHERE [Document Type] = '"+documentType+"' AND [Document No] = '"+documentNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerEntry");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDocumentDataSet(Database database, int documentType, string documentNo, int type)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Container No], [Source Type], [Source Code], [Type], [Entry Date], [Entry Time], [Position X], [Position Y], [Estimated Arrival Date], [Estimated Arrival Time], [Received Date], [Received Time], [Location Code], [Location Type], [Document Type], [Document No], [Creator Type], [Creator No] FROM [Container Entry] WHERE [Document Type] = '"+documentType+"' AND [Document No] = '"+documentNo+"' AND [Type] = '"+type+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerEntry");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDocumentDataSet(Database database, string containerNo, int documentType, string documentNo, int type)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Container No], [Source Type], [Source Code], [Type], [Entry Date], [Entry Time], [Position X], [Position Y], [Estimated Arrival Date], [Estimated Arrival Time], [Received Date], [Received Time], [Location Code], [Location Type], [Document Type], [Document No], [Creator Type], [Creator No] FROM [Container Entry] WHERE [Document Type] = '"+documentType+"' AND [Document No] = '"+documentNo+"' AND [Type] = '"+type+"' AND [Container No] = '"+containerNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerEntry");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getContainerDataSet(Database database, string containerNo)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT TOP 20 [Entry No], [Container No], [Source Type], [Source Code], [Type], [Entry Date], [Entry Time], [Position X], [Position Y], [Estimated Arrival Date], [Estimated Arrival Time], [Received Date], [Received Time], [Location Code], [Location Type], [Document Type], [Document No], [Creator Type], [Creator No] FROM [Container Entry] WHERE [Container No] = '"+containerNo+"' ORDER BY [Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerEntry");
			adapter.Dispose();

			return dataSet;

		}
	
		public DataSet getContainerDataSet(Database database, string containerNo, DateTime dateTime, int noOfRecords)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT TOP "+noOfRecords+" [Entry No], [Container No], [Source Type], [Source Code], [Type], [Entry Date], [Entry Time], [Position X], [Position Y], [Estimated Arrival Date], [Estimated Arrival Time], [Received Date], [Received Time], [Location Code], [Location Type], [Document Type], [Document No], [Creator Type], [Creator No] FROM [Container Entry] WHERE [Container No] = '"+containerNo+"' AND [Entry Date] <= '"+dateTime.ToString("yyyy-MM-dd")+"' ORDER BY [Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerEntry");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getAgentDataSet(Database database, string agentCode, DateTime dateTime, int noOfRecords)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT TOP "+noOfRecords+" [Entry No], [Container No], [Source Type], [Source Code], [Type], [Entry Date], [Entry Time], [Position X], [Position Y], [Estimated Arrival Date], [Estimated Arrival Time], [Received Date], [Received Time], [Location Code], [Location Type], [Document Type], [Document No], [Creator Type], [Creator No] FROM [Container Entry] WHERE [Source Type] = '' AND [Source Code] = '"+agentCode+"' AND [Entry Date] <= '"+dateTime.ToString("yyyy-MM-dd")+"' ORDER BY [Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerEntry");
			adapter.Dispose();

			return dataSet;

		}



		public DataSet getServiceDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT e.[Entry No], e.[Container No], e.[Source Type], e.[Source Code], e.Type, e.[Entry Date], e.[Entry Time], e.[Position X], e.[Position Y], e.[Estimated Arrival Date], e.[Estimated Arrival Time], e.[Received Date], e.[Received Time], e.[Location Code], e.[Location Type], e.[Document Type], e.[Document No], e.[Creator Type], e.[Creator No] FROM [Container Entry] AS e LEFT OUTER JOIN [Container Link Entry] AS s ON e.[Entry No] = s.[Container Entry No] WHERE (e.Type = 3) AND (s.[Container Service Entry No] IS NULL) ORDER BY e.[Entry No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerEntry");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getServiceDataSet(Database database, string containerNo)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT e.[Entry No], e.[Container No], e.[Source Type], e.[Source Code], e.Type, e.[Entry Date], e.[Entry Time], e.[Position X], e.[Position Y], e.[Estimated Arrival Date], e.[Estimated Arrival Time], e.[Received Date], e.[Received Time], e.[Location Code], e.[Location Type], e.[Document Type], e.[Document No], e.[Creator Type], e.[Creator No] FROM [Container Entry] AS e LEFT OUTER JOIN [Container Link Entry] AS s ON e.[Entry No] = s.[Container Entry No] WHERE (e.Type = 3) AND (s.[Container Service Entry No] IS NULL) AND e.[Container No] = '"+containerNo+"' ORDER BY e.[Entry No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerEntry");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getTypeDataSet(Database database, int type, string containerNo, DateTime dateTime)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Container No], [Source Type], [Source Code], [Type], [Entry Date], [Entry Time], [Position X], [Position Y], [Estimated Arrival Date], [Estimated Arrival Time], [Received Date], [Received Time], [Location Code], [Location Type], [Document Type], [Document No], [Creator Type], [Creator No] FROM [Container Entry] WHERE [Type] = '"+type+"' AND [Container No] = '"+containerNo+"' AND [Entry Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' ORDER BY [Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerEntry");
			adapter.Dispose();

			return dataSet;

		}

	
	}
}
