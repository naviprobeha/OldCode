using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.OSGi.Framework
{

    public class ConsoleCollection : CollectionBase
    {
        public ConsoleConnection this[int index]
        {
            get { return (ConsoleConnection)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ConsoleConnection consoleConnection)
        {
            return (List.Add(consoleConnection));
        }
        public int IndexOf(ConsoleConnection consoleConnection)
        {
            return (List.IndexOf(consoleConnection));
        }
        public void Insert(int index, ConsoleConnection consoleConnection)
        {
            List.Insert(index, consoleConnection);
        }
        public void Remove(ConsoleConnection consoleConnection)
        {
            List.Remove(consoleConnection);
        }
        public bool Contains(ConsoleConnection consoleConnection)
        {
            return (List.Contains(consoleConnection));
        }

    }
}
