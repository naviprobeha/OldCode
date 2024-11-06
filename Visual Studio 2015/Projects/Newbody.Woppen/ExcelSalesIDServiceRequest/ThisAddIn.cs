using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace ExcelSalesIDServiceRequest
{
    public partial class ThisAddIn
    {
        private string webServiceUrl;

        public int customerNoSheet;
        public int customerNoRow;
        public int customerNoCol;

        private int contactUserIdSheet;
        private int contactUserIdRow;
        private int contactUserIdCol;

        private int contactPasswordSheet;
        private int contactPasswordRow;
        private int contactPasswordCol;

        private int regUserIdSheet;
        private int regUserIdRow;
        private int regUserIdCol;

        private int regPasswordSheet;
        private int regPasswordRow;
        private int regPasswordCol;

        private int groupNameSheet;
        private int groupNameRow;
        private int groupNameCol;


        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        public void DisplayWebServiceResults(string customerNo, string description)
        {

            loadConfig();

            // Call the Web service.
            string xmlIn = "<infojet><serviceRequest><serviceName>createExcelSalesId</serviceName><serviceArgument><customerNo>" + customerNo + "</customerNo><description>" + description + "</description></serviceArgument></serviceRequest></infojet>";
            InfojetServiceRequest.InfojetServiceRequest request = new ExcelSalesIDServiceRequest.InfojetServiceRequest.InfojetServiceRequest();
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
                            ((Excel.Range)((Excel.Worksheet)this.Application.Sheets[this.contactUserIdSheet]).Cells[contactUserIdRow, contactUserIdCol]).Value2 = ((XmlElement)salesIdElement.SelectSingleNode("contactUserId")).FirstChild.Value;
                        }
                        if (((XmlElement)salesIdElement.SelectSingleNode("contactPassword")).FirstChild != null)
                        {
                            ((Excel.Range)((Excel.Worksheet)this.Application.Sheets[this.contactPasswordSheet]).Cells[contactPasswordRow, contactPasswordCol]).Value2 = ((XmlElement)salesIdElement.SelectSingleNode("contactPassword")).FirstChild.Value;
                        }

                        if (((XmlElement)salesIdElement.SelectSingleNode("regUserId")).FirstChild != null)
                        {
                            ((Excel.Range)((Excel.Worksheet)this.Application.Sheets[this.regUserIdSheet]).Cells[regUserIdRow, regUserIdCol]).Value2 = ((XmlElement)salesIdElement.SelectSingleNode("regUserId")).FirstChild.Value;
                        }
                        if (((XmlElement)salesIdElement.SelectSingleNode("regPassword")).FirstChild != null)
                        {
                            ((Excel.Range)((Excel.Worksheet)this.Application.Sheets[this.regPasswordSheet]).Cells[regPasswordRow, regPasswordCol]).Value2 = ((XmlElement)salesIdElement.SelectSingleNode("regPassword")).FirstChild.Value;
                        }

                        if (((XmlElement)salesIdElement.SelectSingleNode("customerNo")).FirstChild != null)
                        {                            
                            ((Excel.Range)((Excel.Worksheet)this.Application.Sheets[this.customerNoSheet]).Cells[customerNoRow, customerNoCol]).Value2 = ((XmlElement)salesIdElement.SelectSingleNode("customerNo")).FirstChild.Value;
                        }

                        if (description != "")
                        {
                            ((Excel.Range)((Excel.Worksheet)this.Application.Sheets[this.groupNameSheet]).Cells[groupNameRow, groupNameCol]).Value2 = description;
                        } 
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Anslutning till Navision kunde inte upprättas.", "Felmeddelande");
            }
        }

        public string GetCustomerNo()
        {
            string customerNo = "";

            try
            {
                loadConfig();

                customerNo = ((Excel.Range)((Excel.Worksheet)this.Application.Sheets[this.customerNoSheet]).Cells[customerNoRow, customerNoCol]).Value2.ToString();
            }
            catch (Exception) { }

            return customerNo;
        }

        public void loadConfig()
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
                        this.contactUserIdSheet = int.Parse(contactUserId.GetAttribute("Sheet"));
                        this.contactUserIdRow = int.Parse(contactUserId.GetAttribute("Row"));
                        this.contactUserIdCol = int.Parse(contactUserId.GetAttribute("Col"));
                    }

                    XmlElement contactPassword = (XmlElement)docElement.SelectSingleNode("contactPassword");
                    if (contactPassword != null)
                    {
                        this.contactPasswordSheet = int.Parse(contactPassword.GetAttribute("Sheet"));
                        this.contactPasswordRow = int.Parse(contactPassword.GetAttribute("Row"));
                        this.contactPasswordCol = int.Parse(contactPassword.GetAttribute("Col"));
                    }

                    XmlElement regUserId = (XmlElement)docElement.SelectSingleNode("regUserId");
                    if (regUserId != null)
                    {
                        this.regUserIdSheet = int.Parse(regUserId.GetAttribute("Sheet"));
                        this.regUserIdRow = int.Parse(regUserId.GetAttribute("Row"));
                        this.regUserIdCol = int.Parse(regUserId.GetAttribute("Col"));
                    }

                    XmlElement regPassword = (XmlElement)docElement.SelectSingleNode("regPassword");
                    if (regPassword != null)
                    {
                        this.regPasswordSheet = int.Parse(regPassword.GetAttribute("Sheet"));
                        this.regPasswordRow = int.Parse(regPassword.GetAttribute("Row"));
                        this.regPasswordCol = int.Parse(regPassword.GetAttribute("Col"));
                    }

                    XmlElement customerNo = (XmlElement)docElement.SelectSingleNode("customerNo");
                    if (customerNo != null)
                    {
                        this.customerNoSheet = int.Parse(customerNo.GetAttribute("Sheet"));
                        this.customerNoRow = int.Parse(customerNo.GetAttribute("Row"));
                        this.customerNoCol = int.Parse(customerNo.GetAttribute("Col"));
                    }

                    XmlElement groupName = (XmlElement)docElement.SelectSingleNode("groupName");
                    if (groupName != null)
                    {
                        this.groupNameSheet = int.Parse(groupName.GetAttribute("Sheet"));
                        this.groupNameRow = int.Parse(groupName.GetAttribute("Row"));
                        this.groupNameCol = int.Parse(groupName.GetAttribute("Col"));
                    }

                }

            }
            catch (Exception)
            { }
        }


        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
