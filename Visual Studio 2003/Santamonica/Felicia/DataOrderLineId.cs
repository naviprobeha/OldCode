using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for SyncAction.
	/// </summary>
	public class DataOrderLineId
	{
		public int entryNo;
		public int orderEntryNo;
		public int lineEntryNo;
		public string unitId;
		public bool bseTesting;
		public bool postMortem;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataOrderLineId(SmartDatabase smartDatabase, int entryNo)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = entryNo;
			getFromDb();
		}

		public DataOrderLineId(SmartDatabase smartDatabase, DataOrderLine dataOrderLine)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.orderEntryNo = dataOrderLine.orderEntryNo;
			this.lineEntryNo = dataOrderLine.entryNo;
		}

		public DataOrderLineId(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
		{
			this.smartDatabase = smartDatabase;

			this.entryNo = dataReader.GetInt32(0);
			this.orderEntryNo = dataReader.GetInt32(1);
			this.lineEntryNo = dataReader.GetInt32(2);
			this.unitId = dataReader.GetValue(3).ToString();
			
			this.bseTesting = false;
			if (dataReader.GetValue(4).ToString() == "1") this.bseTesting = true;

			this.postMortem = false;
			if (dataReader.GetValue(5).ToString() == "1") this.postMortem = true;
		}

		public void commit()
		{
			int bseTestingVal = 0;
			if (this.bseTesting) bseTestingVal = 1;

			int postMortemVal = 0;
			if (this.postMortem) postMortemVal = 1;
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM orderLineId WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM orderLineId WHERE entryNo = '"+entryNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE orderLineId SET unitId = '"+unitId+"', orderEntryNo = '"+orderEntryNo+"', lineEntryNo = '"+lineEntryNo+"', bseTesting = '"+bseTestingVal+"', postMortem = '"+postMortemVal+"' WHERE entryNo = '"+entryNo+"'");
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
					smartDatabase.nonQuery("INSERT INTO orderLineId (orderEntryNo, lineEntryNo, unitId, bseTesting, postMortem) VALUES ('"+orderEntryNo+"','"+lineEntryNo+"','"+unitId+"', '"+bseTestingVal+"', '"+postMortemVal+"')");
					this.entryNo = smartDatabase.getInsertedId();
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, orderEntryNo, lineEntryNo, unitId, bseTesting, postMortem FROM orderLineId WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.orderEntryNo = dataReader.GetInt32(1);
					this.lineEntryNo = dataReader.GetInt32(2);
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
