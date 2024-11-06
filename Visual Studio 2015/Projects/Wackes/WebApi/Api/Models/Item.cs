using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Item
    {
        public string no { set; get; } = "";
        public string description { set; get; } = "";
        public string description2 { set; get; } = "";
        public string itemCategory { set; get; } = "";
        public string productGroup { set; get; } = "";
        public decimal unitPrice { set; get; }

        public string imageUrl { get; set; } = "";


        public decimal inventory { get; set; }
        public decimal qtyOnSalesOrder { get; set; }


        public List<ItemTranslation> itemTranslations { get; set; }

        public List<ItemVariant> itemVariants { get; set; }

        public Item()
        { }

        public Item(SqlDataReader dataReader)
        {
            no = dataReader["No_"].ToString();
            description = dataReader["Description"].ToString();
            description2 = dataReader["Description 2"].ToString();
            if (!dataReader.IsDBNull(dataReader.GetOrdinal("Points")))
            {
                unitPrice = dataReader.GetInt32(dataReader.GetOrdinal("Points"));
            }
            itemCategory = dataReader["Item Category Code"].ToString();
            productGroup = dataReader["Product Group Code"].ToString();

            imageUrl = dataReader["Image Url"].ToString();

            itemTranslations = new List<ItemTranslation>();
            itemVariants = new List<ItemVariant>();
        }
    }
}

