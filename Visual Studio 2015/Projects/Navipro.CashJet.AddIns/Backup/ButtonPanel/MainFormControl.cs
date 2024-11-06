using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Dynamics.Framework.UI.Extensibility;
using Microsoft.Dynamics.Framework.UI.Extensibility.WinForms;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;

namespace Navipro.CashJet.AddIns
{

    [ControlAddInExport("Navipro.CashJet.AddIns.MainFormControl")]
    public class MainFormControl : StringControlAddInBase
    {
        private Panel panel;
        private Label transactionNoLabel;
        private Label receiptNoLabel;
        private Label salesPersonNoLabel;
        private Label receiptsOnHoldLabel;
        private Label customerNoLabel;
        private Label customerNameLabel;

        protected override System.Windows.Forms.Control CreateControl()
        {
            panel = new Panel();

            transactionNoLabel = new Label();
            transactionNoLabel.Location = new Point(5, 5);
            transactionNoLabel.Size = new Size(50, 10);
            transactionNoLabel.Text = "Transaktionsnr";
            //transactionNoLabel.Font.Size = 9;

            panel.Controls.Add(transactionNoLabel);

            receiptNoLabel = new Label();
            receiptNoLabel.Location = new Point(50, 10);
            receiptNoLabel.Text = "Kvittonr";
            //receiptNoLabel.Font.Size = 9;

            panel.Controls.Add(receiptNoLabel);

            salesPersonNoLabel = new Label();
            salesPersonNoLabel.Location = new Point(90, 10);
            salesPersonNoLabel.Text = "Säljare";
            //salesPersonNoLabel.Font.Size = 9;

            panel.Controls.Add(salesPersonNoLabel);

            receiptsOnHoldLabel = new Label();
            receiptsOnHoldLabel.Location = new Point(140, 10);
            receiptsOnHoldLabel.Text = "Parkerade kvitton";
            //receiptsOnHoldLabel.Font.Size = 9;

            panel.Controls.Add(receiptsOnHoldLabel);


            customerNoLabel = new Label();
            customerNoLabel.Location = new Point(190, 10);
            customerNoLabel.Text = "Kundnr";
            //customerNoLabel.Font.Size = 9;

            panel.Controls.Add(customerNoLabel);

            customerNameLabel = new Label();
            customerNameLabel.Location = new Point(240, 10);
            customerNameLabel.Text = "Kundnamn";
            //customerNameLabel.Font.Size = 9;

            panel.Controls.Add(customerNameLabel);

            return panel;
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
                
            }
        }

        private void raiseEvent(object sender, EventArgs e)
        {
            this.RaiseControlAddInEvent(0, ((Button)sender).Name);
        }

        [ApplicationVisible]
        public void setCaption(string id, string caption)
        {
        }

    }
}
