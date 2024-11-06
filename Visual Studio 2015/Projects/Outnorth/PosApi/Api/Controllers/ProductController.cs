using Api.Library;
using Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;

namespace Api.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductController : ApiController
    {


        [HeaderAuthorization]
        public async Task<bool> Post()
        {
            string xml = await Request.Content.ReadAsStringAsync();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            xmlDoc = RemoveAllNamespaces(xmlDoc.DocumentElement);

            XmlElement docElement = xmlDoc.DocumentElement;
            List<Product> productList = new List<Product>();

            XmlNodeList productNodeList = docElement.SelectNodes("product");
            foreach (XmlNode productNode in productNodeList)
            {
                XmlElement productElement = (XmlElement)productNode;

                string xmlCategoryCode = "";
                string xmlCategoryDesc = "";
                string xmlBrandDesc = "";
                string xmlSeasonCode = "";
                string xmlColorCode = "";
                string xmlSizeCode = "";
                string xmlColorName = "";
                string xmlFolderName = "";
                string xmlBaseColorCode = "";
                string EAN = "";
                decimal price = 0;
                int sorting = 0;


                XmlNodeList nodeList = productElement.SelectNodes("custom_fields");
                foreach (XmlNode node in nodeList)
                {
                    XmlElement xmlElement = (XmlElement)node;

                    if (XmlHelper.GetNodeValue(xmlElement, "name") == "ean_code") EAN = XmlHelper.GetNodeValue(xmlElement, "value");

                    if (XmlHelper.GetNodeValue(xmlElement, "name") == "cat_code") xmlCategoryCode = XmlHelper.GetNodeValue(xmlElement, "value");
                    if (XmlHelper.GetNodeValue(xmlElement, "name") == "cat_desc") xmlCategoryDesc = XmlHelper.GetNodeValue(xmlElement, "value");
                    if (XmlHelper.GetNodeValue(xmlElement, "name") == "brand") xmlBrandDesc = XmlHelper.GetNodeValue(xmlElement, "value");
                    if (XmlHelper.GetNodeValue(xmlElement, "name") == "season") xmlSeasonCode = XmlHelper.GetNodeValue(xmlElement, "value");
                    if (XmlHelper.GetNodeValue(xmlElement, "name") == "color") xmlColorName = XmlHelper.GetNodeValue(xmlElement, "value");
                    if (XmlHelper.GetNodeValue(xmlElement, "name") == "parent_cat_desc") xmlFolderName = XmlHelper.GetNodeValue(xmlElement, "value");
                    if (XmlHelper.GetNodeValue(xmlElement, "name") == "base_color_code") xmlBaseColorCode = XmlHelper.GetNodeValue(xmlElement, "value");
                }

                XmlNodeList nodeList2 = productElement.SelectNodes("spec_fields");
                foreach (XmlNode node in nodeList2)
                {
                    XmlElement xmlElement = (XmlElement)node;

                    if (XmlHelper.GetNodeValue(xmlElement, "name") == "color") xmlColorCode = XmlHelper.GetNodeValue(xmlElement, "value");
                    if (XmlHelper.GetNodeValue(xmlElement, "name") == "size_normalized") xmlSizeCode = XmlHelper.GetNodeValue(xmlElement, "value");
                    if (XmlHelper.GetNodeValue(xmlElement, "name") == "size_sorting") sorting = int.Parse(XmlHelper.GetNodeValue(xmlElement, "value"));

                }

                XmlNodeList nodeList3 = productElement.SelectNodes("price_rows");
                foreach (XmlNode node in nodeList3)
                {
                    XmlElement xmlElement = (XmlElement)node;

                    if ((XmlHelper.GetNodeValue(xmlElement, "pricelist") == "3") && (XmlHelper.GetNodeValue(xmlElement, "currency") == "SEK")) price = ParseDecimal(XmlHelper.GetNodeValue(xmlElement, "price_excl"));
                }


                string vatCode = "MOMS25";
                if (XmlHelper.GetNodeValue(productElement, "account_cat") == "6") vatCode = "MOMS12";
                if (XmlHelper.GetNodeValue(productElement, "account_cat") == "7") vatCode = "MOMS6";

                if (vatCode == "MOMS25") price = Decimal.Multiply(price, Decimal.Parse("1.25", System.Globalization.CultureInfo.InvariantCulture));
                if (vatCode == "MOMS12") price = Decimal.Multiply(price, Decimal.Parse("1.12", System.Globalization.CultureInfo.InvariantCulture));
                if (vatCode == "MOMS6") price = Decimal.Multiply(price, Decimal.Parse("1.06", System.Globalization.CultureInfo.InvariantCulture));


                Product product = new Product
                {
                    sku = XmlHelper.GetNodeValue(productElement, "product_id").Replace(" ", "-"),
                    itemNo = XmlHelper.GetNodeValue(docElement, "updateSettings/parent_key_value"), 
                    variantCode = GetVariantCode(productElement, XmlHelper.GetNodeValue(docElement, "updateSettings/parent_key_value")),
                    description = XmlHelper.GetNodeValue(productElement, "name"),
                    description2 = XmlHelper.GetNodeValue(productElement, "name2"),
                    itemCategoryCode = xmlCategoryCode,
                    itemCategoryDescription = xmlCategoryDesc,
                    productGroupCode = "",
                    productGroupDescription = "",
                    brandCode = XmlHelper.GetNodeValue(productElement, "group_code"),
                    brandDescription = xmlBrandDesc,
                    baseUnitOfMeasureCode = "ST",
                    salesUnitOfMeasureCode = "ST",
                    seasonCode = xmlSeasonCode,
                    composition = "",
                    colorCode = xmlColorCode,
                    sizeCode = xmlSizeCode,
                    colorName = xmlColorName,
                    baseColorCode = xmlBaseColorCode,
                    baseColorName = xmlColorName,
                    folders = xmlFolderName,
                    sortOrder = sorting,
                    productText = "",
                    primaryCrossReferenceNo = EAN,
                    additionalCrossReferenceNo = "",
                    vatProductPostingGroup = vatCode,
                    imageUrl = "",
                    primaryUnitPrice = price,
                    unitCost = ParseDecimal(XmlHelper.GetNodeValue(productElement, "price_calc")),
                    externalId = XmlHelper.GetNodeValue(productElement, "material_number")

                };


                productList.Add(product);

            }

            RequestHandler.PushProducts(productList);
            return true;
        }


        private string GetVariantCode(XmlElement productElement, string itemNo)
        {
            string sku = XmlHelper.GetNodeValue(productElement, "product_id").Replace(" ", "-");

            if (sku == itemNo) return "";

            return sku.Substring(itemNo.Length + 1);

        }


        private XmlDocument RemoveAllNamespaces(XmlElement documentElement)
        {
            var xmlnsPattern = "\\s+xmlns\\s*(:\\w)?\\s*=\\s*\\\"(?<url>[^\\\"]*)\\\"";
            var outerXml = documentElement.OuterXml;
            var matchCol = System.Text.RegularExpressions.Regex.Matches(outerXml, xmlnsPattern);
            foreach (var match in matchCol)
                outerXml = outerXml.Replace(match.ToString(), "");

            var result = new XmlDocument();
            result.LoadXml(outerXml);

            return result;
        }

        private decimal ParseDecimal(string input)
        {
            Decimal dec = 0;
            decimal.TryParse(input, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out dec);

            return dec;
        }
    }
}
