using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataShipmentLine.
	/// </summary>
	public class DataOrderLine
	{
		public int entryNo;
		public int orderEntryNo;
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

		private SmartDatabase smartDatabase;
		private string updateMethod;

		public DataOrderLine(SmartDatabase smartDatabase, DataOrderHeader dataOrderHeader, DataItem dataItem)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			//getNextNo();

			this.orderEntryNo = dataOrderHeader.entryNo;
			this.itemNo = dataItem.no;
			this.description = dataItem.description;
			this.unitPrice = dataItem.unitPrice;
			this.quantity = 1;

		}

		public DataOrderLine(SmartDatabase smartDatabase, int entryNo)
		{
			this.entryNo = entryNo;
			this.smartDatabase = smartDatabase;
			this.getFromDb();

		}


		public void getNextNo()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo FROM orderLine ORDER BY entryNo DESC");

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
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM orderLine WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod == "D"))
				{
					smartDatabase.nonQuery("DELETE FROM orderLine WHERE entryNo = '"+entryNo+"'");
				}
				else
				{
					try
					{
						smartDatabase.nonQuery("UPDATE orderLine SET orderEntryNo = '"+orderEntryNo+"', itemNo = '"+itemNo+"', description = '"+description+"', quantity = '"+quantity+"', connectionQuantity = '"+connectionQuantity+"', unitPrice = '"+unitPrice+"', amount = '"+amount+"', connectionUnitPrice = '"+connectionUnitPrice+"', connectionAmount = '"+connectionAmount+"', totalAmount = '"+totalAmount+"', connectionItemNo = '"+connectionItemNo+"' WHERE entryNo = '"+entryNo+"'");
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
					smartDatabase.nonQuery("INSERT INTO orderLine (orderEntryNo, itemNo, description, quantity, connectionQuantity, unitPrice, amount, connectionUnitPrice, connectionAmount, totalAmount, connectionItemNo) VALUES ('"+orderEntryNo+"', '"+itemNo+"', '"+description+"', '"+quantity+"', '"+connectionQuantity+"', '"+unitPrice+"', '"+amount+"', '"+connectionUnitPrice+"', '"+connectionAmount+"', '"+totalAmount+"', '"+connectionItemNo+"')");
					dataReader = smartDatabase.query("SELECT entryNo FROM orderLine WHERE entryNo = @@IDENTITY");
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, orderEntryNo, itemNo, description, quantity, connectionQuantity, unitPrice, amount, connectionUnitPrice, connectionAmount, totalAmount, connectionItemNo FROM orderLine WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.orderEntryNo = dataReader.GetInt32(1);
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
