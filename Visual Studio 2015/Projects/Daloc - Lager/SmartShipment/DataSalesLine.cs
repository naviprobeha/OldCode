using System;
using System.Data.SqlServerCe;
using System.Data;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for DataSalesLine.
	/// </summary>
	public class DataSalesLine
	{
		private int noValue;
		private int orderNoValue;
		private string itemNoValue;
		private string descriptionValue;
		private float quantityValue;
		private string baseUnitValue;
		private float discountValue;
		private float unitPriceValue;
		private float amountValue;
		private string deliveryDateValue;
		private string hangingValue;
		private string referenceNoValue;

		private bool newLineValue;

		private SmartDatabase smartDatabase;

		public DataSalesLine(DataSalesHeader dataSalesHeader, SmartDatabase smartDatabase)
		{

			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			this.orderNo = dataSalesHeader.no;

			newLineValue = true;
		}

		public DataSalesLine(int no, SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.no = no;
			this.smartDatabase = smartDatabase;
			getFromDb();
		}


		public bool newLine
		{
			get
			{
				return newLineValue;
			}
		}

		public int no
		{
			get
			{
				return noValue;
			}
			set
			{
				noValue = value;
			}
		}

		public int orderNo
		{
			get
			{
				return orderNoValue;
			}
			set
			{
				orderNoValue = value;
			}
		}

		public string itemNo
		{
			get
			{
				return itemNoValue;
			}
			set
			{
				itemNoValue = value;
			}
		}



		public string description
		{
			get
			{
				return descriptionValue;
			}
			set
			{
				descriptionValue = value;
			}
		}

		public float quantity
		{
			get
			{
				return quantityValue;
			}
			set
			{
				quantityValue = value;
			}
		}

		public string baseUnit
		{
			get
			{
				return baseUnitValue;
			}
			set
			{
				baseUnitValue = value;
			}
		}

		public float discount
		{
			get
			{
				return discountValue;
			}
			set
			{
				discountValue = value;
			}
		}

		public float unitPrice
		{
			get
			{
				return unitPriceValue;
			}
			set
			{
				unitPriceValue = value;
			}
		}

		public float amount
		{
			get
			{
				return amountValue;
			}
			set
			{
				amountValue = value;
			}
		}

		public string deliveryDate
		{
			get
			{
				return deliveryDateValue;
			}
			set
			{
				deliveryDateValue = value;
			}
		}

		public string hanging
		{
			get
			{
				return hangingValue;
			}
			set
			{
				hangingValue = value;
			}
		}

		public string referenceNo
		{
			get
			{
				return referenceNoValue;
			}
			set
			{
				referenceNoValue = value;
			}
		}

		public void getNextNo()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT no FROM salesLine ORDER BY no DESC");

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
			noValue = nextNo;
		}

		public void save()
		{
			try
			{
				SqlCeDataReader dataReader;

				dataReader = smartDatabase.query("SELECT * FROM salesLine WHERE orderNo = '"+orderNo+"' AND no = '"+no+"'");

				if (!dataReader.Read())
				{
					if ((quantity > 0) || (quantity < 0))
					{
						smartDatabase.nonQuery("INSERT INTO salesLine (orderNo, itemNo, description, quantity, baseUnit, discount, deliveryDate, unitPrice, amount, hanging, referenceNo) VALUES ('"+orderNo+"', '"+itemNo+"', '"+description+"', '"+quantity+"', '"+baseUnit+"', '"+discount.ToString().Replace(",",".")+"', '"+deliveryDate+"','"+unitPrice.ToString().Replace(",",".")+"','"+amount.ToString().Replace(",",".")+"', '"+hangingValue+"','"+referenceNoValue+"')");
					}
				}
				else
				{
					no = (int)dataReader.GetValue(0);
					
					if (quantity == 0)
					{
						smartDatabase.nonQuery("DELETE FROM salesLine WHERE no = '"+no+"'");
					}
					else
					{
						smartDatabase.nonQuery("UPDATE salesLine SET orderNo = '"+orderNo+"', itemNo = '"+itemNo+"', description = '"+description+"', quantity = '"+quantity+"', baseUnit = '"+baseUnit+"', discount = '"+discount.ToString().Replace(",",".")+"', deliveryDate = '"+deliveryDate+"', unitPrice = '"+unitPrice.ToString().Replace(",",".")+"', amount = '"+amount.ToString().Replace(",",".")+"', hanging = '"+hangingValue+"', referenceNo = '"+referenceNoValue+"' WHERE no = '"+no+"'");
					}
					dataReader.Close();
				}
				dataReader.Dispose();

				newLineValue = false;
			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}
		}

		public void delete()
		{
			try
			{
				smartDatabase.nonQuery("DELETE FROM salesLine WHERE no = '"+no+"'");

			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}

		}


		public static void delete(SmartDatabase smartDatabase, DataSalesHeader dataSalesHeader, DataItem dataItem)
		{
			try
			{
				smartDatabase.nonQuery("DELETE FROM salesLine WHERE orderNo = '"+dataSalesHeader.no+"' AND itemNo = '"+dataItem.no+"'");
			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}
		}

		public bool getFromDb()
		{
			SqlCeDataReader dataReader;

			dataReader = smartDatabase.query("SELECT * FROM salesLine WHERE no = '"+no+"'");

			if (dataReader.Read())
			{
				try
				{
					this.no = dataReader.GetInt32(0);
					this.orderNoValue = dataReader.GetInt32(1);
					this.itemNoValue = (string)dataReader.GetValue(2);
					this.descriptionValue = (string)dataReader.GetValue(3);
					this.baseUnitValue = (string)dataReader.GetValue(4);
					this.quantityValue = float.Parse(dataReader.GetValue(5).ToString());
                    this.discountValue = float.Parse(dataReader.GetValue(6).ToString());
					this.deliveryDateValue = (string)dataReader.GetValue(7);
                    this.unitPriceValue = float.Parse(dataReader.GetValue(8).ToString());
                    this.amountValue = float.Parse(dataReader.GetValue(9).ToString());
					this.hangingValue = (string)dataReader.GetValue(10);
					this.referenceNoValue = (string)dataReader.GetValue(11);

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
