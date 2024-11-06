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

    [ControlAddInExport("Navipro.CashJet.AddIns.SingleTextBox")]
    public class SingleTextBox : StringControlAddInBase
    {
        private Panel panel;
        private int width = 100;
        private int height = 30;
        private string bgColor = "white";
        private string fgColor = "black";
        private int textAlign = 0;
        private int fontSize = 12;
        private string textValue;
        private TextBox textBox;

        protected override System.Windows.Forms.Control CreateControl()
        {
            panel = new Panel();
            panel.AutoSize = true;

            textBox = new TextBox();
            textBox.BorderStyle = BorderStyle.None;
            textBox.Location = new Point(2, 2);
            textBox.Multiline = true;
            textBox.AcceptsReturn = true;
            textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);

            panel.Controls.Add(textBox);

            update();

            return panel;
        }

        void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //System.Windows.Forms.MessageBox.Show(sender.ToString());
                this.RaiseControlAddInEvent(0, "ENTER");
                e.Handled = true;
            }

        }

        private void update()
        {

            if (textBox != null)
            {
                textBox.Width = width;
                textBox.Height = height;
                textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)fontSize, System.Drawing.FontStyle.Bold);
                if (textAlign == 0) textBox.TextAlign = HorizontalAlignment.Left;
                if (textAlign == 1) textBox.TextAlign = HorizontalAlignment.Right;
                if (textAlign == 2) textBox.TextAlign = HorizontalAlignment.Center;
                textBox.BackColor = Color.FromName(bgColor);
                textBox.ForeColor = Color.FromName(fgColor);
                textBox.Text = textValue;

            }
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
                return "";
            }
            set
            {
                if (Convert.ToString(value) != "")
                {
                    String xmlStr = value;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlStr);

                    XmlElement displayerElement = xmlDoc.DocumentElement;


                    width = int.Parse(displayerElement.GetAttribute("width"));
                    height = int.Parse(displayerElement.GetAttribute("height"));
                    textValue = displayerElement.GetAttribute("value");
                    bgColor = displayerElement.GetAttribute("bgColor");
                    fgColor = displayerElement.GetAttribute("fgColor");
                    fontSize = int.Parse(displayerElement.GetAttribute("fontSize"));
                    textAlign = int.Parse(displayerElement.GetAttribute("textAlign"));

                    update();

                    this.ApplySize(new DisplaySize(width), new DisplaySize(height));

                }
            }
        }


        [ApplicationVisible]
        public void setAppearance(int width, int height, string bgColor, string fgColor, int fontSize, int textAlign)
        {
            this.width = width;
            this.height = height;
            this.bgColor = bgColor;
            this.fgColor = fgColor;
            this.fontSize = fontSize;
            this.textAlign = textAlign;

            update();
        }

        [ApplicationVisible]
        public void setValue(string value)
        {
            textBox.Text = value;

        }

        [ApplicationVisible]
        public string getValue()
        {
            return textBox.Text;

        }

        [ApplicationVisible]
        public void setFocus(string id)
        {
            int i = 0;
            while (i < panel.Controls.Count)
            {
                if (panel.Controls[i].Name == id)
                {
                    ((TextBox)panel.Controls[i]).Focus();
                    return;
                }
                i++;
            }
        }



    }
}
