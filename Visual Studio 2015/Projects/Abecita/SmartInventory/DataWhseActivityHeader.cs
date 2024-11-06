using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataWhseActivityHeader
	{
		private string noValue;
		private int typeValue;
		private int noOfLinesValue;
	
		private bool existsValue;
		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataWhseActivityHeader()
		{
			noValue = "";
			typeValue = 0;
			noOfLinesValue = 0;

			//
			// TODO: Add constructor logic here
			//
		}

		public DataWhseActivityHeader(XmlElement element, SmartDatabase smartDatabase, bool updateDb)
		{
			this.smartDatabase = smartDatabase;
			fromDOM(element);				
			if (updateDb) commit();
		}

		public DataWhseActivityHeader(string no, int type)
		{
			noValue = no;
			typeValue = type;
		}

		public DataWhseActivityHeader(string no, int type, SmartDatabase smartDatabase)
		{
			noValue = no;
			typeValue = type;
			this.smartDatabase = smartDatabase;
			existsValue = getFromDb();
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

		public int type
		{
			get
			{
				return typeValue;
			}
			set
			{
				typeValue = value;
			}
		}

		public int noOfLines
		{
			get
			{
				return noOfLinesValue;
			}
			set
			{
				noOfLinesValue = value;
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
				typeValue = int.Parse(key.FirstChild.Value.Substring(startPos,endPos-startPos)); 

				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);
				noValue = key.FirstChild.Value.Substring(startPos,endPos-startPos); 

			}

			XmlNodeList fields = recordElement.GetElementsByTagName("F");
			int i = 0;
			while (i < fields.Count)
			{
				XmlElement field = (XmlElement)fields.Item(i);
	
				XmlAttribute fieldNo = field.GetAttributeNode("NO");
				String fieldValue = "";

				if (field.HasChildNodes) fieldValue = field.FirstChild.Value;
				
				if (fieldNo.FirstChild.Value.Equals("1")) typeValue = int.Parse(fieldValue);
				if (fieldNo.FirstChild.Value.Equals("2")) noValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("13")) noOfLinesValue = int.Parse(fieldValue);
	
				i++;
			}
		}

		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM whseActivityHeader WHERE no = '"+noValue+"' AND type = '"+typeValue+"'");

			if (dataReader.Read())
			{
				if (updateMethod.Equals("D"))
				{
					smartDatabase.nonQuery("DELETE FROM whseActivityHeader WHERE no = '"+noValue+"' AND type = '"+typeValue+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE whseActivityHeader SET noOfLines = '"+noOfLinesValue+"' WHERE no = '"+noValue+"' AND type = '"+typeValue+"'");
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
					smartDatabase.nonQuery("INSERT INTO whseActivityHeader (no, type, noOfLines) VALUES ('"+noValue+"','"+typeValue+"','"+noOfLinesValue+"')");
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
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM whseActivityHeader WHERE no = '"+noValue+"' AND type = '"+typeValue+"'");

			if (dataReader.Read())
			{
				try
				{
					this.typeValue = dataReader.GetInt32(1);
					this.noOfLinesValue = dataReader.GetInt32(2);
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
