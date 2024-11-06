using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Navipro.OSGi.Interfaces;
using System.Threading;

namespace Navipro.OSGi.Framework
{
    public class ModuleHandler
    {
        private FrameworkFactory _loader;
        private ModuleCollection _moduleCollection;
        private bool _running;

        public ModuleHandler(FrameworkFactory loader)
        {
            _loader = loader;
            _moduleCollection = new ModuleCollection();

            _running = true;

        }


        public void stop()
        {
            _running = false;
            stopAllModules();
        }

        public void loadModule(string moduleName, string assemblyPath, string className)
        {
            if (_moduleCollection.getByName(moduleName) != null)
            {
                throw new Exception("Module " + moduleName + " already loaded.");
            }

            Module module = new Module(_loader, moduleName, assemblyPath, className);
            module.load();

           
            _moduleCollection.Add(module);

        }

        public void startModule(string moduleName)
        {
            Module module = _moduleCollection.getByName(moduleName);
            if (module == null) throw new Exception("Module "+moduleName+" not loaded.");

            module.osgiModule.start();
        }

        public void stopModule(string moduleName)
        {
            Module module = _moduleCollection.getByName(moduleName);
            if (module == null) throw new Exception("Module " + moduleName + " not loaded.");
            if (module.osgiModule != null) module.osgiModule.stop();
        }

        public void unloadModule(string moduleName)
        {
            Module module = _moduleCollection.getByName(moduleName);
            if (module == null) throw new Exception("Module " + moduleName + " not loaded.");

            try
            {
                if (module.osgiModule != null) module.osgiModule.stop();
                module.unload();
            }
            catch (Exception e)
            {
                _moduleCollection.Remove(module);
                throw new Exception(e.Message);
            }
        }

        public void reloadModule(string moduleName)
        {
            Module module = _moduleCollection.getByName(moduleName);
            if (module == null) throw new Exception("Module " + moduleName + " not loaded.");

            if (module.osgiModule != null) module.osgiModule.stop();
            module.load();


        }

        private void stopAllModules()
        {
            int i = 0;
            while (i < _moduleCollection.Count)
            {
                stopModule(_moduleCollection[i].name);
                unloadModule(_moduleCollection[i].name);

                i++;
            }
        }

        private void updateStatusForAllModules()
        {
            int i = 0;
            while (i < _moduleCollection.Count)
            {
                _moduleCollection[i].updateStatus();
                i++;
            }

        }

        public ModuleCollection getModuleCollection()
        {
            return _moduleCollection;
        }
    }
}
