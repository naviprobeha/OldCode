using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class AgentTransaction
	{
		public string agentCode;
		public int entryNo;
		public DateTime updatedDateTime;
		public int positionX;
		public int positionY;
		public decimal speed;
		public decimal height;
		public decimal heading;
		public int status;
		public string userName;
		public int tripMeter;

		public AgentTransaction(SqlDataReader dataReader)
		{
			this.agentCode = dataReader.GetValue(0).ToString();
			this.entryNo = dataReader.GetInt32(1);
			
			string updatedDate = dataReader.GetValue(2).ToString().Substring(0, 10);
			string updatedTime = dataReader.GetValue(3).ToString().Substring(11);
			updatedDateTime = DateTime.Parse(updatedDate+" "+updatedTime);

			this.positionX = dataReader.GetInt32(4);
			this.positionY = dataReader.GetInt32(5);
			this.speed = dataReader.GetDecimal(6);
			this.height = dataReader.GetDecimal(7);
			this.heading = dataReader.GetDecimal(8);
			this.status = dataReader.GetInt32(9);	
			this.userName = dataReader.GetValue(10).ToString();
			this.tripMeter = dataReader.GetInt32(11);

		}

		public AgentTransaction(DataRow dataRow)
		{
			this.agentCode = dataRow.ItemArray.GetValue(0).ToString();
			this.entryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
			
			string updatedDate = dataRow.ItemArray.GetValue(2).ToString().Substring(0, 10);
			string updatedTime = dataRow.ItemArray.GetValue(3).ToString().Substring(11);
			updatedDateTime = DateTime.Parse(updatedDate+" "+updatedTime);

			this.positionX = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.positionY = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
			this.speed = decimal.Parse(dataRow.ItemArray.GetValue(6).ToString());
			this.height = decimal.Parse(dataRow.ItemArray.GetValue(7).ToString());
			this.heading = decimal.Parse(dataRow.ItemArray.GetValue(8).ToString());
			this.status = int.Parse(dataRow.ItemArray.GetValue(9).ToString());	
			this.userName = dataRow.ItemArray.GetValue(10).ToString();
			this.tripMeter = int.Parse(dataRow.ItemArray.GetValue(11).ToString());

		}


		public AgentTransaction(string agentCode, int positionX, int positionY, decimal heading, decimal speed, decimal height, int status, string userName, int tripMeter)
		{
			//
			// TODO: Add constructor logic here
			//
			this.agentCode = agentCode;
			this.positionX = positionX;
			this.positionY = positionY;
			this.heading = heading;
			this.speed = speed;
			this.status = status;
			this.height = height;
			this.userName = userName;
			this.tripMeter = tripMeter;
			this.updatedDateTime = DateTime.Now;
		}

		public void save(Database database)
		{
			database.nonQuery("INSERT INTO [Agent Status] ([Agent Code], Date, Timestamp, [Position X], [Position Y], [Heading], [Speed], [Height], [Status], [User Name], [Trip Meter]) VALUES ('"+agentCode+"','"+updatedDateTime.ToString("yyyy-MM-dd")+"','"+updatedDateTime.ToString("1754-01-01 HH:mm:ss")+"', "+positionX+","+positionY+","+heading+","+speed+","+height+","+status+",'"+userName+"', '"+this.tripMeter+"')");

			Agents agents = new Agents();
			agents.updateAgent(database, this);
		}

	}
}
