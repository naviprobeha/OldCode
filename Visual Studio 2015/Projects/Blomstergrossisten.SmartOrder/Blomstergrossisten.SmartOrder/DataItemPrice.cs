using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemPrice.
    /// </summary>
    public class DataItemPrice
    {
        private string itemNoValue;
        private string baseItemNoValue;
        private string salesCodeValue;
        private string startDateValue;
        private int typeValue;
        private float amountValue;
        private float discountAmountValue;
        private float discountValue;
        private string seasonCodeValue;
        private string variantCodeValue;

        private string updateMethod;
        private SmartDatabase smartDatabase;

        public DataItemPrice()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItemPrice(DataItem dataItem, DataColor dataColor, DataSize dataSize, DataSize2 dataSize2, DataCustomer dataCustomer, SmartDatabase smartDatabase)
        {
            DataSetup dataSetup = new DataSetup(smartDatabase);

            string delimiter = dataSetup.spiraDelimiter;
            if (dataSetup.spiraDelimiter == "B") delimiter = " ";

            this.smartDatabase = smartDatabase;
            this.baseItemNo = dataItem.no;

            if (dataSize2 == null)
            {
                this.itemNo = dataItem.no + delimiter + dataColor.code + delimiter + dataSize.code;
            }
            else
            {
                this.itemNo = dataItem.no + delimiter + dataColor.code + delimiter + dataSize2.code + delimiter + dataSize.code;
            }



            bool foundPrice = true;

            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemPrice WHERE itemNo = '" + this.itemNo + "' AND ((type = '0' AND priceGroupCode = '" + dataCustomer.no + "') OR (type = '1' AND priceGroupCode = '" + dataCustomer.priceGroupCode + "') OR (type = '2')) AND (startDate = '1900-01-01 00:00:00' OR startDate <= GETDATE()) AND variantCode = '" + variantCodeValue + "'");

            while (dataReader.Read())
            {

                try
                {
                    if (dataReader.GetFloat(4) < this.amountValue)
                    {
                        this.itemNo = (string)dataReader.GetValue(0);
                        this.salesCodeValue = (string)dataReader.GetValue(1);
                        this.startDateValue = dataReader.GetDateTime(2).ToString();
                        this.baseItemNoValue = (string)dataReader.GetValue(3);
                        this.amountValue = dataReader.GetFloat(4);
                        //this.vatAmountValue = dataReader.GetFloat(5);
                        this.discountValue = dataReader.GetFloat(6);
                        this.discountAmountValue = dataReader.GetFloat(7);
                        this.seasonCodeValue = (string)dataReader.GetValue(8);
                        this.typeValue = dataReader.GetInt32(9);
                        this.variantCodeValue = (string)dataReader.GetValue(10);
                    }

                }
                catch (SqlCeException e)
                {
                    smartDatabase.ShowErrors(e);
                }

            }
            dataReader.Dispose();
        }

        public DataItemPrice(DataItem dataItem, DataItemVariantDim dataItemVariantDim, DataCustomer dataCustomer, SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;

            if (dataItemVariantDim != null) variantCode = dataItemVariantDim.code;

            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemPrice WHERE itemNo = '" + this.itemNo + "' AND ((type = '0' AND priceGroupCode = '" + dataCustomer.no + "') OR (type = '1' AND priceGroupCode = '" + dataCustomer.priceGroupCode + "') OR (type = '2')) AND (startDate = '1900-01-01 00:00:00' OR startDate <= GETDATE()) AND variantCode = '" + variantCodeValue + "'");

            while (dataReader.Read())
            {

                try
                {
                    if (dataReader.GetFloat(4) < this.amountValue)
                    {
                        this.itemNo = (string)dataReader.GetValue(0);
                        this.salesCodeValue = (string)dataReader.GetValue(1);
                        this.startDateValue = dataReader.GetDateTime(2).ToString();
                        this.baseItemNoValue = (string)dataReader.GetValue(3);
                        this.amountValue = dataReader.GetFloat(4);
                        //this.vatAmountValue = dataReader.GetFloat(5);
                        this.discountValue = dataReader.GetFloat(6);
                        this.discountAmountValue = dataReader.GetFloat(7);
                        this.seasonCodeValue = (string)dataReader.GetValue(8);
                        this.typeValue = dataReader.GetInt32(9);
                        this.variantCodeValue = (string)dataReader.GetValue(10);
                    }

                }
                catch (SqlCeException e)
                {
                    smartDatabase.ShowErrors(e);
                }
            }
        }

        public DataItemPrice(XmlElement itemPriceElement, SmartDatabase smartDatabase, bool updateDb)
        {
            this.smartDatabase = smartDatabase;
            fromDOM(itemPriceElement);
            if (updateDb) commit();
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

        public float amount
        {
            get
            {
                return amountValue;
            }
            set
            {
                amountValue = value;
            }
        }

        public float discountAmount
        {
            get
            {
                return discountAmountValue;
            }
            set
            {
                discountAmountValue = value;
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


        public void fromDOM(XmlElement recordElement)
        {
            XmlAttribute updateMethod = recordElement.GetAttributeNode("M");
            XmlAttribute key = recordElement.GetAttributeNode("KEY");
            this.updateMethod = updateMethod.FirstChild.Value;

            if (this.updateMethod.Equals("D"))
            {
                // Item No.
                int startPos = key.FirstChild.Value.IndexOf("(") + 1;
                int endPos = key.FirstChild.Value.IndexOf(")");
                itemNoValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

                // Sales Type
                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);
                typeValue = int.Parse(key.FirstChild.Value.Substring(startPos, endPos - startPos));

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


                // Starting Date
                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);
                startDateValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);

                // Unit Code
                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);

                // Minimal Quantity
                startPos = key.FirstChild.Value.IndexOf("(", endPos) + 1;
                endPos = key.FirstChild.Value.IndexOf(")", startPos);

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

                if (fieldNo.FirstChild.Value.Equals("1")) itemNoValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("2")) salesCodeValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("4")) startDateValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("5")) amountValue = float.Parse(fieldValue.Replace(".", ","));
                if (fieldNo.FirstChild.Value.Equals("13")) typeValue = int.Parse(fieldValue);
                if (fieldNo.FirstChild.Value.Equals("5700")) variantCodeValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("50001")) discountAmountValue = float.Parse(fieldValue.Replace(".", ","));
                if (fieldNo.FirstChild.Value.Equals("50003")) discountValue = float.Parse(fieldValue.Replace(".", ","));
                if (fieldNo.FirstChild.Value.Equals("56000")) seasonCodeValue = fieldValue;
                if (fieldNo.FirstChild.Value.Equals("56005")) baseItemNoValue = fieldValue;

                i++;
            }

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

        }

        public void commit()
        {
            //System.Windows.Forms.MessageBox.Show("SELECT * FROM itemPrice WHERE itemNo = '"+itemNoValue+"' AND priceGroupCode = '"+priceGroupCodeValue+"' AND startDate = '"+startDateValue+"'");
            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemPrice WHERE itemNo = '" + itemNoValue + "' AND priceGroupCode = '" + salesCodeValue + "' AND startDate = '" + startDateValue + "' AND type = '" + typeValue + "' AND variantCode = '" + variantCodeValue + "'");

            if (dataReader != null)
            {
                if (dataReader.Read())
                {
                    if (updateMethod.Equals("D"))
                    {
                        smartDatabase.nonQuery("DELETE FROM itemPrice WHERE itemNo = '" + itemNoValue + "' AND priceGroupCode = '" + salesCodeValue + "' AND startDate = '" + startDateValue + "' AND type = '" + typeValue + "' AND variantCode = '" + variantCodeValue + "'");
                    }
                    else
                    {

                        try
                        {
                            smartDatabase.nonQuery("UPDATE itemPrice SET amount = '" + amountValue.ToString().Replace(",", ".") + "', discount = '" + discountValue.ToString().Replace(",", ".") + "', discountAmount = '" + discountAmountValue.ToString().Replace(",", ".") + "', seasonCode = '" + seasonCodeValue + "' WHERE itemNo = '" + itemNoValue + "' AND priceGroupCode = '" + salesCodeValue + "' AND startDate = '" + startDateValue + "' AND type = '" + typeValue + "' AND variantCode = '" + variantCodeValue + "'");
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
                        smartDatabase.nonQuery("INSERT INTO itemPrice (itemNo, priceGroupCode, startDate, baseItemNo, amount, discount, discountAmount, seasonCode, type, variantCode) VALUES ('" + itemNoValue + "','" + salesCodeValue + "','" + startDateValue + "','" + baseItemNo + "','" + amountValue.ToString().Replace(",", ".") + "','" + discountValue.ToString().Replace(",", ".") + "','" + discountAmountValue.ToString().Replace(",", ".") + "','" + seasonCodeValue + "', '" + typeValue + "','" + variantCodeValue + "')");
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
}
