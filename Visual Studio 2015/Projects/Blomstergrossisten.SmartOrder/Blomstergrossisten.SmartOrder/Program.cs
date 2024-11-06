using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SmartOrder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            SmartDatabase smartDatabase = new SmartDatabase("SmartOrder.sdf");
            if (!smartDatabase.init()) smartDatabase.createDatabase();

            Application.Run(new StartForm(smartDatabase));
        }
    }
}