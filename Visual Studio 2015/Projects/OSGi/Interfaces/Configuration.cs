using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Navipro.OSGi.Interfaces
{
    public class Configuration : MarshalByRefObject
    {
        Hashtable configurationTable;

        public Configuration()
        {
            configurationTable = new Hashtable();
        }

        public void setValue(string key, string value)
        {
            if (configurationTable[key] == null)
            {
                configurationTable.Add(key, value);
            }
            else
            {
                configurationTable[key] = value;
            }
        }

        public string getValue(string key)
        {
            return (string)configurationTable[key];
        }

    }
}
