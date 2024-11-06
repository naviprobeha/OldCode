using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Containers.
	/// </summary>
	public class Containers
	{
		private string searchContainerNo = "";
		private string searchContainerTypeCode = "";
		private string searchCurrentPositionType = "";
		private string searchCurrentPositionCode = "";

		public Containers()
		{

		}

		public Container getEntry(Database database, string no)
		{
			Container container = null;
			
			SqlDataReader dataReader = database.query("SELECT [No], [Description], [Container Type Code], [Current Location Type], [Current Location Code] FROM [Container] WHERE [No] = '"+no+"'");
			if (dataReader.Read())
			{
				container = new Container(dataReader);
			}
			
			dataReader.Close();
			return container;
		}

		public DataSet getDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Description], [Container Type Code], [Current Location Type], [Current Location Code] FROM [Container] ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "container");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getLocationDataSet(Database database, int type, string code)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Description], [Container Type Code], [Current Location Type], [Current Location Code] FROM [Container] WHERE [Current Location Type] = '"+type+"' AND [Current Location Code] = '"+code+"' ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "container");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSetEntry(Database database, string no)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Description], [Container Type Code], [Current Location Type], [Current Location Code] FROM [Container] WHERE [No] = '"+no+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "container");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getFullDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT c.[No], c.[Description], c.[Container Type Code], ct.[Weight], ct.[Volume], c.[Current Location Type], c.[Current Location Code], ct.[Unit Code] FROM [Container] c, [Container Type] ct WHERE c.[Container Type Code] = ct.[Code] "+this.searchContainerNo+this.searchContainerTypeCode+this.searchCurrentPositionType+this.searchCurrentPositionCode+" ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "container");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getFullLocationDataSet(Database database, int type, string code)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT c.[No], c.[Description], c.[Container Type Code], ct.[Weight], ct.[Volume], c.[Current Location Type], c.[Current Location Code], ct.[Unit Code] FROM [Container] c, [Container Type] ct WHERE c.[Container Type Code] = ct.[Code] AND [Current Location Type] = '"+type+"' AND [Current Location Code] = '"+code+"' ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "container");
			adapter.Dispose();

			return dataSet;

		}

		public void setSearchCriteria(string containerNo, string containerTypeCode, string currentPositionType, string currentPositionCode)
		{
			if (containerNo != "") this.searchContainerNo = " AND UPPER([No]) LIKE UPPER('%"+containerNo+"%')";
			if (containerTypeCode != "") this.searchContainerTypeCode = " AND UPPER([Container Type Code]) = UPPER('"+containerTypeCode+"')";
			if (currentPositionType != "") this.searchCurrentPositionType = " AND UPPER([Current Location Type]) = UPPER('"+currentPositionType+"')";
			if (currentPositionCode != "") this.searchCurrentPositionCode = " AND UPPER([Current Location Code]) = UPPER('"+currentPositionCode+"')";
		}

	}
}
