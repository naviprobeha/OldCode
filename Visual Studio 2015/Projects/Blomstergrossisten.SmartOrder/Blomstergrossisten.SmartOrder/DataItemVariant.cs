using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemVariant.
    /// </summary>
    public class DataItemVariant
    {
        private string baseItemNoValue;
        private string colorCodeValue;
        private string sizeCodeValue;
        private string size2CodeValue;
        private int validValue;
        private string eanCodeValue;

        private string updateMethod;
        private SmartDatabase smartDatabase;

        public DataItemVariant()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItemVariant(XmlElement itemVariantElement, SmartDatabase smartDatabase, bool updateDb)
        {
            this.smartDatabase = smartDatabase;
            fromDOM(itemVariantElement);
            if (updateDb) commit();
        }

        public DataItemVariant(string baseItemNo, string colorCode, string sizeCode, string size2Code, SmartDatabase smartDatabase)
        {
            baseItemNoValue = baseItemNo;
            colorCodeValue = colorCode;
            sizeCodeValue = sizeCode;
            size2CodeValue = size2Code;

            if (size2Code == null) size2CodeValue = "";

            this.smartDatabase = smartDatabase;
            getFromDb();
        }

        public string baseItemNo
        {
            get
            {
                return baseItemNoValue;
            }
            set
            {
                baseItemNoValue = value;
            }
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

        public bool valid
        {
            get
            {
                if (validValue == 1) return true;
                return false;
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
                baseItemNoValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);
                colorCodeValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);
                sizeCodeValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);
                size2CodeValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);
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

                    if (fieldNo.FirstChild.Value.Equals("1")) baseItemNoValue = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("2")) colorCodeValue = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("3")) sizeCodeValue = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("4")) size2CodeValue = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("12"))
                    {
                        if (fieldValue == "TRUE") validValue = 1;
                    }
                }
                catch
                {
                    throw new Exception("Variant error. Item: " + baseItemNoValue + ", Field: " + fieldNo.FirstChild.Value + ", Value: " + fieldValue);

                }

                i++;
            }
        }

        public void commit()
        {

            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemVariant WHERE baseItemNo = '" + baseItemNoValue + "' AND colorCode = '" + colorCodeValue + "' AND sizeCode = '" + sizeCodeValue + "' AND size2Code = '" + size2CodeValue + "'");

            if (dataReader.Read())
            {
                if (updateMethod.Equals("DELETE"))
                {
                    smartDatabase.nonQuery("DELETE FROM itemVariant WHERE baseItemNo = '" + baseItemNoValue + "' AND colorCode = '" + colorCodeValue + "' AND sizeCode = '" + sizeCodeValue + "' AND size2Code = '" + size2CodeValue + "'");
                }
                else
                {

                    try
                    {
                        smartDatabase.nonQuery("UPDATE itemVariant SET valid = '" + validValue + "' WHERE baseItemNo = '" + baseItemNoValue + "' AND colorCode = '" + colorCodeValue + "' AND sizeCode = '" + sizeCodeValue + "' AND size2Code = '" + size2CodeValue + "'");
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
                    smartDatabase.nonQuery("INSERT INTO itemVariant (baseItemNo, colorCode, sizeCode, size2Code, valid, eanCode) VALUES ('" + baseItemNoValue + "','" + colorCodeValue + "','" + sizeCodeValue + "','" + size2CodeValue + "','" + validValue + "','" + eanCodeValue + "')");
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
            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemVariant WHERE baseItemNo = '" + baseItemNoValue + "' AND colorCode = '" + colorCodeValue + "' AND sizeCode = '" + sizeCodeValue + "' AND size2Code = '" + size2CodeValue + "'");

            if (dataReader.Read())
            {
                try
                {
                    this.validValue = dataReader.GetInt32(4);
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
