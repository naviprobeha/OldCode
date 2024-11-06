using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataBin : ServiceArgument
	{
		private string codeValue;
		private string locationCodeValue;
		private string zoneCodeValue;
		private int emptyValue;
		private int blockingValue;
		private int inCompleteValue;

		private bool existsValue;
	
		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataBin()
		{
			codeValue = "";
			locationCodeValue = "";

			//
			// TODO: Add constructor logic here
			//
		}

		public DataBin(XmlElement element, SmartDatabase smartDatabase, bool updateDb)
		{
			this.smartDatabase = smartDatabase;
			fromDOM(element);				
			if (updateDb) commit();
		}

		public DataBin(string locationCode, string code)
		{
			locationCodeValue = locationCode;
			codeValue = code;
		}

		public DataBin(string locationCode, string code, SmartDatabase smartDatabase)
		{
			locationCodeValue = locationCode;
			codeValue = code;
			this.smartDatabase = smartDatabase;
			this.existsValue = getFromDb();
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

		public bool empty
		{
			get
			{
				if (emptyValue == 1) return true;
				return false;
			}
			set
			{
				emptyValue = 0;
				if (value == true) emptyValue = 1;
			}
		}

		public bool inComplete
		{
			get
			{
				if (inCompleteValue == 1) return true;
				return false;
			}
			set
			{
				inCompleteValue = 0;
				if (value == true) inCompleteValue = 1;
			}
		}

		public int blocking
		{
			get
			{
				return blockingValue;
			}
			set
			{
				blockingValue = value;
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
				locationCodeValue = key.FirstChild.Value.Substring(startPos,endPos-startPos); 

				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);
				codeValue = key.FirstChild.Value.Substring(startPos,endPos-startPos); 

			}

			emptyValue = 1;
			inCompleteValue = 0;

			XmlNodeList fields = recordElement.GetElementsByTagName("F");
			int i = 0;
			while (i < fields.Count)
			{
				XmlElement field = (XmlElement)fields.Item(i);
	
				XmlAttribute fieldNo = field.GetAttributeNode("NO");
				String fieldValue = "";

				if (field.HasChildNodes) fieldValue = field.FirstChild.Value;
				
				if (fieldNo.FirstChild.Value.Equals("1")) locationCodeValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("2")) codeValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("4")) zoneCodeValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("12")) blockingValue = int.Parse(fieldValue);
				if (fieldNo.FirstChild.Value.Equals("30"))
				{
					//if (fieldValue == "TRUE") emptyValue = 1;
				}
				if (fieldNo.FirstChild.Value.Equals("41"))
				{
					if (fieldValue == "TRUE") inCompleteValue = 1;
				}
	
				i++;
			}

		}


		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM bin WHERE code = '"+codeValue+"' AND locationCode = '"+locationCodeValue+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM bin WHERE code = '"+codeValue+"' AND locationCode = '"+locationCodeValue+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE bin SET zoneCode = '"+zoneCodeValue+"', blocking = '"+blockingValue+"', empty = '"+emptyValue+"', inComplete = '"+inCompleteValue+"' WHERE code = '"+codeValue+"' AND locationCode = '"+locationCodeValue+"'");
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
					smartDatabase.nonQuery("INSERT INTO bin (locationCode, code, zoneCode, blocking, empty, inComplete) VALUES ('"+locationCode+"','"+codeValue+"','"+zoneCodeValue+"','"+blockingValue+"','"+emptyValue+"','"+inCompleteValue+"')");
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
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM bin WHERE code = '"+codeValue+"' AND locationCode = '"+locationCodeValue+"'");

			if (dataReader.Read())
			{
				try
				{
					this.codeValue = (string)dataReader.GetValue(0);
					this.locationCodeValue = (string)dataReader.GetValue(1);
					this.zoneCodeValue = (string)dataReader.GetValue(2);
					this.blockingValue = dataReader.GetInt32(3);
					this.emptyValue = dataReader.GetInt32(4);
					this.inCompleteValue = dataReader.GetInt32(5);
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
		#region ServiceArgument Members

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			// TODO:  Add DataBin.toDOM implementation

			XmlElement binElement = xmlDocumentContext.CreateElement("BIN");
			
			XmlElement codeElement = xmlDocumentContext.CreateElement("CODE");
			codeElement.AppendChild(xmlDocumentContext.CreateTextNode(code));

			XmlElement locationElement = xmlDocumentContext.CreateElement("LOCATION_CODE");
			locationElement.AppendChild(xmlDocumentContext.CreateTextNode(locationCode));

			binElement.AppendChild(codeElement);
			binElement.AppendChild(locationElement);


			return binElement;
		}

		public void postDOM()
		{
			// TODO:  Add DataBin.postDOM implementation
		}

		#endregion
	}
}
