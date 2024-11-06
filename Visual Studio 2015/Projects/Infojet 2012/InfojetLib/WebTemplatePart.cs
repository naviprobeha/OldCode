using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Navipro.Infojet.Lib
{
    public class WebTemplatePart
    {
        private string _webSiteCode;
        private string _webTemplateCode;
        private string _code;
        private string _description;
        private int _sortOrder;
        private int _positionX;
        private int _positionY;
        private int _width;
        private int _height;
        private int _componentPlacement;

        private Infojet infojetContext;

        public WebTemplatePart()
        {
        }

        public WebTemplatePart(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            _webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
            _webTemplateCode = dataRow.ItemArray.GetValue(1).ToString();
            _code = dataRow.ItemArray.GetValue(2).ToString();
            _description = dataRow.ItemArray.GetValue(3).ToString();
            _sortOrder = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
            _positionX = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
            _positionY = int.Parse(dataRow.ItemArray.GetValue(6).ToString());
            _width = int.Parse(dataRow.ItemArray.GetValue(7).ToString());
            _height = int.Parse(dataRow.ItemArray.GetValue(8).ToString());
            _componentPlacement = int.Parse(dataRow.ItemArray.GetValue(9).ToString());
        }

        public string webSiteCode { get { return _webSiteCode; } set { _webSiteCode = value; } }
        public string webTemplateCode { get { return _webTemplateCode; } set { _webTemplateCode = value; } }
        public string code { get { return _code; } set { _code = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public int sortOrder { get { return _sortOrder; } set { _sortOrder = value; } }
        public int positionX { get { return _positionX; } set { _positionX = value; } }
        public int positionY { get { return _positionY; } set { _positionY = value; } }
        public int width { get { return _width; } set { _width = value; } }
        public int height { get { return _height; } set { _height = value; } }
        public int componentPlacement { get { return _componentPlacement; } set { _componentPlacement = value; } }


    }
}
