using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Threading;


namespace Update
{
    public partial class Update : Form
    {
        private string url = "";
        private string languageCode = "";
        
        public Update()
        {
            InitializeComponent();

            if (!getConfiguration())
            {
                Application.Exit();
            }

            button1.Text = Translation.translate(languageCode, "Uppdatera");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.getUpdateInfo();

            Application.Exit();
        }

        public void run()
        {
            getUpdateInfo();
        }

        public void downloadFile(string fileUrl, string fileName)
        {
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(fileUrl);
            req.Method = "GET";

            try
            {
                WebResponse response = req.GetResponse();

                Stream stream = response.GetResponseStream();

                int cbRead = 0;

                message.Text = Translation.translate(languageCode, "Laddar ner ") + fileName + "...";
                Application.DoEvents();

                FileStream wrt = new FileStream(fileName, FileMode.Create);

                Byte[] data = new Byte[1024];

                long totalSize = response.ContentLength;
                int received = 0;

                cbRead = stream.Read(data, 0, 1024);
                while (cbRead > 0)
                {
                    wrt.Write(data, 0, cbRead);

                    received = received + cbRead;
                    int progress = (int)((received / totalSize) * 100);
                    progressBar1.Value = progress;
                    Application.DoEvents();

                    cbRead = stream.Read(data, 0, 1024);
                }

                wrt.Close();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

            Cursor.Current = Cursors.Default;
            Cursor.Hide();


        }



        private bool getConfiguration()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("\\Program Files\\Update\\update.xml");

                XmlElement docElement = xmlDoc.DocumentElement;

                caption.Text = docElement.GetElementsByTagName("title").Item(0).FirstChild.Value;

                if (docElement.SelectSingleNode("languageCode") != null)
                {
                    languageCode = docElement.GetElementsByTagName("languageCode").Item(0).FirstChild.Value;
                }

                this.Text = caption.Text;
                url = docElement.GetElementsByTagName("url").Item(0).FirstChild.Value;
                return true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                return false;
            }

        }

        private void getUpdateInfo()
        {
            message.Text = Translation.translate(languageCode, "Letar efter uppdateringar...");
            Application.DoEvents();


            XmlDocument xmlDoc = new XmlDocument();

            //try
            //{
            xmlDoc.Load(url);



            XmlElement docElement = xmlDoc.DocumentElement;

            XmlNodeList xmlNodeList = docElement.GetElementsByTagName("file");
            int i = 0;
            while (i < xmlNodeList.Count)
            {
                XmlElement fileElement = ((XmlElement)xmlNodeList.Item(i));
                string fileUrl = fileElement.GetElementsByTagName("url").Item(0).FirstChild.Value;
                string fileName = fileElement.GetElementsByTagName("name").Item(0).FirstChild.Value;

                downloadFile(fileUrl, fileName);

                Application.DoEvents();
                i++;
            }
            //}
            //catch(Exception e)
            //{
            //	message.Text = "Inga uppdateringar funna...";
            //	Thread.Sleep(2000);
            //}

            message.Text = Translation.translate(languageCode, "Nedladdning klar.");

        }

        static void Main()
        {
            Application.Run(new Update());
        }
    }
}