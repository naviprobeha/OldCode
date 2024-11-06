using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for SalesHeaders.
    /// </summary>
    public class DataSalesHeaders : DataCollection
    {
        private SmartDatabase smartDatabase;

        public DataSalesHeaders(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            //
            // TODO: Add constructor logic here
            //
        }

        public void fromDOM(XmlElement tableElement, SmartDatabase smartDatabase)
        {
        }

        public DataSet getDataSet(bool readyFlag)
        {
            SqlCeDataAdapter salesHeaderAdapter;

            Agent agent = new Agent(smartDatabase);

            if (readyFlag)
            {
                salesHeaderAdapter = smartDatabase.dataAdapterQuery("SELECT *, '" + agent.agentId + "'+LTRIM(STR(no)) as orderNo FROM salesHeader WHERE ready = 1");
            }
            else
            {
                salesHeaderAdapter = smartDatabase.dataAdapterQuery("SELECT *, '" + agent.agentId + "'+LTRIM(STR(no)) as orderNo FROM salesHeader WHERE ready = 0");
            }


            DataSet salesHeaderDataSet = new DataSet();
            salesHeaderAdapter.Fill(salesHeaderDataSet, "salesHeader");
            salesHeaderAdapter.Dispose();

            return salesHeaderDataSet;
        }

        public void deleteReadySalesHeaders()
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM salesHeader WHERE ready = 1");

            while (dataReader.Read())
            {
                try
                {
                    smartDatabase.nonQuery("DELETE FROM salesLine WHERE orderNo = " + (int)dataReader.GetValue(0));
                }
                catch (SqlCeException e)
                {
                    smartDatabase.ShowErrors(e);
                }

            }
            dataReader.Close();
            dataReader.Dispose();
            smartDatabase.nonQuery("DELETE FROM salesHeader WHERE ready = 1");

        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            Agent agent = new Agent(smartDatabase);



            XmlElement tableElement = xmlDocumentContext.CreateElement("T");
            tableElement.SetAttribute("NO", "36");

            SqlCeDataReader dataReader = smartDatabase.query("SELECT no, customerNo, name, address, address2, zipCode, city, deliveryCode, deliveryName, deliveryAddress, deliveryAddress2, deliveryZipCode, deliveryCity, deliveryContact, ready, contact, phoneNo, paymentMethod, postingMethod, discount, referenceCode, salesPersonCode, preInvoiceNo, orderDate FROM salesHeader WHERE ready = 1");

            while (dataReader.Read())
            {
                try
                {
                    XmlElement recordElement = xmlDocumentContext.CreateElement("R");
                    recordElement.SetAttribute("M", "I");

                    XmlElement fieldElement;

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "1");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("1"));
                    recordElement.AppendChild(fieldElement);


                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "2");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(1)));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "3");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(agent.agentId + "" + (int)dataReader.GetValue(0)));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "5");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii(((string)dataReader.GetValue(2)).Replace("&", ""))));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "7");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii(((string)dataReader.GetValue(3)).Replace("&", ""))));
                    recordElement.AppendChild(fieldElement);


                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "8");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii(((string)dataReader.GetValue(4)).Replace("&", ""))));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "85");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii(((string)dataReader.GetValue(5)).Replace("&", ""))));
                    recordElement.AppendChild(fieldElement);


                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "9");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii(((string)dataReader.GetValue(6)).Replace("&", ""))));
                    recordElement.AppendChild(fieldElement);


                    string deliveryCode = (string)dataReader.GetValue(7);
                    if (!deliveryCode.Equals("Standard"))
                    {
                        fieldElement = xmlDocumentContext.CreateElement("F");
                        fieldElement.SetAttribute("NO", "12");
                        fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(7))));
                        recordElement.AppendChild(fieldElement);
                    }

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "13");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii(((string)dataReader.GetValue(8)).Replace("&", ""))));
                    recordElement.AppendChild(fieldElement);


                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "15");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii(((string)dataReader.GetValue(9)).Replace("&", ""))));
                    recordElement.AppendChild(fieldElement);


                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "16");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii(((string)dataReader.GetValue(10)).Replace("&", ""))));
                    recordElement.AppendChild(fieldElement);


                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "91");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii(((string)dataReader.GetValue(11)).Replace("&", ""))));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "17");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii(((string)dataReader.GetValue(12)).Replace("&", ""))));
                    recordElement.AppendChild(fieldElement);


                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "18");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(13))));
                    recordElement.AppendChild(fieldElement);


                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "104");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(17))));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "80901");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetValue(18).ToString()));
                    recordElement.AppendChild(fieldElement);


                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "37");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(float.Parse(dataReader.GetValue(19).ToString()).ToString()));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "35");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("TRUE"));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "43");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetValue(21).ToString()));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "80903");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetValue(20).ToString()));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "63");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetValue(22).ToString()));
                    recordElement.AppendChild(fieldElement);

                    fieldElement = xmlDocumentContext.CreateElement("F");
                    fieldElement.SetAttribute("NO", "19");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetDateTime(23).ToString("yyyy-MM-dd")));
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
    }
}
