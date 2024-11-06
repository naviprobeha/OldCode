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

    [ControlAddInExport("Navipro.CashJet.AddIns.ButtonPanel")]
    public class ButtonPanel : StringControlAddInBase
    {
        private Panel buttonPanel;

        protected override System.Windows.Forms.Control CreateControl()
        {
            buttonPanel = new Panel();
            //buttonPanel.AutoSize = true;

            return buttonPanel;
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
                int buttonWidth = 60;
                int buttonHeight = 54;
                if (Convert.ToString(value) != "")
                {
                    buttonPanel.Controls.Clear();

                    String xmlStr = value;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlStr);

                    XmlNodeList nodeList = xmlDoc.SelectNodes("buttons/button");
                    int i = 0;
                    int maxWidth = 0;
                    int maxHeight = 0;

                    while (i < nodeList.Count)
                    {
                        XmlElement buttonElement = (XmlElement)nodeList[i];

                        
                        int posX = int.Parse(buttonElement.GetAttribute("posX"));
                        int posY = int.Parse(buttonElement.GetAttribute("posY"));
                        int width = int.Parse(buttonElement.GetAttribute("width"));
                        int height = int.Parse(buttonElement.GetAttribute("height"));
                        string caption = buttonElement.GetAttribute("caption");
                        string id = buttonElement.GetAttribute("id");
                        string bgColor = buttonElement.GetAttribute("bgColor");
                        string fgColor = buttonElement.GetAttribute("fgColor");
                        int fontSize = int.Parse(buttonElement.GetAttribute("fontSize"));

                       
                        Button button = new Button();
                        button.Location = new Point((posX - 1) * buttonWidth, (posY - 1) * buttonHeight);
                        button.FlatStyle = FlatStyle.Flat;
                        button.FlatAppearance.BorderColor = Color.White;

                        button.Width = width * buttonWidth;
                        button.Height = height * buttonHeight;
                        button.Text = caption;
                        button.Name = id;
                        button.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)fontSize, System.Drawing.FontStyle.Bold);
                        button.Click += new EventHandler(button_Click);

                        try
                        {
                            button.BackColor = Color.FromName(bgColor);
                        }
                        catch (Exception) { };
                        try
                        {
                            button.ForeColor = Color.FromName(fgColor);
                        }
                        catch (Exception) { };

                        if (maxWidth < ((posX - 1) * buttonWidth) + (width * buttonWidth)) maxWidth = ((posX - 1) * buttonWidth) + (width * buttonWidth);
                        if (maxHeight < ((posY - 1) * buttonHeight) + (height * buttonHeight)) maxHeight = ((posY - 1) * buttonHeight) + (height * buttonHeight);

                        buttonPanel.Controls.Add(button);

                        i++;
                    }

                    //System.Windows.Forms.MessageBox.Show(maxWidth + ":" + maxHeight);
                    this.ApplySize(new DisplaySize(maxWidth*1), new DisplaySize(maxHeight*1));

                }
            }
        }

        void button_Click(object sender, EventArgs e)
        {
            this.RaiseControlAddInEvent(0, ((Button)sender).Name);
        }

        [ApplicationVisible]
        public void setCaption(string id, string caption)
        {
            int i = 0;
            while (i < buttonPanel.Controls.Count)
            {
                if (buttonPanel.Controls[i].Name == id) ((Button)buttonPanel.Controls[i]).Text = caption;
                i++;
            }
        }

    }
}
