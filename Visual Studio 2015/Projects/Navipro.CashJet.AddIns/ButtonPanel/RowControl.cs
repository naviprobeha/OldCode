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

    [ControlAddInExport("Navipro.CashJet.AddIns.RowControl")]
    public class RowControl : WinFormsControlAddInBase
    {
        private RowItemCollection rows;
        private Panel panel;

        private int rowsStartPosY = 10;
        private int rowHeight = 50;
        private int rowMargin = 10;
        private string _selectedRowId;

        protected override void OnInitialize()
        {

            base.OnInitialize();

            DisplaySize width = new DisplaySize(50, 600, 1600);
            DisplaySize height = new DisplaySize(50, 400, 1000);

            this.ApplySize(width, height);

            _selectedRowId = "";
        }

        protected override System.Windows.Forms.Control CreateControl()
        {
            if (rows == null) rows = new RowItemCollection();

            panel = new Panel();
            panel.AutoSize = true;
            panel.BackColor = Color.White;
            panel.Resize += new EventHandler(panel_Resize);
          
            //vScrollBar = new VScrollBar();
            //vScrollBar.Dock = DockStyle.Right;

            panel.HorizontalScroll.Maximum = 0;
            panel.AutoScroll = false;
            panel.VerticalScroll.Visible = false;
            panel.AutoScroll = true;

            redraw();
            return panel;

            
        }

        void panel_Resize(object sender, EventArgs e)
        {
            
            redraw();
        }

 

        private void redraw()
        {
            //panel.Controls.Clear();

            if (rows.Count == 1)
            {
                panel.Controls.Clear();
                
            }

            if (rows.Count < panel.Controls.Count)
            {
                panel.Controls.Clear();
            }
         
            int i = 0;
            for (i = 0; i < rows.Count; i++)
            {
                addRowPanel(rows[i], i);
            }
                        
        }

        private void addRowPanel(RowItem rowItem, int order)
        {
            panel.VerticalScroll.Value = 0;

            Panel rowPanel = null;
            Control[] controlArray = panel.Controls.Find(rowItem.id, false);
            if (controlArray.Length > 0)
            {
                rowPanel = ((Panel)controlArray[0]);
            }
            else
            {
               
                rowPanel = new Panel();
                panel.Controls.Add(rowPanel);                
            }

            rowPanel.Name = rowItem.id;
            rowPanel.Size = new Size(panel.Size.Width - 20, rowItem.height);
            rowPanel.MinimumSize = new Size(50, 26);
            rowPanel.Location = new Point(1, (order * (rowHeight + rowMargin)) + rowsStartPosY);
            rowPanel.BackColor = Color.White;
            rowPanel.AutoSize = true;
            rowPanel.BorderStyle = BorderStyle.None;
            rowPanel.Paint += new PaintEventHandler(rowPanel_Paint);
            rowPanel.Click += new EventHandler(rowPanel_Click);

            panel.ScrollControlIntoView(rowPanel);

            if (_selectedRowId == rowItem.id)
            {
                rowPanel.BackColor = Color.FromArgb(229, 229, 229);
            }


            if (rowItem.fields.Count < rowPanel.Controls.Count)
            {
                rowPanel.Controls.Clear();
            }

            int i = 0;
            while (i < rowItem.fields.Count)
            {
                RowItemField field = rowItem.fields[i];

                Label label = null;
                Control[] fieldArray = rowPanel.Controls.Find(field.name, false);
                if (fieldArray.Length > 0)
                {
                    label = ((Label)fieldArray[0]);
                }
                else
                {
                    label = new Label();
                    rowPanel.Controls.Add(label);
                }

                label.Text = field.text;
                label.Name = field.name;

                if (field.relative.ToUpper() == "LEFT")
                {
                    label.Location = new Point(field.posX, field.posY);
                }
                if (field.relative.ToUpper() == "RIGHT")
                {
                    label.Location = new Point(rowPanel.Width - field.posX, field.posY);
                }

                FontStyle fontStyle = FontStyle.Regular;
                if (field.fontStyle.ToUpper() == "REGULAR") fontStyle = FontStyle.Regular;
                if (field.fontStyle.ToUpper() == "ITALIC") fontStyle = FontStyle.Italic;
                if (field.fontStyle.ToUpper() == "BOLD") fontStyle = FontStyle.Bold;
                if (field.fontStyle.ToUpper() == "STRIKEOUT") fontStyle = FontStyle.Strikeout;
                if (field.fontStyle.ToUpper() == "UNDERLINE") fontStyle = FontStyle.Underline;

                label.Font = new System.Drawing.Font(field.fontName, field.fontSize, fontStyle);

                if (field.anchor.ToUpper() == "LEFT")
                {
                    label.Size = new Size(field.width, field.height);
                }
 
                if (field.anchor.ToUpper() == "RIGHT")
                {
                    label.Size = new Size(rowPanel.Width - field.width, field.height);
                }
                
                
                if (field.foreColor != "")
                {
                    label.ForeColor = getColor(field.foreColor);
                }
                if (field.backColor != "")
                {
                    label.BackColor = getColor(field.backColor);
                }

                ContentAlignment contentAlignment = ContentAlignment.TopLeft;
                if (field.textAlign.ToUpper() == "TOPLEFT") contentAlignment = ContentAlignment.TopLeft;
                if (field.textAlign.ToUpper() == "TOPCENTER") contentAlignment = ContentAlignment.TopCenter;
                if (field.textAlign.ToUpper() == "TOPRIGHT") contentAlignment = ContentAlignment.TopRight;
                if (field.textAlign.ToUpper() == "MIDDLELEFT") contentAlignment = ContentAlignment.MiddleLeft;
                if (field.textAlign.ToUpper() == "MIDDLECENTER") contentAlignment = ContentAlignment.MiddleCenter;
                if (field.textAlign.ToUpper() == "MIDDLERIGHT") contentAlignment = ContentAlignment.MiddleRight;
                if (field.textAlign.ToUpper() == "BOTTOMLEFT") contentAlignment = ContentAlignment.BottomLeft;
                if (field.textAlign.ToUpper() == "BOTTOMCENTER") contentAlignment = ContentAlignment.BottomCenter;
                if (field.textAlign.ToUpper() == "BOTTOMRIGHT") contentAlignment = ContentAlignment.BottomRight;

                label.TextAlign = contentAlignment;

                label.Click += new EventHandler(label_Click);
                               
                i++;
            }
            
        }

        void label_Click(object sender, EventArgs e)
        {
            //((Panel)((Label)sender).Parent).BackColor = Color.FromArgb(229, 229, 229);   
            rowPanel_Click(((Label)sender).Parent, e);
        }

        void rowPanel_Click(object sender, EventArgs e)
        {
            if (_selectedRowId != "")
            {
                Control[] controlArray = panel.Controls.Find(_selectedRowId, false);
                if (controlArray.Length > 0) ((Panel)controlArray[0]).BackColor = Color.White;
            }
            
            ((Panel)sender).BackColor = Color.FromArgb(229, 229, 229);
            _selectedRowId = ((Panel)sender).Name;
        }

        void rowPanel_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(0, 0, ((Panel)sender).ClientRectangle.Width - 1, ((Panel)sender).ClientRectangle.Height - 1);
            Pen p = new Pen(Color.LightGray, 1);
            e.Graphics.DrawRectangle(p, r);
        }

        public override bool AllowCaptionControl
        {
            get
            {
                return false;
            }
        }

        private void raiseEvent(object sender, EventArgs e)
        {            
            
        }

        [ApplicationVisible]
        public string getSelectedRow()
        {
            return _selectedRowId;
        }

        [ApplicationVisible]
        public void setSelectedRow(string id)
        {
            _selectedRowId = id;
        }

        [ApplicationVisible]
        public void updateRows(string xmlDocStr)
        {
            rows = new RowItemCollection();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlDocStr);

            XmlElement docElement = xmlDoc.DocumentElement;

            this.rowsStartPosY = int.Parse(docElement.GetAttribute("rowsStartPosY"));
            this.rowHeight = int.Parse(docElement.GetAttribute("rowHeight"));
            this.rowMargin = int.Parse(docElement.GetAttribute("rowMargin"));


            XmlNodeList lineNodeList = docElement.SelectNodes("line");
            int i = 0;
            while (i < lineNodeList.Count)
            {
                XmlElement xmlLineElement = (XmlElement)lineNodeList[i];

                RowItem rowItem = new RowItem();
                rowItem.id = xmlLineElement.GetAttribute("lineNo");
                rowItem.height = rowHeight;

                int j = 0;
                XmlNodeList fieldNodeList = xmlLineElement.SelectNodes("field");

                while (j < fieldNodeList.Count)
                {
                    XmlElement xmlFieldElement = (XmlElement)fieldNodeList[j];

                    string name = xmlFieldElement.GetAttribute("name");
                    string text = xmlFieldElement.GetAttribute("text");
                    int posX = int.Parse(xmlFieldElement.GetAttribute("posX"));
                    int posY = int.Parse(xmlFieldElement.GetAttribute("posY"));
                    string relative = xmlFieldElement.GetAttribute("relative");
                    string fontName = xmlFieldElement.GetAttribute("fontName");
                    int fontSize = int.Parse(xmlFieldElement.GetAttribute("fontSize"));
                    string fontStyle = xmlFieldElement.GetAttribute("fontStyle");
                    int width = int.Parse(xmlFieldElement.GetAttribute("width"));
                    int height = int.Parse(xmlFieldElement.GetAttribute("height"));
                    string textAlign = xmlFieldElement.GetAttribute("textAlign");
                    string anchor = xmlFieldElement.GetAttribute("anchor");
                    string foreColor = xmlFieldElement.GetAttribute("foreColor");
                    string backColor = xmlFieldElement.GetAttribute("backColor");

                    rowItem.createField(name, text, posX, posY, relative, fontName, fontSize, fontStyle, width, height, textAlign, anchor, foreColor, backColor);

                    j++;
                }

                rows.Add(rowItem);

                i++;
            }

            redraw();
            
        }

        private Color getColor(string color)
        {
            if (color[0] == '#')
            {
                return System.Drawing.ColorTranslator.FromHtml(color);
            }
            return System.Drawing.Color.FromName(color);

        }


    }
}
