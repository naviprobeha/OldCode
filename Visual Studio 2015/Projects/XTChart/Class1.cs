using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Dynamics.Framework.UI.Extensibility.WinForms;
using Microsoft.Dynamics.Framework.UI.Extensibility;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;

namespace XTCharts
{

    [ControlAddInExport("XTChart")]
    public class XTChartClass : StringControlAddInBase
    {
        protected override Control CreateControl()
        {
            Chart XChart = new Chart();
            XChart.Series.Add("Series1");
            XChart.ChartAreas.Add("Default");
            XChart.Width = 150;
            XChart.Height = 200;            
            XChart.MouseDoubleClick += XChart_MouseDoubleClick;
            
            return (XChart);
        }

        private void XChart_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.RaiseControlAddInEvent(1, "");
        }


        public override string Value
        {
            get
            {
                return base.Value;
            }
            set
            {                
                try
                {
                    if (Convert.ToString(value) != "")
                    {
                        // Clean up
                        ((Chart)this.Control).Series["Series1"].Points.Clear();                       
                        if (((Chart)this.Control).Series.IsUniqueName("Series2") == false)
                          ((Chart)this.Control).Series["Series2"].Points.Clear();
                        
                        // Load data
                        String TempXML = value;
                        XmlDocument XMLDoc = new XmlDocument();                          
                        XMLDoc.LoadXml(TempXML);

                        // Chart Type
                        XmlNode Node = XMLDoc.SelectSingleNode("Chart/ChartType");
                        switch (Convert.ToString(Node.InnerText))
                        { 
                            case "1":
                                // Lines
                                ((Chart)this.Control).Series["Series1"].ChartType = SeriesChartType.Line;
                                ((Chart)this.Control).BackColor = Color.AliceBlue;
                                break;
                            case "2":
                                //Doughnut
                                ((Chart)this.Control).Series["Series1"].ChartType = SeriesChartType.Doughnut;
                                ((Chart)this.Control).Series["Series1"]["PieDrawingStyle"] = "SoftEdge";                                
                                break;

                            case "3":
                                // 3D Lines
                                ((Chart)this.Control).ChartAreas["Default"].Area3DStyle.Enable3D = true;
                                ((Chart)this.Control).ChartAreas["Default"].Area3DStyle.IsRightAngleAxes = false;
                                ((Chart)this.Control).ChartAreas["Default"].Area3DStyle.Inclination = 40;
                                ((Chart)this.Control).ChartAreas["Default"].Area3DStyle.Rotation = 10;
                                ((Chart)this.Control).ChartAreas["Default"].Area3DStyle.LightStyle = LightStyle.Realistic;
                                ((Chart)this.Control).ChartAreas["Default"].BackColor = Color.Transparent;
                                ((Chart)this.Control).Series["Series1"].ChartType = SeriesChartType.Spline;
                                break;
                            case "4":
                                // Column
                                ((Chart)this.Control).Series["Series1"].ChartType = SeriesChartType.Column;
                                ((Chart)this.Control).BackColor = Color.AliceBlue;
                                break;
                            case "5":
                                // Column
                                ((Chart)this.Control).Series["Series1"].ChartType = SeriesChartType.BoxPlot;
                                ((Chart)this.Control).BackColor = Color.AliceBlue;
                                break;
                            default:
                                MessageBox.Show("Invalid ChartType " + Convert.ToString(Node.InnerText));
                                break;                        
                        }

                        // Chart Data
                        // Legends
                        XmlNode LegendNode = XMLDoc.SelectSingleNode("Chart/Data");
                        XmlNode LegendAttribute = LegendNode.Attributes.GetNamedItem("Title");
                        if (LegendAttribute != null) {
                            ((Chart)this.Control).Series["Series1"].LegendText = LegendAttribute.Value;
                            if (((Chart)this.Control).Legends.IsUniqueName("Legend1"))
                                ((Chart)this.Control).Legends.Add(((Chart)this.Control).Series["Series1"].Legend);
                        }

                        // Data
                        XmlNodeList Nodes = XMLDoc.SelectNodes("Chart/Data/*");
                        
                        // Series2
                        XmlNode Node2 = XMLDoc.SelectSingleNode("Chart/Data2");
                        Boolean SecondSeriesExist = false;
                        
                        if (Node2 != null) { SecondSeriesExist = true; }
                        if (SecondSeriesExist)
                        {
                            if (((Chart)this.Control).Series.IsUniqueName("Series2"))
                            {
                                ((Chart)this.Control).Series.Add("Series2");
                                ((Chart)this.Control).Series["Series2"].ChartType = ((Chart)this.Control).Series["Series1"].ChartType;


                                // Legend

                                LegendNode = XMLDoc.SelectSingleNode("Chart/Data2");
                                LegendAttribute = LegendNode.Attributes.GetNamedItem("Title");
                                if (LegendAttribute != null)
                                {
                                    ((Chart)this.Control).Series["Series2"].LegendText = LegendAttribute.Value;
                                    
                                }
                            }
                        }

                        for (int i = 0; i < Nodes.Count ; i++) {
                            Node = Nodes.Item(i);                            
                            XmlNode Attr = Node.Attributes.GetNamedItem("Title");                            
                            ((Chart)this.Control).Series["Series1"].Points.AddXY(Attr.Value, Convert.ToDouble(Node.InnerText));
                            
                            // Series2
                            if (SecondSeriesExist)
                            {
                                // Formatting
                                ((Chart)this.Control).BackColor = Color.White;
                                
                                // Data
                                Node2 = XMLDoc.SelectSingleNode("Chart/Data2/" + Node.Name);
                                XmlNode Attr2 = Node2.Attributes.GetNamedItem("Title");                                
                                ((Chart)this.Control).Series["Series2"].Points.AddXY(Attr.Value, Convert.ToDouble(Node2.InnerText));
                            }
                        }
                    }                
                }
                catch { MessageBox.Show("Error with data from NAV " + value);  }
            }
        }
    
    
    }

}
