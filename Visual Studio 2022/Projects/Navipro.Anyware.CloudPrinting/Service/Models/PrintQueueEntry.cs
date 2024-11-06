using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Navipro.Anyware.CloudPrinting.Service.Models
{
    public class PrintQueueEntry
    {
        [PrimaryKey, MaxLength(50)]
        public string id { get; set; }

        [MaxLength(50)]
        public string printerName { get; set; }
        [MaxLength(100)]
        public string uncPrinterPath { get; set; }
        [MaxLength(50)]
        public string serviceId { get; set; }
        
        public string base64Document { get; set; }
    }
}
