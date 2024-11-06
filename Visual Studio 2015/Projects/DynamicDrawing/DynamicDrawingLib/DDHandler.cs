using System;
using System.Xml;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Navipro.DynamicDrawing
{
    public class DDHandler
    {
        private Hashtable argumentTable;
        private XmlDocument xmlDoc;
        private int width;
        private int height;

        public DDHandler()
        {
            argumentTable = new Hashtable();
        }

        public void init(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void setDrawingFile(string fileName)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            if (xmlDoc.DocumentElement == null)
            {
                throw new Exception("DocumentElement is null. Probably an invalid xml document.");
            }
        }
        
        public void setArgument(string key, string value)
        {
            if (argumentTable.Contains(key))
            {
                argumentTable[key] = value;
            }
            else
            {
                argumentTable.Add(key, value);
            }

        }

        public void exportDrawing(string fileName)
        {
            Bitmap background = new Bitmap(width, height);

            Graphics graphics = Graphics.FromImage(background);

            XmlElement docElement = xmlDoc.DocumentElement;

            XmlNodeList nodeList = docElement.ChildNodes;
            int i = 0;
            while (i < nodeList.Count)
            {
                XmlNode drawingNode = nodeList[i];

                handleDrawingElement(graphics, drawingNode);

                i++;
            }

            background.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);

        }

        private void handleDrawingElement(Graphics graphics, XmlNode drawingNode)
        {
            if (drawingNode.Name == "line") drawLine(graphics, (XmlElement)drawingNode);
            if (drawingNode.Name == "ellipse") drawEllipse(graphics, (XmlElement)drawingNode);
            if (drawingNode.Name == "filledRectangle") drawFilledRectangle(graphics, (XmlElement)drawingNode);
            if (drawingNode.Name == "text") drawText(graphics, (XmlElement)drawingNode);
        }

        private void drawLine(Graphics graphics, XmlElement drawingElement)
        {
            int red = int.Parse(drawingElement.GetAttribute("colorR"));
            int green = int.Parse(drawingElement.GetAttribute("colorG"));
            int blue = int.Parse(drawingElement.GetAttribute("colorB"));
            int lineWidth = int.Parse(drawingElement.GetAttribute("lineWidth"));
            int x1 = int.Parse(drawingElement.GetAttribute("x1"));
            int y1 = int.Parse(drawingElement.GetAttribute("y1"));
            int x2 = int.Parse(drawingElement.GetAttribute("x2"));
            int y2 = int.Parse(drawingElement.GetAttribute("y2"));
            string type = drawingElement.GetAttribute("type");

            Color color = Color.FromArgb(red, green, blue);
            Pen pen = new Pen(color, lineWidth);

            if (type == "dotted") pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            if (type == "dashed") pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (type == "dashDotted") pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

            graphics.DrawLine(pen, new Point(x1, y1), new Point(x2, y2));

        }

        private void drawEllipse(Graphics graphics, XmlElement drawingElement)
        {
            int red = int.Parse(drawingElement.GetAttribute("colorR"));
            int green = int.Parse(drawingElement.GetAttribute("colorG"));
            int blue = int.Parse(drawingElement.GetAttribute("colorB"));
            int lineWidth = int.Parse(drawingElement.GetAttribute("lineWidth"));
            int x = int.Parse(drawingElement.GetAttribute("x"));
            int y = int.Parse(drawingElement.GetAttribute("y"));
            int width = int.Parse(drawingElement.GetAttribute("width"));
            int height = int.Parse(drawingElement.GetAttribute("height"));
            string type = drawingElement.GetAttribute("type");

            Color color = Color.FromArgb(red, green, blue);
            Pen pen = new Pen(color, lineWidth);

            if (type == "dotted") pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            if (type == "dashed") pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (type == "dashDotted") pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

            graphics.DrawEllipse(pen, x, y, width, height);

        }


        private void drawText(Graphics graphics, XmlElement drawingElement)
        {
            int red = int.Parse(drawingElement.GetAttribute("colorR"));
            int green = int.Parse(drawingElement.GetAttribute("colorG"));
            int blue = int.Parse(drawingElement.GetAttribute("colorB"));
            string text = drawingElement.GetAttribute("text");
            string fontName = drawingElement.GetAttribute("font");
            int size = int.Parse(drawingElement.GetAttribute("size"));
            string style = drawingElement.GetAttribute("style");
            string align = drawingElement.GetAttribute("align");
            string direction = drawingElement.GetAttribute("direction");

            int x = int.Parse(drawingElement.GetAttribute("x"));
            int y = int.Parse(drawingElement.GetAttribute("y"));
            int width = int.Parse(drawingElement.GetAttribute("width"));
            int height = int.Parse(drawingElement.GetAttribute("height"));

            Color color = Color.FromArgb(red, green, blue);
            SolidBrush brush = new SolidBrush(color);

            Font font = new Font(fontName, size, FontStyle.Regular);
            if (style == "bold") font = new Font(fontName, size, FontStyle.Bold);
            if (style == "italic") font = new Font(fontName, size, FontStyle.Italic);
            if (style == "strikeout") font = new Font(fontName, size, FontStyle.Strikeout);
            if (style == "underline") font = new Font(fontName, size, FontStyle.Underline);

            StringFormat stringFormat = new StringFormat();
            if (align == "center") stringFormat.Alignment = StringAlignment.Center;

            if (direction == "vertical") stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            
            graphics.DrawString(text, font, brush, new RectangleF(new PointF(x, y), new SizeF(width, height)), stringFormat);

        }

        private void drawFilledRectangle(Graphics graphics, XmlElement drawingElement)
        {
            int red = int.Parse(drawingElement.GetAttribute("colorR"));
            int green = int.Parse(drawingElement.GetAttribute("colorG"));
            int blue = int.Parse(drawingElement.GetAttribute("colorB"));
            int width = int.Parse(drawingElement.GetAttribute("width"));
            int height = int.Parse(drawingElement.GetAttribute("height"));
            int x = int.Parse(drawingElement.GetAttribute("x"));
            int y = int.Parse(drawingElement.GetAttribute("y"));

            Color color = Color.FromArgb(red, green, blue);
            SolidBrush brush = new SolidBrush(color);
            graphics.FillRectangle(brush, x, y, width, height);

        }

    }
}
