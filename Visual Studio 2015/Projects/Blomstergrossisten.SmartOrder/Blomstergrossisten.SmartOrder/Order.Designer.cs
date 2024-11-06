namespace SmartOrder
{
    partial class Order
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.address = new System.Windows.Forms.TextBox();
            this.zipCode = new System.Windows.Forms.TextBox();
            this.userReferenceCode = new System.Windows.Forms.ComboBox();
            this.salesPersonCode = new System.Windows.Forms.ComboBox();
            this.city = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.orderNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.deliveryCity = new System.Windows.Forms.TextBox();
            this.deliveryZipCode = new System.Windows.Forms.TextBox();
            this.deliveryAddress = new System.Windows.Forms.TextBox();
            this.deliveryName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.deliveryCode = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.deliveryAddress2 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.deliveryContact = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.salesLineGrid = new System.Windows.Forms.DataGrid();
            this.addLine = new System.Windows.Forms.Button();
            this.showLine = new System.Windows.Forms.Button();
            this.deleteLine = new System.Windows.Forms.Button();
            this.paymentMethod = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.discountBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.preInvoiceBox = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.releaseBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.salesLineTable = new System.Windows.Forms.DataGridTableStyle();
            this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.baseUnitCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.discountCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.deliveryDateCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.unitPriceCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.amountCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.colorCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.scanCode = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(247, 265);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.orderNo);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.city);
            this.tabPage1.Controls.Add(this.salesPersonCode);
            this.tabPage1.Controls.Add(this.userReferenceCode);
            this.tabPage1.Controls.Add(this.zipCode);
            this.tabPage1.Controls.Add(this.address);
            this.tabPage1.Controls.Add(this.name);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(239, 236);
            this.tabPage1.Text = "Kund";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.deliveryContact);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.deliveryAddress2);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.deliveryCode);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.deliveryCity);
            this.tabPage2.Controls.Add(this.deliveryZipCode);
            this.tabPage2.Controls.Add(this.deliveryAddress);
            this.tabPage2.Controls.Add(this.deliveryName);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(239, 236);
            this.tabPage2.Text = "Leverans";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.scanCode);
            this.tabPage3.Controls.Add(this.deleteLine);
            this.tabPage3.Controls.Add(this.showLine);
            this.tabPage3.Controls.Add(this.addLine);
            this.tabPage3.Controls.Add(this.salesLineGrid);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(239, 236);
            this.tabPage3.Text = "Artiklar";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.preInvoiceBox);
            this.tabPage4.Controls.Add(this.label20);
            this.tabPage4.Controls.Add(this.label19);
            this.tabPage4.Controls.Add(this.discountBox);
            this.tabPage4.Controls.Add(this.label18);
            this.tabPage4.Controls.Add(this.paymentMethod);
            this.tabPage4.Controls.Add(this.label16);
            this.tabPage4.Controls.Add(this.label17);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(239, 236);
            this.tabPage4.Text = "Bokföring";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 20);
            this.label1.Text = "Order: Kunduppgifter";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Namn:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Adress:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Postadress:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.Text = "Vår ref.:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(4, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "Säljare:";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(82, 61);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(139, 23);
            this.name.TabIndex = 6;
            this.name.GotFocus += new System.EventHandler(this.name_GotFocus);
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(82, 86);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(139, 23);
            this.address.TabIndex = 7;
            this.address.GotFocus += new System.EventHandler(this.address_GotFocus);
            // 
            // zipCode
            // 
            this.zipCode.Location = new System.Drawing.Point(82, 111);
            this.zipCode.Name = "zipCode";
            this.zipCode.Size = new System.Drawing.Size(50, 23);
            this.zipCode.TabIndex = 8;
            this.zipCode.GotFocus += new System.EventHandler(this.zipCode_GotFocus);
            // 
            // userReferenceCode
            // 
            this.userReferenceCode.DisplayMember = "code";
            this.userReferenceCode.Location = new System.Drawing.Point(82, 151);
            this.userReferenceCode.Name = "userReferenceCode";
            this.userReferenceCode.Size = new System.Drawing.Size(139, 23);
            this.userReferenceCode.TabIndex = 9;
            this.userReferenceCode.ValueMember = "code";
            // 
            // salesPersonCode
            // 
            this.salesPersonCode.DisplayMember = "code";
            this.salesPersonCode.Location = new System.Drawing.Point(82, 177);
            this.salesPersonCode.Name = "salesPersonCode";
            this.salesPersonCode.Size = new System.Drawing.Size(139, 23);
            this.salesPersonCode.TabIndex = 10;
            this.salesPersonCode.ValueMember = "code";
            // 
            // city
            // 
            this.city.Location = new System.Drawing.Point(138, 111);
            this.city.Name = "city";
            this.city.Size = new System.Drawing.Size(83, 23);
            this.city.TabIndex = 11;
            this.city.GotFocus += new System.EventHandler(this.city_GotFocus);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(4, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 20);
            this.label7.Text = "Ordernr:";
            // 
            // orderNo
            // 
            this.orderNo.Location = new System.Drawing.Point(82, 27);
            this.orderNo.Name = "orderNo";
            this.orderNo.ReadOnly = true;
            this.orderNo.Size = new System.Drawing.Size(139, 23);
            this.orderNo.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(4, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(157, 20);
            this.label8.Text = "Order: Leverans";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(4, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 20);
            this.label9.Text = "Leveranskod:";
            // 
            // deliveryCity
            // 
            this.deliveryCity.Location = new System.Drawing.Point(138, 137);
            this.deliveryCity.Name = "deliveryCity";
            this.deliveryCity.Size = new System.Drawing.Size(83, 23);
            this.deliveryCity.TabIndex = 21;
            this.deliveryCity.GotFocus += new System.EventHandler(this.deliveryCity_GotFocus);
            // 
            // deliveryZipCode
            // 
            this.deliveryZipCode.Location = new System.Drawing.Point(82, 137);
            this.deliveryZipCode.Name = "deliveryZipCode";
            this.deliveryZipCode.Size = new System.Drawing.Size(50, 23);
            this.deliveryZipCode.TabIndex = 20;
            this.deliveryZipCode.GotFocus += new System.EventHandler(this.deliveryZipCode_GotFocus);
            // 
            // deliveryAddress
            // 
            this.deliveryAddress.Location = new System.Drawing.Point(82, 86);
            this.deliveryAddress.Name = "deliveryAddress";
            this.deliveryAddress.Size = new System.Drawing.Size(139, 23);
            this.deliveryAddress.TabIndex = 19;
            this.deliveryAddress.GotFocus += new System.EventHandler(this.deliveryAddress_GotFocus);
            // 
            // deliveryName
            // 
            this.deliveryName.Location = new System.Drawing.Point(82, 61);
            this.deliveryName.Name = "deliveryName";
            this.deliveryName.Size = new System.Drawing.Size(139, 23);
            this.deliveryName.TabIndex = 18;
            this.deliveryName.GotFocus += new System.EventHandler(this.deliveryName_GotFocus);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(4, 139);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 20);
            this.label10.Text = "Postadress:";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(4, 88);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 20);
            this.label11.Text = "Adress:";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(4, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 20);
            this.label12.Text = "Namn:";
            // 
            // deliveryCode
            // 
            this.deliveryCode.Location = new System.Drawing.Point(82, 27);
            this.deliveryCode.Name = "deliveryCode";
            this.deliveryCode.Size = new System.Drawing.Size(139, 23);
            this.deliveryCode.TabIndex = 27;
            this.deliveryCode.SelectedIndexChanged += new System.EventHandler(this.deliveryCode_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(4, 114);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 20);
            this.label13.Text = "Adress 2:";
            // 
            // deliveryAddress2
            // 
            this.deliveryAddress2.Location = new System.Drawing.Point(82, 112);
            this.deliveryAddress2.Name = "deliveryAddress2";
            this.deliveryAddress2.Size = new System.Drawing.Size(139, 23);
            this.deliveryAddress2.TabIndex = 29;
            this.deliveryAddress2.GotFocus += new System.EventHandler(this.deliveryAddress2_GotFocus);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(4, 175);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 20);
            this.label14.Text = "Kontakt:";
            // 
            // deliveryContact
            // 
            this.deliveryContact.Location = new System.Drawing.Point(82, 173);
            this.deliveryContact.Name = "deliveryContact";
            this.deliveryContact.Size = new System.Drawing.Size(139, 23);
            this.deliveryContact.TabIndex = 31;
            this.deliveryContact.GotFocus += new System.EventHandler(this.deliveryContact_GotFocus);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(4, 4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 20);
            this.label15.Text = "Scanna:";
            // 
            // salesLineGrid
            // 
            this.salesLineGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.salesLineGrid.Location = new System.Drawing.Point(0, 28);
            this.salesLineGrid.Name = "salesLineGrid";
            this.salesLineGrid.Size = new System.Drawing.Size(240, 164);
            this.salesLineGrid.TabIndex = 2;
            this.salesLineGrid.TableStyles.Add(this.salesLineTable);
            // 
            // addLine
            // 
            this.addLine.Location = new System.Drawing.Point(162, 199);
            this.addLine.Name = "addLine";
            this.addLine.Size = new System.Drawing.Size(72, 34);
            this.addLine.TabIndex = 3;
            this.addLine.Text = "Lägg till";
            this.addLine.Click += new System.EventHandler(this.addLine_Click);
            // 
            // showLine
            // 
            this.showLine.Location = new System.Drawing.Point(84, 198);
            this.showLine.Name = "showLine";
            this.showLine.Size = new System.Drawing.Size(72, 34);
            this.showLine.TabIndex = 4;
            this.showLine.Text = "Visa";
            this.showLine.Click += new System.EventHandler(this.showLine_Click);
            // 
            // deleteLine
            // 
            this.deleteLine.Location = new System.Drawing.Point(6, 198);
            this.deleteLine.Name = "deleteLine";
            this.deleteLine.Size = new System.Drawing.Size(72, 34);
            this.deleteLine.TabIndex = 5;
            this.deleteLine.Text = "Ta bort";
            this.deleteLine.Click += new System.EventHandler(this.deleteLine_Click);
            // 
            // paymentMethod
            // 
            this.paymentMethod.DisplayMember = "code";
            this.paymentMethod.Location = new System.Drawing.Point(82, 27);
            this.paymentMethod.Name = "paymentMethod";
            this.paymentMethod.Size = new System.Drawing.Size(139, 23);
            this.paymentMethod.TabIndex = 30;
            this.paymentMethod.ValueMember = "code";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(4, 29);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 20);
            this.label16.Text = "Betalning:";
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(4, 4);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(157, 20);
            this.label17.Text = "Order: Bokföring";
            // 
            // discountBox
            // 
            this.discountBox.Location = new System.Drawing.Point(82, 70);
            this.discountBox.Name = "discountBox";
            this.discountBox.Size = new System.Drawing.Size(47, 23);
            this.discountBox.TabIndex = 34;
            this.discountBox.GotFocus += new System.EventHandler(this.discountBox_GotFocus);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(4, 72);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(100, 20);
            this.label18.Text = "Totalrabatt:";
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(133, 72);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(28, 20);
            this.label19.Text = "%";
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(4, 96);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 20);
            this.label20.Text = "Direktfaktura:";
            // 
            // preInvoiceBox
            // 
            this.preInvoiceBox.Location = new System.Drawing.Point(82, 95);
            this.preInvoiceBox.Name = "preInvoiceBox";
            this.preInvoiceBox.Size = new System.Drawing.Size(100, 20);
            this.preInvoiceBox.TabIndex = 38;
            this.preInvoiceBox.Text = "Ja";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(170, 271);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(63, 33);
            this.button4.TabIndex = 5;
            this.button4.Text = "Stäng";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // releaseBtn
            // 
            this.releaseBtn.Location = new System.Drawing.Point(102, 271);
            this.releaseBtn.Name = "releaseBtn";
            this.releaseBtn.Size = new System.Drawing.Size(63, 33);
            this.releaseBtn.TabIndex = 6;
            this.releaseBtn.Text = "Klar";
            this.releaseBtn.Click += new System.EventHandler(this.releaseBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(8, 271);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(63, 33);
            this.deleteBtn.TabIndex = 7;
            this.deleteBtn.Text = "Tabort";
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // salesLineTable
            // 
            this.salesLineTable.GridColumnStyles.Add(this.itemNoCol);
            this.salesLineTable.GridColumnStyles.Add(this.descriptionCol);
            this.salesLineTable.GridColumnStyles.Add(this.quantityCol);
            this.salesLineTable.GridColumnStyles.Add(this.baseUnitCol);
            this.salesLineTable.GridColumnStyles.Add(this.discountCol);
            this.salesLineTable.GridColumnStyles.Add(this.deliveryDateCol);
            this.salesLineTable.GridColumnStyles.Add(this.unitPriceCol);
            this.salesLineTable.GridColumnStyles.Add(this.amountCol);
            this.salesLineTable.GridColumnStyles.Add(this.colorCol);
            this.salesLineTable.MappingName = "salesLine";
            // 
            // itemNoCol
            // 
            this.itemNoCol.Format = "";
            this.itemNoCol.FormatInfo = null;
            this.itemNoCol.HeaderText = "Artikelnr";
            this.itemNoCol.MappingName = "itemNo";
            this.itemNoCol.NullText = "";
            // 
            // descriptionCol
            // 
            this.descriptionCol.Format = "";
            this.descriptionCol.FormatInfo = null;
            this.descriptionCol.HeaderText = "Beskrivning";
            this.descriptionCol.MappingName = "description";
            this.descriptionCol.NullText = "";
            // 
            // quantityCol
            // 
            this.quantityCol.Format = "";
            this.quantityCol.FormatInfo = null;
            this.quantityCol.HeaderText = "Antal";
            this.quantityCol.MappingName = "quantity";
            this.quantityCol.NullText = "";
            // 
            // baseUnitCol
            // 
            this.baseUnitCol.Format = "";
            this.baseUnitCol.FormatInfo = null;
            this.baseUnitCol.HeaderText = "Enhetskod";
            this.baseUnitCol.MappingName = "baseUnit";
            this.baseUnitCol.NullText = "";
            // 
            // discountCol
            // 
            this.discountCol.Format = "";
            this.discountCol.FormatInfo = null;
            this.discountCol.HeaderText = "Rabatt";
            this.discountCol.MappingName = "discount";
            this.discountCol.NullText = "";
            // 
            // deliveryDateCol
            // 
            this.deliveryDateCol.Format = "";
            this.deliveryDateCol.FormatInfo = null;
            this.deliveryDateCol.HeaderText = "Leveransdatum";
            this.deliveryDateCol.MappingName = "deliveryDate";
            this.deliveryDateCol.NullText = "";
            // 
            // unitPriceCol
            // 
            this.unitPriceCol.Format = "";
            this.unitPriceCol.FormatInfo = null;
            this.unitPriceCol.HeaderText = "A-pris";
            this.unitPriceCol.MappingName = "formatedUnitPrice";
            this.unitPriceCol.NullText = "";
            // 
            // amountCol
            // 
            this.amountCol.Format = "";
            this.amountCol.FormatInfo = null;
            this.amountCol.HeaderText = "Belopp";
            this.amountCol.MappingName = "formatedAmount";
            this.amountCol.NullText = "";
            // 
            // colorCol
            // 
            this.colorCol.Format = "";
            this.colorCol.FormatInfo = null;
            this.colorCol.HeaderText = "Färg";
            this.colorCol.MappingName = "color";
            this.colorCol.NullText = "";
            // 
            // scanCode
            // 
            this.scanCode.Location = new System.Drawing.Point(65, 2);
            this.scanCode.Name = "scanCode";
            this.scanCode.Size = new System.Drawing.Size(164, 23);
            this.scanCode.TabIndex = 6;
            this.scanCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanCode_KeyPress);
            // 
            // Order
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(247, 312);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.releaseBtn);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Order";
            this.Text = "Order";
            this.Load += new System.EventHandler(this.Order_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Order_Closing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox zipCode;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ComboBox salesPersonCode;
        private System.Windows.Forms.ComboBox userReferenceCode;
        private System.Windows.Forms.TextBox orderNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox city;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox deliveryCity;
        private System.Windows.Forms.TextBox deliveryZipCode;
        private System.Windows.Forms.TextBox deliveryAddress;
        private System.Windows.Forms.TextBox deliveryName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox deliveryAddress2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox deliveryCode;
        private System.Windows.Forms.TextBox deliveryContact;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button showLine;
        private System.Windows.Forms.Button addLine;
        private System.Windows.Forms.DataGrid salesLineGrid;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button deleteLine;
        private System.Windows.Forms.ComboBox paymentMethod;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox discountBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox preInvoiceBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button releaseBtn;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.DataGridTableStyle salesLineTable;
        private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
        private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
        private System.Windows.Forms.DataGridTextBoxColumn baseUnitCol;
        private System.Windows.Forms.DataGridTextBoxColumn discountCol;
        private System.Windows.Forms.DataGridTextBoxColumn deliveryDateCol;
        private System.Windows.Forms.DataGridTextBoxColumn unitPriceCol;
        private System.Windows.Forms.DataGridTextBoxColumn amountCol;
        private System.Windows.Forms.DataGridTextBoxColumn colorCol;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.TextBox scanCode;
    }
}