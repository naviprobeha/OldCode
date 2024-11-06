using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.Woppen.Library
{
    /// <summary>
    /// En lista av ett antal SalesIDSalesPerson-objekt.
    /// </summary>
    public class SalesIDSalesPersonCollection : CollectionBase
    {
        /// <summary>
        /// Returnerar indexerat objekt.
        /// </summary>
        /// <param name="index">Anger vilken position i listan som skall returneras.</param>
        /// <returns>Returnerar ett SalesIDSalesPerson-objekt.</returns>
        public SalesIDSalesPerson this[int index]
        {
            get { return (SalesIDSalesPerson)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Lägger till ett SalesIDSalesPerson-objekt i listan.
        /// </summary>
        /// <param name="salesIdSalesPerson">Ett SalesIDSalesPerson-objekt.</param>
        /// <returns>Returnerar positionen i listan.</returns>
        public int Add(SalesIDSalesPerson salesIdSalesPerson)
        {
            return (List.Add(salesIdSalesPerson));
        }

        /// <summary>
        /// Returnerar positionen i listan för ett givet SalesIDSalesPerson-objekt.
        /// </summary>
        /// <param name="salesIdSalesPerson">Ett SalesIDSalesPerson-objekt.</param>
        /// <returns></returns>
        public int IndexOf(SalesIDSalesPerson salesIdSalesPerson)
        {
            return (List.IndexOf(salesIdSalesPerson));
        }

        /// <summary>
        /// Lägger in ett SalesIDSalesPerson-objekt på en given position i listan.
        /// </summary>
        /// <param name="index">Position.</param>
        /// <param name="salesIdSalesPerson">SalesIDSalesPerson-objekt att lägga till i listan.</param>
        public void Insert(int index, SalesIDSalesPerson salesIdSalesPerson)
        {
            List.Insert(index, salesIdSalesPerson);
        }

        /// <summary>
        /// Tar bort ett SalesIDSalesPerson-objekt ur listan.
        /// </summary>
        /// <param name="salesIdSalesPerson">SalesIDSalesPerson-objekt att radera.</param>
        public void Remove(SalesIDSalesPerson salesIdSalesPerson)
        {
            List.Remove(salesIdSalesPerson);
        }

        /// <summary>
        /// Kontrollerar om listan innehåller ett givet SalesIDSalesPerson-objekt.
        /// </summary>
        /// <param name="salesIdSalesPerson">SalesIDSalesPerson-objekt att lesa efter.</param>
        /// <returns>Returnerar true eller false beroende på om objektet finns i listan eller inte.</returns>
        public bool Contains(SalesIDSalesPerson salesIdSalesPerson)
        {
            return (List.Contains(salesIdSalesPerson));
        }

        /// <summary>
        /// Sätter länken på samtliga säljare i listan till deras resp. detaljsidor.
        /// </summary>
        /// <param name="pageUrl">Länk.</param>
        public void setPageUrl(string pageUrl)
        {
            int i = 0;
            while (i < List.Count)
            {
                ((SalesIDSalesPerson)List[i]).pageUrl = pageUrl + "&salesId=" + ((SalesIDSalesPerson)List[i]).salesId + "&salesPersonWebUserAccountNo=" + ((SalesIDSalesPerson)List[i]).webUserAccountNo;
                i++;
            }

        }

        public void applySalesPersonHistory(Hashtable userHistoryPackageTable, Hashtable userHistoryItemTable)
        {
            int i = 0;
            while (i < this.Count)
            {
                if (userHistoryPackageTable[this[i].salesId + "_" + this[i].webUserAccountNo] != null)
                {
                    this[i].historyPackages = (int)userHistoryPackageTable[this[i].salesId + "_" + this[i].webUserAccountNo];
                }
                if (userHistoryItemTable[this[i].salesId + "_" + this[i].webUserAccountNo] != null)
                {
                    this[i].historyItems = (int)userHistoryItemTable[this[i].salesId + "_" + this[i].webUserAccountNo];
                }

                i++;
            }

        }

        public static Hashtable getSoldPackages(Navipro.Infojet.Lib.Database database, string salesId)
        {
            Hashtable soldPackagesTable = new Hashtable();

            DatabaseQuery databaseQuery2 = database.prepare("SELECT [Web User Account No], SUM(Quantity) FROM [" + database.getTableName("Web Cart Line") + "] WITH (NOLOCK) WHERE [Extra 2] = @salesId GROUP BY [Web User Account No]");
            databaseQuery2.addStringParameter("@salesId", salesId, 20);

            SqlDataReader dataReader = databaseQuery2.executeQuery();

            while (dataReader.Read())
            {
                string webUserAccountNo = dataReader.GetValue(0).ToString();
                int soldPackages = 0;
                if (!dataReader.IsDBNull(1)) soldPackages = (int)float.Parse(dataReader.GetValue(1).ToString());

                soldPackagesTable.Add(webUserAccountNo, soldPackages);
            }

            dataReader.Close();


            return soldPackagesTable;


        }

    }
}
