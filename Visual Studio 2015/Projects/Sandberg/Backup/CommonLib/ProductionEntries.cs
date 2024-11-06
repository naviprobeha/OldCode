using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Sandberg.Common
{
    public class ProductionEntries
    {
        public DataSet getDataSet(Navipro.Base.Common.Database database, string itemNo)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Registration Date], [Item No_], [Paper Item No_], [Kg of Paper], [m_ of Prima], [Prima %], [Colour 1 Item No_], [Kg of Colour 1], [Colour 2 Item No_], [Kg of Colour 2], [Colour 3 Item No_], [Kg_ of Colour 3], [Colour 4 Item No_], [Kg_ of Colour 4], [Colour 5 Item No_], [Kg_ of Colour 5], [Colour 6 Item No_], [Kg_ of Colour 6], [Colour 7 Item No_], [Kg_ of Colour 7], [Colour 8 Item No_], [Kg_ of Colour 8], [Comment], [Variant Code] FROM [" + database.getNavTableName("Prodution Entry") + "] WHERE [Item No_] LIKE '" + itemNo + "%' ORDER BY [Item No_]");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "productionEntry");
            adapter.Dispose();

            return dataSet;

        }
    }
}
