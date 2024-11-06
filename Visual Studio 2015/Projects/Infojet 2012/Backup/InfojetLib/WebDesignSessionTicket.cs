using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for Item.
    /// </summary>
    public class WebDesignSessionTicket : ServiceArgument
    {
        private Infojet infojetContext;

        public string webSiteCode;
        public string ticketId;
        public string sessionId;
        public DateTime createdDateTime;

        public WebDesignSessionTicket(Infojet infojetContext, string ticketId)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this.ticketId = ticketId;

            getFromDatabase();
        }

        public WebDesignSessionTicket(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this.webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
            this.ticketId = dataRow.ItemArray.GetValue(1).ToString();
            this.sessionId = dataRow.ItemArray.GetValue(2).ToString();
            DateTime createdDate = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());
            DateTime createdTime = DateTime.Parse(dataRow.ItemArray.GetValue(4).ToString());
            this.createdDateTime = DateTime.Parse(createdDate.ToString("yyyy-MM-dd") + " " + createdTime.ToString("HH:mm:ss"));
        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Ticket ID], [Session ID], [Created Date], [Created Time] FROM [" + infojetContext.systemDatabase.getTableName("Web Design Session Ticket") + "] WHERE [Ticket ID] = @ticketId");
            databaseQuery.addStringParameter("ticketId", ticketId, 100);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                webSiteCode = dataReader.GetValue(0).ToString();
                ticketId = dataReader.GetValue(1).ToString();
                sessionId = dataReader.GetValue(2).ToString();
                DateTime createdDate = dataReader.GetDateTime(3);
                DateTime createdTime = dataReader.GetDateTime(4);
                createdDateTime = DateTime.Parse(createdDate.ToString("yyyy-MM-dd") + " " + createdTime.ToString("HH:mm:ss"));
            }

            dataReader.Close();


        }


        #region ServiceArgument Members

        public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDoc)
        {
            XmlElement xmlTicketElement = xmlDoc.CreateElement("webDesignSessionTicket");

            XmlAttribute webSiteCodeAttribute = xmlDoc.CreateAttribute("webSiteCode");
            webSiteCodeAttribute.Value = this.webSiteCode;
            xmlTicketElement.Attributes.Append(webSiteCodeAttribute);

            XmlAttribute ticketIdAttribute = xmlDoc.CreateAttribute("ticketId");
            ticketIdAttribute.Value = this.ticketId;
            xmlTicketElement.Attributes.Append(ticketIdAttribute);

            XmlAttribute sessionIdAttribute = xmlDoc.CreateAttribute("sessionId");
            sessionIdAttribute.Value = this.sessionId;
            xmlTicketElement.Attributes.Append(sessionIdAttribute);

            return xmlTicketElement;
        }

        #endregion
    }
}
