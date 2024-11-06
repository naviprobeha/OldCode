using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;

namespace SmyckaReceiptService.Models
{
    public class FormField
    {
        [Key, MaxLength(30)]
        public string id { get; set; }
        public string formCode { get; set; }
        [MaxLength(50)]
        public string code { get; set; }
        [MaxLength(30)]
        public string description { get; set; }
        public int type { get; set; }
        [MaxLength(50)]
        public string defaultValue { get; set; }
        [MaxLength(250)]
        public string optionValues { get; set; }
        [MaxLength(20)]
        public string connectionField { get; set; }
        public int sortOrder { get; set; }
        public bool readOnly { get; set; }
        public bool required { get; set; }

        public string typeStr
        {
            get
            {
                if (type == 0) return "Textfield";
                if (type == 1) return "Drop Down";
                if (type == 2) return "Checkbox";
                return "";

            }
        }

        public FormField()
        { }

 

    }
}
