using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.Woppen.Library
{
    public class PackingImages
    {

        public static PackingImage getPackingImage(Navipro.Infojet.Lib.Infojet infojetContext, int noOfPackages)
        {
            PackingImage packingImage = null;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Image Code], [Description] FROM [" + infojetContext.systemDatabase.getTableName("Packing Image") + "] WITH (NOLOCK) WHERE [From Qty] <= @quantity AND [To Qty] > @quantity AND [Language Code] = '"+infojetContext.languageCode+"'");
            databaseQuery.addIntParameter("@quantity", noOfPackages);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                packingImage = new PackingImage();
                packingImage.webImageCode = dataReader.GetValue(0).ToString();
                packingImage.description = dataReader.GetValue(1).ToString();
            }

            dataReader.Close();

            return packingImage;
        }
    }
}
