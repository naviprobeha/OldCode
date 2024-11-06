using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Navipro.Infojet.Lib
{
    public class GlobalCache
    {
        public static object checkCache(string className, string key)
        {
            Hashtable globalCacheTable = (Hashtable)System.Web.HttpContext.Current.Session["globalCache"];
            if (globalCacheTable == null) globalCacheTable = new Hashtable();

            if (globalCacheTable.Contains(className + "_" + key))
            {
                return globalCacheTable[className + "_" + key];
            }

            return null;
        }

        public static void cacheObject(string className, string key, object obj)
        {
            Hashtable globalCacheTable = (Hashtable)System.Web.HttpContext.Current.Session["globalCache"];
            if (globalCacheTable == null)
            {
                globalCacheTable = new Hashtable();
                System.Web.HttpContext.Current.Session.Add("globalCache", globalCacheTable);
            }

            if (globalCacheTable.Contains(className + "_" + key))
            {
                globalCacheTable[className + "_" + key] = obj;
            }
            else
            {
                globalCacheTable.Add(className + "_" + key, obj);
            }

            System.Web.HttpContext.Current.Session["globalCache"] = globalCacheTable;

        }
    }



}
