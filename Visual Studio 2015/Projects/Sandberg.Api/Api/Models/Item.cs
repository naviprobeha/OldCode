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
        public string description { set; get; }
        public string description2 { set; get; }
        public string collection_name { set; get; }
        public string item_category { set; get; }
        public string product_group { set; get; }
        public bool stock_item_only { set; get; }
        public DateTime end_date { set; get; }
        public decimal unit_price { set; get; }
        public decimal net_price { set; get; }
        public decimal unit_price_incl_vat { set; get; }
        public decimal net_price_incl_vat { set; get; }

        public decimal retail_price { set; get; }
        public string unit_of_measure { set; get; }
        public DateTime customer_valid_to { get; set; }
        public bool customer_valid { get; set; }
        public DateTime earliest_stock_date { set; get; }

        public string matching { get; set; }
        public string roll_length { get; set; }
        public string idiom { get; set; }
        public string match_type { get; set; }
        public string designer { get; set; }
        public string pattern_width { get; set; }

        public decimal gross_weight { get; set; }

        public decimal available_stock { get; set; }

        public string vat_prod_posting_group { get; set; }

        public List<ItemBatch> batches { get; set; }
        public List<SampleItem> sample_products { get; set; }

        public Item()
        { }

        public Item(SqlDataReader dataReader)
        {
            no = dataReader["No_"].ToString();
            description = dataReader["Description"].ToString();
            description2 = dataReader["Description 2"].ToString();
            unit_of_measure = dataReader["Base Unit of Measure"].ToString();
            if (!dataReader.IsDBNull(dataReader.GetOrdinal("UnitPrice")))
            {
                unit_price = dataReader.GetDecimal(dataReader.GetOrdinal("UnitPrice"));
            }
            if (!dataReader.IsDBNull(dataReader.GetOrdinal("RetailPrice")))
            {
                retail_price = dataReader.GetDecimal(dataReader.GetOrdinal("RetailPrice"));
            }
            collection_name = dataReader["CollectionName"].ToString();
            if (!dataReader.IsDBNull(dataReader.GetOrdinal("EarliestStockDate")))
            {
                earliest_stock_date = dataReader.GetDateTime(dataReader.GetOrdinal("EarliestStockDate"));
            }
            net_price = unit_price;
            if (!dataReader.IsDBNull(dataReader.GetOrdinal("LineDiscountProc")))
            {
                decimal lineDisc = dataReader.GetDecimal(dataReader.GetOrdinal("LineDiscountProc"));
                net_price = unit_price * (1 - (lineDisc / 100));
            }
            item_category = dataReader["Item Category Code"].ToString();
            product_group = dataReader["Alt Produktgrupp"].ToString();

            if (dataReader["Stock Item Only"].ToString() == "1") stock_item_only = true;

            if (!dataReader.IsDBNull(dataReader.GetOrdinal("End Date")))
            {
                end_date = dataReader.GetDateTime(dataReader.GetOrdinal("End Date"));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal("validUntil")))
            {
                customer_valid_to = dataReader.GetDateTime(dataReader.GetOrdinal("validUntil"));
            }
            if (customer_valid_to >= DateTime.Parse("1753-01-01")) customer_valid = true;

            matching = dataReader["Match Code"].ToString();
            roll_length = dataReader["Roll Length Code"].ToString();
            idiom = dataReader["Idiom Code"].ToString();
            match_type = dataReader["Match Type Code"].ToString();
            designer = dataReader["Designer Code"].ToString();
            pattern_width = dataReader["Pattern Width Code"].ToString();

            if (!dataReader.IsDBNull(dataReader.GetOrdinal("Gross Weight")))
            {
                gross_weight = dataReader.GetDecimal(dataReader.GetOrdinal("Gross Weight"));
            }

            vat_prod_posting_group = dataReader["VAT Prod_ Posting Group"].ToString();

            batches = new List<ItemBatch>();
        }
    }
}

