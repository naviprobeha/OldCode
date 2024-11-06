using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataShipOrderLineId
	{
		public int entryNo;
		public int shipOrderEntryNo;
		public int shipOrderLineEntryNo;
		public string unitId;
		public bool bseTesting;
		public bool postMortem;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataShipOrderLineId(SmartDatabase smartDatabase, int entryNo)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = entryNo;
			getFromDb();
		}

		public DataShipOrderLineId(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{

			entryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
			shipOrderEntryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
			shipOrderLineEntryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
			unitId = dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
			
			bseTesting = false;
			if (dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() == "1") bseTesting = true;

			postMortem = false;
			if (dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() == "1") postMortem = true;

		}


		public void commit()
		{
			int bseTestingVal = 0;
			if (bseTesting) bseTestingVal = 1;

			int postMortemVal = 0;
			if (postMortem) postMortemVal = 1;
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM shipOrderLineId WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM shipOrderLineId WHERE entryNo = '"+entryNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE shipOrderLineId SET entryNo = '"+entryNo.ToString()+"', shipOrderEntryNo = '"+shipOrderEntryNo.ToString()+"', shipOrderLineEntryNo = '"+shipOrderLineEntryNo.ToString()+"', unitId = '"+unitId+"', bseTesting = '"+bseTestingVal+"', postMortem = '"+postMortemVal+"' WHERE entryNo = '"+entryNo+"'");
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
						smartDatabase.nonQuery("INSERT INTO shipOrderLineId (entryNo, shipOrderEntryNo, shipOrderLineEntryNo, unitId, bseTesting, postMortem) VALUES ('"+entryNo.ToString()+"','"+shipOrderEntryNo.ToString()+"','"+shipOrderLineEntryNo.ToString()+"','"+unitId+"', '"+bseTestingVal+"', '"+postMortemVal+"')");
					
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM shipOrderLineId WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.shipOrderEntryNo = dataReader.GetInt32(1);
					this.shipOrderLineEntryNo = dataReader.GetInt32(2);
					this.unitId = dataReader.GetValue(3).ToString();

					this.bseTesting = false;
					if (dataReader.GetValue(4).ToString() == "1") this.bseTesting = true;

					this.postMortem = false;
					if (dataReader.GetValue(5).ToString() == "1") this.postMortem = true;
					
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
