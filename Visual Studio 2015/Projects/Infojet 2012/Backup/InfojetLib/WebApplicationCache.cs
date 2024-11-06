using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    public class WebApplicationCache
    {
        public WebApplicationCache()
        {

        }

        public static void cacheObject(Infojet infojetContext, string name, object obj)
        {
            cacheObject(infojetContext.systemDatabase, name, obj);
        }

        public static void cacheObject(Database database, string name, object obj)
        {
            string base64 = serializeBase64(obj);

            save(database, name, base64);

        }

        public static object getCashedObject(Infojet infojetContext, string name)
        {
            return getCashedObject(infojetContext.systemDatabase, name);
        }

        public static object getCashedObject(Database database, string name)
        {
            string base64 = "";
            if (read(database, name, out base64))
            {
                object obj = deserializeBase64(base64);
                return obj;
            }

            return null;
        }


        private static void save(Database database, string name, string base64)
        {
            DatabaseQuery databaseQuery = database.prepare("DELETE FROM [" + database.getTableName("Web Application Cache") + "] WHERE [Name] = @name");
            databaseQuery.addStringParameter("name", name, 250);
            databaseQuery.execute();

            databaseQuery = database.prepare("DELETE FROM [" + database.getTableName("Web Application Cache") + "] WHERE [Created DateTime] < @oldDateTime");
            databaseQuery.addDateTimeParameter("oldDateTime", DateTime.Now.AddDays(-14));
            databaseQuery.execute();

            databaseQuery = database.prepare("INSERT INTO [" + database.getTableName("Web Application Cache") + "] ([Name], [Value], [Created DateTime]) VALUES (@name, @value, @createdDateTime)");
            databaseQuery.addStringParameter("name", name, 250);
            databaseQuery.addImageParameter("value", System.Text.Encoding.Default.GetBytes(base64));
            databaseQuery.addDateTimeParameter("createdDateTime", DateTime.Now);
            
            databaseQuery.execute();

        }

        private static bool read(Database database, string name, out string base64)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Value] FROM [" + database.getTableName("Web Application Cache") + "] WHERE [Name] = @name");
            databaseQuery.addStringParameter("name", name, 250);

            byte[] byteArray = (byte[])databaseQuery.executeScalar();
            if (byteArray != null)
            {
                base64 = System.Text.Encoding.Default.GetString(byteArray);
                return true;
            }

            base64 = "";
            return false;

        }




        private static string serializeBase64(object o)
        {
            // Serialize to a base 64 string
            byte[] bytes;
            long length = 0;
            MemoryStream ws = new MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter sf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            sf.Serialize(ws, o);
            length = ws.Length;
            bytes = ws.GetBuffer();
            string encodedData = bytes.Length + ":" + Convert.ToBase64String(bytes, 0, bytes.Length, Base64FormattingOptions.None);
            return encodedData;
        }


        private static object deserializeBase64(string s)
        {
            // We need to know the exact length of the string - Base64 can sometimes pad us by a byte or two

            int p = s.IndexOf(':');
            int length = Convert.ToInt32(s.Substring(0, p));

            // Extract data from the base 64 string!
            byte[] memorydata = Convert.FromBase64String(s.Substring(p + 1));
            MemoryStream rs = new MemoryStream(memorydata, 0, length);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter sf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            
            object o = sf.Deserialize(rs);
            return o;

        }


    }
}
