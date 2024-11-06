using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{

    public class LineOrderCollection : CollectionBase
    {
        public LineOrder this[int index]
        {
            get { return (LineOrder)List[index]; }
            set { List[index] = value; }
        }
        public int Add(LineOrder lineOrder)
        {
            return (List.Add(lineOrder));
        }
        public int IndexOf(LineOrder lineOrder)
        {
            return (List.IndexOf(lineOrder));
        }
        public void Insert(int index, LineOrder lineOrder)
        {
            List.Insert(index, lineOrder);
        }
        public void Remove(LineOrder lineOrder)
        {
            List.Remove(lineOrder);
        }
        public bool Contains(LineOrder lineOrder)
        {
            return (List.Contains(lineOrder));
        }

        public static LineOrderCollection fromJson(string jsonContent)
        {
            LineOrderCollection col = new LineOrderCollection();

            JArray jsonArray = JArray.Parse(jsonContent);
            foreach (JObject jsonObject in jsonArray)
            {
                LineOrder syncEntry = LineOrder.fromJsonObject(jsonObject);
                col.Add(syncEntry);

            }

            return col;
        }
    }
}
