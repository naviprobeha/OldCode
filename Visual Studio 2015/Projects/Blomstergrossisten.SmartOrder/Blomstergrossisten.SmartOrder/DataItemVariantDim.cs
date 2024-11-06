using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemSize.
    /// </summary>
    public class DataItemVariantDim
    {
        private string itemNoValue;
        private string codeValue;
        private string descriptionValue;
        private string description2Value;
        private float boxQuantityValue;
        private float unitPriceValue;

        private string updateMethod;
        private SmartDatabase smartDatabase;

        public DataItemVariantDim()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItemVariantDim(DataItem dataItem, string code)
        {
            //
            // TODO: Add constructor logic here
            //
            this.itemNoValue = dataItem.no;
            this.codeValue = code;
        }

        public DataItemVariantDim(DataItem dataItem, string code, SmartDatabase smartDatabase)
        {
            //
            // TODO: Add constructor logic here
            //
            this.itemNoValue = dataItem.no;
            this.codeValue = code;
            this.smartDatabase = smartDatabase;
            getFromDb();
        }

        public DataItemVariantDim(XmlElement itemSizeElement, SmartDatabase smartDatabase, bool updateDb)
        {
            this.smartDatabase = smartDatabase;
            fromDOM(itemSizeElement);
            if (updateDb) commit();
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

        public string description2
        {
            get
            {
                return description2Value;
            }
            set
            {
                description2Value = value;
            }
        }

        public float boxQuantity
        {
            get
            {
                return boxQuantityValue;
            }
            set
            {
                boxQuantityValue = value;
            }
        }

        public float unitPrice
        {
            get
            {
                return unitPriceValue;
            }
            set
            {
                unitPriceValue = value;
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
                codeValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);
            }

            XmlNodeList fields = recordElement.GetElementsByTagName("F");
            int i = 0;
            while (i < fields.Count)
            {
                XmlElement field = (XmlElement)fields.Item(i);

                XmlAttribute fieldNo = field.GetAttributeNode("NO");
                String fieldValue = "";

                try
                {

                    if (field.HasChildNodes) fieldValue = field.FirstChild.Value;

                    if (fieldNo.FirstChild.Value.Equals("1")) codeValue = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("2")) itemNoValue = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("3")) descriptionValue = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("4")) description2Value = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("50004")) boxQuantityValue = float.Parse(fieldValue);
                    if (fieldNo.FirstChild.Value.Equals("50005")) unitPriceValue = float.Parse(fieldValue);

                }
                catch
                {
                    throw new Exception("Variant error. Item: " + itemNoValue + ", Field: " + fieldNo.FirstChild.Value + ", Value: " + fieldValue);

                }

                i++;
            }
        }


        public void commit()
        {

            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemVariantDim WHERE itemNo = '" + itemNoValue + "' AND code = '" + codeValue + "'");

            if (dataReader.Read())
            {
                if (updateMethod.Equals("D"))
                {
                    smartDatabase.nonQuery("DELETE FROM itemVariantDim WHERE itemNo = '" + itemNoValue + "' AND code = '" + codeValue + "'");
                }
                else
                {

                    try
                    {
                        smartDatabase.nonQuery("UPDATE itemVariantDim SET description = '" + descriptionValue + "', description2 = '" + description2Value + "', boxQuantity = '" + boxQuantityValue + "', unitPrice = '" + unitPriceValue + "' WHERE itemNo = '" + itemNoValue + "' AND code = '" + codeValue + "'");
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
                    smartDatabase.nonQuery("INSERT INTO itemVariantDim (itemNo, code, description, description2, boxQuantity, unitPrice) VALUES ('" + itemNoValue + "','" + codeValue + "','" + descriptionValue + "','" + description2Value + "','" + boxQuantityValue + "','" + unitPriceValue + "')");
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
            SqlCeDataReader dataReader = smartDatabase.query("SELECT description, description2, unitPrice FROM itemVariantDim WHERE itemNo = '" + itemNoValue + "' AND code = '" + codeValue + "'");

            if (dataReader.Read())
            {
                try
                {
                    this.descriptionValue = (string)dataReader.GetValue(0);
                    this.description2Value = (string)dataReader.GetValue(1);
                    this.unitPrice = dataReader.GetFloat(2);
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
