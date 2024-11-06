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
    /// ProductGroup motsvarar en post i tabellen med samma namn. Klassen illustrerar en produktgrupp.
    /// </summary>
    public class PackingImage
    {
        private string _imageCode;
        private string _description;

        public PackingImage()
        { }

        /// <summary>
        /// Konstruktor som skapar en instans av klassen utifrån ett dataset.
        /// </summary>
        /// <param name="dataRow">Ett dataset innehållande kod och beskrivning.</param>
        public PackingImage(DataRow dataRow)
        {
            this._imageCode = dataRow.ItemArray.GetValue(0).ToString();
            this._description = dataRow.ItemArray.GetValue(1).ToString();
        }

 
        /// <summary>
        /// Returnerar ett PackingImage-objekt motsvarande ett antal paket.
        /// </summary>
        /// <param name="database">En instans till databas-objektet. Fås ifrån infojet-instansen, egenskapen systemDatabase.</param>
        /// <param name="parentItemNo">Sortimentskod.</param>
        /// <returns></returns>
        public static PackingImage getPackingImage(Navipro.Infojet.Lib.Infojet infojetContext, int noOfPackages, string languageCode)
        {

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Image Code], [Description] FROM [" + infojetContext.systemDatabase.getTableName("Packing Image") + "] WITH (NOLOCK) WHERE [From Qty] <= @quantity AND [To Qty] >= @quantity AND [Language Code] = @languageCode");
            databaseQuery.addIntParameter("@quantity", noOfPackages);
            databaseQuery.addStringParameter("@languageCode", languageCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                PackingImage packingImage = new PackingImage(dataSet.Tables[0].Rows[0]);
                return packingImage;
            }
            return null;
        }

        public string imageCode { get { return _imageCode; } set { _imageCode = value; } }
        public string description { get { return _description; } set { _description = value; } }
    }
}
