using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{

    public class ShipmentLineCollection : CollectionBase
    {
        public ShipmentLine this[int index]
        {
            get { return (ShipmentLine)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ShipmentLine shipmentLine)
        {
            return (List.Add(shipmentLine));
        }
        public int IndexOf(ShipmentLine shipmentLine)
        {
            return (List.IndexOf(shipmentLine));
        }
        public void Insert(int index, ShipmentLine shipmentLine)
        {
            List.Insert(index, shipmentLine);
        }
        public void Remove(ShipmentLine shipmentLine)
        {
            List.Remove(shipmentLine);
        }
        public bool Contains(ShipmentLine shipmentLine)
        {
            return (List.Contains(shipmentLine));
        }


    }
}
