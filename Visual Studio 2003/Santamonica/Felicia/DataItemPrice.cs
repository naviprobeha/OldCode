using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItemPrice.
	/// </summary>
	public class DataItemPrice
	{
		public int entryNo;
		public string itemNo;
		public int salesType;
		public string salesCode;
		public DateTime startingDate;
		public int minimumQuantity;
		public DateTime endingDate;
		public float unitPrice;

		public SmartDatabase smartDatabase;
		public string updateMethod;

		public DataItemPrice(SmartDatabase smartDatabase, int entryNo)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = entryNo;
			getFromDb();
		}

		public DataItemPrice(SmartDatabase smartDatabase, DataItem dataItem, int quantity, DataCustomer dataCustomer)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;

			getActualPrice(dataItem, quantity, dataCustomer);

		}

		public DataItemPrice(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);
			commit();
		}

		public void fromDataSet(DataSet dataset)
		{

			entryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
			itemNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			salesType = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
			salesCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
			startingDate = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString());
			minimumQuantity = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString());
			endingDate = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(6).ToString());
			unitPrice = float.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(7).ToString());

			// Fix för att synka datum...
			startingDate = startingDate.AddHours(9);
			endingDate = endingDate.AddHours(9);

		}

		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemPrice WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM itemPrice WHERE entryNo = '"+entryNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE itemPrice SET itemNo = '"+itemNo+"', salesType = '"+salesType+"', salesCode = '"+salesCode+"', startingDate = '"+startingDate.ToString("yyyy-MM-dd")+"', minimumQuantity = '"+minimumQuantity+"', endingDate = '"+endingDate.ToString("yyyy-MM-dd")+"', unitPrice = '"+unitPrice+"' WHERE entryNo = '"+entryNo+"'");
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
					smartDatabase.nonQuery("INSERT INTO itemPrice (entryNo, itemNo, salesType, salesCode, startingDate, minimumQuantity, endingDate, unitPrice) VALUES ('"+entryNo+"','"+itemNo+"','"+salesType+"','"+salesCode+"','"+startingDate.ToString("yyyy-MM-dd")+"','"+minimumQuantity+"','"+endingDate.ToString("yyyy-MM-dd")+"','"+unitPrice+"')");
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, itemNo, salesType, salesCode, startingDate, minimumQuantity, endingDate, unitPrice FROM itemPrice WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.itemNo = dataReader.GetValue(1).ToString();
					this.salesType = dataReader.GetInt32(2);
					this.salesCode = dataReader.GetValue(3).ToString();
					this.startingDate = dataReader.GetDateTime(4);
					this.minimumQuantity = dataReader.GetInt32(5);
					this.endingDate = dataReader.GetDateTime(6);
					this.unitPrice = dataReader.GetFloat(7);
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
			smartDatabase.nonQuery("DELETE FROM itemPrice");
		}

		public void getActualPrice(DataItem dataItem, int quantity, DataCustomer dataCustomer)
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, itemNo, salesType, salesCode, startingDate, minimumQuantity, endingDate, unitPrice FROM itemPrice WHERE itemNo = '"+dataItem.no+"' AND (startingDate <= GETDATE() OR startingDate = '1953-01-01 00:00:00' OR startingDate = '1753-01-01 00:00:00') AND (endingDate >= GETDATE() OR endingDate = '1953-01-01 00:00:00' OR endingDate = '1753-01-01 00:00:00') AND minimumQuantity <= '"+quantity+"' AND (salesType = 0 AND salesCode = '"+dataCustomer.no+"') ORDER BY unitPrice ");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.itemNo = dataReader.GetValue(1).ToString();
					this.salesType = dataReader.GetInt32(2);
					this.salesCode = dataReader.GetValue(3).ToString();
					this.startingDate = dataReader.GetDateTime(4);
					this.minimumQuantity = dataReader.GetInt32(5);
					this.endingDate = dataReader.GetDateTime(6);
					this.unitPrice = dataReader.GetFloat(7);
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

				dataReader = smartDatabase.query("SELECT entryNo, itemNo, salesType, salesCode, startingDate, minimumQuantity, endingDate, unitPrice FROM itemPrice WHERE itemNo = '"+dataItem.no+"' AND (startingDate <= GETDATE() OR startingDate = '1953-01-01 00:00:00' OR startingDate = '1753-01-01 00:00:00') AND (endingDate >= GETDATE() OR endingDate = '1953-01-01 00:00:00' OR endingDate = '1753-01-01 00:00:00') AND minimumQuantity <= '"+quantity+"' AND (salesType = 1 AND salesCode = '"+dataCustomer.priceGroupCode+"') ORDER BY unitPrice ");

				if (dataReader.Read())
				{
					try
					{
						this.entryNo = dataReader.GetInt32(0);
						this.itemNo = dataReader.GetValue(1).ToString();
						this.salesType = dataReader.GetInt32(2);
						this.salesCode = dataReader.GetValue(3).ToString();
						this.startingDate = dataReader.GetDateTime(4);
						this.minimumQuantity = dataReader.GetInt32(5);
						this.endingDate = dataReader.GetDateTime(6);
						this.unitPrice = dataReader.GetFloat(7);
						dataReader.Dispose();

					}
					catch (SqlCeException e) 
					{
						smartDatabase.ShowErrors(e);
					}
				}
				else
				{
					dataReader = smartDatabase.query("SELECT entryNo, itemNo, salesType, salesCode, startingDate, minimumQuantity, endingDate, unitPrice FROM itemPrice WHERE itemNo = '"+dataItem.no+"' AND (startingDate <= GETDATE() OR startingDate = '1953-01-01 00:00:00' OR startingDate = '1753-01-01 00:00:00') AND (endingDate >= GETDATE() OR endingDate = '1953-01-01 00:00:00' OR endingDate = '1753-01-01 00:00:00') AND minimumQuantity <= '"+quantity+"' AND (salesType = 2) ORDER BY unitPrice ");

					if (dataReader.Read())
					{
						try
						{
							this.entryNo = dataReader.GetInt32(0);
							this.itemNo = dataReader.GetValue(1).ToString();
							this.salesType = dataReader.GetInt32(2);
							this.salesCode = dataReader.GetValue(3).ToString();
							this.startingDate = dataReader.GetDateTime(4);
							this.minimumQuantity = dataReader.GetInt32(5);
							this.endingDate = dataReader.GetDateTime(6);
							this.unitPrice = dataReader.GetFloat(7);

						}
						catch (SqlCeException e) 
						{
							smartDatabase.ShowErrors(e);
						}
						dataReader.Dispose();
					}

				}
			}
		}

	}
}
