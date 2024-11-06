using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Navipro.Cashjet.Library;
using System.Web.UI.DataVisualization.Charting;

namespace Navipro.Cashjet.Dashboard
{
    public partial class _Default : System.Web.UI.Page
    {
        private Database database;
        private Configuration configuration;

        protected void Page_Load(object sender, EventArgs e)
        {
            configuration = new Configuration();
            configuration.init();

            database = new Database(null, configuration);

            if (!this.IsPostBack)
            {
                cashSiteBox.DataSource = CashSite.getCollection(database);
                cashSiteBox.DataBind();

                cashSiteBox.Items.Insert(0, new ListItem(Translation.getTranslation("Alla butiker", configuration.language), "-"));
                

                DateHelper.fillYearBox(ref yearBox);
                DateHelper.fillMonthBox(ref monthBox);
                DateHelper.fillDayBox(ref dayBox);
                DateHelper.fillIntervals(ref intervalBox, configuration);

                DateHelper.fillYearBox(ref year2Box);
                DateHelper.fillMonthBox(ref month2Box);
                DateHelper.fillDayBox(ref day2Box);


                //levelBox.Items.Add(new ListItem("Normal", "0"));
                //levelBox.Items.Add(new ListItem("Expert", "1"));
                //levelBox.SelectedValue = "0";

                //reportType.Items.Add(new ListItem("Omsättning", "0"));
                //reportType.Items.Add(new ListItem("Kunder", "1"));
                //reportType.Items.Add(new ListItem("Artiklar", "2"));
                //reportType.Items.Add(new ListItem("Top 10", "3"));
            }

            //if (levelBox.SelectedValue == "1") Response.Redirect("expert.aspx");
            if (intervalBox.SelectedValue == "4") intervalDate.Visible = true;
            if (intervalBox.SelectedValue != "4") intervalDate.Visible = false;

            DateTime dateTime = new DateTime(int.Parse(yearBox.Text), int.Parse(monthBox.Text), int.Parse(dayBox.Text));
            DateTime dateTime2 = new DateTime(int.Parse(year2Box.Text), int.Parse(month2Box.Text), int.Parse(day2Box.Text));

            bool includeVat = true;
            if (pricesInclVatBox.SelectedValue == "1") includeVat = false;

            DataCollection dataCollection = DataEntry.getCollection(database, cashSiteBox.Text, dateTime, int.Parse(intervalBox.Text), dateTime2, includeVat);
            dataEntries.ItemDataBound += new RepeaterItemEventHandler(dataEntries_ItemDataBound);
            dataEntries.DataSource = dataCollection;
            dataEntries.DataBind();

            
            turnoverChart.ChartAreas["Default"].BackColor = System.Drawing.Color.White;
            turnoverChart.ChartAreas["Default"].BackSecondaryColor = System.Drawing.Color.LightSteelBlue;
            turnoverChart.ChartAreas["Default"].BackGradientStyle = System.Web.UI.DataVisualization.Charting.GradientStyle.DiagonalRight;

            turnoverChart.ChartAreas["Default"].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightSlateGray;
            turnoverChart.ChartAreas["Default"].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightSlateGray;
            turnoverChart.ChartAreas["Default"].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 6.0f);
            turnoverChart.ChartAreas["Default"].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 6.0f);



            turnoverChart.Titles.Add(new Title(Translation.getTranslation("Omsättning", configuration.language), Docking.Top, new System.Drawing.Font("Verdana", 12.0f), System.Drawing.Color.Gray));

            turnoverChart.Series["currentYear"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            turnoverChart.Series["currentYear"].IsValueShownAsLabel = false;
            turnoverChart.Series["currentYear"]["PointWidth"] = "0.6";
            turnoverChart.Series["currentYear"]["DrawingStyle"] = "Cylinder";
            turnoverChart.Series["currentYear"].LegendText = Translation.getTranslation("Omsättning", configuration.language);
            turnoverChart.Legends.Add(turnoverChart.Series["currentYear"].Legend);
            turnoverChart.Legends["Legend1"].LegendStyle = LegendStyle.Table;
            turnoverChart.Legends["Legend1"].TableStyle = LegendTableStyle.Auto;
            turnoverChart.Legends["Legend1"].Docking = Docking.Bottom;
            turnoverChart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            turnoverChart.ChartAreas[0].AxisX.Interval = 1;

            turnoverChart.Series["currentYear"].Font = new System.Drawing.Font("Verdana", 6.0f, System.Drawing.FontStyle.Italic);

            turnoverChart.Series["lastYear"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            turnoverChart.Series["lastYear"].IsValueShownAsLabel = false;
            turnoverChart.Series["lastYear"]["PointWidth"] = "0.6";
            turnoverChart.Series["lastYear"]["DrawingStyle"] = "Cylinder";
            turnoverChart.Series["lastYear"].LegendText = Translation.getTranslation("Omsättning f.å.", configuration.language);

            turnoverChart.Series["lastYear"].Font = new System.Drawing.Font("Verdana", 6.0f, System.Drawing.FontStyle.Italic);

            turnoverChart.Series["budget"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            turnoverChart.Series["budget"].IsValueShownAsLabel = false;
            turnoverChart.Series["budget"]["PointWidth"] = "0.6";
            turnoverChart.Series["budget"]["DrawingStyle"] = "Cylinder";
            turnoverChart.Series["budget"].LegendText = "Budget";

            turnoverChart.Series["budget"].Font = new System.Drawing.Font("Verdana", 6.0f, System.Drawing.FontStyle.Italic);

            int i = 0;
            while (i < dataCollection.Count)
            {
                DataEntry dataEntry = dataCollection[i];
                if (dataEntry.title.ToUpper() != "TOTAL")
                {
                    turnoverChart.Series["currentYear"].Points.AddXY(dataEntry.title, dataEntry.turnOver);
                    turnoverChart.Series["lastYear"].Points.AddXY(dataEntry.title, dataEntry.turnOverLastYear);
                    turnoverChart.Series["budget"].Points.AddXY(dataEntry.title, dataEntry.budget);
                }
                i++;
            }



        }


        void dataEntries_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataEntry dataEntry = (DataEntry)e.Item.DataItem;

            Chart hourChart = (Chart)e.Item.FindControl("hourChart");
            hourChart.ImageLocation = "~/_assets/charts/hour"+dataEntry.cashSite+"_"+dataEntry.fromDate.ToString("yyyyMMdd");
            hourChart.ChartAreas["Default"].BackColor = System.Drawing.Color.White;
            hourChart.ChartAreas["Default"].BackSecondaryColor = System.Drawing.Color.LightSteelBlue;
            hourChart.ChartAreas["Default"].BackGradientStyle = System.Web.UI.DataVisualization.Charting.GradientStyle.DiagonalRight;

            hourChart.ChartAreas["Default"].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightSlateGray;
            hourChart.ChartAreas["Default"].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightSlateGray;
            hourChart.ChartAreas["Default"].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 6.0f);
            hourChart.ChartAreas["Default"].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 6.0f);

            hourChart.Titles.Add(new Title(Translation.getTranslation("Antal köp per timme", configuration.language), Docking.Top, new System.Drawing.Font("Verdana", 12.0f), System.Drawing.Color.Gray));

            hourChart.Series["hours"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            hourChart.Series["hours"].IsValueShownAsLabel = false;
            hourChart.Series["hours"]["PointWidth"] = "0.6";
            hourChart.Series["hours"]["DrawingStyle"] = "Cylinder";
            hourChart.Series["hours"].LabelAngle = 90;

            hourChart.Series["hours"].Font = new System.Drawing.Font("Verdana", 6.0f, System.Drawing.FontStyle.Italic);

            DataHourCollection dataHourCollection = dataEntry.dataHourCollection;


            int j = 0;
            while (j < dataHourCollection.Count)
            {
                DataHourEntry dataHourEntry = dataHourCollection[j];

                hourChart.Series["hours"].Points.AddXY(dataHourEntry.hour, dataHourEntry.count);

                j++;
            }


            /*
            Chart productGroupChart = (Chart)e.Item.FindControl("productGroupChart");
            productGroupChart.ImageLocation = "~/_assets/charts/productGroups" + dataEntry.cashSite + "_" + dataEntry.fromDate.ToString("yyyyMMdd");
            productGroupChart.ChartAreas["Default"].BackColor = System.Drawing.Color.White;
            productGroupChart.ChartAreas["Default"].BackSecondaryColor = System.Drawing.Color.LightSteelBlue;
            productGroupChart.ChartAreas["Default"].BackGradientStyle = System.Web.UI.DataVisualization.Charting.GradientStyle.DiagonalRight;

            productGroupChart.ChartAreas["Default"].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightSlateGray;
            productGroupChart.ChartAreas["Default"].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightSlateGray;
            productGroupChart.ChartAreas["Default"].AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 6.0f);
            productGroupChart.ChartAreas["Default"].AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 6.0f);

            productGroupChart.Titles.Add(new Title("Försäljning per produktgrupp", Docking.Top, new System.Drawing.Font("Verdana", 12.0f), System.Drawing.Color.Gray));

            productGroupChart.Series["productGroups"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            productGroupChart.Series["productGroups"].IsValueShownAsLabel = false;
            productGroupChart.Series["productGroups"]["PointWidth"] = "0.6";
            productGroupChart.Series["productGroups"]["DrawingStyle"] = "Cylinder";

            productGroupChart.Series["productGroups"].Font = new System.Drawing.Font("Verdana", 6.0f, System.Drawing.FontStyle.Italic);

            DataProductCollection dataProductCollection = dataEntry.dataProductCollection;

            j = 0;
            while (j < dataProductCollection.Count)
            {
                DataProductEntry dataProductEntry = dataProductCollection[j];

                productGroupChart.Series["productGroups"].Points.AddXY(dataProductEntry.description, dataProductEntry.quantity);

                j++;
            }

            */
        }

    }
}
