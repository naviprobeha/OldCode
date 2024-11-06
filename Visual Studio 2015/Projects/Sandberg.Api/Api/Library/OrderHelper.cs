using Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;

namespace Api.Library
{
    public class OrderHelper
    {
 
        public static void SubmitOrder(string customerNo, string customerRef, XmlDocument orderXmlDoc)
        {

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            if (customerRef == null) customerRef = "";

            DatabaseQuery databaseQuery = database.prepare("INSERT INTO ["+database.getTableName("API Order Queue")+"] ([Entry Date Time], [GUID], [Direction], [Date], [Timestamp], [Customer No_], [Document], [Document No_], [Status], [Message Type], [Customer Ref_]) VALUES (@datetime, @guid, 0, @date, @time, @customerNo, @document, '', @status, @messageType, @customerRef)");
            databaseQuery.addDateTimeParameter("datetime", DateTime.Now);
            databaseQuery.addStringParameter("guid", Guid.NewGuid().ToString(), 100);
            databaseQuery.addDateTimeParameter("date", DateTime.Today);
            databaseQuery.addDateTimeParameter("time", DateTime.Parse("1754-01-01 " + DateTime.Now.ToString("HH:mm:ss")));
            databaseQuery.addStringParameter("customerNo", customerNo, 20);
            databaseQuery.addStringParameter("messageType", "ORDER", 20);
            databaseQuery.addBlobParameter("document", System.Text.Encoding.UTF8.GetBytes(orderXmlDoc.OuterXml), System.Text.Encoding.UTF8.GetByteCount(orderXmlDoc.OuterXml));
            databaseQuery.addIntParameter("status", 0);
            databaseQuery.addStringParameter("customerRef", customerRef, 50);

            databaseQuery.execute();

            database.close();


        }

    }
}