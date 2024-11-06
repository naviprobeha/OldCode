using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemSize2.
    /// </summary>
    public class DataItemSize2
    {
        private string itemNoValue;
        private string size2CodeValue;

        private string updateMethod;
        private SmartDatabase smartDatabase;

        public DataItemSize2()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItemSize2(XmlElement itemSize2Element, SmartDatabase smartDatabase, bool updateDb)
        {
            this.smartDatabase = smartDatabase;
            fromDOM(itemSize2Element);
            if (updateDb) commit();
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

        public void fromDOM(XmlElement recordElement)
        {
            XmlAttribute updateMethod = recordElement.GetAttributeNode("M");
            XmlAttribute key = recordElement.GetAttributeNode("KEY");
            this.updateMethod = updateMethod.FirstChild.Value;

            if (this.updateMethod.Equals("D"))
            {
                int startPos = key.FirstChild.Value.IndexOf("(") + 1;
                int endPos = key.FirstChild.Value.IndexOf(")");
                itemNoValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

                startPos = key.FirstChild.Value.IndexOf("(", startPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", endPos + 1);
                size2CodeValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);
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
                if (fieldNo.FirstChild.Value.Equals("2")) size2CodeValue = fieldValue;

                i++;
            }
        }

        public void commit()
        {

            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemSize2 WHERE itemNo = '" + itemNoValue + "' AND size2Code = '" + size2CodeValue + "'");

            if (dataReader.Read())
            {
                if (updateMethod.Equals("D"))
                {
                    smartDatabase.nonQuery("DELETE FROM itemSize2 WHERE itemNo = '" + itemNoValue + "' AND size2Code = '" + size2CodeValue + "'");
                }
            }
            else
            {

                try
                {
                    smartDatabase.nonQuery("INSERT INTO itemSize2 (itemNo, size2Code) VALUES ('" + itemNoValue + "','" + size2CodeValue + "')");
                }
                catch (SqlCeException e)
                {
                    smartDatabase.ShowErrors(e);
                }
            }
            dataReader.Dispose();
        }
    }
}
