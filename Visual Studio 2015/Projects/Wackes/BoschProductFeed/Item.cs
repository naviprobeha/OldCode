using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoschProductFeed
{
    public class Item
    {
        public string no { get; set; }
        public string description { get; set; }
        public string unitOfMeasureCode { get; set; }
        public string productText { get; set; }
        public string imageUrl { get; set; }
        public string itemCategoryCode { get; set; }
        public string productGroupCode { get; set; }
        public int stock { get; set; }
        public decimal points { get; set; }
        public List<ItemTranslation> translations { get; set; }
        public List<ProductText> productTexts { get; set; }
        public List<ItemVariant> variants { get; set; }

        public Item(SqlDataReader dataReader)
        {
            no = dataReader.GetValue(0).ToString();
            unitOfMeasureCode = dataReader.GetValue(1).ToString();
            itemCategoryCode = dataReader.GetValue(2).ToString();
            productGroupCode = dataReader.GetValue(3).ToString();
            description = dataReader.GetValue(4).ToString();
            imageUrl = dataReader.GetValue(5).ToString();
            points = dataReader.GetDecimal(6);

            translations = new List<ItemTranslation>();
            variants = new List<ItemVariant>();
            productTexts = new List<ProductText>();
        }
    }
}
