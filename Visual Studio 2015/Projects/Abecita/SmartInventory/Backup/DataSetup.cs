using System;
using System.Data.SqlServerCe;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataSetup.
	/// </summary>
	public class DataSetup
	{
		private string hostValue;
		private string receiverValue;
		private int synchMethodValue;
		
		private string locationCodeValue;
		private int allowDecimalValue;

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
					synchMethodValue = dataReader.GetInt32(7);
					locationCodeValue = (string)dataReader.GetValue(8);
					allowDecimalValue = dataReader.GetInt32(9);
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();
		}


		public void save()
		{
			try
			{
				SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM setup");

				if (!dataReader.Read())
				{
					smartDatabase.nonQuery("INSERT INTO setup (primaryKey, host, receiver, synchMethod, locationCode, allowDecimal) VALUES (1, '"+host+"','"+receiver+"','"+synchMethod+"','"+locationCodeValue+"', '"+allowDecimalValue+"')");
				}
				else
				{
					smartDatabase.nonQuery("UPDATE setup SET host = '"+host+"', receiver = '"+receiver+"', synchMethod = '"+synchMethod+"', locationCode = '"+locationCodeValue+"', allowDecimal = '"+allowDecimalValue+"' WHERE primaryKey = "+dataReader.GetValue(0));
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

		public int synchMethod
		{
			set
			{
				synchMethodValue = value;
			}
			get
			{
				return synchMethodValue;
			}
		}

		public string locationCode
		{
			set
			{
				locationCodeValue = value;
			}
			get
			{
				return locationCodeValue;
			}
		}

		public bool allowDecimal
		{
			set
			{
				allowDecimalValue = 0;
				if (value == true) allowDecimalValue = 1;
			}
			get
			{
				if (allowDecimalValue == 1) return true;
				return false;
			}
		}
	}
}
