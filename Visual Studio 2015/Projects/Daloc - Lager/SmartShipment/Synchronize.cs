using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace SmartShipment
{
    public partial class Synchronize : Form, Logger
    {
        private DataSetup dataSetup;

        private SmartDatabase smartDatabase;
        private string serviceName;
        private bool fullSynch;

        private int status;

        private System.Threading.Timer closingTimer;

        public Synchronize(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.serviceName = "synchronization";
            this.fullSynch = true;
            this.dataSetup = smartDatabase.getSetup();

        }

		public Synchronize(SmartDatabase smartDatabase, DataSetup dataSetup, string serviceName)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.smartDatabase = smartDatabase;
			this.serviceName = serviceName;
			this.dataSetup = dataSetup;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            write("--- Synkronisering ---");
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            button1.Enabled = false;
            Service synchService = new Service(serviceName, smartDatabase, dataSetup);
            synchService.setLogger(this);

            synchService.serviceRequest.setServiceArgument(new Publication(smartDatabase, this));

            ServiceResponse serviceResponse = synchService.performService();

            if (serviceResponse != null)
            {
                DataSalesHeaders salesHeaders = new DataSalesHeaders(smartDatabase);

                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.error.status + ": " + serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                    write("Synkroniseringen misslyckades.");
                    write("Raderar skickade order.");
                    salesHeaders.deleteReadySalesHeaders();
                }
                else
                {
                    write("Raderar skickade order.");
                    salesHeaders.deleteReadySalesHeaders();

                    write("Synkronisering klar.");
                    status = 1;
                }
            }
            else
            {
                write("Synkroniseringen misslyckades.");
            }


            button1.Enabled = true;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();
        }

        public void write(string message)
        {
            listBox1.Items.Add(message);
            Application.DoEvents();
        }

        private void importPackage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Xml files (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                System.Windows.Forms.Cursor.Show();

                write("Importerar " + openFileDialog.FileName);

                XmlDocument packageXml = new XmlDocument();
                packageXml.Load(openFileDialog.FileName);

                if (packageXml.DocumentElement != null)
                {
                    XmlElement publication = packageXml.DocumentElement;
                    if (publication != null)
                    {
                        Publication publicationObject = new Publication(publication, smartDatabase, this);
                    }

                }

                int i = 1;
                string fileName = openFileDialog.FileName.Replace(".xml", "_" + i + ".xml");
                while (File.Exists(fileName))
                {
                    packageXml = new XmlDocument();
                    packageXml.Load(fileName);

                    if (packageXml.DocumentElement != null)
                    {
                        XmlElement publication = packageXml.DocumentElement;
                        if (publication != null)
                        {
                            Publication publicationObject = new Publication(publication, smartDatabase, this);
                        }

                    }

                    i++;
                    fileName = openFileDialog.FileName.Replace(".xml", "_" + i + ".xml");
                }

                write("Import klar.");

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();

            }
        }

        public void doClose(Object state)
        {
            this.Close();
        }

        private void Synchronize_Load(object sender, EventArgs e)
        {
            status = 0;

            if (!fullSynch)
            {
                button1_Click(null, null);

                if (status == 1)
                {
                    //
                }
            }
        }
    }
}