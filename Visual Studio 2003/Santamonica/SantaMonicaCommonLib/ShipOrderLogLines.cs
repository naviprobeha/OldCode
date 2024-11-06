using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLines.
	/// </summary>
	public class ShipOrderLogLines
	{
		public ShipOrderLogLines()
		{
			
		}

		public DataSet getDataSet(Database database, int shipOrderEntryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Ship Order Entry No], [Date], [Time Of Day], [Source], [Text] FROM [Ship Order Log Line] WHERE [Ship Order Entry No] = '"+shipOrderEntryNo+"' ORDER BY [Entry No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrderLogLine");
			adapter.Dispose();

			return dataSet;
			
		}

		public void add(Database database, ShipOrder shipOrder, string source, string text)
		{
			ShipOrderLogLine shipOrderLogLine = new ShipOrderLogLine(shipOrder);
			shipOrderLogLine.date = DateTime.Today;
			shipOrderLogLine.timeOfDay = DateTime.Now;
			shipOrderLogLine.source = source;
			shipOrderLogLine.text = text;

			shipOrderLogLine.save(database);

		}
	}
}
