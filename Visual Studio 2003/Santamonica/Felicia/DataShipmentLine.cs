using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataShipmentLine.
	/// </summary>
	public class DataShipmentLine
	{
		public int entryNo;
		public int shipmentEntryNo;
		public string itemNo;
		public string description;
		public int quantity;
		public int connectionQuantity;
		public float unitPrice;
		public float amount;
		public float connectionUnitPrice;
		public float connectionAmount;
		public float totalAmount;
		public string connectionItemNo;
		public bool extraPayment;
		public int testQuantity;

		private SmartDatabase smartDatabase;
		private string updateMethod;

		public DataShipmentLine(SmartDatabase smartDatabase, DataShipmentHeader dataShipmentHeader, DataItem dataItem)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			//getNextNo();

			this.shipmentEntryNo = dataShipmentHeader.entryNo;
			setItem(dataItem);
			this.quantity = 1;

		}

		public void setItem(DataItem dataItem)
		{
			this.itemNo = dataItem.no;
			this.description = dataItem.description;
			this.unitPrice = dataItem.unitPrice;

		}

		public DataShipmentLine(SmartDatabase smartDatabase, DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;

			this.entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			this.shipmentEntryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.itemNo = dataRow.ItemArray.GetValue(2).ToString();
			this.description = dataRow.ItemArray.GetValue(3).ToString();
			this.quantity = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.connectionQuantity = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
			this.unitPrice = float.Parse(dataRow.ItemArray.GetValue(6).ToString());
			this.amount = float.Parse(dataRow.ItemArray.GetValue(7).ToString());
			this.connectionUnitPrice = float.Parse(dataRow.ItemArray.GetValue(8).ToString());
			this.connectionAmount = float.Parse(dataRow.ItemArray.GetValue(9).ToString());
			this.totalAmount = float.Parse(dataRow.ItemArray.GetValue(10).ToString());
			this.connectionItemNo = dataRow.ItemArray.GetValue(11).ToString();
			
			this.extraPayment = false;
			if (dataRow.ItemArray.GetValue(12).ToString() == "1") this.extraPayment = true;
			
			this.testQuantity = int.Parse(dataRow.ItemArray.GetValue(13).ToString());

		}

		public DataShipmentLine(SmartDatabase smartDatabase, int entryNo)
		{
			this.entryNo = entryNo;
			this.smartDatabase = smartDatabase;
			this.getFromDb();

		}


		public void getNextNo()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo FROM shipmentLine ORDER BY entryNo DESC");

			int nextNo = 1;

			if (dataReader.Read())
			{
				try
				{
					nextNo = (int)dataReader.GetValue(0);
					nextNo++;
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();		
			entryNo = nextNo;
		}

		public void commit()
		{
			int extraPaymentVal = 0;
			if (extraPayment) extraPaymentVal = 1;

			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM shipmentLine WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod == "D"))
				{
					smartDatabase.nonQuery("DELETE FROM shipmentLine WHERE entryNo = '"+entryNo+"'");
				}
				else
				{
					try
					{
						smartDatabase.nonQuery("UPDATE shipmentLine SET shipmentEntryNo = '"+shipmentEntryNo+"', itemNo = '"+itemNo+"', description = '"+description+"', quantity = '"+quantity+"', connectionQuantity = '"+connectionQuantity+"', unitPrice = '"+unitPrice+"', amount = '"+amount+"', connectionUnitPrice = '"+connectionUnitPrice+"', connectionAmount = '"+connectionAmount+"', totalAmount = '"+totalAmount+"', connectionItemNo = '"+connectionItemNo+"', extraPayment = '"+extraPaymentVal+"', testQuantity = '"+testQuantity+"' WHERE entryNo = '"+entryNo+"'");
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
					smartDatabase.nonQuery("INSERT INTO shipmentLine (shipmentEntryNo, itemNo, description, quantity, connectionQuantity, unitPrice, amount, connectionUnitPrice, connectionAmount, totalAmount, connectionItemNo, extraPayment, testQuantity) VALUES ('"+shipmentEntryNo+"', '"+itemNo+"', '"+description+"', '"+quantity+"', '"+connectionQuantity+"', '"+unitPrice+"', '"+amount+"', '"+connectionUnitPrice+"', '"+connectionAmount+"', '"+totalAmount+"', '"+connectionItemNo+"', '"+extraPaymentVal+"', '"+testQuantity+"')");
					dataReader = smartDatabase.query("SELECT entryNo FROM shipmentLine WHERE entryNo = @@IDENTITY");
					if (dataReader.Read())
					{
						this.entryNo = dataReader.GetInt32(0);
					}
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, shipmentEntryNo, itemNo, description, quantity, connectionQuantity, unitPrice, amount, connectionUnitPrice, connectionAmount, totalAmount, connectionItemNo, extraPayment, testQuantity FROM shipmentLine WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.shipmentEntryNo = dataReader.GetInt32(1);
					this.itemNo = dataReader.GetValue(2).ToString();
					this.description = dataReader.GetValue(3).ToString();
					this.quantity = dataReader.GetInt32(4);
					this.connectionQuantity = dataReader.GetInt32(5);
					this.unitPrice = dataReader.GetFloat(6);
					this.amount = dataReader.GetFloat(7);	
					this.connectionUnitPrice = dataReader.GetFloat(8);
					this.connectionAmount = dataReader.GetFloat(9);	
					this.totalAmount = dataReader.GetFloat(10);	
					this.connectionItemNo = dataReader.GetValue(11).ToString();

					if (dataReader.GetInt32(12) == 1) this.extraPayment = true;

					this.testQuantity = dataReader.GetInt32(13);					

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

		public void updateTestings()
		{
			DataShipmentLineIds dataShipmentLineIds = new DataShipmentLineIds(smartDatabase);
			
			this.testQuantity = dataShipmentLineIds.countTestings(this);
			
			this.commit();
		}

	}
}
