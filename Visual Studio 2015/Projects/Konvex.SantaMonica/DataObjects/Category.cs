using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class Category
    {
        private string _code;
        private string _description;

        public Category()
        { }

        public Category(Navipro.SantaMonica.Common.Category category)
        {
            _code = category.code;
            _description = category.description;
        }

        public string code { get { return _code; } set { _code = value; } }
        public string description { get { return _description; } set { _description = value; } }
    }
}
