using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.Woppen.Library
{
    /// <summary>
    /// Klassen används som bufferklass i samband med inrapportering av återstående visningspaket, beställning av packmaterial samt val av avräkningsmetod.
    /// </summary>
    public class SalesIdShowCase
    {
        public string salesId;
        public string itemNo;
        public float quantityReceived;
        public float qtyPackingMaterial;
        public float qtyPackingSlips;
        public int method;

        public bool isEntered;

        private Database database;

        /// <summary>
        /// Konstruktor som initierar klassen utifrån en rad i ett dataset.
        /// </summary>
        /// <param name="database">Referens till databas-objektet i Infojet-klassen.</param>
        /// <param name="dataRow">En rad i ett dataset.</param>
        public SalesIdShowCase(Database database, DataRow dataRow)
        {
            this.database = database;

            this.salesId = dataRow.ItemArray.GetValue(0).ToString();
            this.itemNo = dataRow.ItemArray.GetValue(1).ToString();
            this.quantityReceived = float.Parse(dataRow.ItemArray.GetValue(2).ToString());
            this.qtyPackingMaterial = float.Parse(dataRow.ItemArray.GetValue(3).ToString());
            this.qtyPackingSlips = float.Parse(dataRow.ItemArray.GetValue(4).ToString());
            this.method = int.Parse(dataRow.ItemArray.GetValue(5).ToString());

        }

        /// <summary>
        /// Konstruktor som initierar klassen ufifrån ett försäljnings-ID samt artikelnr.
        /// </summary>
        /// <param name="database">Referens till databas-objektet i Infojet-klassen.</param>
        /// <param name="salesId">Försäljnings-ID.</param>
        /// <param name="itemNo">Artikelnr.</param>
        public SalesIdShowCase(Database database, string salesId, string itemNo)
        {
            this.database = database;

            this.salesId = salesId;
            this.itemNo = itemNo;

            getFromDatabase();
        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Sales ID], [Item No_], [Quantity Received], [Qty Packing Material], [Qty Packing Slips], [Method] FROM [" + database.getTableName("Sales ID ShowCase") + "] WHERE [Sales ID] = @salesId AND [Item No_] = @itemNo");
            databaseQuery.addStringParameter("@salesId", salesId, 20);
            databaseQuery.addStringParameter("@itemNo", itemNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();


            if (dataReader.Read())
            {
                this.salesId = dataReader.GetValue(0).ToString();
                this.itemNo = dataReader.GetValue(1).ToString();
                this.quantityReceived = float.Parse(dataReader.GetValue(2).ToString());
                this.qtyPackingMaterial = float.Parse(dataReader.GetValue(3).ToString());
                this.qtyPackingSlips = float.Parse(dataReader.GetValue(4).ToString());
                this.method = int.Parse(dataReader.GetValue(5).ToString());
                this.isEntered = true;
            }

            dataReader.Close();


        }
    }
}
