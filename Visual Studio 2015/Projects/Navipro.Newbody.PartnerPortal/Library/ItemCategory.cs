using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.PartnerPortal.Library
{
    /// <summary>
    /// ItemCategory motsvarar en post i tabellen med samma namn. Klassen illustrerar en artikelkategori.
    /// </summary>
    public class ItemCategory
    {
        public string code;
        public string description;

        private Database database;

        public ItemCategory()
        {
        }

        /// <summary>
        /// Konstruktor som skapar en instans av klassen utifrån ett dataset.
        /// </summary>
        /// <param name="database">En instans till databas-objektet. Fås ifrån infojet-instansen, egenskapen systemDatabase.</param>
        /// <param name="dataRow">Ett dataset innehållande kod och beskrivning.</param>
        public ItemCategory(Database database, DataRow dataRow)
        {
            this.database = database;

            this.code = dataRow.ItemArray.GetValue(0).ToString();
            this.description = dataRow.ItemArray.GetValue(1).ToString();
        }

        /// <summary>
        /// Konstruktor som skapar en instans av klassen utifrån en kategorikod.
        /// </summary>
        /// <param name="database">En instans till databas-objektet. Fås ifrån infojet-instansen, egenskapen systemDatabase.</param>
        /// <param name="code">Artikelkategorikod.</param>
        public ItemCategory(Database database, string code)
        {
            this.database = database;

            this.code = code;

            getFromDatabase();
        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Code], [Description] FROM [" + database.getTableName("Item Category") + "] WHERE [Code] = @code");
            databaseQuery.addStringParameter("@code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();


            if (dataReader.Read())
            {
                this.code = dataReader.GetValue(0).ToString();
                this.description = dataReader.GetValue(1).ToString();
            }

            dataReader.Close();


        }

        public static ItemCategory[] getDataSetArray(Navipro.Infojet.Lib.Database database, SalesID salesId)
        {
            
            

            DatabaseQuery databaseQuery = database.prepare("SELECT [Code], [Description] FROM [" + database.getTableName("Item Category") + "] WHERE [Code] IN (SELECT p.[Item Category Code] FROM [" + database.getTableName("BOM Component") + "] b WITH (NOLOCK), [" + database.getTableName("Item") + "] i WITH (NOLOCK), [" + database.getTableName("Product Group") + "] p WITH (NOLOCK), [" + database.getTableName("Item Category") + "] c WITH (NOLOCK) WHERE b.[Parent Item No_] = @parentItemNo AND b.[Type] = 1 AND b.[No_] = i.[No_] AND i.[Product Group Code] = p.[Code] AND i.[Item Category Code] = p.[Item Category Code] AND p.[Item Category Code] = c.[Code] AND c.[Web Availability] = 0) ORDER BY [Description]");
            databaseQuery.addStringParameter("parentItemNo", salesId.itemSelection, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            ItemCategory[] itemCategoryArray = new ItemCategory[dataSet.Tables[0].Rows.Count];

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                ItemCategory itemCategory = new ItemCategory(database, dataSet.Tables[0].Rows[i]);
                itemCategoryArray[i] = itemCategory;

                i++;
            }

            return itemCategoryArray;

        }
    }
}
