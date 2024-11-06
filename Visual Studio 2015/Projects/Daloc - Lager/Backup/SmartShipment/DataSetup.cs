using System;
using System.Data.SqlServerCe;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for DataSetup.
	/// </summary>
	public class DataSetup
	{
		private string hostValue;
		private string receiverValue;

		private Agent agent;

		private SmartDatabase smartDatabase;
		
		public DataSetup()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSetup(SmartDatabase smartDatabase)
		{
			hostValue = "";
			receiverValue = "";

			agent = new Agent(smartDatabase);

			this.smartDatabase = smartDatabase;
			refresh();
		}

		public void refresh()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM setup");

			if (dataReader.Read())
			{
				try
				{
					hostValue = (string)dataReader.GetValue(1);
					receiverValue = (string)dataReader.GetValue(6);

				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();

			agent.refresh();
		}


		public void save()
		{
			try
			{
				SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM setup");

				if (!dataReader.Read())
				{
					smartDatabase.nonQuery("INSERT INTO setup (primaryKey, host, receiver) VALUES (1, '"+host+"','"+receiver+"')");
				}
				else
				{
					smartDatabase.nonQuery("UPDATE setup SET host = '"+host+"', receiver = '"+receiver+"' WHERE primaryKey = "+dataReader.GetValue(0));
					dataReader.Close();
				}
				dataReader.Dispose();

			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}

		}

		public string host
		{
			set
			{
				hostValue = value;
			}
			get
			{
				return hostValue;
			}
		}

		public string receiver
		{
			set
			{
				receiverValue = value;
			}
			get
			{
				return receiverValue;
			}
		}


		public Agent getAgent()
		{
			return agent;
		}

		public int getNextOrderNo()
		{
			int orderNo = 0;
			try
			{
				SqlCeDataReader dataReader = smartDatabase.query("SELECT lastOrderNo FROM setup");

				if (dataReader.Read())
				{
					orderNo = dataReader.GetInt32(0);
					dataReader.Close();
					dataReader.Dispose();
					
				}
				orderNo++;

				smartDatabase.nonQuery("UPDATE setup SET lastOrderNo = '"+orderNo.ToString()+"'");

			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}

			return orderNo;
		}
	}
}
