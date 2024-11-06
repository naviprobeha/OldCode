using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using SmyckaReceiptService.Models;

namespace SmyckaReceiptService.Reports
{
    public class ReceiptReport
    {
        private int totalHeight = 0;
        private int logoHeight = 0;
        private int headerHeight = 0;
        private int salesLineHeight = 0;
        private int paymentLineHeight = 0;
        private int totalLineHeight = 0;
        private int returnLineHeight = 0;
        private int vatHeaderHeight = 0;
        private int vatLineHeight = 0;
        private int vatFooterHeight = 0;
        private int receiptTextHeight = 0;
        private int footerHeight = 0;
        private int cardPaymentInformationHeight = 0;
        private string cardPaymentReceiptInfo = "";


        public ReceiptReport()
        {

        }

        public Image createLayout(POSTransactionHeader posTransactionHeader, Store store, string layout)
        {

            if (layout == "") layout = "default";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("C:/temp/PrinterLayouts/ReceiptLayout_" + layout + ".xml");

            XmlElement docElement = xmlDoc.DocumentElement;


            int height = calcHeight(docElement, posTransactionHeader, store);
            int currentPosY = 0;

            Size size = new Size(600, height);

            Bitmap receiptImage = new Bitmap(600, height);

            Graphics context = Graphics.FromImage(receiptImage);
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                context.FillRectangle(brush, 0, 0, 600, height);
            }

            
            if (store != null)
            {
                try
                {
                    if (store.logo.Length > 0)
                    {
                        createLayoutLogo(context, store, currentPosY);
                        currentPosY = currentPosY + logoHeight;
                    }
                    else
                    {
                        currentPosY = currentPosY + 30;
                    }
                }
                catch (Exception)
                { }
            }

            currentPosY = currentPosY + 30;

            createLayoutHeader(context, docElement, "header", posTransactionHeader, store, currentPosY);
            currentPosY = currentPosY + headerHeight;

            createLayoutLines(context, docElement, posTransactionHeader, currentPosY);
            currentPosY = currentPosY + (salesLineHeight + returnLineHeight + paymentLineHeight + totalLineHeight);

            createLayoutHeader(context, docElement, "vatSpecification/vatHeader", posTransactionHeader, store, currentPosY);
            currentPosY = currentPosY + vatHeaderHeight;

            createLayoutVatLines(context, docElement, posTransactionHeader, currentPosY);
            currentPosY = currentPosY + vatLineHeight;

            createLayoutHeader(context, docElement, "vatSpecification/vatFooter", posTransactionHeader, store, currentPosY);
            currentPosY = currentPosY + vatFooterHeight;

            createCardPaymentLines(context, docElement, posTransactionHeader, currentPosY);
            currentPosY = currentPosY + cardPaymentInformationHeight;

            createLayoutReceiptText(context, docElement, posTransactionHeader, store, currentPosY);
            currentPosY = currentPosY + receiptTextHeight;

            createLayoutHeader(context, docElement, "footer", posTransactionHeader, store, currentPosY);
            currentPosY = currentPosY + footerHeight;


            // Create new image

            return receiptImage;
        }

        private void createLayoutLogo(Graphics context, Store store, int currentPosY)
        {



            Image image = byteArrayToImage(store.logo);
            if (image == null) return;

            int x = 10;
            int width = image.Size.Width;
            int height = image.Size.Height;
            int ratio = height / width;

            if (width > 550)
            {
                width = 550;
                height = width * ratio;
            }

            if (width < 550)
            {
                x = (int)((550 - width) / 2);
            }
            context.DrawImage(image, new Rectangle(x, currentPosY + 20, width, height));

        }

        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                return Image.FromStream(ms, true);//Exception occurs here
            }
            catch { }
            return null;
        }

        private void createLayoutHeader(Graphics context, XmlElement docElement, string sectionName, POSTransactionHeader posTransactionHeader, Store store, int currentPosY)
        {
            XmlElement headerElement = (XmlElement)docElement.SelectSingleNode(sectionName);
            XmlNodeList nodeList = headerElement.ChildNodes;


            foreach (XmlNode xmlNode in nodeList)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;

                //text="Bruttobelopp" fontSize="24" xPos="400" yPos="10" textAlign="right"
                int xPos = int.Parse(xmlElement.GetAttribute("xPos"));
                int yPos = int.Parse(xmlElement.GetAttribute("yPos"));
                int width = int.Parse(xmlElement.GetAttribute("width"));
                int height = int.Parse(xmlElement.GetAttribute("height"));
                string labelText = xmlElement.GetAttribute("text");
                string textAlign = xmlElement.GetAttribute("textAlign");
                string bold = xmlElement.GetAttribute("fontBold");
                string name = xmlElement.GetAttribute("name");
                string isHeader = xmlElement.GetAttribute("isHeader");

                int realYPos = currentPosY + yPos;

                if (isHeader.ToUpper() == "TRUE")
                {
                    realYPos = realYPos - 20;

                }
                if (xmlElement.Name == "label")
                {
                    int fontSize = int.Parse(xmlElement.GetAttribute("fontSize"));
                    drawText(context, labelText, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                }
                if (xmlElement.Name == "field")
                {
                    int fontSize = int.Parse(xmlElement.GetAttribute("fontSize"));
                    string text = getHeaderFieldContent(name, posTransactionHeader, store);
                    drawText(context, text, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                }
                if (xmlElement.Name == "line")
                {
                    drawLine(context, xPos, realYPos + 10, width);
                }
            }
        }

        private void createLayoutReceiptText(Graphics context, XmlElement docElement, POSTransactionHeader posTransactionHeader, Store store, int currentPosY)
        {
            XmlElement headerElement = (XmlElement)docElement.SelectSingleNode("receiptTexts");
            if (headerElement == null) return;

            XmlElement xmlElement = (XmlElement)headerElement.SelectSingleNode("field");
            int sectionHeight = int.Parse(headerElement.GetAttribute("height"));

            int xPos = int.Parse(xmlElement.GetAttribute("xPos"));
            int yPos = int.Parse(xmlElement.GetAttribute("yPos"));
            int width = int.Parse(xmlElement.GetAttribute("width"));
            int height = int.Parse(xmlElement.GetAttribute("height"));
            string labelText = xmlElement.GetAttribute("text");
            string textAlign = xmlElement.GetAttribute("textAlign");
            string bold = xmlElement.GetAttribute("fontBold");
            string name = xmlElement.GetAttribute("name");
            int fontSize = int.Parse(xmlElement.GetAttribute("fontSize"));

            currentPosY = currentPosY + 20;


            if (store == null) store = new Store();

            if ((store.receiptTextLine1 != "") && (store.receiptTextLine1 != null))
            {
                //throw new Exception("1: " + posContext.posStore.receiptTextLine1);
                int realYPos = currentPosY + yPos;
                drawText(context, store.receiptTextLine1, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                currentPosY = currentPosY + sectionHeight;
            }
            if ((store.receiptTextLine2 != "") && (store.receiptTextLine2 != null))
            {
                int realYPos = currentPosY + yPos;
                drawText(context, store.receiptTextLine2, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                currentPosY = currentPosY + sectionHeight;
            }
            if ((store.receiptTextLine3 != "") && (store.receiptTextLine3 != null))
            {
                int realYPos = currentPosY + yPos;
                drawText(context, store.receiptTextLine3, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                currentPosY = currentPosY + sectionHeight;
            }
            if ((store.receiptTextLine4 != "") && (store.receiptTextLine4 != null))
            {
                int realYPos = currentPosY + yPos;
                drawText(context, store.receiptTextLine4, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                currentPosY = currentPosY + sectionHeight;
            }
            if ((store.receiptTextLine5 != "") && (store.receiptTextLine5 != null))
            {
                int realYPos = currentPosY + yPos;
                drawText(context, store.receiptTextLine5, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                currentPosY = currentPosY + sectionHeight;
            }
            if ((store.receiptTextLine6 != "") && (store.receiptTextLine6 != null))
            {
                int realYPos = currentPosY + yPos;
                drawText(context, store.receiptTextLine6, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                currentPosY = currentPosY + sectionHeight;
            }
            if ((store.receiptTextLine7 != "") && (store.receiptTextLine7 != null))
            {
                int realYPos = currentPosY + yPos;
                drawText(context, store.receiptTextLine7, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                currentPosY = currentPosY + sectionHeight;
            }
            if ((store.receiptTextLine8 != "") && (store.receiptTextLine8 != null))
            {
                int realYPos = currentPosY + yPos;
                drawText(context, store.receiptTextLine8, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                currentPosY = currentPosY + sectionHeight;
            }
            if ((store.receiptTextLine9 != "") && (store.receiptTextLine9 != null))
            {
                int realYPos = currentPosY + yPos;
                drawText(context, store.receiptTextLine9, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                currentPosY = currentPosY + sectionHeight;
            }
            if ((store.receiptTextLine10 != "") && (store.receiptTextLine10 != null))
            {
                int realYPos = currentPosY + yPos;
                drawText(context, store.receiptTextLine10, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                currentPosY = currentPosY + sectionHeight;
            }


        }

        private void createLayoutLines(Graphics context, XmlElement docElement, POSTransactionHeader posTransactionHeader, int currentPosY)
        {

            List<POSTransactionLine> posTransactionLineList = posTransactionHeader.lines;

            foreach (POSTransactionLine posTransactionLine in posTransactionLineList)
            {
                if (posTransactionLine.salesType == 1) posTransactionLine.salesNo = "";

                XmlElement lineElement = null;
                if (posTransactionLine.lineType == 0) lineElement = (XmlElement)docElement.SelectSingleNode("lines/salesLine");
                if (posTransactionLine.lineType == 1) lineElement = (XmlElement)docElement.SelectSingleNode("lines/paymentLine");
                if (posTransactionLine.lineType == 2) lineElement = (XmlElement)docElement.SelectSingleNode("lines/totalLine");
                if (posTransactionLine.lineType == 4) lineElement = (XmlElement)docElement.SelectSingleNode("lines/paymentLine");
                if (posTransactionLine.lineType == 5) lineElement = (XmlElement)docElement.SelectSingleNode("lines/salesLine");
                if (posTransactionLine.lineType == 6) lineElement = (XmlElement)docElement.SelectSingleNode("lines/expenceLine");
                if (posTransactionLine.returnReasonCode != "") lineElement = (XmlElement)docElement.SelectSingleNode("lines/returnLine");

                if (lineElement != null)
                {

                    int sectionHeight = int.Parse(lineElement.GetAttribute("height"));

                    XmlNodeList nodeList = lineElement.ChildNodes;

                    foreach (XmlNode xmlNode in nodeList)
                    {
                        if (xmlNode.NodeType == XmlNodeType.Element)
                        {
                            XmlElement xmlElement = (XmlElement)xmlNode;

                            //text="Bruttobelopp" fontSize="24" xPos="400" yPos="10" textAlign="right"
                            int xPos = int.Parse(xmlElement.GetAttribute("xPos"));
                            int yPos = int.Parse(xmlElement.GetAttribute("yPos"));
                            int width = int.Parse(xmlElement.GetAttribute("width"));
                            int height = int.Parse(xmlElement.GetAttribute("height"));
                            string labelText = xmlElement.GetAttribute("text");
                            string textAlign = xmlElement.GetAttribute("textAlign");
                            string bold = xmlElement.GetAttribute("fontBold");
                            string name = xmlElement.GetAttribute("name");

                            int realYPos = currentPosY + yPos;

                            if (xmlElement.Name == "label")
                            {
                                int fontSize = int.Parse(xmlElement.GetAttribute("fontSize"));
                                drawText(context, labelText, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                            }
                            if (xmlElement.Name == "field")
                            {
                                int fontSize = int.Parse(xmlElement.GetAttribute("fontSize"));
                                string text = getLineFieldContent(name, posTransactionLine);
                                if (text == null) text = "";
                                drawText(context, text, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                            }
                            if (xmlElement.Name == "line")
                            {
                                drawLine(context, xPos, realYPos - 5, width);
                            }

                        }
                    }

                    currentPosY = currentPosY + sectionHeight;

                }
            }
        }

        private void createLayoutVatLines(Graphics context, XmlElement docElement, POSTransactionHeader posTransactionHeader, int currentPosY)
        {

            Dictionary<string, POSTransactionLine> vatTable = posTransactionHeader.getVatList();

            Dictionary<string, POSTransactionLine>.Enumerator enumerator = vatTable.GetEnumerator();

            while (enumerator.MoveNext())
            {
                XmlElement lineElement = (XmlElement)docElement.SelectSingleNode("vatSpecification/vatLine");

                int sectionHeight = int.Parse(lineElement.GetAttribute("height"));

                XmlNodeList nodeList = lineElement.ChildNodes;

                foreach (XmlNode xmlNode in nodeList)
                {
                    if (xmlNode.NodeType == XmlNodeType.Element)
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode;

                        //text="Bruttobelopp" fontSize="24" xPos="400" yPos="10" textAlign="right"
                        int xPos = int.Parse(xmlElement.GetAttribute("xPos"));
                        int yPos = int.Parse(xmlElement.GetAttribute("yPos"));
                        int width = int.Parse(xmlElement.GetAttribute("width"));
                        int height = int.Parse(xmlElement.GetAttribute("height"));
                        string labelText = xmlElement.GetAttribute("text");
                        string textAlign = xmlElement.GetAttribute("textAlign");
                        string bold = xmlElement.GetAttribute("fontBold");
                        string name = xmlElement.GetAttribute("name");

                        int realYPos = currentPosY + yPos;

                        if (xmlElement.Name == "label")
                        {
                            int fontSize = int.Parse(xmlElement.GetAttribute("fontSize"));
                            drawText(context, labelText, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                        }
                        if (xmlElement.Name == "field")
                        {
                            int fontSize = int.Parse(xmlElement.GetAttribute("fontSize"));
                            string text = "";

                            if (name == "vatProc") text = enumerator.Current.Value.vatProc.ToString();
                            if (name == "netAmount") text = enumerator.Current.Value.amount.ToString("F");
                            if (name == "vatAmount") text = enumerator.Current.Value.vatAmount.ToString("F");
                            if (name == "grossAmount") text = enumerator.Current.Value.amountInclVAT.ToString("F");

                            drawText(context, text, xPos, realYPos, width, height, fontSize, textAlign, (bold == "true"));
                        }
                        if (xmlElement.Name == "line")
                        {
                            drawLine(context, xPos, realYPos - 5, width);
                        }
                    }
                }
                currentPosY = currentPosY + sectionHeight;

            }

        }

        private void createCardPaymentLines(Graphics context, XmlElement docElement, POSTransactionHeader posTransactionHeader, int currentPosY)
        {

            XmlElement xmlElement = (XmlElement)docElement.SelectSingleNode("cardPaymentInformation");
            if (xmlElement == null) return;

            string type = xmlElement.GetAttribute("type");
            int xPos = int.Parse(xmlElement.GetAttribute("xPos"));
            int yPos = int.Parse(xmlElement.GetAttribute("yPos"));
            int width = int.Parse(xmlElement.GetAttribute("width"));
            int height = int.Parse(xmlElement.GetAttribute("height"));
            string textAlign = xmlElement.GetAttribute("textAlign");
            string bold = xmlElement.GetAttribute("fontBold");
            string name = xmlElement.GetAttribute("name");
            int fontSize = int.Parse(xmlElement.GetAttribute("fontSize"));

            if (this.cardPaymentReceiptInfo != "")
            {
                string[] receiptText = cardPaymentReceiptInfo.Split((char)10);

                if (receiptText != null)
                {
                    foreach (string textLine in receiptText)
                    {
                        int realYPos = currentPosY + yPos;
                        drawText(context, textLine, xPos, realYPos + 30, width, height, fontSize, textAlign, (bold == "true"));
                        currentPosY = currentPosY + height;

                    }
                }
                return;
            }

            /*
            List<POSTransactionCardPaymentEntry> cardPaymentLines = tenantDatabase.POSTransactionCardPaymentEntries.Where(t => t.transactionNo == posTransactionHeader.no).ToList();

            foreach (POSTransactionCardPaymentEntry posTransactionCardPaymentEntry in cardPaymentLines)
            {
                if (posTransactionCardPaymentEntry.status == 1)
                {
                    string[] receiptText = null;
                    if (type == "customer")
                    {
                        receiptText = posTransactionCardPaymentEntry.customerReceiptText.Split((char)10);
                    }
                    if (type == "merchant")
                    {
                        receiptText = posTransactionCardPaymentEntry.merchantReceiptText.Split((char)10);
                    }
                    if (receiptText != null)
                    {
                        foreach (string textLine in receiptText)
                        {
                            int realYPos = currentPosY + yPos;
                            drawText(context, textLine, xPos, realYPos + 30, width, height, fontSize, textAlign, (bold == "true"));
                            currentPosY = currentPosY + height;

                        }
                    }
                }

                drawLine(context, xPos, currentPosY, width);

            }
            */

        }


        private int calcHeight(XmlElement docElement, POSTransactionHeader posTransactionHeader, Store store)
        {

            XmlElement element = null;

            element = (XmlElement)docElement.SelectSingleNode("logo");
            if (element != null) logoHeight = int.Parse(element.GetAttribute("height"));

            element = (XmlElement)docElement.SelectSingleNode("header");
            if (element != null) headerHeight = int.Parse(element.GetAttribute("height"));

            int salesLineCount = 0;
            int returnLineCount = 0;
            int paymentLineCount = 0;
            int totalLineCount = 0;
            List<string> vatRateList = new List<string>();

            List<POSTransactionLine> posTransactionLineList = posTransactionHeader.lines;

            //throw new Exception("Lines: " + posTransactionLineList.Count);

            foreach (POSTransactionLine posTransactionLine in posTransactionLineList)
            {
                if (posTransactionLine.lineType == 0)
                {
                    if (posTransactionLine.returnReasonCode != "")
                    {
                        returnLineCount++;
                    }
                    else
                    {
                        salesLineCount++;
                    }
                    if (!vatRateList.Contains(posTransactionLine.vatProc.ToString())) vatRateList.Add(posTransactionLine.vatProc.ToString());
                }
                if (posTransactionLine.lineType == 1) paymentLineCount++;
                if (posTransactionLine.lineType == 2) totalLineCount++;
                if (posTransactionLine.lineType == 4) paymentLineCount++;
                if (posTransactionLine.lineType == 5) salesLineCount++;
                if (posTransactionLine.lineType == 6) salesLineCount++;

            }


            element = (XmlElement)docElement.SelectSingleNode("lines/salesLine");
            if (element != null) salesLineHeight = int.Parse(element.GetAttribute("height")) * salesLineCount;

            element = (XmlElement)docElement.SelectSingleNode("lines/returnLine");
            if (element != null) returnLineHeight = int.Parse(element.GetAttribute("height")) * returnLineCount;

            element = (XmlElement)docElement.SelectSingleNode("lines/totalLine");
            if (element != null) totalLineHeight = int.Parse(element.GetAttribute("height")) * totalLineCount;

            element = (XmlElement)docElement.SelectSingleNode("lines/paymentLine");
            if (element != null) paymentLineHeight = int.Parse(element.GetAttribute("height")) * paymentLineCount;

            element = (XmlElement)docElement.SelectSingleNode("vatSpecification/vatHeader");
            if (element != null) vatHeaderHeight = int.Parse(element.GetAttribute("height"));


            element = (XmlElement)docElement.SelectSingleNode("vatSpecification/vatLine");
            if (element != null) vatLineHeight = int.Parse(element.GetAttribute("height")) * vatRateList.Count;

            element = (XmlElement)docElement.SelectSingleNode("vatSpecification/vatFooter");
            if (element != null) vatFooterHeight = int.Parse(element.GetAttribute("height"));

            cardPaymentInformationHeight = 0;
            XmlElement xmlElement = (XmlElement)docElement.SelectSingleNode("cardPaymentInformation");
            if (xmlElement != null)
            {
                int height = int.Parse(xmlElement.GetAttribute("height"));

                if (cardPaymentReceiptInfo != "")
                {
                    string[] receiptText = cardPaymentReceiptInfo.Split((char)10);

                    foreach (string textLine in receiptText)
                    {
                        cardPaymentInformationHeight = cardPaymentInformationHeight + height;
                    }
                }
                else
                {
                    /*
                    List<POSTransactionCardPaymentEntry> cardPaymentLines = tenantDatabase.POSTransactionCardPaymentEntries.Where(t => t.transactionNo == posTransactionHeader.no).ToList();
                    foreach (POSTransactionCardPaymentEntry posTransactionCardPaymentEntry in cardPaymentLines)
                    {
                        if (posTransactionCardPaymentEntry.status == 1)
                        {
                            string[] receiptText = posTransactionCardPaymentEntry.customerReceiptText.Split((char)10);

                            foreach (string textLine in receiptText)
                            {
                                cardPaymentInformationHeight = cardPaymentInformationHeight + height;
                            }
                        }

                    }
                    */
                }

            }

            
            if (store == null) store = new Store();

            int receiptTextLineHeight = 0;
            element = (XmlElement)docElement.SelectSingleNode("receiptTexts");
            if (element != null) receiptTextLineHeight = int.Parse(element.GetAttribute("height"));
            receiptTextHeight = receiptTextLineHeight * 10;
            if ((store.receiptTextLine10 == "") || (store.receiptTextLine10 == null)) receiptTextHeight = receiptTextHeight - receiptTextLineHeight;
            if ((store.receiptTextLine9 == "") || (store.receiptTextLine9 == null)) receiptTextHeight = receiptTextHeight - receiptTextLineHeight;
            if ((store.receiptTextLine8 == "") || (store.receiptTextLine8 == null)) receiptTextHeight = receiptTextHeight - receiptTextLineHeight;
            if ((store.receiptTextLine7 == "") || (store.receiptTextLine7 == null)) receiptTextHeight = receiptTextHeight - receiptTextLineHeight;
            if ((store.receiptTextLine6 == "") || (store.receiptTextLine6 == null)) receiptTextHeight = receiptTextHeight - receiptTextLineHeight;
            if ((store.receiptTextLine5 == "") || (store.receiptTextLine5 == null)) receiptTextHeight = receiptTextHeight - receiptTextLineHeight;
            if ((store.receiptTextLine4 == "") || (store.receiptTextLine4 == null)) receiptTextHeight = receiptTextHeight - receiptTextLineHeight;
            if ((store.receiptTextLine3 == "") || (store.receiptTextLine3 == null)) receiptTextHeight = receiptTextHeight - receiptTextLineHeight;
            if ((store.receiptTextLine2 == "") || (store.receiptTextLine2 == null)) receiptTextHeight = receiptTextHeight - receiptTextLineHeight;
            if ((store.receiptTextLine1 == "") || (store.receiptTextLine1 == null)) receiptTextHeight = receiptTextHeight - receiptTextLineHeight;


            element = (XmlElement)docElement.SelectSingleNode("footer");
            if (element != null) footerHeight = int.Parse(element.GetAttribute("height"));

            totalHeight = logoHeight + headerHeight + salesLineHeight + returnLineHeight + totalLineHeight + paymentLineHeight + vatHeaderHeight + vatLineHeight + vatFooterHeight + cardPaymentInformationHeight + receiptTextHeight + footerHeight;

            return totalHeight;
        }

        private void drawText(Graphics context, string text, int x, int y, int width, int height, int fontSize, string textAlign, bool bold)
        {
            if (text == ".") text = "";

            StringFormat stringFormat = new StringFormat(StringFormatFlags.LineLimit | StringFormatFlags.NoWrap, 1003);
            stringFormat.Trimming = StringTrimming.None;

            if (textAlign == "right") stringFormat.Alignment = StringAlignment.Far;
            if (textAlign == "center") stringFormat.Alignment = StringAlignment.Center;
            string fontName = "ArialMT";
            if (bold) fontName = "Arial-BoldMT";

            fontSize = fontSize - 8;

            context.DrawString(text, new Font(fontName, fontSize), Brushes.Black, new RectangleF(x, y, width, height), stringFormat);


        }

        private void drawLine(Graphics context, int x, int y, int width)
        {
            context.DrawLine(new Pen(Color.Black, 1), x, y, x + width, y);


        }



        private string getHeaderFieldContent(string name, POSTransactionHeader posTransactionHeader, Store store)
        {
            
            if (store == null) store = new Store();

            if (name == "registeredReceiptNo") return posTransactionHeader.registeredTransactionNo;
            if (name == "salesPersonName")
            {
                return posTransactionHeader.salesPersonName;
            }
            if (name == "registeredDate")
            {
                if (posTransactionHeader.registeredDateTime.Year == 1970) return "";
                return posTransactionHeader.registeredDateTime.ToString("yyyy-MM-dd");
            }
            if (name == "registeredTime")
            {
                if (posTransactionHeader.registeredDateTime.Hour == 0) return "";
                return posTransactionHeader.registeredDateTime.ToString("HH:mm:ss");
            }
            if (name == "registeredDateTime")
            {
                if (posTransactionHeader.registeredDateTime.Year == 1970) return "";
                if (posTransactionHeader.registeredDateTime.Hour == 0) return "";
                return posTransactionHeader.registeredDateTime.ToString("yyyy-MM-dd HH:mm");
            }
            if (name == "controlUnitId") return posTransactionHeader.unitId;
            if (name == "posDeviceId") return posTransactionHeader.posDeviceID;
            if (name == "transactionNo") return posTransactionHeader.no;
            if (name == "storeName") return store.name;
            if (name == "storeAddress") return store.address;
            if (name == "storePostalAddress") return store.postalAddress;
            if (name == "registrationNo") return store.registrationNo;
            if (name == "vatRegistrationNo") return store.vatRegistrationNo;
            if (name == "phoneNo") return store.phoneNo;
            if (name == "email") return store.email;
            if (name == "homePage") return store.homePage;


            return "";
        }

        private string getLineFieldContent(string name, POSTransactionLine posTransactionLine)
        {
            if ((posTransactionLine.lineType == 0) && (posTransactionLine.salesType == 0))
            {
                if (name == "description") return posTransactionLine.description;
                if (name == "description2") return posTransactionLine.description2;
                return "";
            }

            if (name == "no") return posTransactionLine.salesNo;
            if (name == "variantCode") return posTransactionLine.variantCode;
            if (name == "sku") return posTransactionLine.salesNo + " " + posTransactionLine.variantCode;
            if (name == "description") return posTransactionLine.description;
            if (name == "description2") return posTransactionLine.description2;
            if (name == "quantity") return posTransactionLine.quantity.ToString();
            if (name == "unitPrice") return posTransactionLine.unitPriceInclVAT.ToString("F");
            if (name == "presentationAmount") return posTransactionLine.presentationAmountInclVAT.ToString("F");
            if (name == "discount") return posTransactionLine.discountText;
            if (name == "returnReasonCode") return posTransactionLine.returnReasonCode;
            if (name == "returnReasonDescription") return posTransactionLine.returnReasonDescription;
            if (name == "deliveryMethodDescription") return posTransactionLine.deliveryMethodDescription;



            return "";
        }
    }
}
