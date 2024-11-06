using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataLineOrderContainer
	{
		public int entryNo;
		public int lineOrderEntryNo;
		public string containerNo;
		public string categoryCode;
		public float weight;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataLineOrderContainer(SmartDatabase smartDatabase, int entryNo)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = entryNo;
			getFromDb();
		}


		public DataLineOrderContainer(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{
			entryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
			lineOrderEntryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
			containerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
			categoryCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
			weight = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString());

		}


		public void commit()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM lineOrderContainer WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM lineOrderContainer WHERE entryNo = '"+entryNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE lineOrderContainer SET entryNo = '"+entryNo.ToString()+"', lineOrderEntryNo = '"+lineOrderEntryNo+"', containerNo = '"+containerNo+"', categoryCode = '"+categoryCode+"', weight = '"+weight+"' WHERE entryNo = '"+entryNo+"'");
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
						smartDatabase.nonQuery("INSERT INTO lineOrderContainer (entryNo, lineOrderEntryNo, containerNo, categoryCode, weight) VALUES ('"+entryNo.ToString()+"','"+lineOrderEntryNo+"','"+containerNo+"','"+categoryCode+"', '"+weight+"')");
					
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, lineOrderEntryNo, containerNo, categoryCode, weight FROM lineOrderContainer WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.lineOrderEntryNo = dataReader.GetInt32(1);
					this.containerNo = dataReader.GetValue(2).ToString();
					this.categoryCode = dataReader.GetValue(3).ToString();
					this.weight = float.Parse(dataReader.GetValue(4).ToString());
					
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
