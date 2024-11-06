using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using WooCommerceWrapper;

namespace WooCommerceWrapperTestGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    var requestDoc = new XmlDocument();
                    requestDoc.Load(file);

                    string errorMessage = string.Empty;
                    var responseDoc = new XmlDocument();

                    //const string url = @"https://woo.wackes.se/wc-api/v2";
                    //var restWrapper = new Rest(url, "ck_487eaa2e88a555defc9ba6afc24ddb46", "cs_95e15b99a913e056c3f0bb8b97745554");//Wackes

                    //const string url = @"https://brandstore.wackes.com/friskissvettis/wc-api/v3";
                    //var restWrapper = new Rest(url, "ck_39280ff132905a381ca118024fdf9212eb2cb716", "cs_6e420a29b7d3a6bcc0d4b96b336915efaeb7939e");//Wackes Friskis


                    const string url = @"https://brandstore.wackes.com/swedbank/wc-api/v3";
                    var restWrapper = new Rest(url, "ck_dbd8a94093c293230b9c53953cd8887982d1406c", "cs_c88a7f9e4b102c773c02a3b8e350ca677ce5ed16");//Wackes LR

                    //const string url = @"https://whyred.com/wc-api/v2";
                    //var restWrapper = new Rest(url, "ck_114ca0fe5fbc3f29f02f4690991d22f0", "cs_a0235bf4be82909780e94f1b12a9d483");//Whyred Skarpt

                    //const string url = @"https://shop.whyredshop.com/wc-api/v2";
                    //var restWrapper = new Rest(url, "ck_114ca0fe5fbc3f29f02f4690991d22f0", "cs_a0235bf4be82909780e94f1b12a9d483");//Whyred TEST


                    bool wrapperSendOk = restWrapper.Execute(ref requestDoc, ref errorMessage, ref responseDoc);
                   

                    //bool wrapperSendOk = new ZoinedWrapper.REST().Execute("https://push.zoined.com", "114ca0fe5fbc3f29f02f4690991d22f0", ref requestDoc, ref errorMessage);


                    if (wrapperSendOk)
                    {
                        responseDoc.Save(@"C:\Temp\rest.xml");
                        textBox1.Text = responseDoc.InnerText;
                    }
                    else
                    {
                        textBox1.Text = errorMessage;
                    }
                }
                catch (IOException ex)
                {
                    textBox1.Text = ex.Message;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


    }


   



}
