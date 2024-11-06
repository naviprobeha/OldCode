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
    /// ItemCategory motsvarar en post i tabellen med samma namn. Klassen illustrerar en artikelkategori.
    /// </summary>
    public class ItemCategory
    {
        public string code;
        public string description;
        
        private Database database;

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
    }
}
