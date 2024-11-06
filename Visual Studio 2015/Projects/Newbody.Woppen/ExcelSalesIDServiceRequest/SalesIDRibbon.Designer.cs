namespace ExcelSalesIDServiceRequest
{
    partial class SalesIDRibbon
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = new Microsoft.Office.Tools.Ribbon.RibbonTab();
            this.group1 = new Microsoft.Office.Tools.Ribbon.RibbonGroup();
            this.label1 = new Microsoft.Office.Tools.Ribbon.RibbonLabel();
            this.customerNoBox = new Microsoft.Office.Tools.Ribbon.RibbonEditBox();
            this.descriptionBox = new Microsoft.Office.Tools.Ribbon.RibbonEditBox();
            this.label3 = new Microsoft.Office.Tools.Ribbon.RibbonLabel();
            this.label2 = new Microsoft.Office.Tools.Ribbon.RibbonLabel();
            this.button1 = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.button2 = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "Navision";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.label1);
            this.group1.Items.Add(this.customerNoBox);
            this.group1.Items.Add(this.descriptionBox);
            this.group1.Items.Add(this.label3);
            this.group1.Items.Add(this.button2);
            this.group1.Items.Add(this.button1);
            this.group1.Items.Add(this.label2);
            this.group1.Label = "Försäljnings-ID";
            this.group1.Name = "group1";
            // 
            // label1
            // 
            this.label1.Label = "Skapa försäljnings-id";
            this.label1.Name = "label1";
            // 
            // customerNoBox
            // 
            this.customerNoBox.Label = "Kundnr:";
            this.customerNoBox.Name = "customerNoBox";
            this.customerNoBox.Text = null;
            // 
            // descriptionBox
            // 
            this.descriptionBox.Label = "Beskrivning:";
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Text = null;
            // 
            // label3
            // 
            this.label3.Label = " ";
            this.label3.Name = "label3";
            // 
            // label2
            // 
            this.label2.Label = " ";
            this.label2.Name = "label2";
            // 
            // button1
            // 
            this.button1.Label = "Skapa";
            this.button1.Name = "button1";
            this.button1.ShowImage = true;
            this.button1.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Label = "Hämta kundnr";
            this.button2.Name = "button2";
            this.button2.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.button2_Click);
            // 
            // SalesIDRibbon
            // 
            this.Name = "SalesIDRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonUIEventArgs>(this.SalesIDRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel label1;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox customerNoBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox descriptionBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel label3;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel label2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
    }

    partial class ThisRibbonCollection : Microsoft.Office.Tools.Ribbon.RibbonReadOnlyCollection
    {
        internal SalesIDRibbon SalesIDRibbon
        {
            get { return this.GetRibbon<SalesIDRibbon>(); }
        }
    }
}
