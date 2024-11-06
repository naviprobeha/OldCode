using System;
using System.Data;
using System.Data.SqlClient;
using Navipro.Base.Common;

/// <summary>
/// Summary description for Maps
/// </summary>
/// 
namespace Navipro.MapServer.Lib
{

    public class Maps
    {
        private Database database;

        public Maps(Database database)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = database;
        }

        public Map selectMap(int x, int y)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Position X1], [Position Y1], [Position X2], [Position Y2], [Filename] FROM [Map] WHERE [Position X1] < '" + x + "' AND [Position X2] > '" + x + "' AND [Position Y1] > '" + y + "' AND [Position Y2] < '" + y + "'");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "map");
            adapter.Dispose();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                Map map = new Map(database, dataSet.Tables[0].Rows[0]);
                return map;
            }

            return null;
        }

        public Map selectMap(string fileName)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Position X1], [Position Y1], [Position X2], [Position Y2], [Filename] FROM [Map] WHERE [Filename] = '"+fileName.ToUpper()+"'");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "map");
            adapter.Dispose();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                Map map = new Map(database, dataSet.Tables[0].Rows[0]);
                return map;
            }

            return null;
        }


        public DataSet selectMaps(int x1, int y1, int x2, int y2)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Position X1], [Position Y1], [Position X2], [Position Y2], [Filename] FROM [Map] WHERE (([Position X1] < '" + x1 + "' AND [Position X2] > '" + x1 + "') OR ([Position X1] > '" + x1 + "' AND [Position X2] > '" + x1 + "' AND [Position X1] < '" + x2 + "' AND [Position X2] < '" + x2 + "') OR ([Position X1] < '" + x2 + "' AND [Position X2] > '" + x2 + "') OR ([Position X1] < '" + x1 + "' AND [Position X2] > '" + x2 + "')) AND (([Position Y1] > '" + y1 + "' AND [Position Y2] < '" + y1 + "') OR ([Position Y1] > '" + y1 + "' AND [Position Y2] > '" + y1 + "' AND [Position Y1] < '" + y2 + "' AND [Position Y2] < '" + y2 + "') OR ([Position Y1] > '" + y2 + "' AND [Position Y2] < '" + y2 + "') OR ([Position Y1] > '" + y1 + "' AND [Position Y2] < '" + y2 + "'))");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "map");
            adapter.Dispose();

            return dataSet;
        }


        public DataSet getDataSet()
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Position X1], [Position Y1], [Position X2], [Position Y2], [Filename] FROM [Map]");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "map");
            adapter.Dispose();

            return dataSet;
        }


    }
}