using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataWhseActivityLine : ServiceArgument
	{
		private int seqNoValue;
		private string whseActivityNoValue;
		private int whseActivityTypeValue;
		private int lineNoValue;
		private string zoneValue;
		private string locationCodeValue;
		private string binCodeValue;
		private string itemNoValue;
		private float quantityValue;
		private string handleUnitIdValue;
		private int freqValue;
		private int actionValue;
		private int statusValue;
		private int linkedToLineNoValue;

		private bool existsValue;
		private string updateMethod;
		private SmartDatabase smartDatabase;


		public DataWhseActivityLine(int seqNo)
		{
			this.seqNoValue = seqNo;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataWhseActivityLine(XmlElement element, SmartDatabase smartDatabase, bool updateDb)
		{
			this.updateMethod = "";
			this.smartDatabase = smartDatabase;
			fromDOM(element);				
			if (updateDb) commit();
		}

		public DataWhseActivityLine(int seqNo, SmartDatabase smartDatabase)
		{
			this.updateMethod = "";
			seqNoValue = seqNo;
			this.smartDatabase = smartDatabase;
			existsValue = getFromDb("SELECT seqNo, whseActivityNo, whseActivityType, lineEntryNo, zone, locationCode, binCode, itemNo, quantity, freq, action, handleUnitId, status, linkedToLineNo FROM whseActivityLine WHERE seqNo = '"+seqNoValue+"'");
		}

		public DataWhseActivityLine(DataWhseActivityHeader dataWhseActivityHeader, int freq, DataBin dataBin, SmartDatabase smartDatabase)
		{
			this.updateMethod = "";
			this.whseActivityNoValue = dataWhseActivityHeader.no;
			this.whseActivityTypeValue = dataWhseActivityHeader.type;
			this.freqValue = freq;
			this.binCodeValue = dataBin.code;

			this.smartDatabase = smartDatabase;

			existsValue = getFromDb("SELECT seqNo, whseActivityNo, whseActivityType, lineEntryNo, zone, locationCode, binCode, itemNo, quantity, freq, action, handleUnitId, status, linkedToLineNo FROM whseActivityLine WHERE whseActivityNo = '"+whseActivityNoValue+"' AND whseActivityType = '"+whseActivityTypeValue+"' AND freq = '"+freqValue+"' AND binCode = '"+binCodeValue+"'");
		}

		public DataWhseActivityLine(DataBin dataBin, SmartDatabase smartDatabase)
		{
			this.updateMethod = "";
			this.binCodeValue = dataBin.code;

			this.smartDatabase = smartDatabase;

			existsValue = getFromDb("SELECT seqNo, whseActivityNo, whseActivityType, lineEntryNo, zone, locationCode, binCode, itemNo, quantity, freq, action, handleUnitId, status, linkedToLineNo FROM whseActivityLine WHERE binCode = '"+binCodeValue+"'");
		}

		public DataWhseActivityLine(DataWhseActivityHeader dataWhseActivityHeader, int freq, int action, DataItemUnit dataItemUnit, SmartDatabase smartDatabase)
		{
			this.updateMethod = "";
			this.whseActivityNoValue = dataWhseActivityHeader.no;
			this.whseActivityTypeValue = dataWhseActivityHeader.type;
			this.freqValue = freq;
			this.itemNoValue = dataItemUnit.itemNo;
			this.handleUnitIdValue = dataItemUnit.code;

			this.smartDatabase = smartDatabase;
			existsValue = getFromDb("SELECT seqNo, whseActivityNo, whseActivityType, lineEntryNo, zone, locationCode, binCode, itemNo, quantity, freq, action, handleUnitId, status, linkedToLineNo FROM whseActivityLine WHERE whseActivityNo = '"+whseActivityNoValue+"' AND whseActivityType = '"+whseActivityTypeValue+"' AND freq = '"+freq+"' AND action = '"+action+"' AND handleUnitId = '"+dataItemUnit.code+"'");
		}

		public DataWhseActivityLine(string handleUnitId, SmartDatabase smartDatabase)
		{
			this.updateMethod = "";
			this.freqValue = freq;
			this.handleUnitId = handleUnitId;

			this.smartDatabase = smartDatabase;

			existsValue = getFromDb("SELECT seqNo, whseActivityNo, whseActivityType, lineEntryNo, zone, locationCode, binCode, itemNo, quantity, freq, action, handleUnitId, status, linkedToLineNo FROM whseActivityLine WHERE handleUnitId = '"+handleUnitIdValue+"'");
		}

		public DataWhseActivityLine(DataWhseActivityLine dataWhseActivityLine, SmartDatabase smartDatabase)
		{
			this.updateMethod = "";
			this.whseActivityNoValue = dataWhseActivityLine.whseActivityNo;
			this.whseActivityTypeValue = dataWhseActivityLine.whseActivityType;
			this.linkedToLineNoValue = dataWhseActivityLine.lineNo;

			this.smartDatabase = smartDatabase;
			existsValue = getFromDb("SELECT seqNo, whseActivityNo, whseActivityType, lineEntryNo, zone, locationCode, binCode, itemNo, quantity, freq, action, handleUnitId, status, linkedToLineNo FROM whseActivityLine WHERE whseActivityNo = '"+whseActivityNoValue+"' AND whseActivityType = '"+whseActivityTypeValue+"' AND linkedToLineNo = '"+linkedToLineNoValue+"'");
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

		public string whseActivityNo
		{
			get
			{
				return whseActivityNoValue;
			}
			set
			{
				whseActivityNoValue = value;
			}
		}

		public int whseActivityType
		{
			get
			{
				return whseActivityTypeValue;
			}
			set
			{
				whseActivityTypeValue = value;
			}
		}

		public int lineNo
		{
			get
			{
				return lineNoValue;
			}
			set
			{
				lineNoValue = value;
			}
		}

		public int freq
		{
			get
			{
				return freqValue;
			}
			set
			{
				freqValue = value;
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

		public int status
		{
			get
			{
				return statusValue;
			}
			set
			{
				statusValue = value;
			}
		}

		public int linkedToLineNo
		{
			get
			{
				return linkedToLineNoValue;
			}
			set
			{
				linkedToLineNoValue = value;
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
				whseActivityTypeValue = int.Parse(key.FirstChild.Value.Substring(startPos,endPos-startPos)); 

				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);
				whseActivityNoValue = key.FirstChild.Value.Substring(startPos,endPos-startPos); 

				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);
				lineNoValue = int.Parse(key.FirstChild.Value.Substring(startPos,endPos-startPos)); 
			}

			XmlNodeList fields = recordElement.GetElementsByTagName("F");
			int i = 0;
			while (i < fields.Count)
			{
				XmlElement field = (XmlElement)fields.Item(i);
	
				XmlAttribute fieldNo = field.GetAttributeNode("NO");
				String fieldValue = "";

				if (field.HasChildNodes) fieldValue = field.FirstChild.Value;
				
				if (fieldNo.FirstChild.Value.Equals("1")) whseActivityTypeValue = int.Parse(fieldValue);
				if (fieldNo.FirstChild.Value.Equals("2")) whseActivityNoValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("3")) lineNoValue = int.Parse(fieldValue);
				if (fieldNo.FirstChild.Value.Equals("14")) itemNoValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("26")) quantityValue = float.Parse(fieldValue);
				if (fieldNo.FirstChild.Value.Equals("50007")) handleUnitIdValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("50008")) actionValue = int.Parse(fieldValue);
				if (fieldNo.FirstChild.Value.Equals("50010")) locationCodeValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("50011")) zoneValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("50012")) freqValue = int.Parse(fieldValue);
				if (fieldNo.FirstChild.Value.Equals("50013")) binCodeValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("50020")) linkedToLineNoValue = int.Parse(fieldValue);
				if (fieldNo.FirstChild.Value.Equals("50021")) statusValue = int.Parse(fieldValue);


	
				i++;
			}
		}

		public void commit()
		{
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM whseActivityLine WHERE whseActivityNo = '"+whseActivityNoValue+"' AND whseActivityType = '"+whseActivityTypeValue+"' AND lineEntryNo = '"+lineNoValue+"'");

			if (dataReader.Read())
			{
				if (updateMethod.Equals("D"))
				{
					smartDatabase.nonQuery("DELETE FROM whseActivityLine WHERE whseActivityNo = '"+whseActivityNoValue+"' AND whseActivityType = '"+whseActivityTypeValue+"' AND lineEntryNo = '"+lineNoValue+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE whseActivityLine SET zone = '"+zoneValue+"', locationCode = '"+locationCodeValue+"', binCode = '"+binCodeValue+"', itemNo = '"+itemNoValue+"', quantity = '"+quantityValue+"', freq = '"+freqValue+"', handleUnitId = '"+handleUnitIdValue+"', action = '"+actionValue+"', status = '"+statusValue+"', linkedToLineNo = '"+linkedToLineNoValue+"' WHERE whseActivityNo = '"+whseActivityNoValue+"' AND whseActivityType = '"+whseActivityTypeValue+"' AND lineEntryNo = '"+lineNoValue+"'");
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
					smartDatabase.nonQuery("INSERT INTO whseActivityLine (whseActivityNo, whseActivityType, lineEntryNo, zone, locationCode, binCode, itemNo, quantity, freq, action, handleUnitId, status, linkedToLineNo) VALUES ('"+whseActivityNoValue+"','"+whseActivityTypeValue+"','"+lineNoValue+"','"+zoneValue+"','"+locationCodeValue+"','"+binCodeValue+"','"+itemNoValue+"','"+quantityValue+"','"+freqValue+"','"+actionValue+"','"+handleUnitIdValue+"','"+statusValue+"','"+linkedToLineNoValue+"')");
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();	
		}

		public bool getFromDb(string query)
		{
			SqlCeDataReader dataReader;

			dataReader = smartDatabase.query(query);


			if (dataReader.Read())
			{
				try
				{
					this.seqNoValue = dataReader.GetInt32(0);
					this.whseActivityNoValue = (string)dataReader.GetValue(1);
					this.whseActivityTypeValue = dataReader.GetInt32(2);
					this.lineNoValue = dataReader.GetInt32(3);
					this.zoneValue = (string)dataReader.GetValue(4);
					this.locationCodeValue = (string)dataReader.GetValue(5);
					this.binCodeValue = (string)dataReader.GetValue(6);
					this.itemNoValue = (string)dataReader.GetValue(7);
					this.quantityValue = dataReader.GetFloat(8);
					this.freqValue = dataReader.GetInt32(9);
					this.actionValue = dataReader.GetInt32(10);
					this.handleUnitIdValue = (string)dataReader.GetValue(11);
					this.statusValue = dataReader.GetInt32(12);
					this.linkedToLineNoValue = dataReader.GetInt32(13);

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
			updateMethod = "D";
			commit();
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			// TODO:  Add DataBin.toDOM implementation

			XmlElement whseLineElement = xmlDocumentContext.CreateElement("WHSEACTIVITYLINE");
			
			whseLineElement.SetAttribute("BIN_CODE", this.binCode);
			whseLineElement.SetAttribute("LOCATION_CODE", this.locationCode);
			whseLineElement.SetAttribute("HANDLE_UNIT_ID", this.handleUnitId);
			//XmlElement binCodeElement = xmlDocumentContext.CreateElement("BIN_CODE");
			//binCodeElement.AppendChild(xmlDocumentContext.CreateTextNode(this.binCode));

			//XmlElement locationElement = xmlDocumentContext.CreateElement("LOCATION_CODE");
			//locationElement.AppendChild(xmlDocumentContext.CreateTextNode(this.locationCode));

			//XmlElement handleUnitIdElement = xmlDocumentContext.CreateElement("HANDLE_UNIT_ID");
			//handleUnitIdElement.AppendChild(xmlDocumentContext.CreateTextNode(this.handleUnitId));

			//whseLineElement.AppendChild(binCodeElement);
			//whseLineElement.AppendChild(locationElement);
			//whseLineElement.AppendChild(handleUnitIdElement);

			return whseLineElement;
		}

		public void postDOM()
		{
			// TODO:  Add DataBin.postDOM implementation
		}

	}
}
