using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartShipment
{
    public partial class OrderHeader : Form
    {

        private SmartDatabase smartDatabase;
        private DataSetup dataSetup;
        private DataSalesHeader dataSalesHeader;

        public OrderHeader(SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.dataSetup = smartDatabase.getSetup();
            this.dataSalesHeader = new DataSalesHeader(smartDatabase);
            dataSalesHeader.save();

            update();

            this.scanBox.Focus();

        }

		public OrderHeader(SmartDatabase smartDatabase, DataSalesHeader dataSalesHeader)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.smartDatabase = smartDatabase;
			this.dataSetup = smartDatabase.getSetup();
			this.dataSalesHeader = dataSalesHeader;

			update();

		}

        private void scanBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void referenceNoBox_GotFocus(object sender, EventArgs e)
        {

        }

        private void referenceNoBox_LostFocus(object sender, EventArgs e)
        {

        }

        private void goodsmarkBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void goodsmarkBox_GotFocus(object sender, EventArgs e)
        {

        }

        private void goodsmarkBox_LostFocus(object sender, EventArgs e)
        {

        }

        private void phoneNoBox_GotFocus(object sender, EventArgs e)
        {

        }

        private void phoneNoBox_LostFocus(object sender, EventArgs e)
        {

        }

        private void contactNameBox_GotFocus(object sender, EventArgs e)
        {

        }

        private void contactNameBox_LostFocus(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void customerNoBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            save();

            if ((dataSalesHeader.customerNo == "") || (dataSalesHeader.customerNo == null))
            {
                System.Windows.Forms.MessageBox.Show("Du måste välja kund.");
            }
            else
            {
                OrderLines orderLines = new OrderLines(dataSalesHeader, smartDatabase, dataSetup);
                orderLines.ShowDialog();

                if (orderLines.getStatus() == 1)
                {
                    if (MessageBox.Show("Vill du skicka ordern?", "Klar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        dataSalesHeader.ready = true;
                        dataSalesHeader.save();

                        Synchronize synch = new Synchronize(smartDatabase, dataSetup, "sendOrders");
                        synch.ShowDialog();

                        synch.Dispose();
                        orderLines.Dispose();
                        this.Close();
                    }
                    else
                    {
                        if (MessageBox.Show("Vill du klarmarkera ordern?", "Klar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            dataSalesHeader.ready = true;
                            dataSalesHeader.save();
                        }
                    }
                }
            }
        }

        private void goodsmarkBox_GotFocus_1(object sender, EventArgs e)
        {
            //inputPanel1.Enabled = true;
        }

        private void contactNameBox_GotFocus_1(object sender, EventArgs e)
        {
            //inputPanel1.Enabled = true;
        }

        private void phoneNoBox_GotFocus_1(object sender, EventArgs e)
        {
            //inputPanel1.Enabled = true;
        }

        private void referenceNoBox_GotFocus_1(object sender, EventArgs e)
        {
            //inputPanel1.Enabled = true;
        }

        private void contactNameBox_LostFocus_1(object sender, EventArgs e)
        {
            //inputPanel1.Enabled = false;
        }

        private void phoneNoBox_LostFocus_1(object sender, EventArgs e)
        {
            //inputPanel1.Enabled = false;
        }

        private void goodsmarkBox_LostFocus_1(object sender, EventArgs e)
        {
            //inputPanel1.Enabled = false;
        }

        private void referenceNoBox_LostFocus_1(object sender, EventArgs e)
        {
            //inputPanel1.Enabled = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CustomerList customerList = new CustomerList(smartDatabase);
            customerList.ShowDialog();
            DataCustomer dataCustomer = customerList.getCustomer();
            if (dataCustomer != null)
            {
                dataSalesHeader.customerNo = dataCustomer.no;
                dataSalesHeader.name = dataCustomer.name;
                dataSalesHeader.address = dataCustomer.address;
                dataSalesHeader.address2 = dataCustomer.address2;
                dataSalesHeader.city = dataCustomer.city;
                dataSalesHeader.zipCode = dataCustomer.zipCode;
                dataSalesHeader.contact = dataCustomer.contact;
                dataSalesHeader.phoneNo = dataCustomer.phoneNo;

                dataSalesHeader.deliveryName = dataCustomer.name;
                dataSalesHeader.deliveryAddress = dataCustomer.address;
                dataSalesHeader.deliveryAddress2 = dataCustomer.address2;
                dataSalesHeader.deliveryCity = dataCustomer.city;
                dataSalesHeader.deliveryZipCode = dataCustomer.zipCode;

                dataSalesHeader.save();
                update();
            }

            customerList.Dispose();
        }

        private void update()
        {
            Agent agent = dataSetup.getAgent();
            this.label1.Text = "Order " + agent.agentId + dataSalesHeader.no.ToString();
            this.Text = label1.Text;
            this.customerNoBox.Text = dataSalesHeader.customerNo;
            this.customerNameBox.Text = dataSalesHeader.name;
            this.contactNameBox.Text = dataSalesHeader.contact;
            this.phoneNoBox.Text = dataSalesHeader.phoneNo;

        }

        private void save()
        {
            dataSalesHeader.contact = this.contactNameBox.Text;
            dataSalesHeader.phoneNo = this.phoneNoBox.Text;
            dataSalesHeader.noteOfGoods = this.goodsmarkBox.Text;
            dataSalesHeader.customerReferenceNo = this.referenceNoBox.Text;
            dataSalesHeader.save();
        }

        private void scanBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;
                DataCustomer dataCustomer = new DataCustomer(scanBox.Text, smartDatabase);

                if (dataCustomer.name != "")
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

                    dataSalesHeader.customerNo = dataCustomer.no;
                    dataSalesHeader.name = dataCustomer.name;
                    dataSalesHeader.address = dataCustomer.address;
                    dataSalesHeader.address2 = dataCustomer.address2;
                    dataSalesHeader.city = dataCustomer.city;
                    dataSalesHeader.zipCode = dataCustomer.zipCode;
                    dataSalesHeader.contact = dataCustomer.contact;
                    dataSalesHeader.phoneNo = dataCustomer.phoneNo;

                    dataSalesHeader.deliveryName = dataCustomer.name;
                    dataSalesHeader.deliveryAddress = dataCustomer.address;
                    dataSalesHeader.deliveryAddress2 = dataCustomer.address2;
                    dataSalesHeader.deliveryCity = dataCustomer.city;
                    dataSalesHeader.deliveryZipCode = dataCustomer.zipCode;

                    dataSalesHeader.save();
                    update();

                }
                else
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

                }

                e.Handled = true;
                this.scanBox.Text = "";
                this.scanBox.Focus();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            save();

            if (System.Windows.Forms.MessageBox.Show("Du har valt att avbryta. Vill du radera ordern?", "Avbryta", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                dataSalesHeader.delete();
            }
            this.Close();
        }
    }
}