using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Newbody.PartnerPortal.Library
{
    /// <summary>
    /// En lista av ett antal SalesIDSalesPerson-objekt.
    /// </summary>
    public class SalesIDSalesPersonCollection : CollectionBase
    {
        /// <summary>
        /// Returnerar indexerat objekt.
        /// </summary>
        /// <param name="index">Anger vilken position i listan som skall returneras.</param>
        /// <returns>Returnerar ett SalesIDSalesPerson-objekt.</returns>
        public SalesIDSalesPerson this[int index]
        {
            get { return (SalesIDSalesPerson)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Lägger till ett SalesIDSalesPerson-objekt i listan.
        /// </summary>
        /// <param name="salesIdSalesPerson">Ett SalesIDSalesPerson-objekt.</param>
        /// <returns>Returnerar positionen i listan.</returns>
        public int Add(SalesIDSalesPerson salesIdSalesPerson)
        {
            return (List.Add(salesIdSalesPerson));
        }

        /// <summary>
        /// Returnerar positionen i listan för ett givet SalesIDSalesPerson-objekt.
        /// </summary>
        /// <param name="salesIdSalesPerson">Ett SalesIDSalesPerson-objekt.</param>
        /// <returns></returns>
        public int IndexOf(SalesIDSalesPerson salesIdSalesPerson)
        {
            return (List.IndexOf(salesIdSalesPerson));
        }

        /// <summary>
        /// Lägger in ett SalesIDSalesPerson-objekt på en given position i listan.
        /// </summary>
        /// <param name="index">Position.</param>
        /// <param name="salesIdSalesPerson">SalesIDSalesPerson-objekt att lägga till i listan.</param>
        public void Insert(int index, SalesIDSalesPerson salesIdSalesPerson)
        {
            List.Insert(index, salesIdSalesPerson);
        }

        /// <summary>
        /// Tar bort ett SalesIDSalesPerson-objekt ur listan.
        /// </summary>
        /// <param name="salesIdSalesPerson">SalesIDSalesPerson-objekt att radera.</param>
        public void Remove(SalesIDSalesPerson salesIdSalesPerson)
        {
            List.Remove(salesIdSalesPerson);
        }

        /// <summary>
        /// Kontrollerar om listan innehåller ett givet SalesIDSalesPerson-objekt.
        /// </summary>
        /// <param name="salesIdSalesPerson">SalesIDSalesPerson-objekt att lesa efter.</param>
        /// <returns>Returnerar true eller false beroende på om objektet finns i listan eller inte.</returns>
        public bool Contains(SalesIDSalesPerson salesIdSalesPerson)
        {
            return (List.Contains(salesIdSalesPerson));
        }



        public void applySalesPersonHistory(Hashtable userHistoryPackageTable)
        {
            int i = 0;
            while (i < this.Count)
            {
                if (userHistoryPackageTable[this[i].salesId + "_" + this[i].webUserAccountNo] != null)
                {
                    //this[i].historyPackages = (int)userHistoryPackageTable[this[i].salesId + "_" + this[i].webUserAccountNo];
                }

                i++;
            }

        }
    }
}
