using LicenseService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseService.Models
{
    public class Usage
    {
        public string id { get; set; }
        public string customerId { get; set; }
        public string productId { get; set; }
        public string environmentName { get; set; } 
        public string userId { get; set; }
        public string applicationAreaCode { get; set; }
        public string description { get; set; }
        public int used { get; set; }
        public string measure {  get; set; }
        public DateTime lastUsed { get; set; }
        
        public void FromUsage(Usage usage)
        {
            this.used = usage.used;
            this.measure = usage.measure;
            this.lastUsed = usage.lastUsed;
        }
    }
}
