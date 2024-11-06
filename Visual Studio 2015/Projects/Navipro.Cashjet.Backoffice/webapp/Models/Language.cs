using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAdminMvc
{
    public class Language
    {
        public string name { get; set; }
        public string language { get; set; }
        public string flag { get; set; }

        public Language(string name, string language, string flag)
        {
            this.name = name;
            this.language = language;
            this.flag = flag;
        }
    }
}