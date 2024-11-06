using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Navipro.Backoffice.Web.Models
{
    public class SearchResult
    {
        public string caption { get; set; }
        public string url { get; set; }
        public string description { get; set; }

    }

}