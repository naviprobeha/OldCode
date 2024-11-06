using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataColor.
    /// </summary>
    public class DataSalesPerson
    {
        private string codeValue;

        private string updateMethod;
        private SmartDatabase smartDatabase;

        public DataSalesPerson()
        {
            codeValue = "";

            //
            // TODO: Add constructor logic here
            //
        }

        public DataSalesPerson(XmlElement customerElement, SmartDatabase smartDatabase, bool updateDb)
        {
            this.smartDatabase = smartDatabase;
            fromDOM(customerElement);
            if (updateDb) commit();
        }

        public DataSalesPerson(string code)
        {
            codeValue = code;
        }

        public DataSalesPerson(string code, SmartDatabase smartDatabase)
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


        public void fromDOM(XmlElement recordElement)
        {
            XmlAttribute updateMethod = recordElement.GetAttributeNode("M");
            XmlAttribute key = recordElement.GetAttributeNode("KEY");
            this.updateMethod = updateMethod.FirstChild.Value;

            if (this.updateMethod.Equals("D"))
            {
                int startPos = key.FirstChild.Value.IndexOf("(") + 1;
                int endPos = key.FirstChild.Value.IndexOf(")");
                codeValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);
            }

            XmlNodeList fields = recordElement.GetElementsByTagName("F");
            int i = 0;
            while (i < fields.Count)
            {
                XmlElement field = (XmlElement)fields.Item(i);

                XmlAttribute fieldNo = field.GetAttributeNode("NO");
                String fieldValue = "";

                if (field.HasChildNodes) fieldValue = field.FirstChild.Value;

                if (fieldNo.FirstChild.Value.Equals("1")) codeValue = fieldValue;

                i++;
            }
        }

        public void commit()
        {

            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM salesPerson WHERE code = '" + codeValue + "'");

            if (dataReader.Read())
            {
                if (updateMethod.Equals("D"))
                {
                    smartDatabase.nonQuery("DELETE FROM salesPerson WHERE code = '" + codeValue + "'");
                }
                else
                {

                    try
                    {
                        //smartDatabase.nonQuery("UPDATE salesPerson SET description = '"+descriptionValue+"' WHERE code = '"+codeValue+"'");
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
                    smartDatabase.nonQuery("INSERT INTO salesPerson (code) VALUES ('" + codeValue + "')");
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
            /*
            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM userReference WHERE code = '"+codeValue+"'");

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
            */
            return true;
        }

    }
}