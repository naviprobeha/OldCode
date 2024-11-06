using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class ItemTranslation
    {
        public string itemNo { get; set; } = "";
        public string languageCode { get; set; } = "";
        public string description { get; set; } = "";

        public string productText { get; set; } = "";


        public ItemTranslation()
        {

        }

        public ItemTranslation(SqlDataReader dataReader)
        {
            // trans.[Item No_], trans.[Language Code], trans.[Description], trans.[Description 2]

            itemNo = dataReader.GetValue(0).ToString();
            languageCode = dataReader.GetValue(1).ToString();
            description = dataReader.GetValue(2).ToString();

        }

        public string getLocale()
        {
            if (languageCode == "SE") return "sv-SE";
            if (languageCode == "SVE") return "sv-SE";
            if (languageCode == "SWE") return "sv-SE";
            if (languageCode == "EN") return "en-US";
            if (languageCode == "ENG") return "en-US";
            if (languageCode == "DK") return "da-DK";
            if (languageCode == "FIN") return "fi-FI";
            if (languageCode == "NO") return "nb-NO";
            return "en-US";
        }
    }
}