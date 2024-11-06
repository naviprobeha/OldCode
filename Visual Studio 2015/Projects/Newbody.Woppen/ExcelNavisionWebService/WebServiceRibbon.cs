using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace ExcelNavisionWebService
{
    public partial class WebServiceRibbon : OfficeRibbon
    {
        public WebServiceRibbon()
        {
            InitializeComponent();
        }

        private void WebServiceRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisWorkbook.DisplayWebServiceResults(this.customerNoBox.Text);
        }
    }
}
