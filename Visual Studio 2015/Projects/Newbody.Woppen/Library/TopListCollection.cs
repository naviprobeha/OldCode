using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.Woppen.Library
{
    /// <summary>
    /// En lista av ett antal TopListItem-objekt.
    /// </summary>
    public class TopListCollection : CollectionBase
    {
        /// <summary>
        /// Returnerar indexerat objekt.
        /// </summary>
        /// <param name="index">Anger vilken position i listan som skall returneras.</param>
        /// <returns>Returnerar ett TopListItem-objekt.</returns>
        public TopListItem this[int index]
        {
            get { return (TopListItem)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Lägger till ett TopListItem-objekt i listan.
        /// </summary>
        /// <param name="topListItem">Ett TopListItem-objekt.</param>
        /// <returns>Returnerar positionen i listan.</returns>
        public int Add(TopListItem topListItem)
        {
            return (List.Add(topListItem));
        }

        /// <summary>
        /// Returnerar positionen i listan för ett givet TopListItem-objekt.
        /// </summary>
        /// <param name="topListItem">Ett TopListItem-objekt.</param>
        /// <returns></returns>
        public int IndexOf(TopListItem topListItem)
        {
            return (List.IndexOf(topListItem));
        }

        /// <summary>
        /// Lägger in ett TopListItem-objekt på en given position i listan.
        /// </summary>
        /// <param name="index">Position.</param>
        /// <param name="topListItem">TopListItem-objekt att lägga till i listan.</param>
        public void Insert(int index, TopListItem topListItem)
        {
            List.Insert(index, topListItem);
        }

        /// <summary>
        /// Tar bort ett TopListItem-objekt ur listan.
        /// </summary>
        /// <param name="topListItem">TopListItem-objekt att radera.</param>
        public void Remove(TopListItem topListItem)
        {
            List.Remove(topListItem);
        }

        /// <summary>
        /// Kontrollerar om listan innehåller ett givet TopListItem-objekt.
        /// </summary>
        /// <param name="topListItem">TopListItem-objekt att lesa efter.</param>
        /// <returns>Returnerar true eller false beroende på om objektet finns i listan eller inte.</returns>
        public bool Contains(TopListItem topListItem)
        {
            return (List.Contains(topListItem));
        }

    }
}
