using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.SmartInventory
{

    public class DataShipmentHeaderCollection : CollectionBase
    {
        public DataShipmentHeader this[int index]
        {
            get { return (DataShipmentHeader)List[index]; }
            set { List[index] = value; }
        }
        public int Add(DataShipmentHeader dataShipmentHeader)
        {
            return (List.Add(dataShipmentHeader));
        }
        public int IndexOf(DataShipmentHeader dataShipmentHeader)
        {
            return (List.IndexOf(dataShipmentHeader));
        }
        public void Insert(int index, DataShipmentHeader dataShipmentHeader)
        {
            List.Insert(index, dataShipmentHeader);
        }
        public void Remove(DataShipmentHeader dataShipmentHeader)
        {
            List.Remove(dataShipmentHeader);
        }
        public bool Contains(DataShipmentHeader dataShipmentHeader)
        {
            return (List.Contains(dataShipmentHeader));
        }


    }
}
