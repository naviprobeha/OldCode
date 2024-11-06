using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataLineOrder
	{
		public int entryNo;
		public string organizationNo;
		public int lineJournalEntryNo;
		public DateTime shipDate;
		public string shippingCustomerNo;
		public string shippingCustomerName;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;
		public string phoneNo;
		public string cellPhoneNo;
		public string details;
		public string comments;
		public int type;
		public int status;
		public DateTime shipTime;
		public string directionComment1;
		public string directionComment2;
		public int optimizingSortOrder;
		public int positionX;
		public int positionY;
		public int loadWaitTime;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataLineOrder(SmartDatabase smartDatabase, int entryNo)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = entryNo;
			getFromDb();
		}


		public DataLineOrder(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{
			entryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
			organizationNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			lineJournalEntryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
			shipDate = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
			shippingCustomerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
			shippingCustomerName = dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
			address = dataset.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
			address2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
			postCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
			city = dataset.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
			countryCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
			phoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
			cellPhoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
			details = dataset.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
			comments = dataset.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
			status = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(15).ToString());
			shipTime = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(17).ToString());
			directionComment1 = dataset.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
			directionComment2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
			positionX = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(21).ToString());
			positionY = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(22).ToString());
			type = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(23).ToString());
			optimizingSortOrder = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(24).ToString());
		}


		public void commit()
		{

			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM lineOrder WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM lineOrder WHERE entryNo = '"+entryNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE lineOrder SET entryNo = '"+entryNo.ToString()+"', organizationNo = '"+organizationNo.ToString()+"', lineJournalEntryNo = '"+this.lineJournalEntryNo+"', shipDate = '"+shipDate.ToString("yyyy-MM-dd")+"', shippingCustomerNo = '"+shippingCustomerNo+"', shippingCustomerName = '"+shippingCustomerName+"', address = '"+this.address+"', address2 = '"+this.address2+"', postCode = '"+this.postCode+"', city = '"+this.city+"', countryCode = '"+this.countryCode+"', phoneNo = '"+this.phoneNo+"', cellPhoneNo = '"+this.cellPhoneNo+"', details = '"+this.details+"', comments = '"+this.comments+"', type = '"+this.type+"', status = '"+status+"', shipTime = '"+shipTime.ToString("yyyy-MM-dd HH:mm:ss")+"', directionComment1 = '"+this.directionComment1+"', directionComment2 = '"+this.directionComment2+"', optimizingSortOrder = '"+this.optimizingSortOrder+"', positionX = '"+this.positionX+"', positionY = '"+this.positionY+"', loadWaitTime = '"+this.loadWaitTime+"' WHERE entryNo = '"+entryNo+"'");
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
						smartDatabase.nonQuery("INSERT INTO lineOrder (entryNo, organizationNo, lineJournalEntryNo, shipDate, shippingCustomerNo, shippingCustomerName, address, address2, postCode, city, countryCode, phoneNo, cellPhoneNo, details, comments, type, status, shipTime, directionComment1, directionComment2, optimizingSortOrder, positionX, positionY, loadWaitTime) VALUES ('"+entryNo.ToString()+"','"+organizationNo+"','"+lineJournalEntryNo+"','"+shipDate.ToString("yyyy-MM-dd")+"','"+shippingCustomerNo+"', '"+shippingCustomerName+"', '"+address+"', '"+address2+"', '"+postCode+"', '"+city+"', '"+this.countryCode+"', '"+phoneNo+"', '"+cellPhoneNo+"', '"+details+"', '"+comments+"', '"+type+"', '"+status+"', '"+shipTime.ToString("yyyy-MM-dd HH:mm:ss")+"', '"+directionComment1+"', '"+directionComment2+"', '"+optimizingSortOrder+"', '"+this.positionX+"', '"+this.positionY+"', '"+loadWaitTime+"')");
					
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, organizationNo, lineJournalEntryNo, shipDate, shippingCustomerNo, shippingCustomerName, address, address2, postCode, city, countryCode, phoneNo, cellPhoneNo, details, comments, type, status, shipTime, directionComment1, directionComment2, optimizingSortOrder, positionX, positionY, loadWaitTime FROM lineOrder WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.organizationNo = dataReader.GetValue(1).ToString();
					this.lineJournalEntryNo = dataReader.GetInt32(2);
					this.shipDate = dataReader.GetDateTime(3);
					this.shippingCustomerNo = dataReader.GetValue(4).ToString();
					this.shippingCustomerName = dataReader.GetValue(5).ToString();
					this.address = dataReader.GetValue(6).ToString();
					this.address2 = dataReader.GetValue(7).ToString();
					this.postCode = dataReader.GetValue(8).ToString();
					this.city = dataReader.GetValue(9).ToString();
					this.countryCode = dataReader.GetValue(10).ToString();
					this.phoneNo = dataReader.GetValue(11).ToString();
					this.cellPhoneNo = dataReader.GetValue(12).ToString();
					this.details = dataReader.GetValue(13).ToString();
					this.comments = dataReader.GetValue(14).ToString();
					this.type = dataReader.GetInt32(15);
					this.status = dataReader.GetInt32(16);
					this.shipTime = dataReader.GetDateTime(17);
					this.directionComment1 = dataReader.GetValue(18).ToString();
					this.directionComment2 = dataReader.GetValue(19).ToString();
					this.optimizingSortOrder = dataReader.GetInt32(20);
					this.positionX = dataReader.GetInt32(21);
					this.positionY = dataReader.GetInt32(22);
					this.loadWaitTime = dataReader.GetInt32(23);
					
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
