using System;
using System.Collections;
using System.IO;
using System.Data;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ReportMaterialOverview.
	/// </summary>
	public class ReportMaterialOverview : Report
	{
		private Hashtable parameterTable;
		private Database database;

		public ReportMaterialOverview()
		{
			//
			// TODO: Add constructor logic here
			//
			parameterTable = new Hashtable();

			parameterTable["FROM_DATE"] = DateTime.Today.AddDays(-7);
			parameterTable["TO_DATE"] = DateTime.Today.AddDays(-1);
		}

		#region Report Members

		public void setDatabase(Database database)
		{
			this.database = database;
		}

		public void setParameter(string parameterCode, string parameterValue)
		{
			if (parameterTable[parameterCode] == null)
			{
				parameterTable.Add(parameterCode, parameterValue);
			}
			else
			{
				parameterTable[parameterCode] = parameterValue;
			}
		}

		public string getParameter(string parameterCode)
		{
			return parameterTable[parameterCode].ToString();
		}
		

		public string renderReport()
		{
			StringWriter stringWriter = new StringWriter();

			DateTime fromDate = DateTime.Parse(parameterTable["FROM_DATE"].ToString());
			DateTime toDate = DateTime.Parse(parameterTable["TO_DATE"].ToString());

			stringWriter.WriteLine("<table cellspacing=\"1\" cellpadding=\"2\" border=\"0\" width=\"100%\" class=\"innerFrame\">");
			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td colspan=\"3\" class=\"jobDescription\"><b>"+fromDate.ToString("yyyy-MM-dd")+" - "+toDate.ToString("yyyy-MM-dd")+"</b></td>");
			stringWriter.WriteLine("</tr>");

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td colspan=\"3\" class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td colspan=\"3\" class=\"jobDescription\"><b>Råvara</b></td>");
			stringWriter.WriteLine("</tr>");

			ShippingCustomers shippingCustomers = new ShippingCustomers();
			DataSet shippingCustomerDataSet = null;
			Factories factories = new Factories();
			DataSet factoryDataSet = factories.getDataSet(database, "KONVEX");
			int i = 0;
			float baseTotal = 0;

			while(i < factoryDataSet.Tables[0].Rows.Count)
			{
				Factory factory = new Factory(factoryDataSet.Tables[0].Rows[i]);

				ScaleEntries scaleEntries = new ScaleEntries();
				float factoryNet = scaleEntries.calcSum(database, "0", factory.no, fromDate, toDate);
				baseTotal = baseTotal + factoryNet;


				int z = 0;
				float exportFactoryNet = 0;

				shippingCustomerDataSet = shippingCustomers.getDataSet(database, "EXPORT UTVÄG");
				while(z < shippingCustomerDataSet.Tables[0].Rows.Count)
				{
					ShippingCustomer shippingCustomer = new ShippingCustomer(shippingCustomerDataSet.Tables[0].Rows[z]);

					float shippingCustomerNet = scaleEntries.calcSum(database, "1", factory.no, shippingCustomer.no, fromDate, toDate);

					exportFactoryNet = exportFactoryNet + shippingCustomerNet;

					z++;
				}


				stringWriter.WriteLine("<tr>");
				stringWriter.WriteLine("<td class=\"jobDescription\" width=\"200\">"+factory.name+"</td>");
				stringWriter.WriteLine("<td class=\"jobDescription\" width=\"100\" align=\"right\">"+decimal.Round(decimal.Parse(factoryNet.ToString()), 2).ToString("N")+" ton</td>");
				if (exportFactoryNet > 0) 
				{
					stringWriter.WriteLine("<td class=\"jobDescription\">(Processat: "+decimal.Round(decimal.Parse((factoryNet-exportFactoryNet).ToString()), 2).ToString("N")+" ton)</td>");
				}
				else
				{
					stringWriter.WriteLine("<td class=\"jobDescription\">&nbsp;</td>");
				}
				stringWriter.WriteLine("</tr>");

				i++;
			}

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td class=\"jobDescription\"><b>Totalt råvara</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\" align=\"right\"><b>"+decimal.Round(decimal.Parse(baseTotal.ToString()), 2).ToString("N")+" ton</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");


			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td colspan=\"3\" class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td colspan=\"3\" class=\"jobDescription\"><b>Biomal</b></td>");
			stringWriter.WriteLine("</tr>");

			shippingCustomerDataSet = shippingCustomers.getDataSet(database, 1);
			i = 0;
			float shippingCustomerTotal = 0;

			while(i < shippingCustomerDataSet.Tables[0].Rows.Count)
			{
				ShippingCustomer shippingCustomer = new ShippingCustomer(shippingCustomerDataSet.Tables[0].Rows[i]);

				FactoryOrders factoryOrders = new FactoryOrders();
				float shippingCustomerNet = factoryOrders.calcFactoryTotal(database, 1, shippingCustomer.no, fromDate, toDate);
				shippingCustomerTotal = shippingCustomerTotal + shippingCustomerNet;

				stringWriter.WriteLine("<tr>");
				stringWriter.WriteLine("<td class=\"jobDescription\" width=\"200\">"+shippingCustomer.name+"</td>");
				stringWriter.WriteLine("<td class=\"jobDescription\" width=\"100\" align=\"right\">"+decimal.Round(decimal.Parse(shippingCustomerNet.ToString()), 2).ToString("N")+" ton</td>");
				stringWriter.WriteLine("<td class=\"jobDescription\">&nbsp;</td>");
				stringWriter.WriteLine("</tr>");

				i++;
			}

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td class=\"jobDescription\"><b>Totalt biomal</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\" align=\"right\"><b>"+decimal.Round(decimal.Parse(shippingCustomerTotal.ToString()), 2).ToString("N")+" ton</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");


			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td colspan=\"3\" class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td colspan=\"3\" class=\"jobDescription\"><b>Export</b></td>");
			stringWriter.WriteLine("</tr>");

			
			shippingCustomerDataSet = shippingCustomers.getDataSet(database, "EXPORT");

			i = 0;
			float exportTotal = 0;

			while(i < shippingCustomerDataSet.Tables[0].Rows.Count)
			{
				ShippingCustomer shippingCustomer = new ShippingCustomer(shippingCustomerDataSet.Tables[0].Rows[i]);

				ScaleEntries scaleEntries = new ScaleEntries();
				float shippingCustomerNet = scaleEntries.calcSum(database, "1", "", shippingCustomer.no, fromDate, toDate);

				exportTotal = exportTotal + shippingCustomerNet;

				stringWriter.WriteLine("<tr>");
				stringWriter.WriteLine("<td class=\"jobDescription\" width=\"200\">"+shippingCustomer.name+"</td>");
				stringWriter.WriteLine("<td class=\"jobDescription\" width=\"100\" align=\"right\">"+decimal.Round(decimal.Parse(shippingCustomerNet.ToString()), 2).ToString("N")+" ton</td>");
				stringWriter.WriteLine("<td class=\"jobDescription\">&nbsp;</td>");
				stringWriter.WriteLine("</tr>");

				i++;
			}


			factoryDataSet = factories.getDataSet(database, "EXPORT");
			i = 0;

			while(i < factoryDataSet.Tables[0].Rows.Count)
			{
				Factory factory = new Factory(factoryDataSet.Tables[0].Rows[i]);

				float factoryNet = 0;

				LineJournals lineJournals = new LineJournals();
				DataSet lineJournalDataSet = lineJournals.getReportedDataSet(database, factory.no, fromDate, toDate);
				int j = 0;
				while (j < lineJournalDataSet.Tables[0].Rows.Count)
				{
					LineJournal lineJournal = new LineJournal(lineJournalDataSet.Tables[0].Rows[j]);
					DataSet containerDataSet = lineJournal.getContainers(database);
					int k = 0;
					while (k < containerDataSet.Tables[0].Rows.Count)
					{
						LineOrderContainer lineOrderContainer = new LineOrderContainer(containerDataSet.Tables[0].Rows[k]);
						factoryNet = factoryNet + (lineOrderContainer.weight / 1000);
						k++;
					}

					j++;
				}

				exportTotal = exportTotal + factoryNet;

				stringWriter.WriteLine("<tr>");
				stringWriter.WriteLine("<td class=\"jobDescription\" width=\"200\">"+factory.name+"</td>");
				stringWriter.WriteLine("<td class=\"jobDescription\" width=\"100\" align=\"right\">"+decimal.Round(decimal.Parse(factoryNet.ToString()), 2).ToString("N")+" ton</td>");
				stringWriter.WriteLine("<td class=\"jobDescription\">Uppskattad</td>");
				stringWriter.WriteLine("</tr>");

				i++;
			}

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td class=\"jobDescription\"><b>Totalt export</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\" align=\"right\"><b>"+decimal.Round(decimal.Parse(exportTotal.ToString()), 2).ToString("N")+" ton</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td colspan=\"3\" class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");

			float total = baseTotal + shippingCustomerTotal + exportTotal;

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td class=\"jobDescription\"><b>Totalt Konvex</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\" align=\"right\"><b>"+decimal.Round(decimal.Parse(total.ToString()), 2).ToString("N")+" ton</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td colspan=\"3\" class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td colspan=\"3\" class=\"jobDescription\"><b>Export via fabrik</b></td>");
			stringWriter.WriteLine("</tr>");

			shippingCustomerDataSet = shippingCustomers.getDataSet(database, "EXPORT UTVÄG");

			i = 0;
			float export2Total = 0;

			while(i < shippingCustomerDataSet.Tables[0].Rows.Count)
			{
				ShippingCustomer shippingCustomer = new ShippingCustomer(shippingCustomerDataSet.Tables[0].Rows[i]);

				ScaleEntries scaleEntries = new ScaleEntries();
				float shippingCustomerNet = scaleEntries.calcSum(database, "1", "", shippingCustomer.no, fromDate, toDate);

				export2Total = export2Total + shippingCustomerNet;

				stringWriter.WriteLine("<tr>");
				stringWriter.WriteLine("<td class=\"jobDescription\" width=\"200\">"+shippingCustomer.name+"</td>");
				stringWriter.WriteLine("<td class=\"jobDescription\" width=\"100\" align=\"right\">"+decimal.Round(decimal.Parse(shippingCustomerNet.ToString()), 2).ToString("N")+" ton</td>");
				stringWriter.WriteLine("<td class=\"jobDescription\">&nbsp;</td>");
				stringWriter.WriteLine("</tr>");

				i++;
			}

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td class=\"jobDescription\"><b>Totalt export via fabrik</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\" align=\"right\"><b>"+decimal.Round(decimal.Parse(export2Total.ToString()), 2).ToString("N")+" ton</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td colspan=\"3\" class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td colspan=\"3\" class=\"jobDescription\"><b>Biomal ut fabrik</b></td>");
			stringWriter.WriteLine("</tr>");

			factoryDataSet = factories.getDataSet(database, "KONVEX");
			i = 0;
			float outFactoryTotal = 0;
			float outTotal = 0;

			while(i < factoryDataSet.Tables[0].Rows.Count)
			{
				Factory factory = new Factory(factoryDataSet.Tables[0].Rows[i]);

				float factoryNet = 0;

				Categories categories = new Categories();
				DataSet categoryDataSet = categories.getDataSet(database, true);
				int z = 0;
				while (z < categoryDataSet.Tables[0].Rows.Count)
				{
					ScaleEntries scaleEntries = new ScaleEntries();
					factoryNet = factoryNet + scaleEntries.calcSum(database, "1", factory.no, "", categoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString(), fromDate, toDate);

					z++;
				}

				outFactoryTotal = outFactoryTotal + factoryNet;
				outTotal = outTotal + factoryNet;

				stringWriter.WriteLine("<tr>");
				stringWriter.WriteLine("<td class=\"jobDescription\" width=\"200\">"+factory.name+"</td>");
				stringWriter.WriteLine("<td class=\"jobDescription\" width=\"100\" align=\"right\">"+decimal.Round(decimal.Parse(factoryNet.ToString()), 2).ToString("N")+" ton</td>");
				stringWriter.WriteLine("<td class=\"jobDescription\">&nbsp;</td>");
				stringWriter.WriteLine("</tr>");

				i++;
			}

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td class=\"jobDescription\"><b>Totalt Biomal ut fabrik</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\" align=\"right\"><b>"+decimal.Round(decimal.Parse(outFactoryTotal.ToString()), 2).ToString("N")+" ton</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");

			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td colspan=\"3\" class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");


			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td class=\"jobDescription\"><b>Totalt Biomal</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\" align=\"right\"><b>"+decimal.Round(decimal.Parse((shippingCustomerTotal+outFactoryTotal).ToString()), 2).ToString("N")+" ton</b></td>");
			stringWriter.WriteLine("<td class=\"jobDescription\">&nbsp;</td>");
			stringWriter.WriteLine("</tr>");

			stringWriter.WriteLine("</table>");

			return stringWriter.ToString();

		}

		public string getName()
		{
			// TODO:  Add ReportMaterialOverview.getName implementation
			return "Materialflöde";
		}

		#endregion
	}
}
