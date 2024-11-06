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

    [ControlAddInExport("Navipro.CashJet.AddIns.DisplayerPanel")]
    public class DisplayerPanel : StringControlAddInBase
    {
        private Panel panel;
        private string setFocusID = "";

        protected override System.Windows.Forms.Control CreateControl()
        {
            panel = new Panel();
            panel.AutoSize = true;
            

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
                int boxWidth = 45;
                int boxHeight = 30;

                if (Convert.ToString(value) != "")
                {
                    panel.Controls.Clear();

                    String xmlStr = value;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlStr);

                    XmlNodeList nodeList = xmlDoc.SelectNodes("displayers/displayer");
                    int i = 0;


                    while (i < nodeList.Count)
                    {
                        XmlElement displayerElement = (XmlElement)nodeList[i];


                        int posX = int.Parse(displayerElement.GetAttribute("posX"));
                        int posY = int.Parse(displayerElement.GetAttribute("posY"));
                        int width = int.Parse(displayerElement.GetAttribute("width"));
                        int height = int.Parse(displayerElement.GetAttribute("height"));
                        string caption = displayerElement.GetAttribute("caption");
                        string textValue = displayerElement.GetAttribute("value");
                        string bgColor = displayerElement.GetAttribute("bgColor");
                        string fgColor = displayerElement.GetAttribute("fgColor");
                        string id = displayerElement.GetAttribute("id");
                        int editable = int.Parse(displayerElement.GetAttribute("editable"));
                        int fontSize = int.Parse(displayerElement.GetAttribute("fontSize"));

                        try
                        {
                            if (displayerElement.GetAttribute("focus") == "true") setFocusID = id;
                        }
                        catch (Exception) { }

                        GroupBox groupBox = new GroupBox();
                        groupBox.Text = caption;
                        groupBox.Name = id + "_box";
                        groupBox.Location = new Point(((posX - 1) * (boxWidth))+5, ((posY - 1) * (boxHeight))+12);

                        TextBox textBox = new TextBox();
                        textBox.BorderStyle = BorderStyle.None;

                        textBox.Location = new Point(5, 16);

                        textBox.Width = (width * (boxWidth)) - 20;
                        textBox.Height = height * boxHeight;
                        textBox.Text = caption;
                        textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)fontSize, System.Drawing.FontStyle.Bold);
                        textBox.Text = textValue;
                        if (editable == 0) textBox.ReadOnly = true;
                        if (editable == 1)
                        {
                            textBox.Multiline = true;
                            textBox.AcceptsReturn = true;
                            textBox.Height = height * 24;
                        }
                        groupBox.Width = (width * boxWidth) - 6;
                        groupBox.Height = (height * boxHeight) + 12;
                        

                        textBox.Name = id;
                        textBox.TabIndex = 100;
                        if (setFocusID == id) textBox.TabIndex = 1;

                        textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);

                        try
                        {
                            textBox.BackColor = Color.FromName(bgColor);
                        }
                        catch (Exception) { };
                        try
                        {
                            textBox.ForeColor = Color.FromName(fgColor);
                        }
                        catch (Exception) { };

                        groupBox.Controls.Add(textBox);

                        panel.Controls.Add(groupBox);

                        i++;
                    }

                    this.RaiseControlAddInEvent(0, "REFRESH");

                }

            }
        }

        void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //System.Windows.Forms.MessageBox.Show(sender.ToString());
                this.RaiseControlAddInEvent(0, ((TextBox)sender).Name);
                e.Handled = true;
            }
        }

        [ApplicationVisible]
        public void setCaption(string id, string caption)
        {
            int i = 0;
            while (i < panel.Controls.Count)
            {
                if (panel.Controls[i].Name == id)
                {
                    ((GroupBox)panel.Controls[i]).Text = caption;
                    return;
                }
                i++;
            }
        }

        [ApplicationVisible]
        public void setValue(string id, string value)
        {
            int i = 0;
            while (i < panel.Controls.Count)
            {
                if (panel.Controls[i].Name == id + "_box")
                {
                    ((TextBox)((GroupBox)panel.Controls[i]).Controls[0]).Text = value;
                    return;
                }
                i++;
            }
        }

        [ApplicationVisible]
        public string getValue(string id)
        {
            int i = 0;
            while (i < panel.Controls.Count)
            {
                if (panel.Controls[i].Name == id + "_box")
                {
                    return ((TextBox)((GroupBox)panel.Controls[i]).Controls[0]).Text;
                }
                i++;
            }
            return "";
        }

        [ApplicationVisible]
        public void setFocus(string id)
        {
            int i = 0;
            while (i < panel.Controls.Count)
            {
                if (panel.Controls[i].Name == id + "_box")
                {
                    ((TextBox)((GroupBox)panel.Controls[i]).Controls[0]).Focus();
                    return;
                }
                i++;
            }
        }

    }
}
