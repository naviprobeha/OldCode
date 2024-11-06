using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;

namespace Navipro.CashJet.ChartControls
{
    public class BarGraph
    {
        private XmlDocument xmlDoc;
        private string fileName;

        public BarGraph(XmlDocument xmlDoc, string fileName)
        {
            this.xmlDoc = xmlDoc;
            this.fileName = fileName;

        }

        public void render()
        {
            XmlElement docElement = xmlDoc.DocumentElement;

            Chart chart = new Chart();
            chart.ChartAreas.Add("Default");

            int width = 400;
            int height = 200;
            try
            {
                width = int.Parse(docElement.GetAttribute("width"));
            }
            catch (Exception) { }
            try
            {
                height = int.Parse(docElement.GetAttribute("height"));
            }
            catch (Exception) { }

            chart.Size = new Size(width, height);

            chart.ChartAreas["Default"].BackColor = Color.White;
            chart.ChartAreas["Default"].BackSecondaryColor  = Color.LightSteelBlue;
            chart.ChartAreas["Default"].BackGradientStyle = GradientStyle.DiagonalRight;

            chart.ChartAreas["Default"].AxisX.MajorGrid.LineColor = Color.LightSlateGray;
            chart.ChartAreas["Default"].AxisY.MajorGrid.LineColor = Color.LightSlateGray;
            chart.ChartAreas["Default"].AxisY.LabelStyle.Font = new Font("Verdana", 6.0f);
            chart.ChartAreas["Default"].AxisX.LabelStyle.Font = new Font("Verdana", 6.0f);


            chart.Titles.Add(new Title(docElement.GetAttribute("title"), Docking.Top, new Font("Verdana", 14.0f), Color.Gray));

            XmlNodeList dataNodeList = docElement.SelectNodes("data");
            int j = 0;
            while (j < dataNodeList.Count)
            {
                XmlElement dataElement = (XmlElement)dataNodeList[j];

                // Column
                chart.Series.Add("data"+j);
                chart.Series["data" + j].ChartType = SeriesChartType.Column;
                chart.Series["data" + j].IsValueShownAsLabel = true;
                chart.Series["data" + j]["PointWidth"] = "0.6";
                chart.Series["data" + j]["DrawingStyle"] = "Cylinder";

                chart.Series["data" + j].Font = new Font("Verdana", 6.0f, FontStyle.Italic);

                string title = dataElement.GetAttribute("title");

                chart.Series["data"+j].LegendText = title;
                if (j == 0) chart.Legends.Add(chart.Series["data"+j].Legend);

                // Data
                XmlNodeList nodeList = dataElement.SelectNodes("point");

                int i = 0;
                while (i < nodeList.Count)
                {

                    XmlElement pointElement = (XmlElement)nodeList[i];

                    chart.Series["data"+j].Points.AddXY(pointElement.GetAttribute("title"), Convert.ToDouble(pointElement.InnerText));

                    i++;
                }

                j++;
            }

            chart.SaveImage(fileName, ChartImageFormat.Bmp);

        }
    }
}
