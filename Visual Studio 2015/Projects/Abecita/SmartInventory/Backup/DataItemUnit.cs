using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataItemUnit
	{
		private string codeValue;
		private string itemNoValue;
		private float quantityValue;
	
		private string updateMethod;
		private SmartDatabase smartDatabase;
		private bool existsValue;

		public DataItemUnit()
		{
			codeValue = "";

			//
			// TODO: Add constructor logic here
			//
		}

		public DataItemUnit(XmlElement element, SmartDatabase smartDatabase, bool updateDb)
		{
			this.smartDatabase = smartDatabase;
			fromDOM(element);				
			if (updateDb) commit();
		}

		public DataItemUnit(string code)
		{
			codeValue = code;
		}

		public DataItemUnit(string code, SmartDatabase smartDatabase)
		{
			codeValue = code;
			this.smartDatabase = smartDatabase;
			this.updateMethod = "";
			existsValue = getFromDb();
		}

		public string code
		{
			get
			{
				return codeValue;
			}
			set
			{
				codeValue = value;
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

		public bool exists
		{
			get
			{
				return existsValue;
			}
		}

		public void fromDOM(XmlElement recordElement)
		{
			XmlAttribute updateMethod = recordElement.GetAttributeNode("M");
			XmlAttribute key = recordElement.GetAttributeNode("KEY");
			this.updateMethod = updateMethod.FirstChild.Value;

			if (this.updateMethod.Equals("D"))
			{
				int startPos = key.FirstChild.Value.IndexOf("(")+1;
				int endPos = key.FirstChild.Value.IndexOf(")");
				itemNoValue = key.FirstChild.Value.Substring(startPos,endPos-startPos); 

				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);
				codeValue = key.FirstChild.Value.Substring(startPos,endPos-startPos); 

			}

			XmlNodeList fields = recordElement.GetElementsByTagName("F");
			int i = 0;
			while (i < fields.Count)
			{
				XmlElement field = (XmlElement)fields.Item(i);
	
				XmlAttribute fieldNo = field.GetAttributeNode("NO");
				String fieldValue = "";

				if (field.HasChildNodes) fieldValue = field.FirstChild.Value;
				
				if (fieldNo.FirstChild.Value.Equals("1")) itemNoValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("2")) codeValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("3")) quantityValue = float.Parse(fieldValue);
	
				i++;
			}
		}

		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemUnit WHERE code = '"+codeValue+"'");

			if (dataReader.Read())
			{
				if (updateMethod.Equals("D"))
				{
					smartDatabase.nonQuery("DELETE FROM itemUnit WHERE code = '"+codeValue+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE itemUnit SET itemNo = '"+itemNoValue+"', quantity = '"+quantityValue+"' WHERE code = '"+codeValue+"'");
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
					smartDatabase.nonQuery("INSERT INTO itemUnit (code, itemNo, quantity) VALUES ('"+codeValue+"','"+itemNoValue+"','"+quantityValue+"')");
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
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemUnit WHERE code = '"+codeValue+"'");

			if (dataReader.Read())
			{
				try
				{
					this.codeValue = (string)dataReader.GetValue(0);
					this.itemNoValue = (string)dataReader.GetValue(1);
					this.quantityValue = dataReader.GetFloat(2);
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
