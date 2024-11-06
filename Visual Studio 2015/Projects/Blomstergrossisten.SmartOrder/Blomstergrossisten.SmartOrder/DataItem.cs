using System;
using System.Xml;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItem.
    /// </summary>
    public class DataItem : ServiceArgument
    {
        private string noValue;
        private string descriptionValue;
        private string baseUnitValue;
        private float priceValue;
        private string seasonValue;
        private string productGroupValue;
        private string eanCodeValue;
        private float defaultQuantityValue;
        private string itemDiscountGroupValue;
        private string lastUpdatedValue;
        private string searchDescriptionValue;
        private string barcode1Value;
        private string barcode2Value;
        private string barcode3Value;
        private string barcode4Value;
        private string barcode5Value;

        private float inventoryValue;

        private string updateMethod;
        private SmartDatabase smartDatabase;

        private bool barCodeMode;
        public bool barCodeFound;

        private bool variantChecked;
        private bool hasVariantsValue;

        public DataItem()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItem(XmlElement itemElement, SmartDatabase smartDatabase, bool updateDb)
        {
            updateMethod = "";
            this.smartDatabase = smartDatabase;
            fromDOM(itemElement);
            if (updateDb) commit();
        }

        public DataItem(XmlElement itemElement, SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            fromDOM(itemElement, true);
        }

        public DataItem(string no)
        {
            updateMethod = "";
            noValue = no;
        }

        public DataItem(string no, SmartDatabase smartDatabase)
        {
            updateMethod = "";
            noValue = no;
            this.smartDatabase = smartDatabase;
            getFromDb();
        }

        public DataItem(string barCode, SmartDatabase smartDatabase, bool barCodeMode)
        {
            updateMethod = "";
            barcode1Value = barCode;
            this.barCodeMode = barCodeMode;
            this.smartDatabase = smartDatabase;
            this.barCodeFound = false;
            if (getFromDb()) this.barCodeFound = true;
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

        public string baseUnit
        {
            get
            {
                return baseUnitValue;
            }
            set
            {
                baseUnitValue = value;
            }
        }

        public float price
        {
            get
            {
                return priceValue;
            }
            set
            {
                priceValue = value;
            }
        }

        public string season
        {
            get
            {
                return seasonValue;
            }
            set
            {
                seasonValue = value;
            }
        }

        public string productGroup
        {
            get
            {
                return productGroupValue;
            }
            set
            {
                productGroupValue = value;
            }
        }

        public float defaultQuantity
        {
            get
            {
                return defaultQuantityValue;
            }
            set
            {
                defaultQuantityValue = value;
            }
        }

        public float inventory
        {
            get
            {
                return inventoryValue;
            }
            set
            {
                inventoryValue = value;
            }
        }

        public string itemDiscountGroup
        {
            get
            {
                return itemDiscountGroupValue;
            }
            set
            {
                itemDiscountGroupValue = value;
            }
        }

        public string barcode1
        {
            get
            {
                return barcode1Value;
            }
            set
            {
                barcode1Value = value;
            }
        }

        public string barcode2
        {
            get
            {
                return barcode2Value;
            }
            set
            {
                barcode2Value = value;
            }
        }

        public string barcode3
        {
            get
            {
                return barcode3Value;
            }
            set
            {
                barcode3Value = value;
            }
        }

        public string barcode4
        {
            get
            {
                return barcode4Value;
            }
            set
            {
                barcode4Value = value;
            }
        }

        public string barcode5
        {
            get
            {
                return barcode5Value;
            }
            set
            {
                barcode5Value = value;
            }
        }
        public string lastUpdated
        {
            get
            {
                return lastUpdatedValue;
            }
        }

        public int getNoOfVariants()
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM itemVariant WHERE baseItemNo = '" + noValue + "'");

            if (dataReader.Read())
            {
                int count = dataReader.GetInt32(0);
                dataReader.Close();
                dataReader.Dispose();
                return count;
            }
            return 0;
        }

        public bool hasColors()
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemColor WHERE itemNo = '" + noValue + "'");

            if (dataReader.Read())
            {
                dataReader.Close();
                dataReader.Dispose();
                return true;
            }
            return false;
        }

        public bool hasVariants()
        {
            if (!variantChecked)
            {
                variantChecked = true;
                SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemVariantDim WHERE itemNo = '" + noValue + "'");

                if (dataReader.Read())
                {
                    dataReader.Close();
                    dataReader.Dispose();

                    hasVariantsValue = true;
                    return true;

                }
                hasVariantsValue = false;
                return false;
            }
            else
            {
                return hasVariantsValue;
            }
        }

        public void setSmartDatabase(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
        }

        public bool hasSize()
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemSize WHERE itemNo = '" + noValue + "'");

            if (dataReader.Read())
            {
                dataReader.Close();
                dataReader.Dispose();
                return true;
            }
            return false;
        }

        public bool hasSize2()
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM itemSize2 WHERE itemNo = '" + noValue + "'");

            if (dataReader.Read())
            {
                dataReader.Close();
                dataReader.Dispose();
                return true;
            }
            return false;
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
                noValue = key.FirstChild.Value.Substring(startPos, endPos - startPos);
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

                    if (fieldNo.FirstChild.Value.Equals("1")) noValue = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("3")) descriptionValue = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("4")) searchDescriptionValue = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("8")) baseUnitValue = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("14")) itemDiscountGroupValue = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("18")) priceValue = float.Parse(fieldValue);
                    if ((fieldNo.FirstChild.Value.Equals("62")) && (fieldValue != "")) lastUpdatedValue = "20" + fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("5702")) productGroupValue = fieldValue;		// Item Category
                    if (fieldNo.FirstChild.Value.Equals("50000")) defaultQuantityValue = float.Parse(fieldValue);
                    if (fieldNo.FirstChild.Value.Equals("56103")) seasonValue = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("50071")) barcode1Value = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("50072")) barcode2Value = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("50073")) barcode3Value = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("50074")) barcode4Value = fieldValue;
                    if (fieldNo.FirstChild.Value.Equals("50075")) barcode5Value = fieldValue;
                }
                catch
                {
                    throw new Exception("Item error. Item: " + noValue + ", Field: " + fieldNo.FirstChild.Value + ", Value: " + fieldValue);
                }

                i++;
            }
        }

        public void fromDOM(XmlElement itemElement, bool byMembers)
        {
            XmlAttribute itemNo = itemElement.GetAttributeNode("NO");
            this.no = itemNo.Value;

            XmlNodeList values = itemElement.GetElementsByTagName("VALUE");
            if (values.Count > 0)
            {
                XmlElement valueElement = (XmlElement)values.Item(0);
                if (valueElement.FirstChild != null)
                    this.inventory = float.Parse(valueElement.FirstChild.Value);
            }

        }

        public void commit()
        {

            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM item WHERE no = '" + noValue + "'");

            if (dataReader.Read())
            {
                if (updateMethod.Equals("D"))
                {
                    smartDatabase.nonQuery("DELETE FROM item WHERE no = '" + noValue + "'");
                }
                else
                {

                    try
                    {
                        smartDatabase.nonQuery("UPDATE item SET description = '" + descriptionValue + "', baseUnit = '" + baseUnitValue + "', price = '" + priceValue.ToString().Replace(",", ".") + "', productGroupCode = '" + productGroupValue + "', seasonCode = '" + seasonValue + "', defaultQuantity = '" + defaultQuantityValue + "', itemDiscountGroup = '" + itemDiscountGroupValue + "', lastUpdated = '" + lastUpdatedValue + "', searchDescription = '" + searchDescriptionValue + "', barcode1 = '" + barcode1Value + "', barcode2 = '" + barcode2Value + "', barcode3 = '" + barcode3Value + "', barcode4 = '" + barcode4Value + "', barcode5 = '" + barcode5Value + "' WHERE no = '" + noValue + "'");
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
                    smartDatabase.nonQuery("INSERT INTO item (no, description, baseUnit, price, productGroupCode, seasonCode, eanCode, defaultQuantity, itemDiscountGroup, lastUpdated, searchDescription, barcode1, barcode2, barcode3, barcode4, barcode5) VALUES ('" + noValue + "','" + descriptionValue + "','" + baseUnitValue + "','" + priceValue.ToString().Replace(",", ".") + "','" + productGroupValue + "','" + seasonValue + "', '', '" + defaultQuantity + "', '" + itemDiscountGroup + "', '" + lastUpdated + "', '" + searchDescriptionValue + "', '" + barcode1Value + "', '" + barcode2Value + "', '" + barcode3Value + "', '" + barcode4Value + "', '" + barcode5Value + "')");
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
            if (barCodeMode)
            {
                SqlCeDataReader dataReader = smartDatabase.query("SELECT no, description, baseUnit, price, defaultQuantity, itemDiscountGroup, lastUpdated, searchDescription, barcode1, barcode2, barcode3, barcode4, barcode5 FROM item WHERE (barcode1 = '" + barcode1Value + "') OR (barcode2 = '" + barcode1Value + "') OR (barcode3 = '" + barcode1Value + "') OR (barcode4 = '" + barcode1Value + "') OR (barcode5 = '" + barcode1Value + "')");

                if (dataReader.Read())
                {
                    try
                    {
                        this.noValue = (string)dataReader.GetValue(0);
                        this.descriptionValue = (string)dataReader.GetValue(1);
                        this.baseUnitValue = (string)dataReader.GetValue(2);
                        this.priceValue = float.Parse(dataReader.GetValue(3).ToString());
                        this.defaultQuantity = float.Parse(dataReader.GetValue(4).ToString());
                        this.itemDiscountGroup = (string)dataReader.GetValue(5);
                        this.lastUpdatedValue = dataReader.GetDateTime(6).ToString("yyyy-MM-dd");
                        this.searchDescriptionValue = (string)dataReader.GetValue(7);
                        this.barcode1Value = (string)dataReader.GetValue(8);
                        this.barcode2Value = (string)dataReader.GetValue(9);
                        this.barcode3Value = (string)dataReader.GetValue(10);
                        this.barcode4Value = (string)dataReader.GetValue(11);
                        this.barcode5Value = (string)dataReader.GetValue(12);
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
            else
            {
                SqlCeDataReader dataReader = smartDatabase.query("SELECT no, description, baseUnit, price, defaultQuantity, itemDiscountGroup, lastUpdated, searchDescription, barcode1, barcode2, barcode3, barcode4, barcode5 FROM item WHERE no = '" + no + "'");

                if (dataReader.Read())
                {
                    try
                    {
                        this.descriptionValue = (string)dataReader.GetValue(1);
                        this.baseUnitValue = (string)dataReader.GetValue(2);
                        this.priceValue = float.Parse(dataReader.GetValue(3).ToString());
                        this.defaultQuantity = float.Parse(dataReader.GetValue(4).ToString());
                        this.itemDiscountGroup = (string)dataReader.GetValue(5);
                        this.lastUpdatedValue = dataReader.GetDateTime(6).ToString("YYYY-mm-dd");
                        this.searchDescriptionValue = (string)dataReader.GetValue(7);
                        this.barcode1Value = (string)dataReader.GetValue(8);
                        this.barcode2Value = (string)dataReader.GetValue(9);
                        this.barcode3Value = (string)dataReader.GetValue(10);
                        this.barcode4Value = (string)dataReader.GetValue(11);
                        this.barcode5Value = (string)dataReader.GetValue(12);
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

        #region ServiceArgument Members

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            // TODO:  Add DataItem.toDOM implementation
            XmlElement itemElement = xmlDocumentContext.CreateElement("ITEM");

            XmlElement noElement = xmlDocumentContext.CreateElement("NO");
            noElement.AppendChild(xmlDocumentContext.CreateTextNode(no));

            XmlElement descriptionElement = xmlDocumentContext.CreateElement("DESCRIPTION");
            descriptionElement.AppendChild(xmlDocumentContext.CreateTextNode(description));

            XmlElement baseUnitElement = xmlDocumentContext.CreateElement("BASE_UNIT");
            baseUnitElement.AppendChild(xmlDocumentContext.CreateTextNode(baseUnit));

            XmlElement priceElement = xmlDocumentContext.CreateElement("PRICE");
            priceElement.AppendChild(xmlDocumentContext.CreateTextNode(price.ToString().Replace(".", ",")));

            itemElement.AppendChild(noElement);
            itemElement.AppendChild(descriptionElement);
            itemElement.AppendChild(baseUnitElement);
            itemElement.AppendChild(priceElement);

            return itemElement;
        }

        #endregion
    }
}
