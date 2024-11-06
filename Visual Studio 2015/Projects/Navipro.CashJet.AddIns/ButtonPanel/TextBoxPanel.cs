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

    [ControlAddInExport("Navipro.CashJet.AddIns.TextBoxPanel")]
    public class TextBoxPanel : StringControlAddInBase
    {
        private Panel panel;

        protected override System.Windows.Forms.Control CreateControl()
        {
            panel = new Panel();
            panel.Width = 200;
            panel.Height = 250;

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
                        int fontSize = int.Parse(displayerElement.GetAttribute("fontSize"));
                        string id = displayerElement.GetAttribute("id");

                        Label label = new Label();
                        label.Text = caption;
                        label.Name = id+"_label";                        
                        label.Location = new Point(((posX - 1) * 50), ((posY - 1) * 30)+2);
                        label.Width = 100;
                        label.Height = height * 30;
                        label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);


                        TextBox textBox = new TextBox();
                        textBox.BorderStyle = BorderStyle.None;
                        textBox.Name = id;
                        textBox.Location = new Point(((posX - 1) * 50)+100, ((posY - 1) * 30));

                        textBox.Width = width * 50;
                        textBox.Height = height * 30;
                        textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)fontSize, System.Drawing.FontStyle.Bold);
                        textBox.Text = textValue;
                        textBox.TextAlign = HorizontalAlignment.Right;
                        textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);

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

                        panel.Controls.Add(label);
                        panel.Controls.Add(textBox);

                        i++;
                    }

                }
            }
        }

        void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                RaiseControlAddInEvent(0, ((TextBox)sender).Name + ";" + ((TextBox)sender).Text);
            }
        }

        [ApplicationVisible]
        public void setCaption(string id, string caption)
        {
            int i = 0;
            while (i < panel.Controls.Count)
            {
                if (panel.Controls[i].Name == id + "_label")
                {
                    ((Label)panel.Controls[i]).Text = caption;
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
                if (panel.Controls[i].Name == id)
                {
                    ((TextBox)panel.Controls[i]).Text = value;
                    return;
                }
                i++;
            }
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
