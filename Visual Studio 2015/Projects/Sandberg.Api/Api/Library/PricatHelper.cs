using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Library
{
    public class PricatHelper
    {


        public byte[] GetPricatFile(string customerNo)
        {
            List<string> dataList = new List<string>();

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            DatabaseQuery databaseQuery = database.prepare("SELECT [Document] FROM [" + database.getTableName("Web Customer Pricat") + "] WHERE [Customer No_] = @customerNo");
            databaseQuery.addStringParameter("@customerNo", customerNo, 20);

            try
            {

                byte[] byteArray = (byte[])databaseQuery.executeScalar();

                return byteArray;
            }
            catch(Exception)
            {

            }

            return null;
           
        }
    }
}