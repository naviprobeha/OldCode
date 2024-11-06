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
using Tools = Microsoft.Office.Tools.Excel;
using InfojetServiceRequest = ExcelNavisionWebService.InfojetServiceRequest.InfojetServiceRequest;

namespace ExcelNavisionWebService
{
    public partial class ThisWorkbook
    {
        private string webServiceUrl;
        
        private int customerNoRow;
        private int customerNoCol;

        private int contactUserIdRow;
        private int contactUserIdCol;

        private int contactPasswordRow;
        private int contactPasswordCol;

        private int regUserIdRow;
        private int regUserIdCol;

        private int regPasswordRow;
        private int regPasswordCol;

        private void ThisWorkbook_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisWorkbook_Shutdown(object sender, System.EventArgs e)
        {
        }

        public void DisplayWebServiceResults(string customerNo)
        {

            loadConfig();

            // Call the Web service.
            string xmlIn = "<infojet><serviceRequest><serviceName>createExcelSalesId</serviceName><serviceArgument><customerNo>" + customerNo + "</customerNo></serviceArgument></serviceRequest></infojet>";
            InfojetServiceRequest.InfojetServiceRequest request = new ExcelNavisionWebService.InfojetServiceRequest.InfojetServiceRequest();
            request.Url = webServiceUrl;
            string xmlOut = request.performservice(xmlIn);


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlOut);

            XmlElement docElement = xmlDoc.DocumentElement;
            if (docElement != null)
            {
                XmlElement errorElement = (XmlElement)docElement.SelectSingleNode("serviceResponse/errorMessage");

                if (errorElement != null)
                {
                    System.Windows.Forms.MessageBox.Show(errorElement.FirstChild.Value, "Felmeddelande");
                }
                else
                {
                    XmlElement salesIdElement = (XmlElement)docElement.SelectSingleNode("serviceResponse/salesId");
                    if (salesIdElement != null)
                    {
                        if (((XmlElement)salesIdElement.SelectSingleNode("contactUserId")).FirstChild != null)
                        {
                            ((Excel.Range)((Excel.Worksheet)this.ActiveSheet).Cells[contactUserIdRow, contactUserIdCol]).Value2 = ((XmlElement)salesIdElement.SelectSingleNode("contactUserId")).FirstChild.Value;
                        }
                        if (((XmlElement)salesIdElement.SelectSingleNode("contactPassword")).FirstChild != null)
                        {
                            ((Excel.Range)((Excel.Worksheet)this.ActiveSheet).Cells[contactPasswordRow, contactPasswordCol]).Value2 = ((XmlElement)salesIdElement.SelectSingleNode("contactPassword")).FirstChild.Value;
                        }

                        if (((XmlElement)salesIdElement.SelectSingleNode("regUserId")).FirstChild != null)
                        {
                            ((Excel.Range)((Excel.Worksheet)this.ActiveSheet).Cells[regUserIdRow, regUserIdCol]).Value2 = ((XmlElement)salesIdElement.SelectSingleNode("regUserId")).FirstChild.Value;
                        }
                        if (((XmlElement)salesIdElement.SelectSingleNode("regPassword")).FirstChild != null)
                        {
                            ((Excel.Range)((Excel.Worksheet)this.ActiveSheet).Cells[regPasswordRow, regPasswordCol]).Value2 = ((XmlElement)salesIdElement.SelectSingleNode("regPassword")).FirstChild.Value;
                        }

                        if (((XmlElement)salesIdElement.SelectSingleNode("customerNo")).FirstChild != null)
                        {
                            ((Excel.Range)((Excel.Worksheet)this.ActiveSheet).Cells[customerNoRow, customerNoCol]).Value2 = ((XmlElement)salesIdElement.SelectSingleNode("customerNo")).FirstChild.Value;
                        }
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Anslutning till Navision kunde inte upprättas.", "Felmeddelande");
            }
        }


        private void loadConfig()
        {
            try
            {
                XmlDocument configDoc = new XmlDocument();
                configDoc.Load("C:\\Windows\\navExcel.xml");

                XmlElement docElement = configDoc.DocumentElement;

                if (docElement != null)
                {
                    XmlElement webServiceUrl = (XmlElement)docElement.SelectSingleNode("webServiceUrl");
                    if (webServiceUrl != null)
                    {
                        this.webServiceUrl = webServiceUrl.FirstChild.Value;
                    }

                    XmlElement contactUserId = (XmlElement)docElement.SelectSingleNode("contactUserId");
                    if (contactUserId != null)
                    {
                        this.contactUserIdRow = int.Parse(contactUserId.GetAttribute("Row"));
                        this.contactUserIdCol = int.Parse(contactUserId.GetAttribute("Col"));
                    }

                    XmlElement contactPassword = (XmlElement)docElement.SelectSingleNode("contactPassword");
                    if (contactPassword != null)
                    {
                        this.contactPasswordRow = int.Parse(contactPassword.GetAttribute("Row"));
                        this.contactPasswordCol = int.Parse(contactPassword.GetAttribute("Col"));
                    }

                    XmlElement regUserId = (XmlElement)docElement.SelectSingleNode("regUserId");
                    if (regUserId != null)
                    {
                        this.regUserIdRow = int.Parse(regUserId.GetAttribute("Row"));
                        this.regUserIdCol = int.Parse(regUserId.GetAttribute("Col"));
                    }

                    XmlElement regPassword = (XmlElement)docElement.SelectSingleNode("regPassword");
                    if (regPassword != null)
                    {
                        this.regPasswordRow = int.Parse(regPassword.GetAttribute("Row"));
                        this.regPasswordCol = int.Parse(regPassword.GetAttribute("Col"));
                    }

                    XmlElement customerNo = (XmlElement)docElement.SelectSingleNode("customerNo");
                    if (customerNo != null)
                    {
                        this.customerNoRow = int.Parse(customerNo.GetAttribute("Row"));
                        this.customerNoCol = int.Parse(customerNo.GetAttribute("Col"));
                    }


                }

            }
            catch (Exception)
            { }
        }


        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
        }

        #endregion

    }
}
