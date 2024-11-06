using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataCustomer.
	/// </summary>
	public class DataStatus
	{

		public string containerNo;
		public DateTime arrivalTime;
		public int tripMeter;

		private string updateMethod;
		private SmartDatabase smartDatabase;


		public DataStatus(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			arrivalTime = getDefaultArrivalTime();
			tripMeter = 0;
			getFromDb();
		}

		public DateTime getDefaultArrivalTime()
		{
			return DateTime.Parse("2000-01-01 16:00:00");
		}

		public void commit()
		{

			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM status");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM status");
				}
				else
				{					
					try
					{
						smartDatabase.nonQuery("UPDATE status SET containerNo = '"+containerNo+"', arrivalTime = '"+this.arrivalTime.ToString("yyyy-MM-dd HH:mm:ss")+"', tripMeter = '"+this.tripMeter+"'");
					}
					catch (SqlCeException e) 
					{
						smartDatabase.ShowErrors(e);
					}
				}
			}
			else
			{
				try
				{
					smartDatabase.nonQuery("INSERT INTO status (primaryKey, containerNo, arrivalTime, tripMeter) VALUES ('1', '"+containerNo+"', '"+arrivalTime.ToString("yyyy-MM-dd HH:mm:ss")+"', '"+tripMeter+"')");
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();	
		}


		public bool getFromDb()
		{
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT containerNo, arrivalTime, tripMeter FROM status");

			if (dataReader.Read())
			{
				try
				{
					this.containerNo = dataReader.GetValue(0).ToString();
					this.arrivalTime = dataReader.GetDateTime(1);
					this.tripMeter = dataReader.GetInt32(2);

					dataReader.Dispose();
					return true;
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}

			dataReader.Dispose();
			return false;
			
		}

		public void delete()
		{
			this.updateMethod = "D";
			commit();
		}

	}
}
