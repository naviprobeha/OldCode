using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Navipro.Backoffice.Web.Models
{
    public class ProfileIndicator
    {
        public string label { get; set; }
        public string caption { get; set; }
        public DataView dataView { get; set; }
        public string icon { get; set; }

        public int lowerLevel { get; set; }
        public int upperLevel { get; set; }
        public int value { get; set; }

        public string badgeStyle
        {
            get
            {
                if (value < lowerLevel) return "label-success";
                if ((value >= lowerLevel) && (value < upperLevel)) return "label-info";
                if (value >= upperLevel) return "label-danger";
                return "label-info";
            }
        }
        public string badgeText
        {
            get
            {
                if (value < lowerLevel) return "OK";
                if ((value >= lowerLevel) && (value < upperLevel)) return "Varning!";
                if (value >= upperLevel) return "För högt!";
                return "Varning!";
            }
        }

        public void update(Navipro.Backoffice.Web.Lib.Database database)
        {
            if (dataView.type == "case")
            {
                value = Case.countView(database, dataView);
            }
        }
    }
}