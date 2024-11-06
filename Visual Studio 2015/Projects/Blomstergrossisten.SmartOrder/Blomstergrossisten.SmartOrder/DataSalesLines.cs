using System;
using System.Xml;
using System.Data.SqlServerCe;
using System.Data;
using System.Collections.Specialized;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataSalesLines.
    /// </summary>
    public class DataSalesLines : DataCollection
    {
        private SmartDatabase smartDatabase;
        private NameValueCollection quantityVector;

        public DataSalesLines(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSalesLines(DataSalesHeader dataSalesHeader, DataItem dataItem, DataColor dataColor, SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            this.quantityVector = new NameValueCollection();

            string colorCode = "";

            if (dataColor != null) colorCode = dataColor.code;

            SqlCeDataReader dataReader = smartDatabase.query("SELECT sizeCode, size2Code, quantity FROM salesLine WHERE orderNo = '" + dataSalesHeader.no + "' AND itemNo = '" + dataItem.no + "' AND colorCode = '" + colorCode + "'");

            while (dataReader.Read())
            {
                try
                {
                    string hashKey = dataReader.GetString(0) + ":" + dataReader.GetString(1);
                    this.quantityVector.Add(hashKey, float.Parse(dataReader.GetValue(2).ToString()).ToString());
                }
                catch (SqlCeException e)
                {
                    smartDatabase.ShowErrors(e);
                }
            }
            dataReader.Dispose();
        }

        public string getQuantity(DataSize dataSize, DataSize2 dataSize2)
        {
            string hashKey = "";

            if (dataSize2 != null)
            {
                hashKey = dataSize.code + ":" + dataSize2.code;
            }
            else
            {
                hashKey = dataSize.code + ":";
            }
            return quantityVector.Get(hashKey);
        }

        public void fromDOM(XmlElement tableElement, SmartDatabase smartDatabase)
        {
        }

        public DataSet getDataSet(DataSalesHeader dataSalesHeader)
        {
            SqlCeDataAdapter salesLineAdapter = smartDatabase.dataAdapterQuery("SELECT itemNo, colorCode, sum(quantity) as sumQuantity, sum(amount) as sumAmount FROM salesLine WHERE orderNo = '" + dataSalesHeader.no + "' GROUP BY itemNo, colorCode");

            DataSet salesLineDataSet = new DataSet();
            salesLineAdapter.Fill(salesLineDataSet, "salesLine");
            salesLineAdapter.Dispose();

            return salesLineDataSet;
        }

        public DataSet getDataSet(DataSalesHeader dataSalesHeader, DataItem dataItem)
        {
            SqlCeDataAdapter salesLineAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM salesLine WHERE orderNo = '" + dataSalesHeader.no + "' AND itemNo = '" + dataItem.no + "'");

            DataSet salesLineDataSet = new DataSet();
            salesLineAdapter.Fill(salesLineDataSet, "salesLine");
            salesLineAdapter.Dispose();

            return salesLineDataSet;
        }

        public DataSet getDataSet(DataSalesHeader dataSalesHeader, DataItem dataItem, DataColor dataColor)
        {
            SqlCeDataAdapter salesLineAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM salesLine WHERE orderNo = '" + dataSalesHeader.no + "' AND itemNo = '" + dataItem.no + "' AND colorCode = '" + dataColor.code + "'");

            DataSet salesLineDataSet = new DataSet();
            salesLineAdapter.Fill(salesLineDataSet, "salesLine");
            salesLineAdapter.Dispose();

            return salesLineDataSet;
        }

        public DataSet getSimpleDataSet(DataSalesHeader dataSalesHeader)
        {
            SqlCeDataAdapter salesLineAdapter = smartDatabase.dataAdapterQuery("SELECT *, Str(unitPrice, 8, 2) as formatedUnitPrice, Str(amount, 8, 2) as formatedAmount FROM salesLine WHERE orderNo = '" + dataSalesHeader.no + "' ORDER BY no DESC");

            DataSet salesLineDataSet = new DataSet();
            salesLineAdapter.Fill(salesLineDataSet, "salesLine");
            salesLineAdapter.Dispose();

            return salesLineDataSet;
        }

        public DataSet getReportDataSet(DataSalesHeader dataSalesHeader)
        {
            SqlCeDataAdapter salesLineAdapter = smartDatabase.dataAdapterQuery("SELECT itemNo, description, quantity, Str(unitPrice, 8, 2) as formatedUnitPrice, Str(amount, 8, 2) as formatedAmount, boxQuantity FROM salesLine WHERE orderNo = '" + dataSalesHeader.no + "' ORDER BY no DESC");

            DataSet salesLineDataSet = new DataSet();
            salesLineAdapter.Fill(salesLineDataSet, "salesLine");
            salesLineAdapter.Dispose();

            return salesLineDataSet;
        }

        public int countSalesLines(DataSalesHeader dataSalesHeader)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM salesLine WHERE orderNo = '" + dataSalesHeader.no + "'");

            if (dataReader.Read())
            {
                return dataReader.GetInt32(0);
            }

            return 0;
        }

        public int countSalesLineBoxes(DataSalesHeader dataSalesHeader)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT SUM(boxQuantity) FROM salesLine WHERE orderNo = '" + dataSalesHeader.no + "'");

            if (dataReader.Read())
            {
                return int.Parse(dataReader.GetValue(0).ToString());
            }

            return 0;
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            DataSetup dataSetup = new DataSetup(smartDatabase);
            Agent agent = new Agent(smartDatabase);

            string delimiter = dataSetup.spiraDelimiter;
            if (dataSetup.spiraDelimiter == "B") delimiter = " ";

            XmlElement tableElement = xmlDocumentContext.CreateElement("T");
            tableElement.SetAttribute("NO", "37");

            SqlCeDataReader dataReader = smartDatabase.query("SELECT l.no, orderNo, customerNo, itemNo, quantity, colorCode, sizeCode, size2Code, l.discount, deliveryDate, unitPrice, amount, boxQuantity FROM salesLine l, salesHeader h WHERE h.ready = 1 AND h.no = l.orderNo");

            while (dataReader.Read())
            {
                try
                {
                    XmlElement recordElement = xmlDocumentContext.CreateElement("R");
                    recordElement.SetAttribute("M", "I");

                    XmlElement fieldElement;

                    string itemNo = "";
                    string variantCode = "";

                    if (dataSetup.spiraEnabled)
                    {
                        if ((string)dataReader.GetValue(5) == "")
                        {
                            if ((string)dataReader.GetValue(7) == "")
                            {
                                itemNo = (string)dataReader.GetValue(3) + delimiter + (string)dataReader.GetValue(6);
                            }
                            else
                            {
                                itemNo = (string)dataReader.GetValue(3) + delimiter + (string)dataReader.GetValue(7) + delimiter + (string)dataReader.GetValue(6);
                            }
                        }
                        else
                        {
                            if ((string)dataReader.GetValue(7) == "")
                            {
                                itemNo = (string)dataReader.GetValue(3) + delimiter + (string)dataReader.GetValue(5) + delimiter + (string)dataReader.GetValue(6);
                            }
                            else
                            {
                                itemNo = (string)dataReader.GetValue(3) + delimiter + (string)dataReader.GetValue(5) + delimiter + (string)dataReader.GetValue(7) + delimiter + (string)dataReader.GetValue(6);
                            }
                        }
                    }
                    else
                    {
                        itemNo = (string)dataReader.GetValue(3);
                        variantCode = (string)dataReader.GetValue(5);
                    }


                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "1");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("1"));
                    recordElement.AppendChild(fieldElement);

                    /*
                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "2");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(2)));
                    recordElement.AppendChild(fieldElement);
                    */

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "3");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(agent.agentId + "" + (int)dataReader.GetValue(1)));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "4");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("" + ((int)dataReader.GetValue(0) * 10000)));
                    recordElement.AppendChild(fieldElement);


                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "5");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("2"));
                    recordElement.AppendChild(fieldElement);


                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "6");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(itemNo.Replace("½", "_-HALF-_")));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "15");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("" + float.Parse(dataReader.GetValue(4).ToString())));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "22");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(""+float.Parse(dataReader.GetValue(10).ToString())));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "29");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(""+float.Parse(dataReader.GetValue(11).ToString())));
                    recordElement.AppendChild(fieldElement);

                    if (variantCode != "")
                    {
                        fieldElement = xmlDocumentContext.CreateElement("F");
                        fieldElement.SetAttribute("NO", "5402");
                        fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(variantCode.Replace("½", "_-HALF-_")));
                        recordElement.AppendChild(fieldElement);
                    }

                    /*
                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "56001");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(3)));
                    recordElement.AppendChild(fieldElement);
                    */

                    /*
                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "56002");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(5)));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "56003");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(6)));
                    recordElement.AppendChild(fieldElement);
								
                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "56004");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(7)));
                    recordElement.AppendChild(fieldElement);
                    */

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "27");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("" + float.Parse(dataReader.GetValue(8).ToString())));
                    recordElement.AppendChild(fieldElement);


                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "5790");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(9)));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "50003");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(""+float.Parse(dataReader.GetValue(12).ToString())));
                    recordElement.AppendChild(fieldElement);


                    tableElement.AppendChild(recordElement);

                }
                catch (SqlCeException e)
                {
                    smartDatabase.ShowErrors(e);
                }
            }
            dataReader.Close();
            dataReader.Dispose();

            return tableElement;
        }

        public void setAdditionalInfo(DataSalesHeader dataSalesHeader, DataItem dataItem, DataColor dataColor, string additionalInfo)
        {
            try
            {
                smartDatabase.nonQuery("UPDATE salesLine SET " + additionalInfo + " WHERE orderNo = '" + dataSalesHeader.no + "' AND itemNo = '" + dataItem.no + "' AND colorCode = '" + dataColor.code + "'");
            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }

        public void setAdditionalInfo(DataSalesHeader dataSalesHeader, DataItem dataItem, string additionalInfo)
        {
            try
            {
                smartDatabase.nonQuery("UPDATE salesLine SET " + additionalInfo + " WHERE orderNo = '" + dataSalesHeader.no + "' AND itemNo = '" + dataItem.no + "'");
            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }

    }
}
