using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.Goldfinger
{

    public class ShipmentLineIDCollection : CollectionBase
    {
        public ShipmentLineID this[int index]
        {
            get { return (ShipmentLineID)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ShipmentLineID shipmentLineId)
        {
            return (List.Add(shipmentLineId));
        }
        public int IndexOf(ShipmentLineID shipmentLineId)
        {
            return (List.IndexOf(shipmentLineId));
        }
        public void Insert(int index, ShipmentLineID shipmentLineId)
        {
            List.Insert(index, shipmentLineId);
        }
        public void Remove(ShipmentLineID shipmentLineId)
        {
            List.Remove(shipmentLineId);
        }
        public bool Contains(ShipmentLineID shipmentLineId)
        {
            return (List.Contains(shipmentLineId));
        }


    }
}
