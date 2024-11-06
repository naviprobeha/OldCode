using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataConsumerStatus
	{
		public string consumerNo;
		public float inventoryLevel;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataConsumerStatus(SmartDatabase smartDatabase, string consumerNo)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.consumerNo = consumerNo;
			this.inventoryLevel = 0;
			getFromDb();
		}


		public DataConsumerStatus(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{
			consumerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
			inventoryLevel = float.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

		}


		public void commit()
		{

			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM consumerStatus WHERE consumerNo = '"+consumerNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM consumerStatus WHERE consumerNo = '"+consumerNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE consumerStatus SET consumerNo = '"+consumerNo+"', inventoryLevel = '"+inventoryLevel+"' WHERE consumerNo = '"+consumerNo+"'");
					}
					catch (SqlCeException e) 
					{
						smartDatabase.ShowErrors(e);
					}
				}
			}
			else
			{
				if ((updateMethod == null) || (!updateMethod.Equals("D")))
				{
					try
					{
						smartDatabase.nonQuery("INSERT INTO consumerStatus (consumerNo, inventoryLevel) VALUES ('"+consumerNo+"', '"+inventoryLevel+"')");
					
					}
					catch (SqlCeException e) 
					{
						smartDatabase.ShowErrors(e);
					}
				}
			}
			dataReader.Dispose();	
		}
		

		public bool getFromDb()
		{
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT consumerNo, inventoryLevel FROM consumerStatus WHERE consumerNo = '"+consumerNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.consumerNo = dataReader.GetValue(0).ToString();
					this.inventoryLevel = dataReader.GetFloat(1);

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

