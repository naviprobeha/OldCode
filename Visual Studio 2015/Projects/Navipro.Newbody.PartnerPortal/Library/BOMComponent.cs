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
    /// BOMComponent motsvarar en artikel som ingår i ett sortiment. Klassen speglar en post i tabellen med samma namn.
    /// </summary>
    public class BOMComponent
    {
        public string parentItemNo;
        public int lineNo;
        public int type;
        public string no;
        public string description;
        public float quantityPer;

        private Database database;

        /// <summary>
        /// Konstruktor som initierar en post utifrån ett dataset från tabellen.
        /// </summary>
        /// <param name="database">En instans till databas-objektet. Fås ifrån infojet-instansen, egenskapen systemDatabase.</param>
        /// <param name="dataRow">En specifik rad i datasetet.</param>
        public BOMComponent(Database database, DataRow dataRow)
        {
            this.database = database;

            this.parentItemNo = dataRow.ItemArray.GetValue(0).ToString();
            this.lineNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
            this.type = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
            this.no = dataRow.ItemArray.GetValue(3).ToString();
            this.description = dataRow.ItemArray.GetValue(4).ToString();
            this.quantityPer = float.Parse(dataRow.ItemArray.GetValue(5).ToString());
        }


        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Parent Item No_], [Line No_], [Type], [No_], [Description], [Quantity per], [Slut] FROM [" + database.getTableName("BOM Component") + "] WHERE [Parent Item No_] = @parentItemNo");
            databaseQuery.addStringParameter("@parentItemNo", parentItemNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                this.parentItemNo = dataReader.GetValue(0).ToString();
                this.lineNo = dataReader.GetInt32(1);
                this.type = dataReader.GetInt32(2);
                this.no = dataReader.GetValue(3).ToString();
                this.description = dataReader.GetValue(4).ToString();
                this.quantityPer = float.Parse(dataReader.GetValue(5).ToString());

            }

            dataReader.Close();


        }

 


        /// <summary>
        /// Hämtar ut produktgrupper ifrån ett givet sortiment. Artiklarna som ingår i sortimentet grupperas efter produktgrupper.
        /// </summary>
        /// <param name="database">En instans till databas-objektet. Fås ifrån infojet-instansen, egenskapen systemDatabase.</param>
        /// <param name="parentItemNo">Sortimentskod.</param>
        /// <returns></returns>
        public static DataSet getProductGroups(Database database, string parentItemNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT p.[Code], p.[Item Category Code], p.[Description], c.[Description] FROM [" + database.getTableName("BOM Component") + "] b WITH (NOLOCK), [" + database.getTableName("Item") + "] i WITH (NOLOCK), [" + database.getTableName("Product Group") + "] p WITH (NOLOCK), [" + database.getTableName("Item Category") + "] c WITH (NOLOCK) WHERE b.[Parent Item No_] = @parentItemNo AND b.[Type] = 1 AND b.[No_] = i.[No_] AND i.[Product Group Code] = p.[Code] AND i.[Item Category Code] = p.[Item Category Code] AND p.[Item Category Code] = c.[Code] AND c.[Web Availability] = 0 ORDER BY b.[No_]");
            databaseQuery.addStringParameter("@parentItemNo", parentItemNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;
        }

        /// <summary>
        /// Hämtar ut produkter i ett givet sortiment. 
        /// </summary>
        /// <param name="database">En instans till databas-objektet. Fås ifrån infojet-instansen, egenskapen systemDatabase.</param>
        /// <param name="parentItemNo">Sortimentskod.</param>
        /// <returns></returns>
        public static DataSet getProducts(Database database, string parentItemNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Description], [Quantity per] FROM [" + database.getTableName("BOM Component") + "] WITH (NOLOCK) WHERE [Parent Item No_] = @parentItemNo AND [Type] = 1 ORDER BY [No_]");
            databaseQuery.addStringParameter("@parentItemNo", parentItemNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;
        }

        /// <summary>
        /// Räknar fram antalet produkter i ett givet sortiment.
        /// </summary>
        /// <param name="database">En instans till databas-objektet. Fås ifrån infojet-instansen, egenskapen systemDatabase.</param>
        /// <param name="parentItemNo">Sortimentskod.</param>
        /// <returns></returns>
        public static int countProducts(Database database, string parentItemNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT SUM([Quantity per]) FROM [" + database.getTableName("BOM Component") + "] WITH (NOLOCK) WHERE [Parent Item No_] = @parentItemNo AND [Type] = 1");
            databaseQuery.addStringParameter("@parentItemNo", parentItemNo, 20);

            float quantity = 0;

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) quantity = float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            return (int)quantity;

        }

        /// <summary>
        /// Räknar fram antalet för en given artikel som ingår i ett givet sortiment.
        /// </summary>
        /// <param name="database">En instans till databas-objektet. Fås ifrån infojet-instansen, egenskapen systemDatabase.</param>
        /// <param name="parentItemNo">Sortimentskod.</param>
        /// <param name="itemNo"></param>
        /// <returns></returns>
        public static int getItemQuantity(Database database, string parentItemNo, string itemNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Quantity per] FROM [" + database.getTableName("BOM Component") + "] WITH (NOLOCK) WHERE [Parent Item No_] = @parentItemNo AND [Type] = 1 AND [No_] = @itemNo");
            databaseQuery.addStringParameter("@parentItemNo", parentItemNo, 20);
            databaseQuery.addStringParameter("@itemNo", itemNo, 20);

            float quantity = 0;

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) quantity = float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            return (int)quantity;


        }
    }
}
