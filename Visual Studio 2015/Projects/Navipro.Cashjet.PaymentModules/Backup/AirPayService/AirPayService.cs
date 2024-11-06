using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Navipro.Cashjet.PaymentModules.AirPay
{
    public partial class AirPayService : ServiceBase, Navipro.Cashjet.PaymentModules.AirPay.Logger
    {
        private Navipro.Cashjet.PaymentModules.AirPay.Server server;

        public AirPayService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            server = new Server(this);
            server.start();
        }

        protected override void OnStop()
        {
            server.stop();
        }

        #region Logger Members

        void Logger.log(string message)
        {
            string path = @"c:\CardPayment\AirPay.log";
            
            
            if (!System.IO.File.Exists(path))
            {
                // Create a file to write to.
                using (System.IO.StreamWriter sw = System.IO.File.CreateText(path))
                {
                    sw.WriteLine("File created: "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+" - " + message);
            }	

        }

        #endregion
    }
}
