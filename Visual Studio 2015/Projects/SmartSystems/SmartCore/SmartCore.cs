using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Navipro.SmartSystems.SmartLibrary;

namespace Navipro.SmartSystems.SmartCore
{
    public class SmartCore : ICore
    {
        private Configuration configuration;

        #region ICore Members

        public void init(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public void start()
        {
            StartForm startForm = new StartForm();
            startForm.ShowDialog();
            startForm.Dispose();
        }

        #endregion
    }
}
