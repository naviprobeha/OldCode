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
    public class ProductCollection : CollectionBase
    {

        /// <summary>
        /// Returnerar indexerat objekt.
        /// </summary>
        /// <param name="index">Anger vilken position i listan som skall returneras.</param>
        /// <returns>Returnerar ett OrderItem-objekt.</returns>
        public Product this[int index]
        {
            get { return (Product)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Lägger till ett OrderItem-objekt i listan.
        /// </summary>
        /// <param name="orderItem">Ett OrderItem-objekt.</param>
        /// <returns>Returnerar positionen i listan.</returns>
        public int Add(Product product)
        {
            return (List.Add(product));
        }

        /// <summary>
        /// Returnerar positionen i listan för ett givet OrderItem-objekt.
        /// </summary>
        /// <param name="orderItem">Ett OrderItem-objekt.</param>
        /// <returns></returns>
        public int IndexOf(Product product)
        {
            return (List.IndexOf(product));
        }

        /// <summary>
        /// Lägger in ett OrderItem-objekt på en given position i listan.
        /// </summary>
        /// <param name="index">Position.</param>
        /// <param name="orderItem">OrderItem-objekt att lägga till i listan.</param>
        public void Insert(int index, Product product)
        {
            List.Insert(index, product);
        }

        /// <summary>
        /// Tar bort ett OrderItem-objekt ur listan.
        /// </summary>
        /// <param name="orderItem">OrderItem-objekt att radera.</param>
        public void Remove(Product product)
        {
            List.Remove(product);
        }

        /// <summary>
        /// Kontrollerar om listan innehåller ett givet OrderItem-objekt.
        /// </summary>
        /// <param name="orderItem">OrderItem-objekt att lesa efter.</param>
        /// <returns>Returnerar true eller false beroende på om objektet finns i listan eller inte.</returns>
        public bool Contains(Product product)
        {
            return (List.Contains(product));
        }


    }
}
