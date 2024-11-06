using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace ExcelNavisionWebService
{
    public partial class Blad1
    {
        private void Blad1_Startup(object sender, System.EventArgs e)
        {
        }

        private void Blad1_Shutdown(object sender, System.EventArgs e)
        {
        }

 
        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Blad1_Startup);
            this.Shutdown += new System.EventHandler(Blad1_Shutdown);
        }

        #endregion

    }
}
