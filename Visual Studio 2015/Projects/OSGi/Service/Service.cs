using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Navipro.OSGi.Framework;

namespace Navipro.OSGi.Service
{
    public partial class Service : ServiceBase
    {
        private FrameworkFactory _frameworkFactory;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _frameworkFactory = new FrameworkFactory();            
        }

        protected override void OnStop()
        {
            _frameworkFactory.stop();
        }
    }
}
