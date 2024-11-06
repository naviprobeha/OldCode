using Navipro.SantaMonica.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.Goldfinger
{

    public class SynchEntryCollection : CollectionBase
    {
        public SynchEntryCollection()
        {
        }


        public SynchEntry this[int index]
        {
            get { return (SynchEntry)List[index]; }
            set { List[index] = value; }
        }
        public int Add(SynchEntry synchEntry)
        {
            return (List.Add(synchEntry));
        }
        public int IndexOf(SynchEntry synchEntry)
        {
            return (List.IndexOf(synchEntry));
        }
        public void Insert(int index, SynchEntry synchEntry)
        {
            List.Insert(index, synchEntry);
        }
        public void Remove(SynchEntry synchEntry)
        {
            List.Remove(synchEntry);
        }
        public bool Contains(SynchEntry synchEntry)
        {
            return (List.Contains(synchEntry));
        }

        public static SynchEntryCollection fromJson(string jsonContent, Agent agent)
        {
            SynchEntryCollection col = new SynchEntryCollection();

            JArray jsonArray = JArray.Parse(jsonContent);
            foreach(JObject jsonObject in jsonArray)
            {
                SynchEntry syncEntry = SynchEntry.fromJsonObject(jsonObject, agent);
                col.Add(syncEntry);

            }

            return col;
        }
    }
}
