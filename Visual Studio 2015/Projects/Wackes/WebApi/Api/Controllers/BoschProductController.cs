using Api.Library;
using Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("api/boschproducts")]
    public class BoschProductController : ApiController
    {
        // GET api/items
        [HeaderAuthorization]
        public BoschProductList Get(int limit = 100, int offset = 0)
        {
            BoschProductList productList = new BoschProductList();
            productList.Total = 100;
            productList.Offset = offset;
            productList.Limit = limit;

            int totalCount = 0;
            List<Item> itemList = ItemAPIHelper.GetItems(true, true, offset, limit, out totalCount);
            productList.Total = totalCount;

            foreach (Item item in itemList)
            {
                productList.Items.Add(new BoschProduct(item));
            }

            

            return productList;
        }

        // GET api/items/5
        [HeaderAuthorization]
        public BoschProduct Get(string id)
        {
            Item item = ItemAPIHelper.GetItem(id);
            if (item != null)
            {
                return new BoschProduct(item);
            }

            return null;
        }

        [HeaderAuthorization]
        public BoschProductList Post([FromBody] string[] itemNoList)
        {
            /*
            StreamReader reader = new StreamReader(Request.Body);
            string json = reader.ReadToEnd();

            string[] itemNoList = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(json);
            */

            BoschProductList productList = new BoschProductList();
            productList.Total = itemNoList.Count();
            productList.Offset = 0;
            productList.Limit = 0;


            foreach(string itemNo in itemNoList)
            {
                Item item = ItemAPIHelper.GetItem(itemNo);
                if (item != null)
                {
                    productList.Items.Add(new BoschProduct(item));
                }

            }

            return productList;
        }

    }
}
