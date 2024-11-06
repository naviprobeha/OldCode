using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Lib;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Navipro.Backoffice.Web.Models
{
    public class ResponseGroup
    {
        public ResponseGroup()
        {


        }

        public ResponseGroup(string uri, string caption)
        {
            this.uri = uri;
            this.caption = caption;

        }

 
        [Required]
        [Display(Name = "Kod")]
        public String uri { get; set; }

        [Required]
        [Display(Name = "Rubrik")]
        public String caption { get; set; }

        public bool primary { get; set; }
    }



}