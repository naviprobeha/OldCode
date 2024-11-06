using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Dynamics.Framework.UI.Extensibility;
using Microsoft.Dynamics.Framework.UI.Extensibility.WinForms;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;

namespace SvensktTenn.Pick
{

    [ControlAddInExport("SvensktTenn.Pick.PickByLight")]
    public class PickByLight : StringControlAddInBase
    {
        private Panel pickScreen;
        private TextBox line1Box;
        private TextBox line2Box;
        private TextBox line3Box;
        private TextBox line4Box;

        private TextBox scanBox;
        private TextBox binBox;
        private TextBox toteBox;
        private TextBox quantityBox;
        private TextBox totalQtyBox;

        private TextBox prevItemLine1Box;
        private TextBox prevItemLine2Box;
        private TextBox prevToteBox;
        private TextBox prevQtyBox;


        private Button acceptButton;
        private Button noBarCodeButton;
        private Button zeroPickButton;
        private Button cancelButton;

        private int column1Width;
        private int column2Width;

        public Dictionary<string, PickBin> binDictionary;
        private PickBin currentPickBin;
        private PickItemOrder currentPickItemOrder;
        private PickItem currentPickItem;


        protected override System.Windows.Forms.Control CreateControl()
        {
            binDictionary = new Dictionary<string, PickBin>();

            pickScreen = new Panel();
            pickScreen.Width = 1300;
            pickScreen.Height = 600;

            column1Width = 600;
            column2Width = 300;

            //Skapa plockvy

            scanBox = new MyTextBox();
            scanBox.Location = new Point(20, 20);
            scanBox.Width = column1Width;
            scanBox.Height = 40;
            scanBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Bold);
            scanBox.Text = "";
            scanBox.BackColor = Color.White;
            scanBox.BorderStyle = BorderStyle.FixedSingle;
            scanBox.TextChanged += ScanBox_TextChanged;
            scanBox.KeyPress += ScanBox_KeyPress;
            pickScreen.Controls.Add(scanBox);

            line1Box = new TextBox();
            line1Box.Location = new Point(20, 80);
            line1Box.Width = column1Width;
            line1Box.Height = 45;
            line1Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Bold);
            line1Box.Text = "";
            line1Box.BackColor = Color.FromArgb(245, 245, 245);
            line1Box.BorderStyle = BorderStyle.None;
            line1Box.TabIndex = 100;
            pickScreen.Controls.Add(line1Box);

            line2Box = new TextBox();
            line2Box.Location = new Point(20, 140);
            line2Box.Width = column1Width;
            line2Box.Height = 45;
            line2Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Bold);
            line2Box.Text = "";
            line2Box.BackColor = Color.FromArgb(245, 245, 245);
            line2Box.BorderStyle = BorderStyle.None;
            line2Box.TabIndex = 100;
            pickScreen.Controls.Add(line2Box);

            line3Box = new TextBox();
            line3Box.Location = new Point(20, 200);
            line3Box.Width = column1Width;
            line3Box.Height = 45;
            line3Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Bold);
            line3Box.Text = "";
            line3Box.BackColor = Color.FromArgb(245, 245, 245);
            line3Box.BorderStyle = BorderStyle.None;
            line3Box.TabIndex = 100;
            pickScreen.Controls.Add(line3Box);

            line4Box = new TextBox();
            line4Box.Location = new Point(column1Width + 40, 200);
            line4Box.Width = column2Width;
            line4Box.Height = 45;
            line4Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Bold);
            line4Box.Text = "";
            line4Box.BackColor = Color.FromArgb(245, 245, 245);
            line4Box.BorderStyle = BorderStyle.None;
            line4Box.GotFocus += Line4Box_GotFocus;
            line4Box.TabIndex = 100;
            pickScreen.Controls.Add(line4Box);


            binBox = new TextBox();
            binBox.Location = new Point(column1Width + 40, 20);
            binBox.Width = column2Width;
            binBox.Height = 45;
            binBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Bold);
            binBox.Text = "";
            binBox.BackColor = Color.FromArgb(245, 245, 245);
            binBox.BorderStyle = BorderStyle.None;
            binBox.TabIndex = 100;
            pickScreen.Controls.Add(binBox);

            toteBox = new TextBox();
            toteBox.Location = new Point(column1Width + 40, 80);
            toteBox.Width = column2Width;
            toteBox.Height = 45;
            toteBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Bold);
            toteBox.Text = "";
            toteBox.BackColor = Color.FromArgb(245, 245, 245);
            toteBox.BorderStyle = BorderStyle.None;
            toteBox.TabIndex = 100;
            pickScreen.Controls.Add(toteBox);


            quantityBox = new TextBox();
            quantityBox.Location = new Point(column1Width + 40, 140);
            quantityBox.Width = (column2Width/2)-10;
            quantityBox.Height = 45;
            quantityBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Bold);
            quantityBox.Text = "";
            quantityBox.BackColor = Color.FromArgb(245, 245, 245);
            quantityBox.BorderStyle = BorderStyle.None;
            pickScreen.Controls.Add(quantityBox);


            totalQtyBox = new TextBox();
            totalQtyBox.Location = new Point(column1Width + 40 + (column2Width / 2) + 10, 140);
            totalQtyBox.Width = (column2Width / 2) - 10;
            totalQtyBox.Height = 45;
            totalQtyBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Bold);
            totalQtyBox.Text = "";
            totalQtyBox.BackColor = Color.FromArgb(245, 245, 245);
            totalQtyBox.BorderStyle = BorderStyle.None;
            pickScreen.Controls.Add(totalQtyBox);



            acceptButton = new Button();
            acceptButton.Location = new Point(20, 260);
            acceptButton.FlatStyle = FlatStyle.Flat;
            acceptButton.FlatAppearance.BorderColor = Color.White;

            acceptButton.Width = column1Width;
            acceptButton.Height = 300;
            acceptButton.Text = "OK";
            acceptButton.Name = "OK";
            acceptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Bold);
            acceptButton.Click += new EventHandler(acceptButton_Click);

            acceptButton.BackColor = Color.FromArgb(229, 229, 229);
            
            pickScreen.Controls.Add(acceptButton);

            noBarCodeButton = new Button();
            noBarCodeButton.Location = new Point(column1Width + 40, 260);
            noBarCodeButton.FlatStyle = FlatStyle.Flat;
            noBarCodeButton.FlatAppearance.BorderColor = Color.White;

            noBarCodeButton.Width = column2Width;
            noBarCodeButton.Height = 300;
            noBarCodeButton.Text = "Ingen streckkod";
            noBarCodeButton.Name = "NoBarCode";
            noBarCodeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Bold);
            noBarCodeButton.Click += NoBarCodeButton_Click; ;


            noBarCodeButton.BackColor = Color.FromArgb(229, 229, 229);

            pickScreen.Controls.Add(noBarCodeButton);


            zeroPickButton = new Button();
            zeroPickButton.Location = new Point(column1Width+60+column2Width, 260);
            zeroPickButton.FlatStyle = FlatStyle.Flat;
            zeroPickButton.FlatAppearance.BorderColor = Color.White;

            zeroPickButton.Width = column2Width;
            zeroPickButton.Height = 140;
            zeroPickButton.Text = "Noll-plock";
            zeroPickButton.Name = "Zero";
            zeroPickButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Bold);
            zeroPickButton.Click += ZeroPickButton_Click;
            

            zeroPickButton.BackColor = Color.FromArgb(229, 229, 229);

            pickScreen.Controls.Add(zeroPickButton);


            cancelButton = new Button();
            cancelButton.Location = new Point(column1Width + 60 + column2Width, 420);
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.FlatAppearance.BorderColor = Color.White;

            cancelButton.Width = column2Width;
            cancelButton.Height = 140;
            cancelButton.Text = "Avsluta";
            cancelButton.Name = "Zero";
            cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Bold);
            cancelButton.Click += CancelButton_Click;
            

            cancelButton.BackColor = Color.FromArgb(229, 229, 229);


            pickScreen.Controls.Add(cancelButton);

            /*
            Label descriptionLabel = new Label();
            descriptionLabel.Location = new Point(18, 4);
            descriptionLabel.Width = column1Width;
            descriptionLabel.Height = 14;
            descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
            descriptionLabel.Text = "Artikelbeskrivning";

            pickScreen.Controls.Add(descriptionLabel);
            */

            Label scanLabel = new Label();
            scanLabel.Location = new Point(18, 4);
            scanLabel.Width = column2Width;
            scanLabel.Height = 14;
            scanLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
            scanLabel.Text = "Scanna artikel";
            
            pickScreen.Controls.Add(scanLabel);


            Label binLabel = new Label();
            binLabel.Location = new Point(column1Width + 38, 4);
            binLabel.Width = column2Width;
            binLabel.Height = 14;
            binLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
            binLabel.Text = "Lagerplats";

            pickScreen.Controls.Add(binLabel);

            Label toteLabel = new Label();
            toteLabel.Location = new Point(column1Width + 38, 64);
            toteLabel.Width = column2Width;
            toteLabel.Height = 14;
            toteLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
            toteLabel.Text = "Vagnsfack";

            pickScreen.Controls.Add(toteLabel);


            Label qtyLabel = new Label();
            qtyLabel.Location = new Point(column1Width + 38, 124);
            qtyLabel.Width = (column2Width / 2) - 10;
            qtyLabel.Height = 14;
            qtyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
            qtyLabel.Text = "Antal att plocka";

            pickScreen.Controls.Add(qtyLabel);

            Label totalQtyLabel = new Label();
            totalQtyLabel.Location = new Point(column1Width + 38 + (column2Width / 2) + 10, 124);
            totalQtyLabel.Width = (column2Width / 2) - 10;
            totalQtyLabel.Height = 14;
            totalQtyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
            totalQtyLabel.Text = "Totalantal";

            pickScreen.Controls.Add(totalQtyLabel);

            Label orderLabel = new Label();
            orderLabel.Location = new Point(column1Width + 38, 184);
            orderLabel.Width = (column2Width / 2) - 10;
            orderLabel.Height = 14;
            orderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
            orderLabel.Text = "Ordernr";

            pickScreen.Controls.Add(orderLabel);

            //Prev. item
            prevItemLine1Box = new TextBox();
            prevItemLine1Box.Location = new Point(column1Width + 60 + column2Width, 20);
            prevItemLine1Box.Width = column2Width;
            prevItemLine1Box.Height = 45;
            prevItemLine1Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Regular);
            prevItemLine1Box.Text = "";
            prevItemLine1Box.BackColor = Color.FromArgb(245, 245, 245);
            prevItemLine1Box.BorderStyle = BorderStyle.None;
            pickScreen.Controls.Add(prevItemLine1Box);

            prevItemLine2Box = new TextBox();
            prevItemLine2Box.Location = new Point(column1Width + 60 + column2Width, 80);
            prevItemLine2Box.Width = column2Width;
            prevItemLine2Box.Height = 45;
            prevItemLine2Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Regular);
            prevItemLine2Box.Text = "";
            prevItemLine2Box.BackColor = Color.FromArgb(245, 245, 245);
            prevItemLine2Box.BorderStyle = BorderStyle.None;
            pickScreen.Controls.Add(prevItemLine2Box);

            prevToteBox = new TextBox();
            prevToteBox.Location = new Point(column1Width + 60 + column2Width, 140);
            prevToteBox.Width = column2Width;
            prevToteBox.Height = 45;
            prevToteBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Regular);
            prevToteBox.Text = "";
            prevToteBox.BackColor = Color.FromArgb(245, 245, 245);
            prevToteBox.BorderStyle = BorderStyle.None;
            pickScreen.Controls.Add(prevToteBox);

            prevQtyBox = new TextBox();
            prevQtyBox.Location = new Point(column1Width + 60 + column2Width, 200);
            prevQtyBox.Width = column2Width;
            prevQtyBox.Height = 45;
            prevQtyBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20, System.Drawing.FontStyle.Regular);
            prevQtyBox.Text = "";
            prevQtyBox.BackColor = Color.FromArgb(245, 245, 245);
            prevQtyBox.BorderStyle = BorderStyle.None;
            pickScreen.Controls.Add(prevQtyBox);


            Label prevItemLine1Label = new Label();
            prevItemLine1Label.Location = new Point(column1Width + 58 + column2Width, 4);
            prevItemLine1Label.Width = column2Width;
            prevItemLine1Label.Height = 14;
            prevItemLine1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
            prevItemLine1Label.Text = "Föregående artikel";

            pickScreen.Controls.Add(prevItemLine1Label);


            Label prevToteLabel = new Label();
            prevToteLabel.Location = new Point(column1Width + 58 + column2Width, 124);
            prevToteLabel.Width = column2Width;
            prevToteLabel.Height = 14;
            prevToteLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
            prevToteLabel.Text = "Föregående vagnsfack";

            pickScreen.Controls.Add(prevToteLabel);


            Label prevQtyLabel = new Label();
            prevQtyLabel.Location = new Point(column1Width + 58 + column2Width, 184);
            prevQtyLabel.Width = (column2Width / 2) - 10;
            prevQtyLabel.Height = 14;
            prevQtyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
            prevQtyLabel.Text = "Föregående antal";

            pickScreen.Controls.Add(prevQtyLabel);

            scanBox.Focus();

            return pickScreen;
        }

        private void NoBarCodeButton_Click(object sender, EventArgs e)
        {
            acceptButton.BackColor = Color.Green;
            showToteInfo();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.RaiseControlAddInEvent(0, "CANCEL");
        }

        private void Line4Box_GotFocus(object sender, EventArgs e)
        {

        }

        private void ZeroPickButton_Click(object sender, EventArgs e)
        {
            reportPicked(0);

            scanBox.Focus();
        }

        private void ScanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                if (scanBox.Text == currentPickItemOrder.ean)
                {
                    acceptButton.BackColor = Color.Green;
                    acceptButton.Text = "OK";
                    showToteInfo();
                }
                else
                {
                    //Beep
                    acceptButton.Text = "Felaktig ean-kod";
                    System.Media.SystemSounds.Beep.Play();
                    acceptButton.BackColor = Color.Red;
                }
                scanBox.Text = "";
                e.Handled = true;

                scanBox.Focus();
            }
        }

 

        private void ScanBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        public override bool AllowCaptionControl
        {
            get
            {
                return false;
            }
        }

        public override string Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                importPickList(value);
            }
        }

        void acceptButton_Click(object sender, EventArgs e)
        {

            reportPicked(currentPickItemOrder.quantity);

            scanBox.Focus();
        }


        private void reportPicked(decimal quantity)
        {
            currentPickItemOrder.quantityPicked = quantity;

            this.RaiseControlAddInEvent(0, "PICK:" + currentPickItemOrder.orderNo + ":" + currentPickItemOrder.lineRef + ":" + currentPickItemOrder.quantityPicked);

            acceptButton.BackColor = Color.Gray;

            prevItemLine1Box.Text = currentPickItem.description;
            prevItemLine2Box.Text = currentPickItem.description2;
            prevToteBox.Text = currentPickItemOrder.toBin;
            prevQtyBox.Text = currentPickItemOrder.quantity.ToString();

            currentPickItem.orderList.Remove(currentPickItemOrder);
            currentPickBin.itemDictionary[currentPickItem.itemNo] = currentPickItem;
            binDictionary[currentPickBin.binCode] = currentPickBin;

            findFirstPick();
        }

        [ApplicationVisible]
        public void addPickListLine(string orderNo, string lineRef, string itemNo, string description, string description2, string description3, string description4, string ean, decimal quantity, string fromBin, string toBin)
        {
            if (!binDictionary.ContainsKey(fromBin))
            {
                PickBin pickBin = new PickBin();
                pickBin.binCode = fromBin;
                binDictionary.Add(fromBin, pickBin);
            }

            PickBin pickBin2 = binDictionary[fromBin];

            if (!pickBin2.itemDictionary.ContainsKey(itemNo))
            {
                PickItem pickItem = new PickItem();
                pickItem.itemNo = itemNo;
                pickItem.description = description;
                pickItem.description2 = description2;
                pickItem.description3 = description3;
                pickItem.description4 = description4;
                pickBin2.itemDictionary.Add(itemNo, pickItem);
            }

            PickItem pickItem2 = pickBin2.itemDictionary[itemNo];
            pickItem2.orderList.Add(new PickItemOrder() { fromBin = fromBin, toBin = toBin, quantity = quantity, orderNo = orderNo, itemNo = itemNo, ean = ean, lineRef = lineRef });
            pickItem2.totalQty = pickItem2.totalQty + quantity;
            pickBin2.itemDictionary[itemNo] = pickItem2;

            
            binDictionary[fromBin] = pickBin2;
        }

        [ApplicationVisible]
        public void startNewPick()
        {
            binDictionary = new Dictionary<string, PickBin>();
        }

        private void findFirstPick()
        {
            List<PickBin> binList = binDictionary.Values.OrderBy(t => t.binCode).ToList();
            PickBin pickBin = binList.FirstOrDefault();
            if (pickBin == null)
            {
                //All picked;
                line1Box.Text = "";
                line2Box.Text = "";
                line3Box.Text = "";
                line4Box.Text = "";

                binBox.Text = "";
                toteBox.Text = "";
                quantityBox.Text = "";
                totalQtyBox.Text = "";

                this.RaiseControlAddInEvent(0, "ALLPICKED");

                return;
            }

            if (pickBin.itemDictionary.Count == 0)
            {
                binDictionary.Remove(pickBin.binCode);
                findFirstPick();
                return;
            }

            PickItem pickItem = pickBin.itemDictionary.Values.FirstOrDefault();
            if (pickItem.orderList.Count == 0)
            {
                pickBin.itemDictionary.Remove(pickItem.itemNo);
                findFirstPick();
                return;
            }

            List<PickItemOrder> orderList = pickItem.orderList.OrderBy(o => o.toBin).ToList();
            PickItemOrder pickItemOrder = orderList.First();

            line1Box.Text = pickItem.description;
            line2Box.Text = pickItem.description2;
            line3Box.Text = pickItem.description3;
            line4Box.Text = pickItem.description4;

            binBox.Text = pickItemOrder.fromBin;
            toteBox.Text = "";
            quantityBox.Text = "";
            totalQtyBox.Text = "";

            currentPickItemOrder = pickItemOrder;
            currentPickItem = pickItem;

            quantityBox.Text = currentPickItemOrder.quantity.ToString();
            totalQtyBox.Text = currentPickItem.pickedQty.ToString() + " / " + currentPickItem.totalQty.ToString();


            if (currentPickBin != pickBin)
            {
                this.RaiseControlAddInEvent(0, "SHOWBIN:"+pickBin.binCode);
            }
            currentPickBin = pickBin;
            
        }

        private void showToteInfo()
        {
            toteBox.Text = currentPickItemOrder.toBin;
            quantityBox.Text = currentPickItemOrder.quantity.ToString();
            totalQtyBox.Text = currentPickItem.pickedQty.ToString() + " / " + currentPickItem.totalQty.ToString();

        }

        private void importPickList(string xmlData)
        {
            
            if (xmlData == "") return;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            binDictionary.Clear();

            XmlElement docElement = xmlDoc.DocumentElement;
            if (docElement == null) return;

            foreach(XmlNode xmlNode in docElement.ChildNodes)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;

                addPickListLine(xmlElement.GetAttribute("orderNo"),
                    xmlElement.GetAttribute("lineRef"),
                    xmlElement.GetAttribute("itemNo"),
                    xmlElement.GetAttribute("description"),
                    xmlElement.GetAttribute("description2"),
                    xmlElement.GetAttribute("description3"),
                    xmlElement.GetAttribute("description4"),
                    xmlElement.GetAttribute("ean"),
                    Decimal.Parse(xmlElement.GetAttribute("quantity")),
                    xmlElement.GetAttribute("fromBin"),
                    xmlElement.GetAttribute("toBin"));
            }

            if (binDictionary.Values.Count > 0) findFirstPick();

            scanBox.Focus();
        }
    }
}
