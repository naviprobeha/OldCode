using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Navipro.OSGi.Interfaces;
using System.Runtime.Remoting.Lifetime;
using System.Security.Permissions;

namespace Navipro.OSGi.Framework
{
    public class Module : MarshalByRefObject, OSGiLogger
    {
        private string _name;
        private string _assemblyName;
        private string _className;
        private OSGiModule _osgiModule;
        private AppDomain _appDomain;
        private OSGiFramework _framework;
        private string _status;
        private DateTime _heartBeat;

        public Module(OSGiFramework framework, string name, string assemblyName, string className)
        {
            _framework = framework;
            _name = name;
            _assemblyName = assemblyName;
            _className = className;
            
        }

        public string name { get { return _name; } set { _name = value; } }
        public string assemblyName { get { return _assemblyName; } set { _assemblyName = value; } }
        public string className { get { return _className; } set { _className = value; } }
        public OSGiModule osgiModule { get { return _osgiModule; } set { _osgiModule = value; } }
        public AppDomain appDomain { get { return _appDomain; } set { _appDomain = value; } }
        public DateTime heartBeat { get { return _heartBeat; } }

        public void load()
        {
            //Assembly moduleAssembly = System.Reflection.Assembly.LoadFile(assemblyPath);
            //OSGiModule moduleInstance = (OSGiModule)moduleAssembly.CreateInstance(className);

            AppDomainSetup appDomainSetup = new AppDomainSetup();
            appDomainSetup.ApplicationBase = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "modules");
            
            System.Security.PermissionSet trustPermissionSet = new System.Security.PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);
            trustPermissionSet.AddPermission(new System.Security.Permissions.SecurityPermission(PermissionState.Unrestricted));

            _appDomain = AppDomain.CreateDomain(_name, new System.Security.Policy.Evidence(), appDomainSetup, trustPermissionSet);
            //System.IO.StreamReader reader = new System.IO.StreamReader(_assemblyName, System.Text.Encoding.GetEncoding(1252), false);
            //byte[] byteArray = new byte[reader.BaseStream.Length];
            //reader.BaseStream.Read(byteArray, 0, System.Convert.ToInt32(reader.BaseStream.Length));

            //_appDomain.Load(byteArray);

            /*
            Assembly[] assemblies = _appDomain.GetAssemblies();
            Assembly assembly = null;

            Console.WriteLine("Listing assemblies....");
            int i = 0;
            while (i < assemblies.Length)
            {
                if ((assemblies[i].GetName().Name + ".dll") == System.IO.Path.GetFileName(_assemblyName))
                {
                    assembly = assemblies[i];
                    Console.WriteLine("Found assembly: "+assembly.FullName);
                }
                i++;
            }

            if (assembly == null) throw new Exception("Assembly not found...");

            Console.WriteLine("Creating class.");

            _osgiModule = (OSGiModule)assembly.CreateInstance(_className);
            Console.WriteLine("Class created.");

            _osgiModule.load(_framework.getConfiguration(), _framework, this);
            */

            
            
            _osgiModule = (OSGiModule)appDomain.CreateInstanceAndUnwrap(_assemblyName, _className);
            _osgiModule.load(_framework.getConfiguration(), _framework, this);

            //System.Runtime.Remoting.Lifetime.ClientSponsor cs = new System.Runtime.Remoting.Lifetime.ClientSponsor();
            //cs.Register((MarshalByRefObject)_osgiModule);

            ISponsor sponsor = new ModuleSponsor();
            ILease lease = (ILease)System.Runtime.Remoting.RemotingServices.GetLifetimeService((MarshalByRefObject)_osgiModule);
            lease.Register(sponsor);

            
        }

        public void unload()
        {
            AppDomain.Unload(_appDomain);
            _appDomain = null;
            osgiModule = null;

        }

        public string getStatus()
        {
            if (osgiModule == null) return "Unloaded";
            return osgiModule.getStatus();
        }

        public void updateStatus()
        {
            if (osgiModule != null) _status = osgiModule.getStatus();
        }

 
        #region OSGiLogger Members

        public void write(int level, string message)
        {
            _framework.writeToLog(this._name, level, message);
        }


        public void updateHeartBeat()
        {
            this._heartBeat = DateTime.Now;
        }

        #endregion
    }
}
