using Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Library
{
    public class ShippingAgentHelper
    {
 
        public static List<ShippingAgentService> GetServices()
        {
            List<ShippingAgentService> shippingAgentServiceList = new List<ShippingAgentService>();

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            SqlDataReader dataReader = database.query("SELECT * FROM [" + database.getTableName("Shipping Agent Services - Mod") + "]");
            while(dataReader.Read())
            {
                ShippingAgentService shippingAgentService = new ShippingAgentService(dataReader);
                shippingAgentServiceList.Add(shippingAgentService);
            }

            dataReader.Close();

            database.close();

            return shippingAgentServiceList;
        }

    }
}