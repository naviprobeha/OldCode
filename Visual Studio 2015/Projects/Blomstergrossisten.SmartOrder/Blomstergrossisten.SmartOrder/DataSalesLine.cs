using System;
using System.Data.SqlServerCe;
using System.Data;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataSalesLine
    {
        private int noValue;
        private int orderNoValue;
        private string itemNoValue;
        private string colorCodeValue;
        private string sizeCodeValue;
        private string size2CodeValue;
        private string descriptionValue;
        private float quantityValue;
        private string baseUnitValue;
        private float discountValue;
        private float unitPriceValue;
        private float amountValue;
        private string deliveryDateValue;
        private string itemVariantDimCode;
        private float priceValue;
        private float boxQuantityValue;

        private int mode;
        private bool newLineValue;
        public static int MODE_SPIRA = 1;
        public static int MODE_SIMPLE = 2;

        private SmartDatabase smartDatabase;

        public DataSalesLine(DataSalesHeader dataSalesHeader, SmartDatabase smartDatabase)
        {
            this.mode = DataSalesLine.MODE_SIMPLE;

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            this.orderNo = dataSalesHeader.no;

            newLineValue = true;
        }

        public DataSalesLine(int no, SmartDatabase smartDatabase)
        {
            //
            // TODO: Add constructor logic here
            //
            this.no = no;
            this.smartDatabase = smartDatabase;
            this.mode = DataSalesLine.MODE_SIMPLE;
            getFromDb();
        }

        public DataSalesLine(DataSalesHeader dataSalesHeader, DataItem dataItem, DataColor dataColor, DataSize dataSize, DataSize2 dataSize2, float quantity, float discount, float unitPrice, float amount, string deliveryDate, float boxQuantity, SmartDatabase smartDatabase)
        {
            this.mode = DataSalesLine.MODE_SPIRA;

            this.smartDatabase = smartDatabase;
            getNextNo();
            this.orderNo = dataSalesHeader.no;
            this.itemNo = dataItem.no;

            if (dataColor != null)
                this.colorCode = dataColor.code;
            else
                this.colorCode = "";

            if (dataSize != null)
                this.sizeCode = dataSize.code;
            else
                this.sizeCode = "";

            if (dataSize2 != null)
                this.size2Code = dataSize2.code;
            else
                this.size2Code = "";

            this.quantity = quantity;
            this.description = dataItem.description;
            this.baseUnit = dataItem.baseUnit;
            this.unitPrice = unitPrice;
            this.amount = amount;

            this.discount = discount;
            this.deliveryDate = deliveryDate;
            this.boxQuantity = boxQuantity;

        }

        public DataSalesLine(DataSalesHeader dataSalesHeader, DataItem dataItem, DataItemVariantDim dataItemVariantDim, float quantity, float discount, float unitPrice, float amount, string deliveryDate, float boxQuantity, SmartDatabase smartDatabase)
        {
            this.mode = DataSalesLine.MODE_SIMPLE;

            this.smartDatabase = smartDatabase;
            getNextNo();
            this.orderNo = dataSalesHeader.no;
            this.itemNo = dataItem.no;

            if (dataItemVariantDim != null)
                this.colorCode = dataItemVariantDim.code;
            else
                this.colorCode = "";

            this.quantity = quantity;
            this.description = dataItem.description;
            this.baseUnit = dataItem.baseUnit;

            this.unitPrice = unitPrice;
            this.amount = amount;
            this.discount = discount;
            this.deliveryDate = deliveryDate;
            this.boxQuantity = boxQuantity;

        }

        public bool newLine
        {
            get
            {
                return newLineValue;
            }
        }

        public int no
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

        public int orderNo
        {
            get
            {
                return orderNoValue;
            }
            set
            {
                orderNoValue = value;
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

        public float quantity
        {
            get
            {
                return quantityValue;
            }
            set
            {
                quantityValue = value;
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

        public string deliveryDate
        {
            get
            {
                return deliveryDateValue;
            }
            set
            {
                deliveryDateValue = value;
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

        public void getNextNo()
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT no FROM salesLine ORDER BY no DESC");

            int nextNo = 1;

            if (dataReader.Read())
            {
                try
                {
                    nextNo = (int)dataReader.GetValue(0);
                    nextNo++;
                }
                catch (SqlCeException e)
                {
                    smartDatabase.ShowErrors(e);
                }
            }
            dataReader.Dispose();
            noValue = nextNo;
        }

        public void save()
        {
            try
            {
                SqlCeDataReader dataReader;

                if (mode == DataSalesLine.MODE_SPIRA)
                {
                    dataReader = smartDatabase.query("SELECT * FROM salesLine WHERE orderNo = '" + orderNo + "' AND itemNo = '" + itemNo + "' AND colorCode = '" + colorCode + "' AND sizeCode = '" + sizeCode + "' AND size2Code = '" + size2Code + "'");
                }
                else
                {
                    dataReader = smartDatabase.query("SELECT * FROM salesLine WHERE orderNo = '" + orderNo + "' AND no = '" + no + "'");
                }

                if (!dataReader.Read())
                {
                    if ((quantity > 0) || (quantity < 0))
                    {
                        smartDatabase.nonQuery("INSERT INTO salesLine (orderNo, itemNo, sizeCode, size2Code, colorCode, description, quantity, baseUnit, discount, deliveryDate, unitPrice, amount, boxQuantity) VALUES ('" + orderNo + "', '" + itemNo + "', '" + sizeCode + "', '" + size2Code + "', '" + colorCode + "', '" + description + "', '" + quantity + "', '" + baseUnit + "', '" + discount.ToString().Replace(",", ".") + "', '" + deliveryDate + "','" + unitPrice.ToString().Replace(",", ".") + "','" + amount.ToString().Replace(",", ".") + "','" + boxQuantity + "')");
                    }
                }
                else
                {
                    no = (int)dataReader.GetValue(0);

                    if (quantity == 0)
                    {
                        smartDatabase.nonQuery("DELETE FROM salesLine WHERE no = '" + no + "'");
                    }
                    else
                    {
                        smartDatabase.nonQuery("UPDATE salesLine SET orderNo = '" + orderNo + "', itemNo = '" + itemNo + "', sizeCode = '" + sizeCode + "', size2Code = '" + size2Code + "', colorCode = '" + colorCode + "', description = '" + description + "', quantity = '" + quantity.ToString().Replace(",", ".") + "', baseUnit = '" + baseUnit + "', discount = '" + discount.ToString().Replace(",", ".") + "', deliveryDate = '" + deliveryDate + "', unitPrice = '" + unitPrice.ToString().Replace(",", ".") + "', amount = '" + amount.ToString().Replace(",", ".") + "', boxQuantity = '" + boxQuantity.ToString().Replace(",", ".") + "' WHERE no = '" + no + "'");
                    }
                    dataReader.Close();
                }
                dataReader.Dispose();

                newLineValue = false;
            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }
        }

        public void delete()
        {
            try
            {
                smartDatabase.nonQuery("DELETE FROM salesLine WHERE no = '" + no + "'");

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }


        public static void deleteAll(SmartDatabase smartDatabase, DataSalesHeader dataSalesHeader, DataItem dataItem, DataColor dataColor)
        {
            try
            {
                smartDatabase.nonQuery("DELETE FROM salesLine WHERE orderNo = '" + dataSalesHeader.no + "' AND itemNo = '" + dataItem.no + "' AND colorCode = '" + dataColor.code + "'");
            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }
        }

        public static void delete(SmartDatabase smartDatabase, DataSalesHeader dataSalesHeader, DataItem dataItem)
        {
            try
            {
                smartDatabase.nonQuery("DELETE FROM salesLine WHERE orderNo = '" + dataSalesHeader.no + "' AND itemNo = '" + dataItem.no + "'");
            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }
        }

        public bool getFromDb()
        {
            SqlCeDataReader dataReader;

            if (mode == DataSalesLine.MODE_SPIRA)
            {
                dataReader = smartDatabase.query("SELECT * FROM salesLine WHERE orderNo = '" + orderNo + "' AND itemNo = '" + itemNo + "' AND colorCode = '" + colorCode + "' AND sizeCode = '" + sizeCode + "' AND size2Code = '" + size2Code + "'");
            }
            else
            {
                dataReader = smartDatabase.query("SELECT * FROM salesLine WHERE no = '" + no + "'");
            }

            if (dataReader.Read())
            {
                try
                {
                    this.no = dataReader.GetInt32(0);
                    this.orderNo = dataReader.GetInt32(1);
                    this.itemNo = (string)dataReader.GetValue(2);
                    this.colorCode = (string)dataReader.GetValue(3);
                    this.sizeCode = (string)dataReader.GetValue(4);
                    this.size2Code = (string)dataReader.GetValue(5);
                    this.description = (string)dataReader.GetValue(6);
                    this.baseUnit = (string)dataReader.GetValue(7);
                    this.quantityValue = float.Parse(dataReader.GetValue(8).ToString());
                    this.discountValue = float.Parse(dataReader.GetValue(9).ToString());
                    this.deliveryDateValue = (string)dataReader.GetValue(10);
                    this.unitPrice = float.Parse(dataReader.GetValue(11).ToString());
                    this.amount = float.Parse(dataReader.GetValue(12).ToString());
                    this.boxQuantity = float.Parse(dataReader.GetValue(13).ToString());
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
