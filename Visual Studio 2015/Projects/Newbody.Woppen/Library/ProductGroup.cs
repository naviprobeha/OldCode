﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.Woppen.Library
{
    /// <summary>
    /// ProductGroup motsvarar en post i tabellen med samma namn. Klassen illustrerar en produktgrupp.
    /// </summary>
    public class ProductGroup
    {
        private string _code;
        private string _itemCategoryCode;
        private string _description;

        private Database database;

        /// <summary>
        /// Konstruktor som skapar en instans av klassen utifrån ett dataset.
        /// </summary>
        /// <param name="database">En instans till databas-objektet. Fås ifrån infojet-instansen, egenskapen systemDatabase.</param>
        /// <param name="dataRow">Ett dataset innehållande kod och beskrivning.</param>
        public ProductGroup(Database database, DataRow dataRow)
        {
            this.database = database;

            this._code = dataRow.ItemArray.GetValue(0).ToString();
            this._itemCategoryCode = dataRow.ItemArray.GetValue(1).ToString();
            this._description = dataRow.ItemArray.GetValue(2).ToString();
        }

        /// <summary>
        /// Returnerar tillhörande artikelkategori-objekt.
        /// </summary>
        /// <returns>Artikelkategori.</returns>
        public ItemCategory getItemCategory()
        {
            return new ItemCategory(database, this.itemCategoryCode);
        }

        /// <summary>
        /// Returnerar enhetspris för artiklar som ingår i aktuell produktgrupp.
        /// </summary>
        /// <returns>Enhetspris.</returns>
        public float getPrice()
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Unit Price] FROM [" + database.getTableName("Item") + "] WHERE [Item Category Code] = @itemCategoryCode AND [Product Group Code] = @productGroupCode");
            databaseQuery.addStringParameter("@itemCategoryCode", itemCategoryCode, 20);
            databaseQuery.addStringParameter("@productGroupCode", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            float unitPrice = 0;

            if (dataReader.Read())
            {
                unitPrice = float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            return unitPrice;
        }

        /// <summary>
        /// Returnerar ett dataset med artiklar som ingår i aktuell produktgrupp ur ett givet sortiment.
        /// </summary>
        /// <param name="database">En instans till databas-objektet. Fås ifrån infojet-instansen, egenskapen systemDatabase.</param>
        /// <param name="parentItemNo">Sortimentskod.</param>
        /// <returns></returns>
        public DataSet getProducts(Database database, string parentItemNo)
        {

            DatabaseQuery databaseQuery = database.prepare("SELECT i.[No_], i.[Description], [Description 2], [Unit Price], [Sales Unit of Measure], [Manufacturer Code], [Lead Time Calculation], [Unit List Price], [Item Disc_ Group], [Size] FROM [" + database.getTableName("Item") + "] i WITH (NOLOCK), [" + database.getTableName("BOM Component") + "] b WITH (NOLOCK) WHERE b.[Parent Item No_] = @parentItemNo AND b.[Type] = 1 AND b.[No_] = i.[No_] AND i.[Item Category Code] = @itemCategoryCode AND i.[Product Group Code] = @productGroupCode ORDER BY i.[No_]");
            databaseQuery.addStringParameter("@parentItemNo", parentItemNo, 20);
            databaseQuery.addStringParameter("@itemCategoryCode", this.itemCategoryCode, 20);
            databaseQuery.addStringParameter("@productGroupCode", this.code, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;
        }

        /// <summary>
        /// Produktgruppskod.
        /// </summary>
        public string code
        {
            get { return _code; }
        }

        /// <summary>
        /// Tillhörande artikelkategorikod.
        /// </summary>
        public string itemCategoryCode
        {
            get { return _itemCategoryCode; }
        }

        /// <summary>
        /// Beskrivning av produktgruppen.
        /// </summary>
        public string description
        {
            get
            {
                if (_description.Length > 20) return _description.Substring(0, 16) + "...";
                return _description;
            }
        }
    }
}
