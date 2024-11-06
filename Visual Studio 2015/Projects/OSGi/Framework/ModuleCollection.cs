using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.OSGi.Framework
{

    public class ModuleCollection : CollectionBase
    {
        public Module this[int index]
        {
            get { return (Module)List[index]; }
            set { List[index] = value; }
        }
        public int Add(Module module)
        {
            return (List.Add(module));
        }
        public int IndexOf(Module module)
        {
            return (List.IndexOf(module));
        }
        public void Insert(int index, Module module)
        {
            List.Insert(index, module);
        }
        public void Remove(Module module)
        {
            List.Remove(module);
        }
        public bool Contains(Module module)
        {
            return (List.Contains(module));
        }

        public Module getByName(string name)
        {
            int i = 0;
            while (i < Count)
            {
                if (this[i].name == name) return this[i];
                i++;
            }

            return null;
        }

        public void updateModuleByName(string name, Module module)
        {
            int i = 0;
            while (i < Count)
            {
                if (this[i].name == name)
                {
                    this[i] = module;
                    return;
                }
                i++;
            }

        }

    }
}
