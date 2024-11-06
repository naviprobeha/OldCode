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


    }
}
