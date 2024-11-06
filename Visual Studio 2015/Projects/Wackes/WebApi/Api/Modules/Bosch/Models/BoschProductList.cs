using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class BoschProductList
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Total { get; set; }
        public List<BoschProduct> Items { get; set; }

        public BoschProductList()
        {
            Items = new List<BoschProduct>();
        }
    }
}