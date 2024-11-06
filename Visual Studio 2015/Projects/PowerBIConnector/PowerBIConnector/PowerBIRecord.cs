using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaviPro.PowerBIConnector
{
    public class PowerBIRecord
    {
        private Dictionary<string, string> fieldList;

        public PowerBIRecord()
        {
            fieldList = new Dictionary<string, string>();
        }

        public void addFieldValue(string fieldName, string value)
        {
            if (fieldList.ContainsKey(fieldName))
            {
                fieldList[fieldName] = value;
                return;
            }
            fieldList.Add(fieldName, value);
        }

        public void addDefaultFieldValue(string fieldName, string value)
        {
            if (fieldList.ContainsKey(fieldName)) return;
            fieldList.Add(fieldName, value);
        }

        public string toJson()
        {
            string recordData = "{ ";
            string columnData = "";
            foreach (string fieldName in fieldList.Keys)
            {
                if (columnData != "") columnData = columnData + ", ";
                columnData = columnData + "	\""+ fieldName.Replace("\"", "") + "\": \"" + fieldList[fieldName].Replace("\"", "") + "\"";
            }
            recordData = recordData + columnData + "}";

            return recordData;
        }
    }
}
