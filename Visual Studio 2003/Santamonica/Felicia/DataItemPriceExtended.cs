using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItemPrice.
	/// </summary>
	public class DataItemPriceExtended
	{
		public int entryNo;
		public string itemNo;
		public DateTime startingDate;
		public DateTime endingDate;
		public string customerPriceGroup;
		public string unitOfMeasureCode;
		public float quantityFrom;
		public float quantityTo;
		public float lineAmount;

		public SmartDatabase smartDatabase;
		public string updateMethod;

		public DataItemPriceExtended(SmartDatabase smartDatabase, int entryNo)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = entryNo;
			getFromDb();
		}

		public DataItemPriceExtended(SmartDatabase smartDatabase, DataItem dataItem, int quantity, DataCustomer dataCustomer)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;

			getActualLineAmount(dataItem, quantity, dataCustomer);

		}

		public DataItemPriceExtended(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);
			commit();
		}

		public void fromDataSet(DataSet dataset)
		{

			entryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
			itemNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			startingDate = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
			endingDate = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
			customerPriceGroup = dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
			unitOfMeasureCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
			quantityFrom = float.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(6).ToString());
			quantityTo = float.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(7).ToString());
			lineAmount = float.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(8).ToString());


			// Fix för att synka datum...
			startingDate = startingDate.AddHours(9);
			endingDate = endingDate.AddHours(9);

		}

		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemPriceExtended WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM itemPriceExtended WHERE entryNo = '"+entryNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE itemPriceExtended SET itemNo = '"+itemNo+"', startingDate = '"+startingDate.ToString("yyyy-MM-dd")+"', endingDate = '"+endingDate.ToString("yyyy-MM-dd")+"', customerPriceGroup = '"+customerPriceGroup+"', unitOfMeasureCode = '"+unitOfMeasureCode+"', quantityFrom = '"+quantityFrom+"', quantityTo = '"+quantityTo+"', lineAmount = '"+lineAmount+"' WHERE entryNo = '"+entryNo+"'");
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
					smartDatabase.nonQuery("INSERT INTO itemPriceExtended (entryNo, itemNo, startingDate, endingDate, customerPriceGroup, unitOfMeasureCode, quantityFrom, quantityTo, lineAmount) VALUES ('"+entryNo+"','"+itemNo+"','"+startingDate.ToString("yyyy-MM-dd")+"','"+endingDate.ToString("yyyy-MM-dd")+"','"+customerPriceGroup+"','"+unitOfMeasureCode+"','"+quantityFrom+"','"+quantityTo+"','"+lineAmount+"')");
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, itemNo, startingDate, endingDate, customerPriceGroup, unitOfMeasureCode, quantityFrom, quantityTo, lineAmount FROM itemPriceExtended WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.itemNo = dataReader.GetValue(1).ToString();
					this.startingDate = dataReader.GetDateTime(2);
					this.endingDate = dataReader.GetDateTime(3);
					this.customerPriceGroup = dataReader.GetValue(4).ToString();
					this.unitOfMeasureCode = dataReader.GetValue(5).ToString();
					this.quantityFrom = dataReader.GetFloat(6);
					this.quantityTo = dataReader.GetFloat(7);
					this.lineAmount = dataReader.GetFloat(8);
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

		public void deleteAll()
		{
			smartDatabase.nonQuery("DELETE FROM itemPriceExtended");
		}


		public void getActualLineAmount(DataItem dataItem, int quantity, DataCustomer dataCustomer)
		{

			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, itemNo, startingDate, endingDate, customerPriceGroup, unitOfMeasureCode, quantityFrom, quantityTo, lineAmount FROM itemPriceExtended WHERE itemNo = '"+dataItem.no+"' AND (startingDate <= GETDATE() OR startingDate = '1953-01-01 00:00:00' OR startingDate = '1753-01-01 00:00:00') AND (endingDate >= GETDATE() OR endingDate = '1953-01-01 00:00:00' OR endingDate = '1753-01-01 00:00:00') AND quantityFrom <= '"+quantity+"' AND quantityTo >= '"+quantity+"' AND (customerPriceGroup = '"+dataCustomer.priceGroupCode+"' OR customerPriceGroup = '') ORDER BY lineAmount ");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.itemNo = dataReader.GetValue(1).ToString();
					this.startingDate = dataReader.GetDateTime(2);
					this.endingDate = dataReader.GetDateTime(3);
					this.customerPriceGroup = dataReader.GetValue(4).ToString();
					this.unitOfMeasureCode = dataReader.GetValue(5).ToString();
					this.quantityFrom = dataReader.GetFloat(6);
					this.quantityTo = dataReader.GetFloat(7);
					this.lineAmount = dataReader.GetFloat(8);
					dataReader.Dispose();

				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			else
			{
				dataReader.Dispose();
			}
			
		}

	}
}
