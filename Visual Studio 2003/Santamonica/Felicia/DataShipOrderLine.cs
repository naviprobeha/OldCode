using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataShipOrderLine
	{
		public int entryNo;
		public int shipOrderEntryNo;
		public string itemNo;
		public int quantity;
		public int connectionQuantity;
		public float unitPrice;
		public float amount;
		public float connectionUnitPrice;
		public float connectionAmount;
		public float totalAmount;
		public string connectionItemNo;
		public int testQuantity;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataShipOrderLine(SmartDatabase smartDatabase, int entryNo)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = entryNo;
			getFromDb();
		}

		public DataShipOrderLine(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{

			entryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
			shipOrderEntryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
			itemNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
			quantity = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
			connectionQuantity = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString());
			unitPrice = float.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString());
			amount = float.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(6).ToString());
			connectionUnitPrice = float.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(7).ToString());
			connectionAmount = float.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(8).ToString());
			totalAmount = float.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(9).ToString());
			connectionItemNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
			testQuantity = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(11).ToString());

		}


		public void commit()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM shipOrderLine WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM shipOrderLine WHERE entryNo = '"+entryNo+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE shipOrderLine SET entryNo = '"+entryNo.ToString()+"', shipOrderEntryNo = '"+shipOrderEntryNo.ToString()+"', itemNo = '"+itemNo+"', quantity = '"+quantity.ToString()+"', connectionQuantity = '"+connectionQuantity.ToString()+"', unitPrice = '"+unitPrice.ToString()+"', amount = '"+amount.ToString()+"', connectionUnitPrice = '"+connectionUnitPrice.ToString()+"', connectionAmount = '"+connectionAmount.ToString()+"', totalAmount = '"+totalAmount.ToString()+"', connectionItemNo = '"+connectionItemNo+"', testQuantity = '"+this.testQuantity+"' WHERE entryNo = '"+entryNo+"'");
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
						smartDatabase.nonQuery("INSERT INTO shipOrderLine (entryNo, shipOrderEntryNo, itemNo, quantity, connectionQuantity, unitPrice, amount, connectionUnitPrice, connectionAmount, totalAmount, connectionItemNo, testQuantity) VALUES ('"+entryNo.ToString()+"','"+shipOrderEntryNo.ToString()+"','"+itemNo+"','"+quantity.ToString()+"','"+connectionQuantity.ToString()+"','"+unitPrice.ToString()+"','"+amount.ToString()+"','"+connectionUnitPrice.ToString()+"','"+connectionAmount.ToString()+"','"+totalAmount.ToString()+"','"+connectionItemNo+"','"+testQuantity+"')");
					
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM shipOrderLine WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.shipOrderEntryNo = dataReader.GetInt32(1);
					this.itemNo = dataReader.GetValue(2).ToString();
					this.quantity = dataReader.GetInt32(3);
					this.connectionQuantity = dataReader.GetInt32(4);
					this.unitPrice = dataReader.GetFloat(5);
					this.amount = dataReader.GetFloat(6);
					this.connectionUnitPrice = dataReader.GetFloat(7);
					this.connectionAmount = dataReader.GetFloat(8);
					this.totalAmount = dataReader.GetFloat(9);
					this.connectionItemNo = dataReader.GetValue(10).ToString();
					this.testQuantity = dataReader.GetInt32(11);
					
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
