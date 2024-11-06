namespace Navipro.SmartInventory
{
    partial class PickItem
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
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pickListNo = new System.Windows.Forms.TextBox();
            this.noOfLinesBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.scanBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.binBox = new System.Windows.Forms.TextBox();
            this.brandBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.description2Box = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.totalBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.currentOrderBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pickedBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.placeBinBox = new System.Windows.Forms.TextBox();
            this.logViewList = new System.Windows.Forms.ListBox();
            this.readyPanel = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.readyPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button5.Location = new System.Drawing.Point(121, 267);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(114, 47);
            this.button5.TabIndex = 5;
            this.button5.Text = "Bekräfta";
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Plocklista";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(121, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Antal rader";
            // 
            // pickListNo
            // 
            this.pickListNo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.pickListNo.Location = new System.Drawing.Point(3, 21);
            this.pickListNo.Name = "pickListNo";
            this.pickListNo.ReadOnly = true;
            this.pickListNo.Size = new System.Drawing.Size(112, 23);
            this.pickListNo.TabIndex = 8;
            // 
            // noOfLinesBox
            // 
            this.noOfLinesBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.noOfLinesBox.Location = new System.Drawing.Point(121, 21);
            this.noOfLinesBox.Name = "noOfLinesBox";
            this.noOfLinesBox.ReadOnly = true;
            this.noOfLinesBox.Size = new System.Drawing.Size(114, 23);
            this.noOfLinesBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Scanna";
            // 
            // scanBox
            // 
            this.scanBox.Location = new System.Drawing.Point(3, 62);
            this.scanBox.Name = "scanBox";
            this.scanBox.Size = new System.Drawing.Size(146, 23);
            this.scanBox.TabIndex = 11;
            this.scanBox.GotFocus += new System.EventHandler(this.scanBox_GotFocus);
            this.scanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBox_KeyPress);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Lagerplats";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(121, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.Text = "Varumärke";
            // 
            // binBox
            // 
            this.binBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.binBox.Location = new System.Drawing.Point(3, 104);
            this.binBox.Name = "binBox";
            this.binBox.ReadOnly = true;
            this.binBox.Size = new System.Drawing.Size(112, 23);
            this.binBox.TabIndex = 14;
            // 
            // brandBox
            // 
            this.brandBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.brandBox.Location = new System.Drawing.Point(121, 104);
            this.brandBox.Name = "brandBox";
            this.brandBox.ReadOnly = true;
            this.brandBox.Size = new System.Drawing.Size(114, 23);
            this.brandBox.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "Artikel";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.descriptionBox.Location = new System.Drawing.Point(3, 145);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(232, 23);
            this.descriptionBox.TabIndex = 17;
            // 
            // description2Box
            // 
            this.description2Box.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.description2Box.Location = new System.Drawing.Point(3, 172);
            this.description2Box.Name = "description2Box";
            this.description2Box.ReadOnly = true;
            this.description2Box.Size = new System.Drawing.Size(232, 23);
            this.description2Box.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 197);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 20);
            this.label7.Text = "Totalt";
            // 
            // totalBox
            // 
            this.totalBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.totalBox.Location = new System.Drawing.Point(3, 214);
            this.totalBox.Name = "totalBox";
            this.totalBox.ReadOnly = true;
            this.totalBox.Size = new System.Drawing.Size(55, 23);
            this.totalBox.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(65, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 20);
            this.label8.Text = "Aktuell order";
            // 
            // currentOrderBox
            // 
            this.currentOrderBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.currentOrderBox.Location = new System.Drawing.Point(65, 214);
            this.currentOrderBox.Name = "currentOrderBox";
            this.currentOrderBox.ReadOnly = true;
            this.currentOrderBox.Size = new System.Drawing.Size(84, 23);
            this.currentOrderBox.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(156, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 20);
            this.label9.Text = "Plockat";
            // 
            // pickedBox
            // 
            this.pickedBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.pickedBox.Location = new System.Drawing.Point(156, 214);
            this.pickedBox.Name = "pickedBox";
            this.pickedBox.ReadOnly = true;
            this.pickedBox.Size = new System.Drawing.Size(79, 23);
            this.pickedBox.TabIndex = 24;
            this.pickedBox.TextChanged += new System.EventHandler(this.pickedBox_TextChanged);
            this.pickedBox.GotFocus += new System.EventHandler(this.pickedBox_GotFocus);
            this.pickedBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pickedBox_KeyPress);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(121, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 47);
            this.button1.TabIndex = 25;
            this.button1.Text = "Avsluta";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // placeBinBox
            // 
            this.placeBinBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.placeBinBox.Location = new System.Drawing.Point(4, 267);
            this.placeBinBox.Name = "placeBinBox";
            this.placeBinBox.Size = new System.Drawing.Size(111, 26);
            this.placeBinBox.TabIndex = 35;
            // 
            // logViewList
            // 
            this.logViewList.Location = new System.Drawing.Point(292, 269);
            this.logViewList.Name = "logViewList";
            this.logViewList.Size = new System.Drawing.Size(100, 98);
            this.logViewList.TabIndex = 45;
            this.logViewList.Visible = false;
            // 
            // readyPanel
            // 
            this.readyPanel.BackColor = System.Drawing.Color.White;
            this.readyPanel.Controls.Add(this.label10);
            this.readyPanel.Location = new System.Drawing.Point(258, 4);
            this.readyPanel.Name = "readyPanel";
            this.readyPanel.Size = new System.Drawing.Size(234, 258);
            this.readyPanel.Visible = false;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(228, 20);
            this.label10.Text = "Plockuppdraget är nu klart!";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(121, 320);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 47);
            this.button2.TabIndex = 56;
            this.button2.Text = "Val";
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(4, 240);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(231, 20);
            this.label11.Text = "Återföring till lagerplats!";
            this.label11.Visible = false;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(156, 51);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 34);
            this.button3.TabIndex = 67;
            this.button3.Text = "Ångra";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // PickItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.logViewList);
            this.Controls.Add(this.readyPanel);
            this.Controls.Add(this.placeBinBox);
            this.Controls.Add(this.pickedBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.currentOrderBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.totalBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.description2Box);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.brandBox);
            this.Controls.Add(this.binBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.scanBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.noOfLinesBox);
            this.Controls.Add(this.pickListNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PickItem";
            this.Text = "PickItem";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.readyPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pickListNo;
        private System.Windows.Forms.TextBox noOfLinesBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox scanBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox binBox;
        private System.Windows.Forms.TextBox brandBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.TextBox description2Box;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox totalBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox currentOrderBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox pickedBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox placeBinBox;
        private System.Windows.Forms.ListBox logViewList;
        private System.Windows.Forms.Panel readyPanel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button3;
    }
}