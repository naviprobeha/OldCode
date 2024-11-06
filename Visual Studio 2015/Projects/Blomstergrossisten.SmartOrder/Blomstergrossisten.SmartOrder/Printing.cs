using System;
using SerialNET;
using System.Data;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for Printer.
    /// </summary>
    public class Printing
    {
        private SmartDatabase smartDatabase;
        private DataSetup dataSetup;
        private Port printerPort;
        private bool enabled;

        private bool copy;

        public Printing(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            //
            // TODO: Add constructor logic here
            //

            this.dataSetup = smartDatabase.getSetup();

            SerialNET.License license = new SerialNET.License();
            license.LicenseKey = "f0FGYKKBhLaWA7I1G5KziVM9kfOhUruyfScd";



            //
            // TODO: Add constructor logic here
            //

        }

        public bool init()
        {

            try
            {
                printerPort = new Port();
                printerPort.ComPort = 4;
                printerPort.BaudRate = 115200;
                printerPort.Parity = SerialNET.Parity.No;
                printerPort.StopBits = SerialNET.StopBits.One;
                printerPort.ByteSize = 8;



                printerPort.Enabled = true;
                this.enabled = true;

                return true;

            }
            catch (Exception e)
            {
                //System.Windows.Forms.MessageBox.Show(e.ToString());
                return false;
            }
        }

        public void test()
        {
            char esc = (char)27;
            char nl = (char)10;

            //write(esc+"w&");
            write(esc + "w)");
            write("Testdata");
            write(nl + "" + nl + "" + nl);
        }

        private void write(string data)
        {
            char ret = (char)13;
            char nl = (char)10;

            char å = (char)140;
            char ä = (char)138;
            char ö = (char)154;

            char Å = (char)129;
            char Ä = (char)128;
            char Ö = (char)133;

            data = data.Replace('å', å);
            data = data.Replace('ä', ä);
            data = data.Replace('ö', ö);

            data = data.Replace('Å', Å);
            data = data.Replace('Ä', Ä);
            data = data.Replace('Ö', Ö);

            printerPort.Write(data + ret + nl);
            System.Threading.Thread.Sleep(200);
        }



        private void writeRaw(string data)
        {
            char ret = (char)13;
            char nl = (char)10;

            printerPort.Write(data + ret + nl);
            System.Threading.Thread.Sleep(200);
        }


        public void printOrder(DataSalesHeader dataSalesHeader)
        {
            if (!this.enabled) return;

            DateTime dueDate = DateTime.Today.AddDays(10);

            Agent agent = new Agent(smartDatabase);

            char esc = (char)27;
            char nl = (char)10;

            write(esc + "w)");
            writeRaw("CARLSSON BLOMSTERGROSSISTEN AB");
            writeRaw("");

            string header = "FÖLJESEDEL";

            if ((dataSalesHeader.paymentMethod == "KONTANT") || (dataSalesHeader.paymentMethod == "KORT"))
            {
                header = "KONTANTNOTA";
            }
            if ((dataSalesHeader.paymentMethod == "FAKTURA") && (dataSalesHeader.preInvoiceNo != ""))
            {
                header = "FAKTURA ";
            }


            if (copy)
            {
                writeRaw(header + " - KOPIA");
            }
            else
            {
                writeRaw(header);
            }

            write(nl + "" + nl);



            write(esc + "wm");


            if ((dataSalesHeader.paymentMethod == "FAKTURA") && (dataSalesHeader.preInvoiceNo != ""))
            {
                write("Förfallodatum");
                write(esc + "w)" + dueDate.ToString("yyyy-MM-dd") + esc + "wm");
                write("");
            }

            if (dataSalesHeader.preInvoiceNo != "")
            {
                if ((dataSalesHeader.paymentMethod == "KONTANT") || (dataSalesHeader.paymentMethod == "KORT"))
                {
                    write("Kvittonr:     " + dataSalesHeader.preInvoiceNo);
                }
                else
                {
                    write("Fakturanr:    " + dataSalesHeader.preInvoiceNo);
                }
            }

            write("Ordernr:      " + agent.agentId + dataSalesHeader.no.ToString());
            write("Datum:        " + DateTime.Now.ToString("yyyy-MM-dd"));
            write("Vår referens: " + dataSalesHeader.referenceCode);
            write(nl + "" + nl);

            write(esc + "wm");
            write("Kundinformation");
            write("===============");
            write(esc + "wm");
            write(dataSalesHeader.name);
            write(dataSalesHeader.address);
            if (dataSalesHeader.address2 != null)
            {
                if (dataSalesHeader.address2 != "") write(dataSalesHeader.address2);
            }

            write(dataSalesHeader.zipCode + " " + dataSalesHeader.city);
            write(nl + "" + nl);


            write(esc + "wm");
            write("Artikelnr   Beskrivning           Antal     A-pris   Belopp");
            write("===========================================================");


            DataSalesLines dataSalesLines = new DataSalesLines(smartDatabase);
            DataSet salesLineDataSet = dataSalesLines.getReportDataSet(dataSalesHeader);

            int i = 0;
            int boxQuantity = 0;

            while (i < salesLineDataSet.Tables[0].Rows.Count)
            {
                string itemNo = salesLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                string description = salesLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                string quantity = salesLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
                string unitPrice = salesLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString();
                string amount = salesLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString();
                boxQuantity = boxQuantity + int.Parse(salesLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString());

                if (description.Length > 21) description = description.Substring(0, 21);

                write(itemNo.PadRight(11, ' ') + " " + description.PadRight(21, ' ') + " " + quantity.PadLeft(5, ' ') + "   " + unitPrice.PadLeft(7, ' ') + " " + amount.PadLeft(6, ' '));

                i++;
            }

            write(nl + "" + nl);

            float totalAmount = 0;
            float vatAmount = 0;
            float totalAmountVat = 0;
            float discountAmount = 0;
            float roundingAmount = 0;

            if (salesLineDataSet.Tables[0].Rows.Count > 0)
            {

                totalAmount = float.Parse(dataSalesHeader.getTotalAmount(smartDatabase));
                if (dataSalesHeader.discount > 0) discountAmount = totalAmount * (dataSalesHeader.discount / 100);

                vatAmount = (float)((totalAmount - discountAmount) * 0.25);
                totalAmountVat = (float)((totalAmount - discountAmount) * 1.25);

                roundingAmount = this.roundingAmount(totalAmountVat);

                totalAmountVat = totalAmountVat + roundingAmount;

            }

            write("                                      Antal lådor: " + boxQuantity);

            if (discountAmount > 0)
            {
                string discountString = "Rabatt (" + dataSalesHeader.discount + "%)";
                write("                                   " + discountString.PadLeft(13, ' ') + ":  " + (String.Format("{0:f}", discountAmount).Replace(",", ".")).PadLeft(8, ' '));
            }

            write("                                  Belopp ex. moms: " + (String.Format("{0:f}", (totalAmount - discountAmount)).Replace(",", ".")).PadLeft(8, ' '));

            if ((dataSalesHeader.paymentMethod == "KONTANT") || (dataSalesHeader.paymentMethod == "KORT"))
            {
                write("                                       Moms (25%): " + (String.Format("{0:f}", vatAmount).Replace(",", ".")).PadLeft(8, ' '));
                if (roundingAmount != 0)
                {
                    write("                                   Öresavrundning: " + (String.Format("{0:f}", roundingAmount).Replace(",", ".")).PadLeft(8, ' '));
                }
                write("                                 Belopp inkl moms: " + (String.Format("{0:f}", totalAmountVat).Replace(",", ".")).PadLeft(8, ' '));
            }

            if ((dataSalesHeader.paymentMethod == "FAKTURA") && (dataSalesHeader.preInvoiceNo != ""))
            {
                write("                                       Moms (25%): " + (String.Format("{0:f}", vatAmount).Replace(",", ".")).PadLeft(8, ' '));
                if (roundingAmount != 0)
                {
                    write("                                   Öresavrundning: " + (String.Format("{0:f}", roundingAmount).Replace(",", ".")).PadLeft(8, ' '));
                }
                write("                                 Belopp inkl moms: " + (String.Format("{0:f}", totalAmountVat).Replace(",", ".")).PadLeft(8, ' '));
            }

            write(nl + "" + nl + "" + nl + "" + nl + "" + nl);

            if ((dataSalesHeader.paymentMethod == "KONTANT") || (dataSalesHeader.paymentMethod == "KORT"))
            {
                write(".................................");
                write("Kvitterad");
                write(nl + "" + nl);
            }

            if ((dataSalesHeader.paymentMethod == "FAKTURA") && (dataSalesHeader.preInvoiceNo != ""))
            {
                write(".................................");
                write("Kvitterad");
                write(nl + "" + nl);
            }

            write("Carlsson Blomstergrossisten AB - Org. nr: 556561-7106");
            write("Getängsvägen 20 - 504 68 BORÅS");
            write("Postgiro: 653286-5 - Bankgiro: 5117-5495");
            write("Telefon: 033-444 999  -  Fax: 033-124 161");

            write(nl + "" + nl + "" + nl + "" + nl + "" + nl);

        }

        public void close()
        {

            printerPort.Enabled = false;
            printerPort.Dispose();
        }

        public void setCopy()
        {
            this.copy = true;
        }

        public float roundingAmount(float amount)
        {
            amount = (float)Math.Round(amount, 2);
            float truncAmount = ((int)amount) + 1;
            float roundToAmount = 0;

            if (amount != truncAmount)
            {
                if (amount > 0)
                {
                    if (((int)amount) != ((int)Math.Round(amount - 0.01, 0)))
                    {
                        roundToAmount = ((int)amount + 1);
                    }
                    else
                    {
                        roundToAmount = (int)amount;
                    }
                }
                else
                {
                    if (((int)amount) != ((int)Math.Round(amount - 0.01, 0)))
                    {
                        int intAmount = (int)amount;
                        roundToAmount = (float)intAmount;
                        roundToAmount = (float)(roundToAmount - 0.5);
                    }
                    else
                    {
                        roundToAmount = (int)amount;
                    }

                }
                float roundAmount = roundToAmount - amount;
                if (roundAmount != 0) return roundAmount;
            }

            return 0;
        }

    }
}
