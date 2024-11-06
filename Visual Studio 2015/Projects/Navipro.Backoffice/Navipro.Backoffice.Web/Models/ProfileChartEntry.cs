using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Navipro.Backoffice.Web.Models
{
    public class ProfileChartEntry
    {
        public string label { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }

        public decimal value { get; set; }
    }
}