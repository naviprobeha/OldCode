using Api.Library;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductController : ApiController
    {
        // GET api/items
        [HeaderAuthorization]
        public List<Item> Get(string no = "", string description = "", string collection_name = "", string item_category_code = "", int offset = 0, int count = 0)
        {
            string customerNo = "";
            if (Request.Headers.Contains("CustomerNo"))
            {
                customerNo = Request.Headers.GetValues("CustomerNo").First();
            }

            if ((no != "") || (description != "") || (collection_name != "") || (item_category_code != ""))
            {
                return ItemHelper.SearchItems(customerNo, no, description, collection_name, item_category_code, offset, count);
            }

            return ItemHelper.GetItems(customerNo, offset, count);
        }


        // GET api/items/5
        [HeaderAuthorization]
        public Item Get(string id)
        {
            string customerNo = "";
            if (Request.Headers.Contains("CustomerNo"))
            {
                customerNo = Request.Headers.GetValues("CustomerNo").First();
            }

            return ItemHelper.GetItem(customerNo, id);
        }

    }
}
