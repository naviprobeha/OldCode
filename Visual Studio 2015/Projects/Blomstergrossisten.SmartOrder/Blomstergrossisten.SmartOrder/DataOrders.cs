using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataOrders.
    /// </summary>
    public class DataOrders : ServiceArgument
    {
        private SmartDatabase smartDatabase;

        public DataOrders(SmartDatabase smartDatabase)
        {
            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            Agent agent = new Agent(smartDatabase);

            XmlElement tableElement = xmlDocumentContext.CreateElement("ORDERS");

            SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM salesHeader WHERE ready = 1");

            while (dataReader.Read())
            {
                try
                {
                    XmlElement recordElement = xmlDocumentContext.CreateElement("ORDER");

                    XmlElement fieldElement;


                    fieldElement = xmlDocumentContext.CreateElement("NO");
                    fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(agent.agentId + dataReader.GetInt32(0).ToString()));
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
