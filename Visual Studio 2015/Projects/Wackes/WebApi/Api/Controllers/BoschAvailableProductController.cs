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
    [RoutePrefix("api/boschavailableproducts")]
    public class BoschAvailableProductController : ApiController
    {
        // GET api/items
        [HeaderAuthorization]
        public BoschItemList Get(int limit = 9999, int offset = 0)
        {
            int totalCount = 0;
            BoschItemList list = new BoschItemList();
            list.Items = ItemAPIHelper.GetItemSkuList(ItemAPIHelper.GetItems(false, false, offset, limit, out totalCount));

            return list;
        }


    }
}
