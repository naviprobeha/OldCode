using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for SyncAction.
	/// </summary>
	public class DataShipmentLineId
	{
		public int entryNo;
		public int shipmentEntryNo;
		public int lineEntryNo;
		public string unitId;
		public int type;
		public string reMarkUnitId;
		public bool bseTesting;
		public bool postMortem;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataShipmentLineId(SmartDatabase smartDatabase, int entryNo)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = entryNo;
			getFromDb();
		}

		public DataShipmentLineId(SmartDatabase smartDatabase, DataShipmentLine dataShipmentLine)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.shipmentEntryNo = dataShipmentLine.shipmentEntryNo;
			this.lineEntryNo = dataShipmentLine.entryNo;
		}

		public DataShipmentLineId(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
		{
			this.smartDatabase = smartDatabase;

			this.entryNo = dataReader.GetInt32(0);
			this.shipmentEntryNo = dataReader.GetInt32(1);
			this.lineEntryNo = dataReader.GetInt32(2);
			this.unitId = dataReader.GetValue(3).ToString();
			this.type = int.Parse(dataReader.GetValue(4).ToString());
			this.reMarkUnitId = dataReader.GetValue(5).ToString();
			
			this.bseTesting = false;
			if (dataReader.GetValue(6).ToString() == "1") this.bseTesting = true;
			
			this.postMortem = false;
			if (dataReader.GetValue(7).ToString() == "1") this.postMortem = true;
		}

		public void commit()
		{
			int bseTestingVal = 0;
			if (this.bseTesting) bseTestingVal = 1;

			int postMortemVal = 0;
			if (this.postMortem) postMortemVal = 1;

			

			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM shipmentLineId WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM shipmentLineId WHERE entryNo = '"+entryNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE shipmentLineId SET unitId = '"+unitId+"', shipmentEntryNo = '"+shipmentEntryNo+"', lineEntryNo = '"+lineEntryNo+"', type = '"+type.ToString()+"', reMarkUnitId = '"+this.reMarkUnitId+"', bseTesting = '"+bseTestingVal+"', postMortem = '"+postMortemVal+"' WHERE entryNo = '"+entryNo+"'");
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
					smartDatabase.nonQuery("INSERT INTO shipmentLineId (shipmentEntryNo, lineEntryNo, unitId, type, reMarkUnitId, bseTesting, postMortem) VALUES ('"+shipmentEntryNo+"','"+lineEntryNo+"','"+unitId+"', '"+type.ToString()+"', '"+reMarkUnitId+"', '"+bseTestingVal+"', '"+postMortemVal+"')");
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, shipmentEntryNo, lineEntryNo, unitId, type, reMarkUnitId, bseTesting, postMortem FROM shipmentLineId WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.shipmentEntryNo = dataReader.GetInt32(1);
					this.lineEntryNo = dataReader.GetInt32(2);
					this.unitId = dataReader.GetValue(3).ToString();
					this.type = dataReader.GetInt32(4);
					this.reMarkUnitId = dataReader.GetValue(5).ToString();
					
					this.bseTesting = false;
					if (dataReader.GetValue(6).ToString() == "1") this.bseTesting = true;

					this.postMortem = false;
					if (dataReader.GetValue(7).ToString() == "1") this.postMortem = true;

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
