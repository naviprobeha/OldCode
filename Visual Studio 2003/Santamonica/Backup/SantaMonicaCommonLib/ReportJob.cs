using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Container.
	/// </summary>
	public class ReportJob
	{

		public string code;
		public string reportName;
		public string assembly;
		public string emailAddress;
		public DateTime initialSendDateTime;
		public int sendInterval;
		public DateTime lastSentDateTime;

		public string updateMethod;

		public ReportJob(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.code = dataReader.GetValue(0).ToString();
			this.reportName = dataReader.GetValue(1).ToString();
			this.emailAddress = dataReader.GetValue(2).ToString();
			this.initialSendDateTime = dataReader.GetDateTime(3);
			this.sendInterval = dataReader.GetInt32(4);
			this.lastSentDateTime = dataReader.GetDateTime(5);
			this.assembly = dataReader.GetValue(6).ToString();
		}

		public ReportJob(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.code = dataRow.ItemArray.GetValue(0).ToString();
			this.reportName = dataRow.ItemArray.GetValue(1).ToString();
			this.emailAddress = dataRow.ItemArray.GetValue(2).ToString();
			this.initialSendDateTime = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.sendInterval = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.lastSentDateTime = DateTime.Parse(dataRow.ItemArray.GetValue(5).ToString());
			this.assembly = dataRow.ItemArray.GetValue(6).ToString();
		}

		public ReportJob()
		{
			this.code = "";
		}

		public void save(Database database)
		{
			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Report Job] WHERE [Code] = '"+code+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Code] FROM [Report Job] WHERE [Code] = '"+code+"'");

					if (dataReader.Read())
					{
						dataReader.Close();

						database.nonQuery("UPDATE [Report Job] SET [Report Name] = '"+reportName+"', [E-mail Address] = '"+emailAddress+"', [Initial Send DateTime] = '"+this.initialSendDateTime.ToString("yyyy-MM-dd HH:mm:ss")+"', [Send Interval] = '"+this.sendInterval+"', [Last Sent DateTime] = '"+this.lastSentDateTime.ToString("yyyy-MM-dd HH:mm:ss")+"', [Assembly] = '"+this.assembly+"' WHERE [Code] = '"+code+"'");						
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Report Job] ([Code], [Report Name], [E-mail Address], [Initial Send DateTime], [Send Interval], [Last Sent DateTime], [Assembly]) VALUES ('"+code+"','"+this.reportName+"','"+this.emailAddress+"','"+this.initialSendDateTime.ToString("yyyy-MM-dd HH:mm:ss")+"', '"+this.sendInterval+"', '"+this.lastSentDateTime.ToString("yyyy-MM-dd HH:mm:ss")+"', '"+this.assembly+"')");
					}
				}
			}
			catch(Exception e)
			{
					
				throw new Exception("Error on report job update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}

		public void delete(Database database)
		{
			this.updateMethod = "D";
			this.save(database);
		}
	}
}
