using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutnorthYodaFeedImport
{
    public class ProductVariant
    {
        public string sku { get; set; } = "";
        public string itemNo { get; set; } = "";
        public string variantCode { get; set; } = "";
        public decimal primarySalesPrice { get; set; }
        public decimal primaryUnitPrice { get; set; }
        public string imageUrl { get; set; }
        public string productUrl { get; set; }
        public string externalId { get; set; }

        public ProductVariant(string[] columns)
        {
            itemNo = columns[0];
            sku = columns[1];
            externalId = columns[8];

            itemNo = itemNo.Replace("\"", "");
            sku = sku.Replace("\"", "");
            externalId = externalId.Replace("\"", "");



            if ((sku != itemNo) && (itemNo.Length < sku.Length))
            {
                try
                {
                    variantCode = sku.Substring(itemNo.Length + 1);
                }
                catch(Exception e)
                {
                    throw new Exception(itemNo + ";" + sku + "; " + e.Message);
                }
            }
            sku = itemNo + '-' + variantCode;

            
            try
            {
                primarySalesPrice = decimal.Parse(columns[7], System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                throw new Exception(columns[7].Replace("\"", "") + ";" + sku + "; " + e.Message);
            }
            try
            {
                primaryUnitPrice = decimal.Parse(columns[6], System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                throw new Exception(columns[6].Replace("\"", "") + ";" + sku + "; " + e.Message);
            }

            productUrl = columns[5];
            imageUrl = columns[4];

            //productUrl = productUrl.Replace("\"", "");
            //imageUrl = imageUrl.Replace("\"", "");

        }
    }
}
