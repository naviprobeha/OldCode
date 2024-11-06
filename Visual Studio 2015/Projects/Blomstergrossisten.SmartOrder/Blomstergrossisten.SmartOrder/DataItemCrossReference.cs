using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemSize.
    /// </summary>
    public class DataItemCrossReference
    {
        private string itemNoValue;
        private string variantDimCodeValue;
        private string unitCodeValue;
        private int typeValue;
        private string noValue;
        private string referenceNoValue;
        private string descriptionValue;

        private DataItem dataItem;

        private string updateMethod;
        private SmartDatabase smartDatabase;

        public DataItemCrossReference()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItemCrossReference(XmlElement itemCrossReferenceElement, SmartDatabase smartDatabase, bool updateDb)
        {
            this.smartDatabase = smartDatabase;
            fromDOM(itemCrossReferenceElement);
            if (updateDb) commit();
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

        public string variantDimCode
        {
            get
            {
                return variantDimCodeValue;
            }
            set
            {
                variantDimCodeValue = value;
            }
        }

        public string unitCode
        {
            get
            {
                return unitCodeValue;
            }
            set
            {
                unitCodeValue = value;
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

        public string referenceNo
        {
            get
            {
                return referenceNoValue;
            }
            set
            {
                referenceNoValue = value;
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

        public DataItem item
        {
            get
            {
                return dataItem;
            }
            set
            {
                dataItem = value;
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
                variantDimCodeValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

                startPos = key.FirstChild.Value.IndexOf("(", startPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", endPos + 1);
                unitCodeValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

                startPos = key.FirstChild.Value.IndexOf("(", startPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", endPos + 1);
                typeValue = int.Parse(key.FirstChild.Value.Substring(startPos, endPos - startPos));

                startPos = key.FirstChild.Value.IndexOf("(", startPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", endPos + 1);
                noValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

                startPos = key.FirstChild.Value.IndexOf("(", startPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", endPos + 1);
                referenceNoValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

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
                if (fieldNo.FirstChild.Value.Equals("2")) variantDimCodeValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("3")) unitCodeValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("4")) typeValue = int.Parse(fieldValue);
                if (fieldNo.FirstChild.Value.Equals("5")) noValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("6")) referenceNoValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("7")) descriptionValue = fieldValue;

                i++;
            }
        }

        public void commit()
        {

            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemCrossReference WHERE itemNo = '" + itemNoValue + "' AND variantDimCode = '" + variantDimCodeValue + "' AND unitCode = '" + unitCodeValue + "' AND type = '" + typeValue + "' AND no = '" + noValue + "' AND referenceNo = '" + referenceNoValue + "'");

            if (dataReader.Read())
            {
                if (updateMethod.Equals("D"))
                {
                    smartDatabase.nonQuery("DELETE FROM itemCrossReference WHERE itemNo = '" + itemNoValue + "' AND variantDimCode = '" + variantDimCodeValue + "' AND unitCode = '" + unitCodeValue + "' AND type = '" + typeValue + "' AND no = '" + noValue + "' AND referenceNo = '" + referenceNoValue + "'");
                }
                else
                {

                    try
                    {
                        smartDatabase.nonQuery("UPDATE itemCrossReference SET description = '" + descriptionValue + "' WHERE itemNo = '" + itemNoValue + "' AND variantDimCode = '" + variantDimCodeValue + "' AND unitCode = '" + unitCodeValue + "' AND type = '" + typeValue + "' AND no = '" + noValue + "' AND referenceNo = '" + referenceNoValue + "'");
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
                    smartDatabase.nonQuery("INSERT INTO itemCrossReference (itemNo, variantDimCode, unitCode, type, no, referenceNo, description) VALUES ('" + itemNoValue + "','" + variantDimCodeValue + "','" + unitCodeValue + "','" + typeValue + "','" + noValue + "','" + referenceNoValue + "','" + descriptionValue + "')");
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
