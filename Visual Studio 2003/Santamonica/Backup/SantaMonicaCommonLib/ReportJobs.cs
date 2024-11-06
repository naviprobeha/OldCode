using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ReportJobs.
	/// </summary>
	public class ReportJobs
	{
		public ReportJobs()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public ReportJob getEntry(Database database, string code)
		{
			ReportJob reportJob = null;
			
			SqlDataReader dataReader = database.query("SELECT [Code], [Report Name], [E-mail Address], [Initial Send DateTime], [Send Interval], [Last Sent DateTime], [Assembly] FROM [Report Job] WHERE [Code] = '"+code+"'");
			if (dataReader.Read())
			{
				reportJob = new ReportJob(dataReader);
			}
			
			dataReader.Close();
			return reportJob;
		}

		public DataSet getDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Report Name], [E-mail Address], [Initial Send DateTime], [Send Interval], [Last Sent DateTime], [Assembly] FROM [Report Job] ORDER BY [Code]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "reportJob");
			adapter.Dispose();

			return dataSet;

		}
	}
}
