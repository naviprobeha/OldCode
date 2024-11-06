using System;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataSetup.
    /// </summary>
    public class DataSetup
    {
        private string hostValue;
        private string receiverValue;

        private string spiraDelimiterValue;
        private int spiraEnabledValue;

        private int itemScannerEnabledValue;
        private int deliveryDateTodayValue;
        private int itemSearchMethodValue;

        private int askSynchronizationValue;
        private int askPostingMethodValue;

        private int showOrderItemsItemNoValue;
        private int showOrderItemsVariantValue;
        private int showOrderItemsBaseUnitValue;
        private int showOrderItemsDeliveryDateValue;

        private int useDynamicPricesValue;

        private int itemDeletionDaysValue;

        private int printOnLocalPrinterValue;


        public int internalProductGroupSelectedIndex;
        public string internalUserReferenceCode;


        private SmartDatabase smartDatabase;

        public DataSetup()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSetup(SmartDatabase smartDatabase)
        {
            hostValue = "";
            receiverValue = "";

            spiraDelimiterValue = "";
            spiraEnabledValue = 0;

            itemScannerEnabledValue = 0;

            this.smartDatabase = smartDatabase;
            refresh();
        }

        public void refresh()
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM setup");

            if (dataReader.Read())
            {
                try
                {
                    hostValue = (string)dataReader.GetValue(1);
                    receiverValue = (string)dataReader.GetValue(6);
                    spiraEnabledValue = dataReader.GetInt32(7);
                    spiraDelimiterValue = (string)dataReader.GetValue(8);
                    itemScannerEnabledValue = dataReader.GetInt32(9);
                    deliveryDateTodayValue = dataReader.GetInt32(11);
                    itemSearchMethodValue = dataReader.GetInt32(12);
                    askSynchronizationValue = dataReader.GetInt32(13);
                    askPostingMethodValue = dataReader.GetInt32(14);
                    showOrderItemsItemNoValue = dataReader.GetInt32(15);
                    showOrderItemsVariantValue = dataReader.GetInt32(16);
                    showOrderItemsBaseUnitValue = dataReader.GetInt32(17);
                    showOrderItemsDeliveryDateValue = dataReader.GetInt32(18);
                    useDynamicPricesValue = dataReader.GetInt32(19);
                    itemDeletionDaysValue = dataReader.GetInt32(20);
                    printOnLocalPrinterValue = dataReader.GetInt32(21);

                }
                catch (SqlCeException e)
                {
                    smartDatabase.ShowErrors(e);
                }
            }
            dataReader.Dispose();
        }


        public void save()
        {
            try
            {
                SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM setup");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO setup (primaryKey, host, receiver, spiraEnabled, spiraDelimiter, itemScannerEnabled, deliveryDateToday, itemSearchMethod, askSynchronization, askPostingMethod, showOrderItemsItemNo, showOrderItemsVariant, showOrderItemsBaseUnit, showOrderItemsDeliveryDate, useDynamicPrices, itemDeletionDays, printOnLocalPrinter) VALUES (1, '" + host + "','" + receiver + "', '" + spiraEnabledValue + "','" + spiraDelimiter + "','" + itemScannerEnabledValue + "','" + deliveryDateTodayValue + "','" + itemSearchMethodValue + "','" + askSynchronizationValue + "','" + askPostingMethodValue + "','" + showOrderItemsItemNoValue + "','" + showOrderItemsVariantValue + "','" + showOrderItemsBaseUnitValue + "','" + showOrderItemsDeliveryDateValue + "','" + useDynamicPricesValue + "','" + itemDeletionDaysValue + "','" + printOnLocalPrinterValue + "')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE setup SET host = '" + host + "', receiver = '" + receiver + "', spiraEnabled = '" + spiraEnabledValue + "', spiraDelimiter = '" + spiraDelimiter + "', itemScannerEnabled = '" + itemScannerEnabledValue + "', deliveryDateToday = '" + deliveryDateTodayValue + "', itemSearchMethod = '" + itemSearchMethodValue + "', askSynchronization = '" + askSynchronizationValue + "', askPostingMethod = '" + askPostingMethodValue + "', showOrderItemsItemNo = '" + showOrderItemsItemNoValue + "', showOrderItemsVariant = '" + showOrderItemsVariantValue + "', showOrderItemsBaseUnit = '" + showOrderItemsBaseUnitValue + "', showOrderItemsDeliveryDate = '" + showOrderItemsDeliveryDateValue + "', useDynamicPrices = '" + useDynamicPricesValue + "', itemDeletionDays = '" + itemDeletionDaysValue + "', printOnLocalPrinter = '" + printOnLocalPrinterValue + "' WHERE primaryKey = " + dataReader.GetValue(0));
                    dataReader.Close();
                }
                dataReader.Dispose();

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }

        public string host
        {
            set
            {
                hostValue = value;
            }
            get
            {
                return hostValue;
            }
        }

        public string receiver
        {
            set
            {
                receiverValue = value;
            }
            get
            {
                return receiverValue;
            }
        }

        public bool spiraEnabled
        {
            set
            {
                if (value == true)
                {
                    spiraEnabledValue = 1;
                }
                else
                {
                    spiraEnabledValue = 0;
                }
            }
            get
            {
                if (spiraEnabledValue == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string spiraDelimiter
        {
            set
            {
                spiraDelimiterValue = value;
            }
            get
            {
                return spiraDelimiterValue;
            }
        }

        public bool itemScannerEnabled
        {
            set
            {
                if (value == true)
                {
                    itemScannerEnabledValue = 1;
                }
                else
                {
                    itemScannerEnabledValue = 0;
                }
            }
            get
            {
                if (itemScannerEnabledValue == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool deliveryDateToday
        {
            set
            {
                if (value == true)
                {
                    deliveryDateTodayValue = 1;
                }
                else
                {
                    deliveryDateTodayValue = 0;
                }
            }
            get
            {
                if (deliveryDateTodayValue == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int itemSearchMethod
        {
            set
            {
                itemSearchMethodValue = value;
            }
            get
            {
                return itemSearchMethodValue;
            }
        }

        public bool askSynchronization
        {
            set
            {
                if (value == true)
                {
                    askSynchronizationValue = 1;
                }
                else
                {
                    askSynchronizationValue = 0;
                }
            }
            get
            {
                if (askSynchronizationValue == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool askPostingMethod
        {
            set
            {
                if (value == true)
                {
                    askPostingMethodValue = 1;
                }
                else
                {
                    askPostingMethodValue = 0;
                }
            }
            get
            {
                if (askPostingMethodValue == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool showOrderItemsItemNo
        {
            set
            {
                if (value == true)
                {
                    showOrderItemsItemNoValue = 1;
                }
                else
                {
                    showOrderItemsItemNoValue = 0;
                }
            }
            get
            {
                if (showOrderItemsItemNoValue == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool showOrderItemsVariant
        {
            set
            {
                if (value == true)
                {
                    showOrderItemsVariantValue = 1;
                }
                else
                {
                    showOrderItemsVariantValue = 0;
                }
            }
            get
            {
                if (showOrderItemsVariantValue == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool showOrderItemsBaseUnit
        {
            set
            {
                if (value == true)
                {
                    showOrderItemsBaseUnitValue = 1;
                }
                else
                {
                    showOrderItemsBaseUnitValue = 0;
                }
            }
            get
            {
                if (showOrderItemsBaseUnitValue == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool showOrderItemsDeliveryDate
        {
            set
            {
                if (value == true)
                {
                    showOrderItemsDeliveryDateValue = 1;
                }
                else
                {
                    showOrderItemsDeliveryDateValue = 0;
                }
            }
            get
            {
                if (showOrderItemsDeliveryDateValue == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool useDynamicPrices
        {
            set
            {
                if (value == true)
                {
                    useDynamicPricesValue = 1;
                }
                else
                {
                    useDynamicPricesValue = 0;
                }
            }
            get
            {
                if (useDynamicPricesValue == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int itemDeletionDays
        {
            set
            {
                itemDeletionDaysValue = value;
            }
            get
            {
                return itemDeletionDaysValue;
            }
        }

        public bool printOnLocalPrinter
        {
            set
            {
                printOnLocalPrinterValue = 0;
                if (value == true) printOnLocalPrinterValue = 1;
            }
            get
            {
                if (printOnLocalPrinterValue == 1) return true;
                return false;
            }
        }

    }
}
