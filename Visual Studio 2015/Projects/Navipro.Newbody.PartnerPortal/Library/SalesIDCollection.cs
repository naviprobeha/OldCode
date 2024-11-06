using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Newbody.PartnerPortal.Library
{
    /// <summary>
    /// En lista av ett antal SalesIDSalesPerson-objekt.
    /// </summary>
    public class SalesIDCollection : CollectionBase
    {
        /// <summary>
        /// Returnerar indexerat objekt.
        /// </summary>
        /// <param name="index">Anger vilken position i listan som skall returneras.</param>
        /// <returns>Returnerar ett SalesID-objekt.</returns>
        public SalesID this[int index]
        {
            get { return (SalesID)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Lägger till ett SalesID-objekt i listan.
        /// </summary>
        /// <param name="salesID">Ett SalesID-objekt.</param>
        /// <returns>Returnerar positionen i listan.</returns>
        public int Add(SalesID salesID)
        {
            return (List.Add(salesID));
        }

        /// <summary>
        /// Returnerar positionen i listan för ett givet SalesID-objekt.
        /// </summary>
        /// <param name="salesID">Ett SalesID-objekt.</param>
        /// <returns></returns>
        public int IndexOf(SalesID salesID)
        {
            return (List.IndexOf(salesID));
        }

        /// <summary>
        /// Lägger in ett SalesID-objekt på en given position i listan.
        /// </summary>
        /// <param name="index">Position.</param>
        /// <param name="salesID">SalesID-objekt att lägga till i listan.</param>
        public void Insert(int index, SalesID salesID)
        {
            List.Insert(index, salesID);
        }

        /// <summary>
        /// Tar bort ett SalesID-objekt ur listan.
        /// </summary>
        /// <param name="salesID">SalesID-objekt att radera.</param>
        public void Remove(SalesID salesID)
        {
            List.Remove(salesID);
        }

        /// <summary>
        /// Kontrollerar om listan innehåller ett givet SalesID-objekt.
        /// </summary>
        /// <param name="salesID">SalesID-objekt att lesa efter.</param>
        /// <returns>Returnerar true eller false beroende på om objektet finns i listan eller inte.</returns>
        public bool Contains(SalesID salesID)
        {
            return (List.Contains(salesID));
        }



    }
}
