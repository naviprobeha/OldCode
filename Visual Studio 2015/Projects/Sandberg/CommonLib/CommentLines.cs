using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Sandberg.Common
{
    public class CommentLines
    {

        public DataSet getDataSet(Navipro.Base.Common.Database database, string itemNo, string itemLotNo)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Table Name], [No_], [Line No_], [Date], [Code], [Comment], [Variant Code] FROM [" + database.getNavTableName("Comment Line") + "] WHERE [Table Name] = '3' AND [No_] = '" + itemNo + "' AND [Variant Code] = '" + itemLotNo + "' ORDER BY [Line No_]");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "commentLine");
            adapter.Dispose();

            return dataSet;

        }

        public string getComposedComments(Navipro.Base.Common.Database database, string itemNo, string itemLotNo)
        {
            string comments = "";

            DataSet dataSet = getDataSet(database, itemNo, itemLotNo);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                while (i < dataSet.Tables[0].Rows.Count)
                {
                    if (comments != "") comments = comments + "<br/>";

                    CommentLine commentLine = new CommentLine(dataSet.Tables[0].Rows[i]);

                    comments = comments + commentLine.date.ToString("yyyy-MM-dd")+": "+commentLine.comment;

                    i++;
                }
            }

            return comments;
        }

    }
}
