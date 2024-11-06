using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Navipro.Base.Common;

namespace Navipro.SmartSystems.SystemCore
{
    public class Modules
    {

        public Modules()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet getDataSet(Database database)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Type], [Name], [Class Name], [Version No], [Changed Date], [Changed Time] FROM [Module]");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "module");
            adapter.Dispose();

            return dataSet;

        }

 
        public Module getModule(Database database, int entryNo)
        {
            SqlDataReader dataReader = database.query("SELECT [Entry No], [Type], [Name], [Class Name], [Version No], [Changed Date], [Changed Time] FROM [Module] WHERE [Entry No] = '" + entryNo + "'");

            Module module = null;

            if (dataReader.Read())
            {
                module = new Module(dataReader);
            }
            dataReader.Close();

            return module;


        }


    }
}
