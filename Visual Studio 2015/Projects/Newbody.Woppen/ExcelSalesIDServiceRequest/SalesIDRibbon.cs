using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelSalesIDServiceRequest
{
    public partial class SalesIDRibbon : OfficeRibbon
    {
        public SalesIDRibbon()
        {
            InitializeComponent();
        }

        private void SalesIDRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.DisplayWebServiceResults(customerNoBox.Text, descriptionBox.Text);
            customerNoBox.Text = "";
            descriptionBox.Text = "";
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            customerNoBox.Text = Globals.ThisAddIn.GetCustomerNo();
        }
    }
}
