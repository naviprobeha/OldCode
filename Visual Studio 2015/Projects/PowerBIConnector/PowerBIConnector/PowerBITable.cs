using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaviPro.PowerBIConnector
{
    public class PowerBITable
    {
        public string name { get; set; }
        private Dictionary<string, string> fieldList;

        public PowerBITable(string name)
        {
            this.name = name;
            fieldList = new Dictionary<string, string>();
        }

        public void addField(string fieldName, string dataType)
        {
            fieldList.Add(fieldName, dataType);
        }

        public string toJson()
        {
            string jsonData = "{ \"name\": \"" + name.Replace("\"", "") + "\", \"columns\": [ ";
            string columnData = "";
            foreach (string fieldName in fieldList.Keys)
            {
                if (columnData != "") columnData = columnData + ", ";
                columnData = columnData + "{	\"name\": \"" + fieldName.Replace("\"", "") + "\", \"dataType\": \"" + fieldList[fieldName].Replace("\"", "") + "\" }";
            }
            jsonData = jsonData + columnData + "] }";

            return jsonData;
        }
    }
}
