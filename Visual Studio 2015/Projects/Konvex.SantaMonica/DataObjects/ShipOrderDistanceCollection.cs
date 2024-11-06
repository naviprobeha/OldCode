using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{

    public class ShipOrderDistanceCollection : CollectionBase
    {
        public ShipOrderDistanceCollection()
        {
        }

        public ShipOrderDistance this[int index]
        {
            get { return (ShipOrderDistance)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ShipOrderDistance shipOrderDistance)
        {
            return (List.Add(shipOrderDistance));
        }
        public int IndexOf(ShipOrderDistance shipOrderDistance)
        {
            return (List.IndexOf(shipOrderDistance));
        }
        public void Insert(int index, ShipOrderDistance shipOrderDistance)
        {
            List.Insert(index, shipOrderDistance);
        }
        public void Remove(ShipOrderDistance shipOrderDistance)
        {
            List.Remove(shipOrderDistance);
        }
        public bool Contains(ShipOrderDistance shipOrderDistance)
        {
            return (List.Contains(shipOrderDistance));
        }


    }
}
