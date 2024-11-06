using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemColor.
    /// </summary>
    public class DataItemColor
    {
        private string itemNoValue;
        private string colorCodeValue;

        private string updateMethod;
        private SmartDatabase smartDatabase;

        public DataItemColor()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItemColor(XmlElement itemColorElement, SmartDatabase smartDatabase, bool updateDb)
        {
            this.smartDatabase = smartDatabase;
            fromDOM(itemColorElement);
            if (updateDb) commit();
        }

        public string colorCode
        {
            get
            {
                return colorCodeValue;
            }
            set
            {
                colorCodeValue = value;
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
                colorCodeValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);
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
                if (fieldNo.FirstChild.Value.Equals("2")) colorCodeValue = fieldValue;

                i++;
            }
        }

        public void commit()
        {

            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemColor WHERE itemNo = '" + itemNoValue + "' AND colorCode = '" + colorCodeValue + "'");

            if (dataReader.Read())
            {
                if (updateMethod.Equals("D"))
                {
                    smartDatabase.nonQuery("DELETE FROM itemColor WHERE itemNo = '" + itemNoValue + "' AND colorCode = '" + colorCodeValue + "'");
                }
            }
            else
            {

                try
                {
                    smartDatabase.nonQuery("INSERT INTO itemColor (itemNo, colorCode) VALUES ('" + itemNoValue + "','" + colorCodeValue + "')");
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
