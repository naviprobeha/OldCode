namespace ExcelNavisionWebService
{
    partial class WebServiceRibbon
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
            this.button1 = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.label1);
            this.group1.Items.Add(this.customerNoBox);
            this.group1.Items.Add(this.button1);
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
            // button1
            // 
            this.button1.Label = "Skapa";
            this.button1.Name = "button1";
            this.button1.ShowImage = true;
            this.button1.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.button1_Click);
            // 
            // WebServiceRibbon
            // 
            this.Name = "WebServiceRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonUIEventArgs>(this.WebServiceRibbon_Load);
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
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
    }

    partial class ThisRibbonCollection : Microsoft.Office.Tools.Ribbon.RibbonReadOnlyCollection
    {
        internal WebServiceRibbon WebServiceRibbon
        {
            get { return this.GetRibbon<WebServiceRibbon>(); }
        }
    }
}
