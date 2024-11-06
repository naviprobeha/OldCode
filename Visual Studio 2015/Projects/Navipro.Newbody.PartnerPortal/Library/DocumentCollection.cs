using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.PartnerPortal.Library
{
    /// <summary>
    /// En lista av ett antal OrderItem-objekt.
    /// </summary>
    public class DocumentCollection : CollectionBase
    {

        /// <summary>
        /// Returnerar indexerat objekt.
        /// </summary>
        /// <param name="index">Anger vilken position i listan som skall returneras.</param>
        /// <returns>Returnerar ett OrderItem-objekt.</returns>
        public Document this[int index]
        {
            get { return (Document)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Lägger till ett OrderItem-objekt i listan.
        /// </summary>
        /// <param name="orderItem">Ett OrderItem-objekt.</param>
        /// <returns>Returnerar positionen i listan.</returns>
        public int Add(Document document)
        {
            return (List.Add(document));
        }

        /// <summary>
        /// Returnerar positionen i listan för ett givet OrderItem-objekt.
        /// </summary>
        /// <param name="orderItem">Ett OrderItem-objekt.</param>
        /// <returns></returns>
        public int IndexOf(Document document)
        {
            return (List.IndexOf(document));
        }

        /// <summary>
        /// Lägger in ett OrderItem-objekt på en given position i listan.
        /// </summary>
        /// <param name="index">Position.</param>
        /// <param name="orderItem">OrderItem-objekt att lägga till i listan.</param>
        public void Insert(int index, Document document)
        {
            List.Insert(index, document);
        }

        /// <summary>
        /// Tar bort ett OrderItem-objekt ur listan.
        /// </summary>
        /// <param name="orderItem">OrderItem-objekt att radera.</param>
        public void Remove(Document document)
        {
            List.Remove(document);
        }

        /// <summary>
        /// Kontrollerar om listan innehåller ett givet OrderItem-objekt.
        /// </summary>
        /// <param name="orderItem">OrderItem-objekt att lesa efter.</param>
        /// <returns>Returnerar true eller false beroende på om objektet finns i listan eller inte.</returns>
        public bool Contains(Document document)
        {
            return (List.Contains(document));
        }

 
    }
}
