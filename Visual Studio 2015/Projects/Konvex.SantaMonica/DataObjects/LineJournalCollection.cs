using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{

    public class LineJournalCollection : CollectionBase
    {
        public LineJournal this[int index]
        {
            get { return (LineJournal)List[index]; }
            set { List[index] = value; }
        }
        public int Add(LineJournal lineJournal)
        {
            return (List.Add(lineJournal));
        }
        public int IndexOf(LineJournal lineJournal)
        {
            return (List.IndexOf(lineJournal));
        }
        public void Insert(int index, LineJournal lineJournal)
        {
            List.Insert(index, lineJournal);
        }
        public void Remove(LineJournal lineJournal)
        {
            List.Remove(lineJournal);
        }
        public bool Contains(LineJournal lineJournal)
        {
            return (List.Contains(lineJournal));
        }

        public static LineJournalCollection fromJson(string jsonContent)
        {
            LineJournalCollection col = new LineJournalCollection();

            JArray jsonArray = JArray.Parse(jsonContent);
            foreach (JObject jsonObject in jsonArray)
            {
                LineJournal syncEntry = LineJournal.fromJsonObject(jsonObject);
                col.Add(syncEntry);

            }

            return col;
        }
    }
}
