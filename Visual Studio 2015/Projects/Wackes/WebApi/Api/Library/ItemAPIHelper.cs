using Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Api.Library
{
    public class ItemAPIHelper
    {
        public static List<Item> GetItems(bool includeInventory, bool includeTranslations, int offset, int count, out int totalCount)
        {
            Configuration configuration = new Configuration();
            configuration.init();


            string token = OAuthHelper.GetToken(configuration.domain, configuration.clientId, configuration.clientSecret);

            string jsonContent = CallBCAPI("wackes.com/production/api/navipro/webapi/v1.0/items?company=WACKES&$filter=npxWebShop eq 'BOSCH'", token);



            BCItemList bcItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<BCItemList>(jsonContent);
            List<Item> itemList = bcItemList.value;
            totalCount = itemList.Count;

            if (count == 0) count = 100;

            if (offset > 0) itemList = itemList.Skip(offset).ToList();
            if (count > 0) itemList = itemList.Take(count).ToList();


            if (includeInventory) itemList = ApplyVariants(itemList, "", token);
            if (includeTranslations) itemList = ApplyTranslations(itemList, "", token);

            return itemList;
        }

        public static Item GetItem(string no)
        {

            Configuration configuration = new Configuration();
            configuration.init();


            string token = OAuthHelper.GetToken(configuration.domain, configuration.clientId, configuration.clientSecret);

            string jsonContent = CallBCAPI("wackes.com/production/api/navipro/webapi/v1.0/items?company=WACKES&$filter=npxWebShop eq 'BOSCH' and no eq '" + no+"'", token);



            BCItemList bcItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<BCItemList>(jsonContent);
            List<Item> itemList = bcItemList.value;


            itemList = ApplyVariants(itemList, no, token);
            itemList = ApplyTranslations(itemList, no, token);



            if (itemList.Count == 1) return itemList[0];
            return null;
        }

        public static List<Item> ApplyVariants(List<Item> itemList, string itemNo, string token)
        {
            string itemNoFilter = "";
            if (itemNo != "") itemNoFilter = " and itemNo eq '" + itemNo + "'";

            string jsonContent = CallBCAPI("wackes.com/production/api/navipro/webapi/v1.0/itemVariants?company=WACKES&$filter=npxapiWebShop eq 'BOSCH'" + itemNoFilter, token);

            BCItemVariantList variantList = Newtonsoft.Json.JsonConvert.DeserializeObject<BCItemVariantList>(jsonContent);

            List<ItemVariant> itemVariantList = variantList.value;

            int i = 0;
            while (i < itemList.Count)
            {
                itemList[i].itemVariants = itemVariantList.Where(l => l.itemNo == itemList[i].no).ToList();

                int j = 0;
                while (j < itemList[i].itemVariants.Count)
                {
                    itemList[i].itemVariants[j].inventory = itemList[i].itemVariants[j].inventory - itemList[i].itemVariants[j].qtyOnSalesOrder;

                    j++;
                }

                i++;
            }


            return itemList;
        }


        public static List<Item> ApplyTranslations(List<Item> itemList, string itemNo, string token)
        {
            string itemNoFilter = "";
            if (itemNo != "") itemNoFilter = " and itemNo eq '" + itemNo + "'";

            Dictionary<string, ItemTranslation> productTextList = new Dictionary<string, ItemTranslation>();

            string jsonContent = CallBCAPI("wackes.com/production/api/navipro/webapi/v1.0/itemTranslations?company=WACKES&$filter=npxapiWebShop eq 'BOSCH'" + itemNoFilter, token);

            BCItemTranslationList bcTransList = Newtonsoft.Json.JsonConvert.DeserializeObject<BCItemTranslationList>(jsonContent);

            List<ItemTranslation> itemTranslationList = bcTransList.value;

            foreach(ItemTranslation trans in itemTranslationList)
            {
                productTextList.Add(trans.itemNo + "_" + trans.languageCode, trans);

            }

            
            if (itemNo != "") itemNoFilter = " and no eq '" + itemNo + "'";

            jsonContent = CallBCAPI("wackes.com/production/api/navipro/webapi/v1.0/extendedTextLines?company=WACKES&$filter=npxapiWebShop eq 'BOSCH'" + itemNoFilter, token);

            BCExtTextLineList bcExtTextList = Newtonsoft.Json.JsonConvert.DeserializeObject<BCExtTextLineList>(jsonContent);

            List<ExtTextLine> extTextLineList = bcExtTextList.value;

            foreach (ExtTextLine line in extTextLineList)
            {
                if (productTextList.ContainsKey(line.no +"_" + line.languageCode))
                {
                    ItemTranslation productText = productTextList[line.no + "_" + line.languageCode];
                    if (productText.productText != "") productText.productText = productText.productText + " ";
                    productText.productText = productText.productText + line.text;
                    productTextList[line.no + "_" + line.languageCode] = productText;
                }
            }


            int i = 0;
            while (i < itemList.Count)
            {
                itemList[i].itemTranslations = productTextList.Values.Where(l => l.itemNo == itemList[i].no).ToList();
                i++;
            }



            return itemList;
        }

        public static List<string> GetItemSkuList(List<Item> itemList)
        {
            return itemList.Select(l => l.no).ToList();
        }

        public static string CallBCAPI(string endpoint, string token)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.businesscentral.dynamics.com/v2.0/"+endpoint);
            request.Headers.Add("Authorization", "Bearer "+token);

            request.Method = "GET";
            request.Accept = "application/json";


            WebResponse response = request.GetResponse();

            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string content2 = streamReader.ReadToEnd();

            Console.WriteLine(content2);

            return content2;
        }
    }
}