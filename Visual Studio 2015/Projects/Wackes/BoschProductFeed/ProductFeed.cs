using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoschProductFeed
{
    public class ProductFeed
    {
        private Configuration configuration;
        public ProductFeed(string serverName, string databaseName, string userId, string password, string companyName, string locationCode)
        {
            configuration = new Configuration();
            configuration.ServerName = serverName;
            configuration.DatabaseName = databaseName;
            configuration.UserID = userId;
            configuration.Password = password;
            configuration.CompanyName = companyName;
            configuration.LocationCode = locationCode;
            
        }

        public string getJson(string webShopCode)
        {
            ItemHelper itemHelper = new ItemHelper(configuration);
            List<Item> itemList = itemHelper.getItems(webShopCode);
            itemList = itemHelper.applyItemTranslations(itemList, webShopCode);
            itemList = itemHelper.applyItemVariants(itemList, webShopCode);
            itemList = itemHelper.applyProductText(itemList, webShopCode);
            itemList = itemHelper.applyInventory(itemList, webShopCode, configuration.LocationCode);

            return Newtonsoft.Json.JsonConvert.SerializeObject(itemList);
        }
    }
}
