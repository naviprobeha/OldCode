using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Dynamics.Framework.UI.Extensibility;
using Microsoft.Dynamics.Framework.UI.Extensibility.WinForms;
using System.Xml;
using System.Threading;

namespace Navipro.CashJet.AddIns
{

    [ControlAddInExport("Navipro.CashJet.AddIns.Callback")]
    public class Callback : StringControlAddInBase
    {
        private Timer timer;

        protected override System.Windows.Forms.Control CreateControl()
        {
            timer = new Timer(new TimerCallback(callback));
            return new System.Windows.Forms.Control();
        }

        public override bool AllowCaptionControl
        {
            get
            {
                return false;
            }
        }

        [ApplicationVisible]
        public void ping(int interval)
        {
            timer.Change(interval, System.Threading.Timeout.Infinite);
        }

        private void callback(Object state)
        {
            this.RaiseControlAddInEvent(0, "EVENT FIRED");
        }


    }
}
