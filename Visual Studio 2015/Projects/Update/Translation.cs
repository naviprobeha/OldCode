using System;

using System.Collections.Generic;
using System.Text;

namespace Update
{
    public class Translation
    {
        public static string translate(string languageCode, string caption)
        {
            if (languageCode == "sv") return caption;
            if (languageCode == "en") return translateEn(caption);
            return caption;
        }

        private static string translateEn(string caption)
        {
            if (caption == "Letar efter uppdateringar...")  return "Looking for updates...";
            if (caption == "Nedladdning klar.")             return "Download complete.";
            if (caption == "Laddar ner ")                   return "Downloading ";
            if (caption == "Uppdatera")                     return "Update";
            return "";
        }
    }
}
