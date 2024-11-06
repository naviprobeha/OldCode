using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Newbody.PartnerPortal.Library
{
    /// <summary>
    /// En lista av ett antal SalesIDSalesPerson-objekt.
    /// </summary>
    public class SalesIDAgreementCollection : CollectionBase
    {
        /// <summary>
        /// Returnerar indexerat objekt.
        /// </summary>
        /// <param name="index">Anger vilken position i listan som skall returneras.</param>
        /// <returns>Returnerar ett SalesIDAgreementLine-objekt.</returns>
        public SalesIDAgreementLine this[int index]
        {
            get { return (SalesIDAgreementLine)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Lägger till ett SalesIDAgreementLine-objekt i listan.
        /// </summary>
        /// <param name="salesIDAgreementLine">Ett SalesIDAgreementLine-objekt.</param>
        /// <returns>Returnerar positionen i listan.</returns>
        public int Add(SalesIDAgreementLine salesIDAgreementLine)
        {
            return (List.Add(salesIDAgreementLine));
        }

        /// <summary>
        /// Returnerar positionen i listan för ett givet SalesIDAgreementLine-objekt.
        /// </summary>
        /// <param name="salesIDAgreementLine">Ett SalesIDAgreementLine-objekt.</param>
        /// <returns></returns>
        public int IndexOf(SalesIDAgreementLine salesIDAgreementLine)
        {
            return (List.IndexOf(salesIDAgreementLine));
        }

        /// <summary>
        /// Lägger in ett SalesIDAgreementLine-objekt på en given position i listan.
        /// </summary>
        /// <param name="index">Position.</param>
        /// <param name="salesIDAgreementLine">SalesIDAgreementLine-objekt att lägga till i listan.</param>
        public void Insert(int index, SalesIDAgreementLine salesIDAgreementLine)
        {
            List.Insert(index, salesIDAgreementLine);
        }

        /// <summary>
        /// Tar bort ett SalesIDAgreementLine-objekt ur listan.
        /// </summary>
        /// <param name="salesIDAgreementLine">SalesIDAgreementLine-objekt att radera.</param>
        public void Remove(SalesIDAgreementLine salesIDAgreementLine)
        {
            List.Remove(salesIDAgreementLine);
        }

        /// <summary>
        /// Kontrollerar om listan innehåller ett givet SalesIDAgreementLine-objekt.
        /// </summary>
        /// <param name="salesIDAgreementLine">SalesIDAgreementLine-objekt att lesa efter.</param>
        /// <returns>Returnerar true eller false beroende på om objektet finns i listan eller inte.</returns>
        public bool Contains(SalesIDAgreementLine salesIDAgreementLine)
        {
            return (List.Contains(salesIDAgreementLine));
        }



     }
}
