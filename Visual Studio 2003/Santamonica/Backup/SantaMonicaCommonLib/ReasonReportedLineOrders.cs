using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class ReasonReportedLineOrders
	{

		public ReasonReportedLineOrders()
		{

		}

		public ReasonReportedLineOrder getEntry(Database database, string reasonCode, int lineOrderEntryNo)
		{
			ReasonReportedLineOrder reasonReportedLineOrder = null;
			
			SqlDataReader dataReader = database.query("SELECT [Reason Code], [Line Order Entry No], [Entry Date], [Entry Time], [Operator No] FROM [Reason Reported Line Order] WHERE [Reason Code] = '"+reasonCode+"' AND [Line Order Entry No] = '"+lineOrderEntryNo+"'");
			if (dataReader.Read())
			{
				reasonReportedLineOrder = new ReasonReportedLineOrder(dataReader);
			}
			
			dataReader.Close();
			
			return reasonReportedLineOrder;
		}

		public DataSet getDataSet(Database database, string reasonCode)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Reason Code], [Line Order Entry No], [Entry Date], [Entry Time], [Operator No] FROM [Reason Reported Line Order] WHERE [Reason Code] = '"+reasonCode+"' ORDER BY [Reason Code]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "reason");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, int lineOrderEntryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Reason Code], [Line Order Entry No], [Entry Date], [Entry Time], [Operator No] FROM [Reason Reported Line Order] WHERE [Line Order Entry No] = '"+lineOrderEntryNo+"' ORDER BY [Reason Code]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "reason");
			adapter.Dispose();

			return dataSet;

		}
	}
}
