using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataWhseItemStore : ServiceArgument
	{
		private int seqNoValue;
		private string locationCodeValue;
		private string binCodeValue;
		private string handleUnitIdValue;
		private string itemNoValue;
		private float quantityValue;
		private string toBinCodeValue;
	
		private bool existsValue;
		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataWhseItemStore(string locationCode, string binCode)
		{
			this.locationCodeValue = locationCode;
			this.binCodeValue = binCode;
			updateMethod = "";
			//
			// TODO: Add constructor logic here
			//
		}


		public DataWhseItemStore(string locationCode, string binCode, SmartDatabase smartDatabase)
		{
			updateMethod = "";
			locationCodeValue = locationCode;
			binCodeValue = binCode;
			toBinCodeValue = "";
			this.smartDatabase = smartDatabase;
			existsValue = getFromDb();
		}

		public DataWhseItemStore(string handleUnitIdCode, SmartDatabase smartDatabase)
		{
			updateMethod = "";
			this.handleUnitIdValue = handleUnitIdCode;
			this.smartDatabase = smartDatabase;
			toBinCodeValue = "";
			existsValue = getFromDb();
		}

		public int seqNo
		{
			get
			{
				return seqNoValue;
			}
			set
			{
				seqNoValue = value;
			}
		}

		public string locationCode
		{
			get
			{
				return locationCodeValue;
			}
			set
			{
				locationCodeValue = value;
			}
		}

		public string binCode
		{
			get
			{
				return binCodeValue;
			}
			set
			{
				binCodeValue = value;
			}
		}

		public string handleUnitId
		{
			get
			{
				return handleUnitIdValue;
			}
			set
			{
				handleUnitIdValue = value;
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

		public string toBinCode
		{
			get
			{
				return toBinCodeValue;
			}
			set
			{
				toBinCodeValue = value;
			}
		}

		public bool exists
		{
			get
			{
				return existsValue;
			}
		}


		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM whseItemStore WHERE locationCode = '"+locationCodeValue+"' AND binCode = '"+binCodeValue+"'");

			if (dataReader.Read())
			{
				if (updateMethod.Equals("D"))
				{
					smartDatabase.nonQuery("DELETE FROM whseItemStore WHERE locationCode = '"+locationCodeValue+"' AND binCode = '"+binCodeValue+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE whseItemStore SET handleUnitId = '"+handleUnitIdValue+"', quantity = '"+quantityValue+"', itemNo = '"+itemNoValue+"', toBinCode = '"+toBinCodeValue+"' WHERE locationCode = '"+locationCodeValue+"' AND binCode = '"+binCodeValue+"'");
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
					smartDatabase.nonQuery("INSERT INTO whseItemStore (locationCode, binCode, handleUnitId, itemNo, quantity, toBinCode) VALUES ('"+locationCodeValue+"','"+binCodeValue+"','"+handleUnitIdValue+"','"+itemNoValue+"','"+quantityValue+"','"+toBinCodeValue+"')");
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
			SqlCeDataReader dataReader;

			if (handleUnitIdValue != null)
			{
				dataReader = smartDatabase.query("SELECT * FROM whseItemStore WHERE handleUnitId = '"+handleUnitIdValue+"'");
			}
			else
			{
				dataReader = smartDatabase.query("SELECT * FROM whseItemStore WHERE locationCode = '"+locationCodeValue+"' AND binCode = '"+binCodeValue+"'");
			}

			if (dataReader.Read())
			{
				try
				{
					this.locationCodeValue = (string)dataReader.GetValue(1);
					this.binCodeValue = (string)dataReader.GetValue(2);
					this.handleUnitIdValue = (string)dataReader.GetValue(3);
					this.itemNoValue = (string)dataReader.GetValue(4);
					this.quantityValue = float.Parse(dataReader.GetValue(5).ToString());
					this.toBinCodeValue = (string)dataReader.GetValue(6);
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
			smartDatabase.nonQuery("DELETE FROM whseItemStore WHERE locationCode = '"+locationCode+"' AND binCode = '"+binCode+"'");
		}

		#region ServiceArgument Members

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			// TODO:  Add DataWhseItemStore.toDOM implementation
			XmlElement whseItemStoreElement = xmlDocumentContext.CreateElement("WHSE_ITEM_STORE");
			whseItemStoreElement.SetAttribute("BIN_CODE", this.binCode);
			whseItemStoreElement.SetAttribute("LOCATION_CODE", this.locationCode);
			whseItemStoreElement.SetAttribute("HANDLE_UNIT", this.handleUnitId);
			whseItemStoreElement.SetAttribute("ITEM_NO", this.itemNo);
			whseItemStoreElement.SetAttribute("TO_BIN_CODE", this.toBinCode);
			
			whseItemStoreElement.AppendChild(xmlDocumentContext.CreateTextNode(quantity.ToString()));

			return whseItemStoreElement;
		}

		public void postDOM()
		{
			// TODO:  Add DataWhseItemStore.postDOM implementation
		}

		#endregion
	}
}
