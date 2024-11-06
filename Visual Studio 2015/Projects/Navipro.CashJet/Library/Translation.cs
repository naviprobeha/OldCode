using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.Library
{
    public class Translation
    {
        public static string getTranslation(string text, string languageCode)
        {
            if (languageCode == "ENG") return translateEnglish(text);
            return text;

        }

        public static string translateEnglish(string text)
        {
            if (text == "Alla butiker") return "All stores";
            if (text == "Omsättning") return "Turnover";
            if (text == "Omsättning f.å.") return "Turnover l.y.";
            if (text == "Antal köp per timme") return "Receipts per hour";
            if (text == "Dag") return "Day";
            if (text == "Vecka") return "Week";
            if (text == "Månad") return "Month";
            if (text == "År") return "Year";
            if (text == "Datumintervall") return "Date interval";
            return text;
        }
    }
}
