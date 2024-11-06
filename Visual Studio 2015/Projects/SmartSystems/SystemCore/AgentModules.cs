using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Navipro.Base.Common;

namespace Navipro.SmartSystems.SystemCore
{
    public class AgentModules
    {

        public AgentModules()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet getDataSet(Database database, string agentSerialNo)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Agent Serial No], [Module Entry No], [Last Updated Date], [Last Updated Time] FROM [Agent Module] WHERE [Agent Serial No] = '" + agentSerialNo + "'");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "agentModule");
            adapter.Dispose();

            return dataSet;

        }

        public DataSet getDataSet(Database database, string agentSerialNo, DateTime lastUpdatedDate)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Agent Serial No], [Module Entry No], [Last Updated Date], [Last Updated Time] FROM [Agent Module] WHERE [Agent Serial No] = '" + agentSerialNo + "' AND [Last Updated Date] <= '" + lastUpdatedDate.ToString("yyyy-MM-dd") + "'");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "agentModule");
            adapter.Dispose();

            return dataSet;

        }

        public AgentModule getAgentModule(Database database, string agentSerialNo, int moduleEntryNo)
        {
            SqlDataReader dataReader = database.query("SELECT [Agent Serial No], [Module Entry No], [Last Updated Date], [Last Updated Time] FROM [Agent Module] WHERE [Agent Serial No] = '" + agentSerialNo + "' AND [Module Entry No] = '" + moduleEntryNo + "'");

            AgentModule agentModule = null;

            if (dataReader.Read())
            {
                agentModule = new AgentModule(dataReader);
            }
            dataReader.Close();

            return agentModule;


        }


    }
}
