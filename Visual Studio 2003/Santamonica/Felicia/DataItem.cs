using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;
using System.IO;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataItem
	{
		public string no;
		public string description;
		public string searchDescription;
		public float unitPrice;
		public bool addStopItem;
		public bool requireId;
		public bool invoiceToJbv;
		public string stopItemNo;
		public string connectionItemNo;
		public string unitOfMeasure;
		public bool putToDeath;
		public bool availableInMobile;
		public bool requireCashPayment;
		public string idGroupCode;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataItem(SmartDatabase smartDatabase, string no)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.no = no;
			getFromDb();
		}

		public DataItem(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{
			no = dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
			description = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			searchDescription = dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
			unitPrice = float.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
			if (dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() == "1") this.addStopItem = true;
			if (dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() == "1") this.requireId = true;
			if (dataset.Tables[0].Rows[0].ItemArray.GetValue(6).ToString() == "1") this.invoiceToJbv = true;
			stopItemNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
			connectionItemNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
			unitOfMeasure = dataset.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
			if (dataset.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() == "1") this.putToDeath = true;			
			if (dataset.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "1") this.availableInMobile = true;			
			if (dataset.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "1") this.requireCashPayment = true;			
			
			idGroupCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
		}


		public void commit()
		{
			int addStopItemVal = 0;
			int requireIdVal = 0;
			int invoiceToJbvVal = 0;
			int putToDeathVal = 0;
			int availableInMobileVal = 0;
			int requireCashPaymentVal = 0;

			if (this.addStopItem) addStopItemVal = 1;
			if (this.requireId) requireIdVal = 1;
			if (this.invoiceToJbv) invoiceToJbvVal = 1;
			if (this.putToDeath) putToDeathVal = 1;
			if (this.availableInMobile) availableInMobileVal = 1;
			if (this.requireCashPayment) requireCashPaymentVal = 1;

			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM item WHERE no = '"+no+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM item WHERE no = '"+no+"'");
					smartDatabase.nonQuery("DELETE FROM itemPrice WHERE itemNo = '"+no+"'");
					smartDatabase.nonQuery("DELETE FROM itemPriceExtended WHERE itemNo = '"+no+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE item SET description = '"+description+"', searchDescription = '"+searchDescription+"', unitPrice = '"+unitPrice+"', addStopItem = '"+addStopItemVal+"', requireId = '"+requireIdVal+"', invoiceToJbv = '"+invoiceToJbvVal+"', stopItemNo = '"+this.stopItemNo+"', connectionItemNo = '"+this.connectionItemNo+"', unitOfMeasure = '"+this.unitOfMeasure+"', putToDeath = '"+putToDeathVal+"', availableInMobile = '"+availableInMobileVal+"', requireCashPayment = '"+requireCashPaymentVal+"', idGroupCode = '"+idGroupCode+"' WHERE no = '"+no+"'");
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
					smartDatabase.nonQuery("INSERT INTO item (no, description, searchDescription, unitPrice, addStopItem, requireId, invoiceToJbv, stopItemNo, connectionItemNo, unitOfMeasure, putToDeath, availableInMobile, requireCashPayment, idGroupCode) VALUES ('"+no+"','"+description+"','"+searchDescription+"','"+unitPrice+"','"+addStopItemVal+"','"+requireIdVal+"','"+invoiceToJbvVal+"','"+stopItemNo+"','"+connectionItemNo+"','"+this.unitOfMeasure+"','"+putToDeathVal+"','"+availableInMobileVal+"','"+requireCashPaymentVal+"', '"+idGroupCode+"')");
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT no, description, searchDescription, unitPrice, addStopItem, requireId, invoiceToJbv, stopItemNo, connectionItemNo, unitOfMeasure, putToDeath, availableInMobile, requireCashPayment, idGroupCode FROM item WHERE no = '"+no+"'");

			if (dataReader.Read())
			{
				try
				{
					this.no = dataReader.GetValue(0).ToString();
					this.description = dataReader.GetValue(1).ToString();
					this.searchDescription = dataReader.GetValue(2).ToString();
					this.unitPrice = dataReader.GetFloat(3);
					if (dataReader.GetInt32(4) == 1) this.addStopItem = true;
					if (dataReader.GetInt32(5) == 1) this.requireId = true;
					if (dataReader.GetInt32(6) == 1) this.invoiceToJbv = true;
					this.stopItemNo = dataReader.GetValue(7).ToString();					
					this.connectionItemNo = dataReader.GetValue(8).ToString();
					this.unitOfMeasure = dataReader.GetValue(9).ToString();
					if (dataReader.GetInt32(10) == 1) this.putToDeath = true;
					if (dataReader.GetInt32(11) == 1) this.availableInMobile = true;					
					if (dataReader.GetInt32(12) == 1) this.requireCashPayment = true;					
					this.idGroupCode = dataReader.GetValue(13).ToString();

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

		public void deletePrices()
		{
			smartDatabase.nonQuery("DELETE FROM itemPrice WHERE itemNo = '"+no+"'");
			smartDatabase.nonQuery("DELETE FROM itemPriceExtended WHERE itemNo = '"+no+"'");
		}
	}
}
