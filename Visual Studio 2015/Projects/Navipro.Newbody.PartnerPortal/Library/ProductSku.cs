using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Newbody.PartnerPortal.Library
{
    public class ProductSku
    {
        private string _itemNo;
        private string _size;
        private string _modelNo;

        public ProductSku()
        {

        }

        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string size { get { return _size; } set { _size = value; } }
        public string modelNo { get { return _modelNo; } set { _modelNo = value; } }

    }
}
