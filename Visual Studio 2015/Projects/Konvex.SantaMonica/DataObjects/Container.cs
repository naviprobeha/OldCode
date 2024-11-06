using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class Container
    {
        private string _no;
        private string _containerTypeCode;
        private int _currentLocationType;
        private string _currentLocationCode;

        public Container()
        { }

        public Container(Navipro.SantaMonica.Common.Container container)
        {
            this._no = container.no;
            this._containerTypeCode = container.containerTypeCode;
            this._currentLocationCode = container.currentLocationCode;
            this._currentLocationType = container.currentLocationType;
        }

        public string no { get { return _no; } set { _no = value; } }
        public string containerTypeCode { get { return _containerTypeCode; } set { _containerTypeCode = value; } }
        public int currentLocationType { get { return _currentLocationType; } set { _currentLocationType = value; } }
        public string currentLocationCode { get { return _currentLocationCode; } set { _currentLocationCode = value; } }


        public static Container fromJsonObject(JObject jsonObject)
        {
            Container entry = new Container();
            entry.no = (string)jsonObject["no"];
            entry.containerTypeCode = (string)jsonObject["containerTypeCode"];
            entry.currentLocationType = (int)jsonObject["currentLocationType"];
            entry.currentLocationCode = (string)jsonObject["currentLocationCode"];


            return entry;

        }
    }
}
