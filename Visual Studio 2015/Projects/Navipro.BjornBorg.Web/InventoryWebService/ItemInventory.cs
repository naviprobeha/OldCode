using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Navipro.BjornBorg.Web
{
    public class ItemInventory
    {
        private string _sku;
        private int _inventory;
        private string _locationCode;

        public string sku { get { return _sku; } set { _sku = value; } }
        public int inventory { get { return _inventory; } set { _inventory = value; } }
        public string locationCode { get { return _locationCode; } set { _locationCode = value; } }
    }
}
