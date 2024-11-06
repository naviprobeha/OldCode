using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemPrice.
    /// </summary>
    public class DataLineDiscount
    {
        private string codeValue;
        private string salesCodeValue;
        private string startDateValue;
        private float discountValue;
        private int salesTypeValue;
        private float minimumQuantityValue;
        private string endDateValue;
        private int typeValue;
        private string variantCodeValue;

        private string updateMethod;
        private SmartDatabase smartDatabase;

        public DataLineDiscount()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataLineDiscount(DataItem dataItem, DataCustomer dataCustomer, DataItemVariantDim dataItemVariant, float quantity, SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            string variantCode = "";
            if (dataItemVariant != null) variantCode = dataItemVariant.code;

            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM lineDiscount WHERE ((type = '0' AND code = '" + dataItem.no + "' AND variantCode = '" + variantCode + "') OR (type = '1' AND code = '" + dataItem.itemDiscountGroup + "')) AND ((salesType = '0' AND salesCode = '" + dataCustomer.no + "') OR (salesType = '1' AND salesCode = '" + dataCustomer.customerDiscountGroup + "') OR (salesType = '2')) AND (startDate = '1900-01-01 00:00:00' OR startDate <= GETDATE()) AND (endDate = '1900-01-01 00:00:00' OR endDate > GETDATE()) AND minimumQuantity <= '" + quantity + "'");

            while (dataReader.Read())
            {
                try
                {
                    if (float.Parse(dataReader.GetValue(7).ToString()) > this.discountValue)
                    {
                        this.typeValue = dataReader.GetInt32(0);
                        this.codeValue = (string)dataReader.GetValue(1);
                        this.salesTypeValue = dataReader.GetInt32(2);
                        this.salesCodeValue = (string)dataReader.GetValue(3);
                        this.startDateValue = dataReader.GetDateTime(4).ToString();
                        this.variantCodeValue = (string)dataReader.GetValue(5);
                        this.minimumQuantityValue = float.Parse(dataReader.GetValue(6).ToString());
                        this.discountValue = float.Parse(dataReader.GetValue(7).ToString());
                        this.endDateValue = dataReader.GetDateTime(8).ToString();
                    }
                }
                catch (SqlCeException e)
                {
                    smartDatabase.ShowErrors(e);
                }
            }
            dataReader.Dispose();

        }

        public DataLineDiscount(XmlElement itemPriceElement, SmartDatabase smartDatabase, bool updateDb)
        {
            this.smartDatabase = smartDatabase;
            fromDOM(itemPriceElement);
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

        public string salesCode
        {
            get
            {
                return salesCodeValue;
            }
            set
            {
                salesCodeValue = value;
            }
        }

        public string startDate
        {
            get
            {
                return startDateValue;
            }
            set
            {
                startDateValue = value;
            }
        }

        public float discount
        {
            get
            {
                return discountValue;
            }
            set
            {
                discountValue = value;
            }
        }

        public int salesType
        {
            get
            {
                return salesTypeValue;
            }
            set
            {
                salesTypeValue = value;
            }
        }

        public float minimumQuantity
        {
            get
            {
                return minimumQuantityValue;
            }
            set
            {
                minimumQuantityValue = value;
            }
        }

        public string endDate
        {
            get
            {
                return endDateValue;
            }
            set
            {
                endDateValue = value;
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

        public string variantCode
        {
            get
            {
                return variantCodeValue;
            }
            set
            {
                variantCodeValue = value;
            }
        }

        public void fromDOM(XmlElement recordElement)
        {
            XmlAttribute updateMethod = recordElement.GetAttributeNode("M");
            XmlAttribute key = recordElement.GetAttributeNode("KEY");
            this.updateMethod = updateMethod.FirstChild.Value;

            if (this.updateMethod.Equals("D"))
            {
                // Type.
                int startPos = key.FirstChild.Value.IndexOf("(") + 1;
                int endPos = key.FirstChild.Value.IndexOf(")");
                typeValue = int.Parse(key.FirstChild.Value.Substring(startPos, endPos - startPos));

                // Code
                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);
                codeValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

                // Sales Type
                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);
                salesTypeValue = int.Parse(key.FirstChild.Value.Substring(startPos, endPos - startPos));

                // Sales Code
                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);
                salesCodeValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

                // Start Date
                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);
                startDateValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

                // Currency Code
                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);

                // Variant Code
                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);
                variantCodeValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

                // Unit of measure Code
                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);

                // Minimum Quantity
                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);
                minimumQuantityValue = int.Parse(key.FirstChild.Value.Substring(startPos, endPos - startPos));

            }

            XmlNodeList fields = recordElement.GetElementsByTagName("F");
            int i = 0;
            while (i < fields.Count)
            {
                XmlElement field = (XmlElement)fields.Item(i);

                XmlAttribute fieldNo = field.GetAttributeNode("NO");
                String fieldValue = "";

                if (field.HasChildNodes) fieldValue = field.FirstChild.Value;

                //System.Windows.Forms.MessageBox.Show(fieldValue);

                if (fieldNo.FirstChild.Value.Equals("1")) codeValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("2")) salesCodeValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("4")) startDateValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("5")) discountValue = float.Parse(fieldValue.Replace(".", ","));
                if (fieldNo.FirstChild.Value.Equals("13")) salesTypeValue = int.Parse(fieldValue);
                if (fieldNo.FirstChild.Value.Equals("14")) minimumQuantityValue = float.Parse(fieldValue);
                if (fieldNo.FirstChild.Value.Equals("15")) endDateValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("21")) typeValue = int.Parse(fieldValue);
                if (fieldNo.FirstChild.Value.Equals("5700")) variantCodeValue = fieldValue;

                if (startDateValue != null)
                {
                    if (startDateValue.Length == 8)
                    {
                        string year = startDateValue.Substring(0, 2);
                        int yearVal = int.Parse(year);
                        if ((yearVal >= 90) && (yearVal <= 99))
                        {
                            startDateValue = "19" + startDateValue;
                        }
                        else
                        {
                            startDateValue = "20" + startDateValue;
                        }
                    }
                }

                if (endDateValue != null)
                {
                    if (endDateValue.Length == 8)
                    {
                        string year = endDateValue.Substring(0, 2);
                        int yearVal = int.Parse(year);
                        if ((yearVal >= 90) && (yearVal <= 99))
                        {
                            endDateValue = "19" + endDateValue;
                        }
                        else
                        {
                            endDateValue = "20" + endDateValue;
                        }
                    }
                }

                i++;
            }
        }

        public void commit()
        {
            //System.Windows.Forms.MessageBox.Show("SELECT * FROM itemPrice WHERE itemNo = '"+itemNoValue+"' AND priceGroupCode = '"+priceGroupCodeValue+"' AND startDate = '"+startDateValue+"'");
            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM lineDiscount WHERE type = '" + typeValue + "' AND code = '" + codeValue + "' AND salesType = '" + salesTypeValue + "' AND salesCode = '" + salesCodeValue + "' AND startDate = '" + startDateValue + "' AND variantCode = '" + variantCodeValue + "' AND minimumQuantity = '" + minimumQuantityValue + "'");

            if (dataReader != null)
            {
                if (dataReader.Read())
                {
                    if (updateMethod.Equals("D"))
                    {
                        smartDatabase.nonQuery("DELETE FROM lineDiscount WHERE type = '" + typeValue + "' AND code = '" + codeValue + "' AND salesType = '" + salesTypeValue + "' AND salesCode = '" + salesCodeValue + "' AND startDate = '" + startDateValue + "' AND variantCode = '" + variantCodeValue + "' AND minimumQuantity = '" + minimumQuantityValue + "'");
                    }
                    else
                    {

                        try
                        {
                            smartDatabase.nonQuery("UPDATE lineDiscount SET discount = '" + discountValue.ToString().Replace(",", ".") + "', endDate = '" + endDateValue.ToString().Replace(",", ".") + "' WHERE type = '" + typeValue + "' AND code = '" + codeValue + "' AND salesType = '" + salesTypeValue + "' AND salesCode = '" + salesCodeValue + "' AND startDate = '" + startDateValue + "' AND variantCode = '" + variantCodeValue + "' AND minimumQuantity = '" + minimumQuantityValue + "'");
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
                        smartDatabase.nonQuery("INSERT INTO lineDiscount (type, code, salesType, salesCode, startDate, variantCode, minimumQuantity, discount, endDate) VALUES ('" + typeValue + "','" + codeValue + "','" + salesTypeValue + "','" + salesCodeValue + "','" + startDateValue + "','" + variantCodeValue + "','" + minimumQuantityValue.ToString().Replace(",", ".") + "','" + discountValue.ToString().Replace(",", ".") + "','" + endDateValue + "')");
                    }
                    catch (SqlCeException e)
                    {
                        smartDatabase.ShowErrors(e);
                    }
                }
                dataReader.Dispose();
            }
        }

        public bool getFromDb()
        {

            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM lineDiscount WHERE type = '" + typeValue + "' AND code = '" + codeValue + "' AND salesType = '" + salesTypeValue + "' AND salesCode = '" + salesCodeValue + "' startDate = '" + startDateValue + "' AND variantCode = '" + variantCodeValue + "' AND minimumQuantity = '" + minimumQuantityValue + "'");

            if (dataReader.Read())
            {
                try
                {
                    this.typeValue = dataReader.GetInt32(0);
                    this.codeValue = (string)dataReader.GetValue(1);
                    this.salesTypeValue = dataReader.GetInt32(2);
                    this.salesCodeValue = (string)dataReader.GetValue(3);
                    this.startDateValue = dataReader.GetDateTime(4).ToString();
                    this.variantCodeValue = (string)dataReader.GetValue(5);
                    this.minimumQuantityValue = float.Parse(dataReader.GetValue(6).ToString());
                    this.discountValue = float.Parse(dataReader.GetValue(7).ToString());
                    this.endDateValue = dataReader.GetDateTime(8).ToString();
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
