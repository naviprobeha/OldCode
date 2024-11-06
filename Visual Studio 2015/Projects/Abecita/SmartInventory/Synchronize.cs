using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class Synchronize : Form, Logger
    {
        private SmartDatabase smartDatabase;
        
        public Synchronize(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
        }

        public bool synchronize()
        {
            bool result;
            write("--- Synkronisering ---");
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            button1.Enabled = false;
            button2.Enabled = false;
            Service synchService = new Service("singleSynchronization", smartDatabase);
            synchService.setLogger(this);

            bool errors = false;
            bool abort = false;

            int count = 1;

            while ((!errors) && (count > 0) && (!abort))
            {
                smartDatabase.clearErrorFlag();

                //synchService.serviceRequest.setServiceArgument(new Publication(smartDatabase, this));

                ServiceResponse serviceResponse = synchService.performService();
                count = serviceResponse.publication.header.synchEntries;
                this.syncCountBox.Text = count.ToString();

                if (serviceResponse != null)
                {
                    if (serviceResponse.hasErrors)
                    {
                        System.Windows.Forms.MessageBox.Show(serviceResponse.error.status + ": " + serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                        errors = true;
                    }
                    else
                    {
                        if (!smartDatabase.checkErrorFlag()) ackSynchronization(serviceResponse.publication.header.entryNo);
                    }
                }
                else
                {
                    errors = true;
                }
                if (smartDatabase.checkErrorFlag()) errors = true;
            }

            if (errors)
            {
                write("Synkroniseringen misslyckades.");
            }
            else
            {
                write("Synkronisering klar.");
            }



            /*
            synchService.serviceRequest.setServiceArgument(new Publication(smartDatabase, this));

            ServiceResponse serviceResponse = synchService.performService();

            if (serviceResponse != null)
            {
                result = true;
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.error.status + ": " + serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                    write("Synkroniseringen misslyckades.");
                    result = false;
                }
                write("Synkronisering klar.");

            }
            else
            {
                write("Synkroniseringen misslyckades.");
                result = true;
            }
             */

            button1.Enabled = true;
            button2.Enabled = true;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();
            return (!errors);
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
                int j = 1;
                string fileName = openFileDialog.FileName.Replace(".xml", "_" + i + "_" + j + ".xml");
                while (File.Exists(fileName))
                {
                    fileName = openFileDialog.FileName.Replace(".xml", "_" + i + "_" + j + ".xml");
                    while (File.Exists(fileName))
                    {
                        this.write("Importerar " + fileName);
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

                        j++;
                        fileName = openFileDialog.FileName.Replace(".xml", "_" + i + "_" + j + ".xml");

                    }
                    i++;
                    j = 1;
                    fileName = openFileDialog.FileName.Replace(".xml", "_" + i + "_" + j + ".xml");
                }

                write("Import klar.");

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            synchronize();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;

            importPackage();

            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void ackSynchronization(string entryNo)
        {
            Service synchService = new Service("ackSynck", smartDatabase);
            synchService.setLogger(this);

            synchService.serviceRequest.setServiceArgument(new AckSynckEntry(entryNo));

            ServiceResponse serviceResponse = synchService.performService();

        }


        #region Logger Members

        public void write(string message)
        {
            this.listBox1.Items.Add(message);
            Application.DoEvents();
        }

        #endregion
    }
}