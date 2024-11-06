using System;
using Navipro.Base.Common;
using System.Data.SqlClient;

namespace Navipro.Base.LogHandlers
{
    public class DatabaseLogger : Logger
    {
        
        private Database database;

        public DatabaseLogger(string serverName, string databaseName, string userName, string password)
        {
            Configuration configuration = new Configuration();
            configuration.setConfigValue("serverName", serverName);
            configuration.setConfigValue("database", databaseName);
            configuration.setConfigValue("userName", userName);
            configuration.setConfigValue("password", password);

            database = new Database(null, configuration);

            SqlDataReader dataReader = database.query("SELECT TOP 1 [Entry No] FROM [Log]");
            if (!dataReader.Read())
            {
                dataReader.Close();
                database.nonQuery("CREATE TABLE [Log] ([Entry No] int IDENTITY(1,1) PRIMARY KEY, [Source] nvarchar(50), [Date Time] datetime, [Level] int, [Message] nvarchar(250))");
            }
            dataReader.Close();

        }

        public void close()
        {
            database.close();
        }

        #region Logger Members

        public void write(string source, int level, string message)
        {
            if (source.Length > 50) source = source.Substring(1, 50);
            if (message.Length > 50) message = message.Substring(1, 250);

            database.query("INSERT INTO [Log] ([Source], [Date Time], [Level], [Message]) VALUES ('"+source+"',GETDATE(),'"+level+"','"+message+"')");

        }

        #endregion
    }
}
