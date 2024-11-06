using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Navipro.Base.Common;

namespace Navipro.SmartSystems.SystemCore
{
    public class Agents
    {

        public Agents()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet getDataSet(Database database, string groupCode)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Serial No], [Code], [Description], [Password], [Enabled], [Last Check-In Date], [Last Check-In Time], [Password Created Date], [Password Created Time], [Group Code], [Configuration Updated Date], [Configuration Updated Time] FROM [Agent] WHERE [Group Code] = '" + groupCode + "' ORDER BY [Code]");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "agent");
            adapter.Dispose();

            return dataSet;

        }


        public DataSet getDataSet(Database database)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Serial No], [Code], [Description], [Password], [Enabled], [Last Check-In Date], [Last Check-In Time], [Password Created Date], [Password Created Time], [Group Code], [Configuration Updated Date], [Configuration Updated Time] FROM [Agent] ORDER BY [Code]");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "agent");
            adapter.Dispose();

            return dataSet;

        }

        public Agent getAgent(Database database, string serialNo)
        {
            SqlDataReader dataReader = database.query("SELECT [Serial No], [Code], [Description], [Password], [Enabled], [Last Check-In Date], [Last Check-In Time], [Password Created Date], [Password Created Time], [Group Code], [Configuration Updated Date], [Configuration Updated Time] FROM [Agent] WHERE [Serial No] = '" + serialNo + "'");

            Agent agent = null;

            if (dataReader.Read())
            {
                agent = new Agent(dataReader);
            }
            dataReader.Close();

            return agent;


        }

        public bool checkPassword(Database database, string serialNo, string password)
        {
            SqlDataReader dataReader = database.query("SELECT [Serial No] FROM [Agent] WHERE [Serial No] = '" + serialNo + "' AND [Password] = '"+password+"'");

            if (dataReader.Read())
            {
                dataReader.Close();
                return true;

            }
            dataReader.Close();

            return false;


        }

    }
}
