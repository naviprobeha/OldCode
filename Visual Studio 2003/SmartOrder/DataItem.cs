using System;
using System.Xml;
using System.Data.SqlServerCe;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for DataItem.
	/// </summary>
	public class DataItem
	{
		private string noValue;
		private string descriptionValue;
		private string baseUnitValue;
		private float priceValue;
	
		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataItem()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataItem(XmlElement customerElement, SmartDatabase smartDatabase, bool updateDb)
		{
			this.smartDatabase = smartDatabase;
			fromDOM(customerElement);				
			if (updateDb) commit();
		}

		public DataItem(string no)
		{
			noValue = no;
		}

		public DataItem(string no, SmartDatabase smartDatabase)
		{
			noValue = no;
			this.smartDatabase = smartDatabase;
			getFromDb();
		}

		public string no
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

		public float price
		{
			get
			{
				return priceValue;
			}
			set
			{
				priceValue = value;
			}
		}

		public bool hasColors()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemColor WHERE itemNo = '"+noValue+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				dataReader.Dispose();
				return true;
			}
			return false;
		}

		public bool hasSize2()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemSize2 WHERE itemNo = '"+noValue+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				dataReader.Dispose();
				return true;
			}
			return false;
		}

		public void fromDOM(XmlElement recordElement)
		{
			XmlAttribute updateMethod = recordElement.GetAttributeNode("METHOD");
			XmlAttribute key = recordElement.GetAttributeNode("KEY");
			this.updateMethod = updateMethod.FirstChild.Value;

			if (this.updateMethod.Equals("DELETE"))
			{
				int startPos = key.FirstChild.Value.IndexOf("(")+1;
				int endPos = key.FirstChild.Value.IndexOf(")");
				noValue = key.FirstChild.Value.Substring(startPos,endPos-startPos); 
			}

			XmlNodeList fields = recordElement.GetElementsByTagName("FIELD");
			int i = 0;
			while (i < fields.Count)
			{
				XmlElement field = (XmlElement)fields.Item(i);
	
				XmlAttribute fieldNo = field.GetAttributeNode("NO");
				String fieldValue = "";

				if (field.HasChildNodes) fieldValue = field.FirstChild.Value;
				
				if (fieldNo.FirstChild.Value.Equals("1")) noValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("3")) descriptionValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("8")) baseUnitValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("18")) priceValue = float.Parse(fieldValue);
	
				i++;
			}
		}

		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM item WHERE no = '"+noValue+"'");

			if (dataReader.Read())
			{
				if (updateMethod.Equals("DELETE"))
				{
					smartDatabase.nonQuery("DELETE FROM item WHERE no = '"+noValue+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE item SET description = '"+descriptionValue+"', baseUnit = '"+baseUnitValue+"', price = '"+priceValue+"' WHERE no = '"+noValue+"'");
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
					smartDatabase.nonQuery("INSERT INTO item (no, description, baseUnit, price) VALUES ('"+noValue+"','"+descriptionValue+"','"+baseUnitValue+"','"+priceValue+"')");
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
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM item WHERE no = '"+no+"'");

			if (dataReader.Read())
			{
				try
				{
					this.descriptionValue = (string)dataReader.GetValue(1);
					this.baseUnitValue = (string)dataReader.GetValue(2);
					//this.priceValue = float.Parse((string)dataReader.GetValue(3));
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
