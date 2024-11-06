using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataCustomer.
	/// </summary>
	public class DataContainer
	{

		public string no;
		public string containerTypeCode;
		public int currentLocationType;
		public string currentLocationCode;

		private string updateMethod;
		private SmartDatabase smartDatabase;


		public DataContainer(SmartDatabase smartDatabase, string no)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			this.no = no;
			getFromDb();
		}

		public DataContainer(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{

			no = dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
			this.containerTypeCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
			this.currentLocationType = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
			this.currentLocationCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
		}


		public void commit()
		{

			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM container WHERE no = '"+no+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM container WHERE no = '"+no+"'");
				}
				else
				{
					try
					{
						smartDatabase.nonQuery("UPDATE container SET containerTypeCode = '"+containerTypeCode+"', currentLocationType = '"+currentLocationType+"', currentLocationCode = '"+currentLocationCode+"' WHERE no = '"+no+"'");
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
					smartDatabase.nonQuery("INSERT INTO container (no, containerTypeCode, currentLocationType, currentLocationCode) VALUES ('"+no+"', '"+this.containerTypeCode+"', '"+this.currentLocationType+"', '"+this.currentLocationCode+"')");
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT no, containerTypeCode, currentLocationType, currentLocationCode FROM container WHERE no = '"+no+"'");

			if (dataReader.Read())
			{
				try
				{
					this.no = dataReader.GetValue(0).ToString();
					this.containerTypeCode = dataReader.GetValue(1).ToString();
					this.currentLocationType = dataReader.GetInt32(2);
					this.currentLocationCode = dataReader.GetValue(3).ToString();

					dataReader.Dispose();
					return true;
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			else
			{
				no = "";
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
