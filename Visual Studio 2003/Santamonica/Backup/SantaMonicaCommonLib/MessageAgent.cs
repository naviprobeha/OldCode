using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for MessageAgent.
	/// </summary>
	public class MessageAgent
	{
		public string organizationNo;
		public int messageEntryNo;
		public string agentCode;
		public int status;
		public DateTime ackDateTime;

		public string updateMethod = "";

		public MessageAgent(string organizationNo)
		{
			//
			// TODO: Add constructor logic here
			//
			this.organizationNo = organizationNo;
		}

		public MessageAgent(SqlDataReader dataReader)
		{
			this.organizationNo = dataReader.GetValue(0).ToString();
			this.messageEntryNo = dataReader.GetInt32(1);
			this.agentCode = dataReader.GetValue(2).ToString();
			this.status = dataReader.GetInt32(3);

			string ackDate = dataReader.GetValue(4).ToString().Substring(0, 10);
			string ackTime = dataReader.GetValue(5).ToString().Substring(11);
			ackDateTime = DateTime.Parse(ackDate+" "+ackTime);

		}

		public void save(Database database)
		{
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();

			if (updateMethod == "D")
			{
				database.nonQuery("DELETE FROM [Message Agent] WHERE [Organization No] = '"+organizationNo+"' AND [Message Entry No] = '"+messageEntryNo+"' AND [Agent Code] = '"+agentCode+"'");
				synchQueue.enqueue(database, this.agentCode, 5, messageEntryNo.ToString(), 2);
			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [Message Entry No] FROM [Message Agent] WHERE [Organization No] = '"+organizationNo+"' AND [Message Entry No] = '"+messageEntryNo+"' AND [Agent Code] = '"+agentCode+"'");

				if (dataReader.Read())
				{
					dataReader.Close();
					database.nonQuery("UPDATE [Message Agent] SET [Status] = '"+status+"', [Acknowledged Date] = '"+ackDateTime.ToString("yyyy-MM-dd")+"', [Acknowledged Time] = '"+ackDateTime.ToString("1754-01-01 HH:mm:ss")+"' WHERE [Organization No] = '"+organizationNo+"' AND [Message Entry No] = '"+messageEntryNo+"' AND [Agent Code] = '"+agentCode+"'");
				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Message Agent] ([Organization No], [Message Entry No], [Agent Code], [Status], [Acknowledged Date], [Acknowledged Time]) VALUES ('"+organizationNo+"','"+messageEntryNo+"','"+agentCode+"','"+status+"','1753-01-01','1754-01-01 00:00:00')");
				}

				if ((this.ackDateTime.Year == 1753) && (this.status < 1)) synchQueue.enqueue(database, this.agentCode, 5, messageEntryNo.ToString(), 0);
			}
		}

		public void delete(Database database)
		{
			updateMethod = "D";
			save(database);
		}

		public string getStatusText()
		{
			if (status == 0) return "Uppköad";
			if (status == 1) return "Skickat";
			if (status == 2) return "Läst";
			return "Uppköad";
		}

		public string getStatusIcon()
		{
			if (status == 0) return "ind_red.gif";
			if (status == 1) return "ind_yellow.gif";
			if (status == 2) return "ind_green.gif";
			return "ind_red.gif";
		}

	}
}
