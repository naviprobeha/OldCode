using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Sandberg.Common
{
    public class ItemJournalLines
    {
        private string itemNoQuery;

        public DataSet getDataSet(Navipro.Base.Common.Database database, string journalTemplateName, string journalBatchName)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Document Date], [Item No_], [Description], [Qty on Order], [Inventory], [Disposable], [Sale Qty], [Expiring Item], [Double Batch], [Qty_ on Sample_Test Book Order] FROM [" + database.getNavTableName("Item Journal Line") + "] WHERE [Journal Template Name] = '" + journalTemplateName + "' AND [Journal Batch Name] = '" + journalBatchName + "' AND [Disposable] < 0 " + itemNoQuery + " ORDER BY [Document Date]");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "itemJournalLine");
            adapter.Dispose();

            return dataSet;

        }

        public DataSet getDoublesDataSet(Navipro.Base.Common.Database database, string journalTemplateName, string journalBatchName)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Document Date], [Item No_], [Description], [Qty on Order], [Inventory], [Disposable], [Sale Qty], [Expiring Item], [Double Batch], [Qty_ on Sample_Test Book Order] FROM [" + database.getNavTableName("Item Journal Line") + "] WHERE [Journal Template Name] = '" + journalTemplateName + "' AND [Journal Batch Name] = '" + journalBatchName + "' AND [Disposable] < 0 AND [Double Batch] = '1' " + itemNoQuery + " ORDER BY [Document Date]");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "itemJournalLine");
            adapter.Dispose();

            return dataSet;

            System.Xml.XmlDocument xmlDoc;
            System.Xml.XmlNodeList list;
            list.Item(
        }

        public void setItemFilter(string fromItemNo, string toItemNo)
        {
            itemNoQuery = " AND [Item No_] >= '" + fromItemNo + "' AND [Item No_] <= '" + toItemNo + "'";
        }
    }
}
