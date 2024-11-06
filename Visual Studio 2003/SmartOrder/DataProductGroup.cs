using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataProductGroup
	{
		private string codeValue;
		private string descriptionValue;
	
		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataProductGroup()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataProductGroup(XmlElement customerElement, SmartDatabase smartDatabase, bool updateDb)
		{
			this.smartDatabase = smartDatabase;
			fromDOM(customerElement);				
			if (updateDb) commit();
		}

		public DataProductGroup(string code)
		{
			codeValue = code;
		}

		public DataProductGroup(string code, SmartDatabase smartDatabase)
		{
			codeValue = code;
			this.smartDatabase = smartDatabase;
			getFromDb();
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

		public void fromDOM(XmlElement recordElement)
		{
			XmlAttribute updateMethod = recordElement.GetAttributeNode("METHOD");
			this.updateMethod = updateMethod.FirstChild.Value;

			XmlNodeList fields = recordElement.GetElementsByTagName("FIELD");
			int i = 0;
			while (i < fields.Count)
			{
				XmlElement field = (XmlElement)fields.Item(i);
	
				XmlAttribute fieldNo = field.GetAttributeNode("NO");
				String fieldValue = "";

				if (field.HasChildNodes) fieldValue = field.FirstChild.Value;
				
				if (fieldNo.FirstChild.Value.Equals("1")) codeValue = fieldValue;
				if (fieldNo.FirstChild.Value.Equals("2")) descriptionValue = fieldValue;
	
				i++;
			}
		}

		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM productGroup WHERE code = '"+codeValue+"'");

			if (dataReader.Read())
			{
				if (updateMethod.Equals("DELETE"))
				{
					smartDatabase.nonQuery("DELETE FROM productGroup WHERE code = '"+codeValue+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE productGroup SET description = '"+descriptionValue+"' WHERE code = '"+codeValue+"'");
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
					smartDatabase.nonQuery("INSERT INTO productGroup (code, description) VALUES ('"+codeValue+"','"+descriptionValue+"')");
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
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM productGroup WHERE code = '"+codeValue+"'");

			if (dataReader.Read())
			{
				try
				{
					this.descriptionValue = (string)dataReader.GetValue(1);
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
