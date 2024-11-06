using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for UserOperator.
	/// </summary>
	public class AgentServiceSchedule
	{

		public string organizationNo;
		public string agentCode;
		public DateTime fromDate;
		public DateTime toDate;

		public AgentServiceSchedule(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.organizationNo = dataReader.GetValue(0).ToString();
			this.agentCode = dataReader.GetValue(1).ToString();
			this.fromDate = dataReader.GetDateTime(2);
			this.toDate = dataReader.GetDateTime(3);

		}

		public AgentServiceSchedule(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//

			this.organizationNo = dataRow.ItemArray.GetValue(0).ToString();
			this.agentCode = dataRow.ItemArray.GetValue(1).ToString();
			this.fromDate = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.toDate = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());

		}

		public void save(Database database)
		{

			SqlDataReader dataReader = database.query("SELECT [Organization No] FROM [Agent Service Schedule] WHERE [Organization No] = '"+organizationNo+"' AND [Agent Code] = '"+agentCode+"' AND [From Date] = '"+this.fromDate.ToString("yyyy-MM-dd")+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				database.nonQuery("UPDATE [Agent Service Schedule] SET [To Date] = '"+this.toDate.ToString("yyyy-MM-dd")+"' WHERE [Organization No] = '"+this.organizationNo+"' AND [Agent Code] = '"+this.agentCode+"' AND [From Date] = '"+this.fromDate.ToString("yyyy-MM-dd")+"'");

			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Agent Service Schedule] ([Organization No], [Agent Code], [From Date], [To Date]) VALUES ('"+organizationNo+"','"+agentCode+"','"+this.fromDate.ToString("yyyy-MM-dd")+"','"+this.toDate.ToString("yyyy-MM-dd")+"')");
			}

			
		}
	}
}
