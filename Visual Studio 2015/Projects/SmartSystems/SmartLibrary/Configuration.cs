using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Navipro.SmartSystems.SmartLibrary
{
    public class Configuration
    {
        private Hashtable configurationValues;

        public Configuration()
        {
            this.configurationValues = new Hashtable();
        }

        public string getConfigValue(string key)
        {
            if (configurationValues[key] != null)
            {
                return (string)configurationValues[key];
            }
            return "";
        }

        public void setConfigValue(string key, string value)
        {
            if (configurationValues[key] != null)
            {
                configurationValues[key] = value;
            }
            else
            {
                configurationValues.Add(key, value);
            }
        }


    }

}
