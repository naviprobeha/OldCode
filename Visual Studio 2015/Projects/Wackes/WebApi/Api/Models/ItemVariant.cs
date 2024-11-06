using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class ItemVariant
    {
        public string itemNo { get; set; }
        public string code { get; set; }
        public string description { get; set; }

        public string sizeCode { get; set; }

        public int sortOrder { get; set; }

        public decimal inventory { get; set; }
        public decimal qtyOnSalesOrder { get; set; }

        public ItemVariant()
        {

        }

        public static ItemVariant FromInventory(SqlDataReader dataReader)
        {
            //ile.[Item No_], ile.[Variant Code], SUM(ile.[Quantity]) 
            ItemVariant variant = new ItemVariant();
            variant.itemNo = dataReader.GetValue(0).ToString();
            variant.code = dataReader.GetValue(1).ToString();

            variant.inventory = dataReader.GetDecimal(2);

            return variant;
        }

        public static ItemVariant FromVariant(SqlDataReader dataReader)
        {
            //ile.[Item No_], ile.[Variant Code], SUM(ile.[Quantity]) 
            ItemVariant variant = new ItemVariant();
            variant.itemNo = dataReader.GetValue(0).ToString();
            variant.code = dataReader.GetValue(1).ToString();

            variant.description = dataReader.GetValue(2).ToString();
            variant.sizeCode = dataReader.GetValue(3).ToString();

            return variant;
        }
    }
}