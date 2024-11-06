using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Navipro.Base.Common;

namespace Navipro.SmartSystems.SystemCore
{
    public class AgentModule
    {

        public string agentSerialNo;
        public int moduleEntryNo;
        public DateTime lastUpdated;

        private string updateMethod;

        public AgentModule(SqlDataReader dataReader)
        {
            agentSerialNo = dataReader.GetValue(0).ToString();
            moduleEntryNo = dataReader.GetInt32(1);

            DateTime lastUpdatedDate = dataReader.GetDateTime(2);
            DateTime lastUpdatedTime = dataReader.GetDateTime(3);

            this.lastUpdated = new DateTime(lastUpdatedDate.Year, lastUpdatedDate.Month, lastUpdatedDate.Day, lastUpdatedTime.Hour, lastUpdatedTime.Minute, lastUpdatedTime.Second);

        }

        public AgentModule(DataRow dataRow)
        {
            agentSerialNo = dataRow.ItemArray.GetValue(0).ToString();
            moduleEntryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());

            DateTime lastUpdatedDate = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
            DateTime lastUpdatedTime = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());

            this.lastUpdated = new DateTime(lastUpdatedDate.Year, lastUpdatedDate.Month, lastUpdatedDate.Day, lastUpdatedTime.Hour, lastUpdatedTime.Minute, lastUpdatedTime.Second);

        }

        public void save(Database database)
        {
            try
            {
                if (updateMethod == "D")
                {
                    database.nonQuery("DELETE FROM [Agent Module] WHERE [Agent Serial No] = '" + agentSerialNo + "' AND [Module Entry No] = '"+moduleEntryNo+"'");

                }
                else
                {
                    SqlDataReader dataReader = database.query("SELECT [Agent Serial No] FROM [Agent Module] WHERE [Agent Serial No] = '" + agentSerialNo + "' AND [Module Entry No] = '" + moduleEntryNo + "'");

                    if (dataReader.Read())
                    {
                        dataReader.Close();
                        database.nonQuery("UPDATE [Agent Module] SET [Last Updated Date] = '" + this.lastUpdated.ToString("yyyy-MM-dd") + "', [Last Updated Time] = '" + this.lastUpdated.ToString("1754-01-01 HH:mm:ss") + "' WHERE [Agent Serial No] = '" + agentSerialNo + "' AND [Module Entry No] = '"+moduleEntryNo+"'");
                    }
                    else
                    {
                        dataReader.Close();
                        database.nonQuery("INSERT INTO [Agent Module] ([Agent Serial No], [Module Entry No], [Last Updated Date], [Last Updated Time]) VALUES ('" + agentSerialNo + "', '" + moduleEntryNo + "','" + lastUpdated.ToString("yyyy-MM-dd 00:00:00") + "', '" + lastUpdated.ToString("1754-01-01 HH:mm:ss") + "')");
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception("Error on agent module update: " + e.Message + " (" + database.getLastSQLCommand() + ")");
            }

        }

        public void delete(Database database)
        {
            updateMethod = "D";
            save(database);
        }

        public Module getModule(Database database)
        {
            Modules modules = new Modules();
            return modules.getModule(database, this.moduleEntryNo);
        }
    }
}
