namespace Navipro.SmartInventory
{
    partial class PickOptions
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.logViewList = new System.Windows.Forms.ListBox();
            this.btnChangeBin = new System.Windows.Forms.Button();
            this.btnViewPickList = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(3, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 54);
            this.button1.TabIndex = 0;
            this.button1.Text = "Tomt på hyllan";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 20);
            this.label1.Text = "Klicka på resp. val nedan.";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(3, 87);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 52);
            this.button2.TabIndex = 3;
            this.button2.Text = "Inventera";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(3, 262);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 52);
            this.button3.TabIndex = 4;
            this.button3.Text = "Återgå";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(127, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 54);
            this.label2.Text = "Antalet artiklar på hyllan räcker inte för att plocka order.";
            this.label2.ParentChanged += new System.EventHandler(this.label2_ParentChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(127, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 52);
            this.label3.Text = "Om antalet på lager- platsen misstämmer med ordern, inventera lagerplatsen här.";
            // 
            // logViewList
            // 
            this.logViewList.Location = new System.Drawing.Point(364, 177);
            this.logViewList.Name = "logViewList";
            this.logViewList.Size = new System.Drawing.Size(100, 98);
            this.logViewList.TabIndex = 46;
            this.logViewList.Visible = false;
            // 
            // btnChangeBin
            // 
            this.btnChangeBin.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnChangeBin.Location = new System.Drawing.Point(3, 146);
            this.btnChangeBin.Name = "btnChangeBin";
            this.btnChangeBin.Size = new System.Drawing.Size(118, 52);
            this.btnChangeBin.TabIndex = 50;
            this.btnChangeBin.Text = "Byt lagerplats";
            this.btnChangeBin.Click += new System.EventHandler(this.btnChangeBin_Click);
            // 
            // btnViewPickList
            // 
            this.btnViewPickList.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnViewPickList.Location = new System.Drawing.Point(3, 204);
            this.btnViewPickList.Name = "btnViewPickList";
            this.btnViewPickList.Size = new System.Drawing.Size(118, 52);
            this.btnViewPickList.TabIndex = 51;
            this.btnViewPickList.Text = "Visa plocklista";
            this.btnViewPickList.Click += new System.EventHandler(this.btnViewPickList_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(127, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 51);
            this.label4.Text = "Välj att plocka från annan lagerplats än vad som visas på plockrad.";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(127, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 52);
            this.label5.Text = "Visa lista över de artiklar som finns på plocklistan.";
            // 
            // PickOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.logViewList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnChangeBin);
            this.Controls.Add(this.btnViewPickList);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PickOptions";
            this.Text = "PickOptions";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox logViewList;
        private System.Windows.Forms.Button btnChangeBin;
        private System.Windows.Forms.Button btnViewPickList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}