using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace ExcelNavisionWebService
{
    public partial class Blad2
    {
        private void Blad2_Startup(object sender, System.EventArgs e)
        {
        }

        private void Blad2_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Blad2_Startup);
            this.Shutdown += new System.EventHandler(Blad2_Shutdown);
        }

        #endregion

    }
}
