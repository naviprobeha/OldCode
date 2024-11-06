using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemSize.
    /// </summary>
    public class DataItemSize
    {
        private string itemNoValue;
        private string sizeCodeValue;
        private int sortOrderValue;

        private string updateMethod;
        private SmartDatabase smartDatabase;

        public DataItemSize()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItemSize(XmlElement itemSizeElement, SmartDatabase smartDatabase, bool updateDb)
        {
            this.smartDatabase = smartDatabase;
            fromDOM(itemSizeElement);
            if (updateDb) commit();
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
                sizeCodeValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);
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
                if (fieldNo.FirstChild.Value.Equals("2")) sizeCodeValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("3")) sortOrderValue = int.Parse(fieldValue);

                i++;
            }
        }

        public void commit()
        {

            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemSize WHERE itemNo = '" + itemNoValue + "' AND sizeCode = '" + sizeCodeValue + "'");

            if (dataReader.Read())
            {
                if (updateMethod.Equals("D"))
                {
                    smartDatabase.nonQuery("DELETE FROM itemSize WHERE itemNo = '" + itemNoValue + "' AND sizeCode = '" + sizeCodeValue + "'");
                }
                else
                {

                    try
                    {
                        smartDatabase.nonQuery("UPDATE itemSize SET sortOrder = '" + sortOrderValue + "' WHERE itemNo = '" + itemNoValue + "' AND sizeCode = '" + sizeCodeValue + "'");
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
                    smartDatabase.nonQuery("INSERT INTO itemSize (itemNo, sizeCode, sortOrder) VALUES ('" + itemNoValue + "','" + sizeCodeValue + "','" + sortOrderValue + "')");
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
