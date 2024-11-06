using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Newbody.PartnerPortal.Library
{
    /// <summary>
    /// Klass som illustrerar en post i 10-i-topp-listan över säljare och deras sålda paket.
    /// </summary>
    public class TopListItem
    {
        private int _rank;
        private string _name;
        private int _soldPackages;

        /// <summary>
        /// Blank konstruktor.
        /// </summary>
        public TopListItem()
        {

        }

        /// <summary>
        /// Indikerar position/rankning i listan.
        /// </summary>
        public int rank { get { return _rank; } set { _rank = value; } }

        /// <summary>
        /// Namn på säljaren.
        /// </summary>
        public string name { get { return _name; } set { _name = value; } }

        /// <summary>
        /// Antal sålda paket.
        /// </summary>
        public int soldPackages { get { return _soldPackages; } set { _soldPackages = value; } }

    }
}
