using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Navipro.Newbody.PartnerPortal.Library
{
    public class GlobalCache
    {
        private DateTime _expireDateTime;
        private object _cachedObject;

        public DateTime expireDateTime { get { return _expireDateTime; } set { _expireDateTime = value; } }
        public object cachedObject { get { return _cachedObject; } set { _cachedObject = value; } }

        public GlobalCache(object cachedObject, int expireMinutes)
        {
            this._cachedObject = cachedObject;
            this._expireDateTime = DateTime.Now.AddSeconds(expireMinutes);
        }

        public static object checkCache(string className, string key)
        {
            Hashtable globalCacheTable = (Hashtable)System.Web.HttpContext.Current.Application["globalCache"];
            if (globalCacheTable == null) globalCacheTable = new Hashtable();

            if (globalCacheTable.Contains(className + "_" + key))
            {
                GlobalCache globalCacheEntry = (GlobalCache)globalCacheTable[className + "_" + key];
                if (globalCacheEntry.expireDateTime > DateTime.Now) return globalCacheEntry.cachedObject;
                globalCacheTable.Remove(className + "_" + key);
            }

            return null;
        }

        public static void cacheObject(string className, string key, object obj, int expireSeconds)
        {
            Hashtable globalCacheTable = (Hashtable)System.Web.HttpContext.Current.Application["globalCache"];
            if (globalCacheTable == null)
            {
                globalCacheTable = new Hashtable();
                System.Web.HttpContext.Current.Application.Add("globalCache", globalCacheTable);
            }

            GlobalCache globalCacheEntry = new GlobalCache(obj, 15);

            if (globalCacheTable.Contains(className + "_" + key))
            {
                globalCacheTable[className + "_" + key] = globalCacheEntry;
            }
            else
            {
                globalCacheTable.Add(className + "_" + key, globalCacheEntry);
            }

            System.Web.HttpContext.Current.Application["globalCache"] = globalCacheTable;

        }
    }



}
