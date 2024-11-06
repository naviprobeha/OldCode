using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataShipmentInvoice.
	/// </summary>
	public class DataShipmentInvoice
	{
		public int no;
		public DateTime updatedDate;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataShipmentInvoice(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			updateMethod = "";
		}

		public string getInvoiceNo(string agentCode)
		{
			getFromDb();
			no = no + 1;
			if (updatedDate.Year < DateTime.Today.Year)
			{
				no = 1;
			}
			commit();

			return DateTime.Today.Year.ToString().Substring(2, 2)+"-"+agentCode+(no.ToString().PadLeft(4, '0'));
		}

		private void commit()
		{
			updatedDate = DateTime.Today;
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM shipmentInvoice");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM shipmentInvoice");
				}
				else
				{					
					try
					{
						smartDatabase.nonQuery("UPDATE shipmentInvoice SET entryNo = '"+no+"', updatedDate = '"+this.updatedDate.ToString("yyyy-MM-dd")+"'");
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
					smartDatabase.nonQuery("INSERT INTO shipmentInvoice (entryNo, updatedDate) VALUES ('"+no+"', '"+updatedDate.ToString("yyyy-MM-dd")+"')");
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();	
		}


		private bool getFromDb()
		{
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, updatedDate FROM shipmentInvoice");

			if (dataReader.Read())
			{
				try
				{
					this.no = dataReader.GetInt32(0);
					this.updatedDate = dataReader.GetDateTime(1);

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

	}
}
