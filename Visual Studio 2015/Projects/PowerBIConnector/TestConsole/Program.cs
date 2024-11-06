using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {



            
            NaviPro.PowerBIConnector.PowerBIConnector pbi = new NaviPro.PowerBIConnector.PowerBIConnector("hakan.bengtsson@navipro.se", "hakanbengtsson1!", "a2f785a7-fda7-4172-aad9-0a983eee4a44", "");

            System.IO.StreamReader reader = new System.IO.StreamReader("../../../dataset.json");
            string jsonData = reader.ReadToEnd();
            reader.Close();

            //string jsonData = "{ \"name\": \"E-Commerce Operations\", \"tables\": [ { \"name\": \"ProductBrowsing\", \"columns\": [ {	\"name\": \"Product\", \"dataType\": \"String\" }, { \"name\": \"Product Category\", \"dataType\": \"String\" }	] } ] }";
            //Console.WriteLine("Creating table");
            //pbi.CreateTableStructure("Cashjet Sales", jsonData);



            //string jsonData2 = "{ \"rows\": [ { \"Product\": \"4CAX-B Helicopter\",	\"Product Category\": \"Co-Axial\" } ] }";
            //pbi.AddData("E-Commerce Operations", "ProductBrowsing", jsonData2);
            //Console.ReadLine();

            pbi.ClearData("Cashjet Sales", "Sales Transaction");

            Navipro.Cashjet.Library.Configuration config = new Navipro.Cashjet.Library.Configuration();
            config.connectionString = "Database=OUTNORTH2009R2;Server=APOLLO1;Connect Timeout=30;User ID=super;Password=b0bbaf3tt;Max Pool Size=10000";
            config.companyName = "Outnorth AB";

            Navipro.Cashjet.Library.Database database = new Navipro.Cashjet.Library.Database(null, config);
            System.Data.SqlClient.SqlDataAdapter lineDataAdapter = database.dataAdapterQuery("SELECT " +
                "l.[Receipt No_], "+
                "l.[Sales Type], " +
                "l.[Sales No_], " +
                "l.[Variant Code], " +
                "l.[Description], " +
                "l.[Unit of Measure Code], " +
                "l.[Item Category Code], " +
                "l.[Product Group Code], " +
                "l.[Quantity], " +
                "l.[Unit Price], " +
                "l.[Unit Price Incl_ VAT], " +
                "l.[Line Discount], " +
                "l.[VAT Amount], " +
                "l.[Amount], " +
                "l.[Amount Incl_ VAT], " +
                "l.[Payment Reference No_], " +
                "l.[Return Receipt No_], " +
                "l.[Return Code], " +
                "l.[Return Description], " +
                "l.[Total Discount Amount], " +
                "l.[Discount Code], " +
                "l.[Cash Register ID], " +
                "h.[User-ID], " +
                "l.[Mix Match], " +
                "l.[Registered Date], " +
                "l.[Registered Time], " +
                "l.[Location Code], " +
                "h.[Customer No_] " +
                "FROM [Outnorth AB$Posted Cash Receipt Line] l, [Outnorth AB$Posted Cash Receipt] h WHERE l.[Receipt No_] = h.[No_] AND l.[Line Type] = 0 AND h.[Registered Date] >= '2016-01-01' AND [Void] = 0");

            System.Data.DataSet lineDataSet = new System.Data.DataSet();
            lineDataAdapter.Fill(lineDataSet);

            int records = lineDataSet.Tables[0].Rows.Count;
            Console.WriteLine("No. of records: " + records);

            string recordJson = "{ \"rows\": [ ";

            int i = 0;
            int recordCount = 0;
            string startReceiptNo = "";

            while (i < lineDataSet.Tables[0].Rows.Count)
            {
                recordCount++;

                string cashRegisterID = lineDataSet.Tables[0].Rows[i]["Cash Register ID"].ToString();
                if (startReceiptNo == "") startReceiptNo = lineDataSet.Tables[0].Rows[i]["Receipt No_"].ToString();

                string cashSiteCode = "";
                string cashSiteName = "";
                string cashSitePostCode = "";
                string cashSiteCity = "";
                string salesLocation = "";
                string salesPersonName = "";

                System.Data.SqlClient.SqlDataAdapter crDataAdapter = database.dataAdapterQuery("SELECT [Cash Site Code], [Sales Location Code] FROM [Outnorth AB$Cash Register] WHERE [Cash Register ID] = '"+cashRegisterID+"'");
                System.Data.DataSet crDataSet = new System.Data.DataSet();
                crDataAdapter.Fill(crDataSet);
                if (crDataSet.Tables[0].Rows.Count > 0)
                {
                    cashSiteCode = crDataSet.Tables[0].Rows[0]["Cash Site Code"].ToString();
                    salesLocation = crDataSet.Tables[0].Rows[0]["Sales Location Code"].ToString();

                    System.Data.SqlClient.SqlDataAdapter csDataAdapter = database.dataAdapterQuery("SELECT [Name], [Post Code], [City] FROM [Outnorth AB$Cash Site] WHERE [Code] = '" + crDataSet.Tables[0].Rows[0]["Cash Site Code"].ToString() + "'");
                    System.Data.DataSet csDataSet = new System.Data.DataSet();
                    csDataAdapter.Fill(csDataSet);

                    if (csDataSet.Tables[0].Rows.Count > 0)
                    {
                        cashSiteName = csDataSet.Tables[0].Rows[0]["Name"].ToString();
                        cashSitePostCode = csDataSet.Tables[0].Rows[0]["Post Code"].ToString();
                        cashSiteCity = csDataSet.Tables[0].Rows[0]["City"].ToString();

                    }
                }

                System.Data.SqlClient.SqlDataAdapter spDataAdapter = database.dataAdapterQuery("SELECT [Name] FROM [Outnorth AB$Cash Register User] WHERE [User-ID] = '" + lineDataSet.Tables[0].Rows[i]["User-ID"].ToString() + "'");
                System.Data.DataSet spDataSet = new System.Data.DataSet();
                spDataAdapter.Fill(spDataSet);
                if (spDataSet.Tables[0].Rows.Count > 0)
                {
                    salesPersonName = spDataSet.Tables[0].Rows[0]["Name"].ToString();
                }

                decimal unitCost = 0;
                string genProdPostingGroup = "";
                string vatProdPostingGroup = "";
                string description2 = "";
                string baseColorCode = "";
                string baseSizeCode = "";
                string baseColorDesc = "";
                string currentReceiptNo = lineDataSet.Tables[0].Rows[0]["Receipt No_"].ToString();

                if (lineDataSet.Tables[0].Rows[0]["Sales Type"].ToString() == "2")
                {
                    System.Data.SqlClient.SqlDataAdapter itemDataAdapter = database.dataAdapterQuery("SELECT [Unit Cost], [Gen_ Prod_ Posting Group], [VAT Prod_ Posting Group] FROM [Outnorth AB$Item] WHERE [No_] = '" + lineDataSet.Tables[0].Rows[0]["Sales No_"].ToString() + "'");
                    System.Data.DataSet itemDataSet = new System.Data.DataSet();
                    itemDataAdapter.Fill(itemDataSet);

                    unitCost = decimal.Parse(itemDataSet.Tables[0].Rows[0]["Unit Cost"].ToString());
                    genProdPostingGroup = itemDataSet.Tables[0].Rows[0]["Gen_ Prod_ Posting Group"].ToString();
                    vatProdPostingGroup = itemDataSet.Tables[0].Rows[0]["VAT Prod_ Posting Group"].ToString();

                    if (lineDataSet.Tables[0].Rows[0]["Variant Code"].ToString() != "")
                    {
                        System.Data.SqlClient.SqlDataAdapter variantDataAdapter = database.dataAdapterQuery("SELECT v.[Description 2], v.[Base Color Code], v.[Base Size Code], b.[Description] FROM [Outnorth AB$Item Variant] v LEFT JOIN [Outnorth AB$Base Code] b ON v.[Base Color Code] = b.[Code] WHERE v.[Item No_] = '" + lineDataSet.Tables[0].Rows[0]["Sales No_"].ToString() + "' AND v.[Code] = '" + lineDataSet.Tables[0].Rows[0]["Variant Code"].ToString() + "' ");
                        System.Data.DataSet variantDataSet = new System.Data.DataSet();
                        variantDataAdapter.Fill(variantDataSet);

                        description2 = variantDataSet.Tables[0].Rows[0]["Description 2"].ToString();
                        baseColorCode = variantDataSet.Tables[0].Rows[0]["Base Color Code"].ToString();
                        baseSizeCode = variantDataSet.Tables[0].Rows[0]["Base Size Code"].ToString();
                        baseColorDesc = variantDataSet.Tables[0].Rows[0]["Description"].ToString();
                    }
                }

                Console.WriteLine("Receipt: " + lineDataSet.Tables[0].Rows[i]["Receipt No_"].ToString() + " (" + i + "/" + records + ")");

                string mixMatch = "false";
                if (lineDataSet.Tables[0].Rows[i]["Mix Match"].ToString() == "1") mixMatch = "true";
                string locationCode = lineDataSet.Tables[0].Rows[i]["Location Code"].ToString();
                if (locationCode == "") locationCode = salesLocation;


                    recordJson = recordJson + " { "+
                    "\"Transaction No\": \""+lineDataSet.Tables[0].Rows[i]["Receipt No_"].ToString()+"\","+
                    "\"Sales Type\": \"" + lineDataSet.Tables[0].Rows[i]["Sales Type"].ToString() + "\"," +
                    "\"Sales No\": \"" + lineDataSet.Tables[0].Rows[i]["Sales No_"].ToString().Replace("\"", "") + "\"," +
                    "\"Variant Code\": \"" + lineDataSet.Tables[0].Rows[i]["Variant Code"].ToString().Replace("\"", "") + "\"," +
                    "\"Description\": \"" + lineDataSet.Tables[0].Rows[i]["Description"].ToString().Replace("\"", "") + "\"," +
                    "\"Description 2\": \"" + description2.Replace("\"", "") + "\"," +
                    "\"Color Code\": \"" + baseColorCode + "\"," +
                    "\"Color Description\": \"" + baseColorDesc + "\"," +
                    "\"Size Code\": \"" + baseSizeCode + "\"," +
                    "\"Unit Of Measure Code\": \"" + lineDataSet.Tables[0].Rows[i]["Unit of Measure Code"].ToString() + "\"," +
                    "\"Item Category Code\": \"" + lineDataSet.Tables[0].Rows[i]["Item Category Code"].ToString() + "\"," +
                    "\"Product Group Code\": \"" + lineDataSet.Tables[0].Rows[i]["Product Group Code"].ToString() + "\"," +
                    "\"Quantity\": \"" + (int)decimal.Round(decimal.Parse(lineDataSet.Tables[0].Rows[i]["Quantity"].ToString()), 0) + "\"," +
                    "\"Unit Price\": \"" + (int)decimal.Round(decimal.Parse(lineDataSet.Tables[0].Rows[i]["Unit Price"].ToString()), 0) + "\"," +
                    "\"Unit Price Incl VAT\": \"" + (int)decimal.Round(decimal.Parse(lineDataSet.Tables[0].Rows[i]["Unit Price Incl_ VAT"].ToString()), 0) + "\"," +
                    "\"Line Discount Amount\": \"" + (int)decimal.Round(decimal.Parse(lineDataSet.Tables[0].Rows[i]["Line Discount"].ToString()), 0) + "\"," +
                    "\"VAT Amount\": \"" + (int)decimal.Round(decimal.Parse(lineDataSet.Tables[0].Rows[i]["VAT Amount"].ToString()), 0) + "\"," +
                    "\"Amount\": \"" + (int)decimal.Round(decimal.Parse(lineDataSet.Tables[0].Rows[i]["Amount"].ToString()), 0) + "\"," +
                    "\"Amount Incl VAT\": \"" + (int)decimal.Round(decimal.Parse(lineDataSet.Tables[0].Rows[i]["Amount Incl_ VAT"].ToString()), 0) + "\"," +
                    "\"Unit Cost\": \"" + (int)decimal.Round(unitCost, 0) + "\"," +
                    "\"Cost Amount\": \"" + (int)decimal.Round(unitCost*decimal.Parse(lineDataSet.Tables[0].Rows[i]["Quantity"].ToString()), 0) +"\"," +
                    "\"Voucher Type Code\": \"\"," +
                    "\"Return Receipt No\": \"" + lineDataSet.Tables[0].Rows[i]["Return Receipt No_"].ToString() + "\"," +
                    "\"Return Reason Code\": \"" + lineDataSet.Tables[0].Rows[i]["Return Code"].ToString() + "\"," +
                    "\"Return Reason Description\": \"" + lineDataSet.Tables[0].Rows[i]["Return Description"].ToString() + "\"," +
                    "\"Total Discount Amount\": \"" + (int)decimal.Round(decimal.Parse(lineDataSet.Tables[0].Rows[i]["Total Discount Amount"].ToString()), 0) + "\"," +
                    "\"Discount Code\": \"" + lineDataSet.Tables[0].Rows[i]["Discount Code"].ToString() + "\"," +
                    "\"Store Code\": \"" + crDataSet.Tables[0].Rows[0]["Cash Site Code"].ToString() + "\"," +
                    "\"POS Device Code\": \"" + lineDataSet.Tables[0].Rows[i]["Cash Register ID"].ToString() + "\"," +
                    "\"Store Name\": \"" + cashSiteName + "\"," +
                    "\"Store Post Code\": \"" + cashSitePostCode + "\"," +
                    "\"Store City\": \"" + cashSiteCity + "\"," +
                    "\"Store Country Code\": \"SE\"," +
                    "\"Salesperson Code\": \"" + lineDataSet.Tables[0].Rows[i]["User-ID"].ToString() + "\"," +
                    "\"Salesperson Name\": \"" + salesPersonName + "\"," +
                    "\"Gen. Prod. Posting Group\": \"" + genProdPostingGroup + "\"," +
                    "\"VAT Prod. Posting Group\": \"" + vatProdPostingGroup + "\"," +
                    "\"Mix Match\": \"" + mixMatch + "\"," +
                    "\"Registered Date\": \"" + DateTime.Parse(lineDataSet.Tables[0].Rows[i]["Registered Date"].ToString()).ToString("yyyy-MM-dd") + "\"," +
                    "\"Registered Hour\": \"" + DateTime.Parse(lineDataSet.Tables[0].Rows[i]["Registered Time"].ToString()).ToString("HH") + "\"," +
                    "\"Location Code\": \"" + locationCode + "\"," +
                    "\"Member No\": \"" + lineDataSet.Tables[0].Rows[i]["Customer No_"].ToString() + "\""+
                    "}";

                //Console.WriteLine(recordJson);
                //Console.ReadLine();

                if (recordCount > 1000)
                {
                    recordJson = recordJson + "]}";
                    try
                    {
                        pbi.AddData("Cashjet Sales", "Sales Transaction", recordJson);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Receipt interval: " + startReceiptNo + " - " + currentReceiptNo);
                        Console.WriteLine(recordJson);
                        Console.ReadLine();
                    }
                    recordJson = "{ \"rows\": [ ";
                    recordCount = 0;
                    startReceiptNo = "";
                }
                if (recordCount > 0) recordJson = recordJson + ",";
                i++;
            }

            if (recordCount > 0)
            {
                recordJson = recordJson + "]}";
                pbi.AddData("Cashjet Sales", "Sales Transaction", recordJson);

                recordJson = "{ \"rows\": [ ";
                recordCount = 0;

            }
            Console.ReadLine();
            database.close();
        }
    }
}
