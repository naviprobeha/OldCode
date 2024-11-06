using System;
using System.Data.SqlServerCe;
using System.Data;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for DataSalesLine.
	/// </summary>
	public class DataSalesLine
	{
		private int noValue;
		private int orderNoValue;
		private string itemNoValue;
		private string colorCodeValue;
		private string sizeCodeValue;
		private string size2CodeValue;
		private string descriptionValue;
		private float quantityValue;
		private string baseUnitValue;

		private SmartDatabase smartDatabase;

		public DataSalesLine(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
		}

		public DataSalesLine(int no, SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.no = no;
			this.smartDatabase = smartDatabase;
		}

		public DataSalesLine(DataSalesHeader dataSalesHeader, DataItem dataItem, DataColor dataColor, DataSize dataSize, DataSize2 dataSize2, float quantity, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			getNextNo();
			this.orderNo = dataSalesHeader.no;
			this.itemNo = dataItem.no;

			if (dataColor != null)
				this.colorCode = dataColor.code;
			else
				this.colorCode = "";

			if (dataSize != null)
				this.sizeCode = dataSize.code;
			else
				this.sizeCode = "";

			if (dataSize2 != null) 
				this.size2Code = dataSize2.code;
			else
				this.size2Code = "";

			this.quantity = quantity;
			this.description = dataItem.description;
			this.baseUnit = dataItem.baseUnit;
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

		public string sizeCode
		{
			get
			{
				return sizeCodeValue;
			}
			set
			{
				sizeCodeValue = value;
			}
		}

		public string size2Code
		{
			get
			{
				return size2CodeValue;
			}
			set
			{
				size2CodeValue = value;
			}
		}

		public string colorCode
		{
			get
			{
				return colorCodeValue;
			}
			set
			{
				colorCodeValue = value;
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
				SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM salesLine WHERE orderNo = '"+orderNo+"' AND itemNo = '"+itemNo+"' AND colorCode = '"+colorCode+"' AND sizeCode = '"+sizeCode+"' AND size2Code = '"+size2Code+"'");

				if (!dataReader.Read())
				{
					if (quantity > 0)
					{
						smartDatabase.nonQuery("INSERT INTO salesLine (no, orderNo, itemNo, sizeCode, size2Code, colorCode, description, quantity, baseUnit) VALUES ('"+no+"', '"+orderNo+"', '"+itemNo+"', '"+sizeCode+"', '"+size2Code+"', '"+colorCode+"', '"+description+"', '"+quantity+"', '"+baseUnit+"')");
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
						smartDatabase.nonQuery("UPDATE salesLine SET orderNo = '"+orderNo+"', itemNo = '"+itemNo+"', sizeCode = '"+sizeCode+"', size2Code = '"+size2Code+"', colorCode = '"+colorCode+"', description = '"+description+"', quantity = '"+quantity+"', baseUnit = '"+baseUnit+"' WHERE no = '"+no+"'");
					}
					dataReader.Close();
				}
				dataReader.Dispose();

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


		public static void deleteAll(SmartDatabase smartDatabase, DataSalesHeader dataSalesHeader, DataItem dataItem, DataColor dataColor)
		{
			try
			{
				smartDatabase.nonQuery("DELETE FROM salesLine WHERE orderNo = '"+dataSalesHeader.no+"' AND itemNo = '"+dataItem.no+"' AND colorCode = '"+dataColor.code+"'");
			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}
		}

		public bool getFromDb()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM salesLine WHERE orderNo = '"+orderNo+"' AND itemNo = '"+itemNo+"' AND colorCode = '"+colorCode+"' AND sizeCode = '"+sizeCode+"' AND size2Code = '"+size2Code+"'");

			if (dataReader.Read())
			{
				try
				{
					this.quantityValue = dataReader.GetFloat(8);
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
