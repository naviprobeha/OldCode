using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartOrder
{
    public partial class Order : Form
    {
        private SmartDatabase smartDatabase;
        private DataSet salesLineDataSet;
        private bool deleteFlag;
        private bool scanMode;
        private DataSetup dataSetup;
        private DataSalesLines dataSalesLines;
        private DataSalesHeader dataSalesHeader;
        private DataItemCrossReference currentCrossReference;
        private DataItem currentDataItem;

        public Order(DataSalesHeader dataSalesHeader, SmartDatabase smartDatabase, bool deleteFlag)
        {
            InitializeComponent();

            this.smartDatabase = smartDatabase;
            this.dataSalesLines = new DataSalesLines(smartDatabase);

            this.dataSetup = smartDatabase.getSetup();

            this.dataSalesHeader = dataSalesHeader;
            this.deleteFlag = deleteFlag;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Order_Load(object sender, EventArgs e)
        {
            Agent agent = smartDatabase.getAgent();

            DataDeliveryAddresses deliveryAddresses = new DataDeliveryAddresses(smartDatabase);
            DataSet deliveryAddressesDataSet = deliveryAddresses.getDataSet(new DataCustomer(dataSalesHeader.customerNo));

            deliveryCode.Items.Add("Standard");

            int i = 0;
            while (i < deliveryAddressesDataSet.Tables[0].Rows.Count)
            {
                deliveryCode.Items.Add((string)deliveryAddressesDataSet.Tables[0].Rows[i].ItemArray.GetValue(2));
                i++;
            }

            DataPaymentMethods paymentMethods = new DataPaymentMethods(smartDatabase);
            DataSet paymentMethodDataSet = paymentMethods.getDataSet();

            DataUserReferences userReferences = new DataUserReferences(smartDatabase);
            DataSet userReferencesDataSet = userReferences.getDataSet();

            DataSalesPersons salesPersons = new DataSalesPersons(smartDatabase);
            DataSet salesPersonsDataSet = salesPersons.getDataSet();

            //i = 0;
            //while (i < paymentMethodDataSet.Tables[0].Rows.Count)
            //{
            //	paymentMethod.Items.Add((string)paymentMethodDataSet.Tables[0].Rows[i].ItemArray.GetValue(0));
            //	i++;
            //}

            paymentMethod.DataSource = paymentMethodDataSet.Tables[0];
            userReferenceCode.DataSource = userReferencesDataSet.Tables[0];
            salesPersonCode.DataSource = salesPersonsDataSet.Tables[0];

            orderNo.Text = agent.agentId + "" + dataSalesHeader.no;
            //customerNo.Text = dataSalesHeader.customerNo;
            name.Text = dataSalesHeader.name;
            address.Text = dataSalesHeader.address;
            zipCode.Text = dataSalesHeader.zipCode;
            city.Text = dataSalesHeader.city;

            deliveryCode.Text = dataSalesHeader.deliveryCode;
            deliveryName.Text = dataSalesHeader.deliveryName;
            deliveryAddress.Text = dataSalesHeader.deliveryAddress;
            deliveryAddress2.Text = dataSalesHeader.deliveryAddress2;
            deliveryZipCode.Text = dataSalesHeader.deliveryZipCode;
            deliveryCity.Text = dataSalesHeader.deliveryCity;
            deliveryContact.Text = dataSalesHeader.deliveryContact;
            paymentMethod.Text = dataSalesHeader.paymentMethod;
            //postingMethod.SelectedIndex = dataSalesHeader.postingMethod;
            discountBox.Text = dataSalesHeader.discount.ToString();
            userReferenceCode.Text = dataSalesHeader.referenceCode;
            salesPersonCode.Text = dataSalesHeader.salesPersonCode;

            if (userReferenceCode.Text == "") userReferenceCode.Text = dataSetup.internalUserReferenceCode;

            if (deliveryCode.Text == "")
            {
                deliveryCode.Text = "Standard";
                deliveryName.Text = name.Text;
                deliveryAddress.Text = address.Text;
                deliveryAddress2.Text = dataSalesHeader.address2;
                deliveryZipCode.Text = zipCode.Text;
                deliveryCity.Text = city.Text;
            }

            if (dataSalesHeader.ready)
            {
                releaseBtn.Text = "Öppna";
            }
            else
            {
                releaseBtn.Text = "Klar";
            }

            updateGrid();
            checkReadyFlag();

            if (deleteFlag) deleteBtn.Visible = true;

            if (dataSetup.itemScannerEnabled)
            {
                scanCode.Visible = true;
                label16.Visible = true;
                label14.Visible = false;
                scanMode = true;
            }
            else
            {
                scanCode.Visible = false;
                label16.Visible = false;
                label14.Visible = true;
                scanMode = false;
            }


            if (!dataSetup.showOrderItemsItemNo) itemNoCol.MappingName = "";
            if (!dataSetup.showOrderItemsVariant) colorCol.MappingName = "";
            if (!dataSetup.showOrderItemsBaseUnit) baseUnitCol.MappingName = "";
            if (!dataSetup.showOrderItemsDeliveryDate) deliveryDateCol.MappingName = "";

            if ((dataSalesHeader.paymentMethod == "FAKTURA") && (dataSalesHeader.preInvoiceNo != ""))
            {
                this.preInvoiceBox.Checked = true;
            }

            tabControl1.Focus();
        }

        private void updateGrid()
        {
            salesLineDataSet = dataSalesLines.getSimpleDataSet(dataSalesHeader);

            //DataColumn lineUnitPriceCol = salesLineDataSet.Tables[0].Columns.Add("lineUnitPrice");
            //DataColumn lineAmountCol = salesLineDataSet.Tables[0].Columns.Add("lineAmount");

            salesLineGrid.DataSource = salesLineDataSet.Tables[0];
            /*
            int i = 0;

            if (salesLineDataSet.Tables[0].Rows.Count > 0)
            {
                while (i < salesLineDataSet.Tables[0].Rows.Count)
                {
			
                    salesLineGrid[i, salesLineTable.GridColumnStyles.IndexOf(this.unitPriceCol)] = String.Format("{0:f}", salesLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(11));
                    salesLineGrid[i, salesLineTable.GridColumnStyles.IndexOf(this.amountCol)] = String.Format("{0:f}", salesLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(12));

                    i++;
                }
	
                salesLineGrid.CurrentRowIndex = 0;
            }
            */
        }

        private void checkReadyFlag()
        {
            if (dataSalesHeader.ready)
            {
                name.ReadOnly = true;
                address.ReadOnly = true;
                zipCode.ReadOnly = true;
                city.ReadOnly = true;

                deliveryCode.Enabled = false;
                deliveryName.ReadOnly = true;
                deliveryAddress.ReadOnly = true;
                deliveryAddress2.ReadOnly = true;
                deliveryZipCode.ReadOnly = true;
                deliveryCity.ReadOnly = true;
                deliveryContact.ReadOnly = true;
                discountBox.ReadOnly = true;

                addLine.Enabled = false;
                showLine.Enabled = false;
                deleteLine.Enabled = false;

            }
            else
            {
                name.ReadOnly = false;
                address.ReadOnly = false;
                //address2.ReadOnly = false;
                zipCode.ReadOnly = false;
                city.ReadOnly = false;

                deliveryCode.Enabled = true;
                deliveryName.ReadOnly = false;
                deliveryAddress.ReadOnly = false;
                deliveryAddress2.ReadOnly = false;
                deliveryZipCode.ReadOnly = false;
                deliveryCity.ReadOnly = false;
                deliveryContact.ReadOnly = false;
                discountBox.ReadOnly = false;

                addLine.Enabled = true;
                showLine.Enabled = true;
                deleteLine.Enabled = true;
            }

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Är du säker?", "Radera order", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                dataSalesHeader.delete();
                this.Close();
            }
        }

        private void name_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;       
        }

        private void address_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void zipCode_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void city_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputPanel1.Enabled = false;

            save();

            tabControl1.Focus();

            if (tabControl1.SelectedIndex == 2)
            {
                if (scanCode.Enabled) scanCode.Focus();
            }
        }

        private void save()
        {
            dataSalesHeader.name = name.Text;
            dataSalesHeader.address = address.Text;
            dataSalesHeader.zipCode = zipCode.Text;
            dataSalesHeader.city = city.Text;

            dataSalesHeader.deliveryCode = deliveryCode.Text;
            dataSalesHeader.deliveryName = deliveryName.Text;
            dataSalesHeader.deliveryAddress = deliveryAddress.Text;
            dataSalesHeader.deliveryAddress2 = deliveryAddress2.Text;
            dataSalesHeader.deliveryZipCode = deliveryZipCode.Text;
            dataSalesHeader.deliveryCity = deliveryCity.Text;
            dataSalesHeader.deliveryContact = deliveryContact.Text;

            dataSalesHeader.paymentMethod = paymentMethod.Text;
            //dataSalesHeader.postingMethod = postingMethod.SelectedIndex;
            dataSalesHeader.discount = float.Parse(discountBox.Text);

            dataSalesHeader.referenceCode = userReferenceCode.Text;
            dataSalesHeader.salesPersonCode = salesPersonCode.Text;

            dataSalesHeader.save();
        }

        private void deliveryName_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void deliveryAddress_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void deliveryAddress2_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void deliveryZipCode_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void deliveryCity_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void deliveryContact_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void deliveryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deliveryCode.Text == "Standard")
            {
                deliveryName.Text = name.Text;
                deliveryAddress.Text = address.Text;
                deliveryAddress2.Text = dataSalesHeader.address2;
                deliveryZipCode.Text = zipCode.Text;
                deliveryCity.Text = city.Text;
            }
            else
            {
                DataDeliveryAddress dataDeliveryAddress = new DataDeliveryAddress(dataSalesHeader.customerNo, deliveryCode.Text, smartDatabase);

                deliveryName.Text = dataDeliveryAddress.name;
                deliveryAddress.Text = dataDeliveryAddress.address;
                deliveryAddress2.Text = dataDeliveryAddress.address2;
                deliveryZipCode.Text = dataDeliveryAddress.zipCode;
                deliveryCity.Text = dataDeliveryAddress.city;
                deliveryContact.Text = dataDeliveryAddress.contact;
            }
        }

        private void releaseBtn_Click(object sender, EventArgs e)
        {
            if (dataSalesHeader.ready)
            {
                if (dataSalesHeader.preInvoiceNo == "")
                {
                    dataSalesHeader.ready = false;
                    releaseBtn.Text = "Klar";
                }
                else
                {
                    MessageBox.Show("Ordern är fakturerad och går inte att öppna.", "Fel");
                }
            }
            else
            {
                dataSalesHeader.ready = true;
                releaseBtn.Text = "Öppna";
            }
            checkReadyFlag();

        }

        private void Order_Closing(object sender, CancelEventArgs e)
        {
            save();

            dataSetup.internalUserReferenceCode = userReferenceCode.Text;

            
            if (dataSalesLines.countSalesLines(dataSalesHeader) == 0)
            {
                if (MessageBox.Show("Inga orderrader registrerade. Vill du radera ordern?", "Radera", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    dataSalesHeader.delete();
                    salesLineGrid.DataSource = null;
                    salesLineGrid.Dispose();
                }
            }
            else
            {
                if (userReferenceCode.Text == "")
                {
                    MessageBox.Show("Du har inte valt referens.");
                    e.Cancel = true;
                }
                else
                {
                    if (!dataSalesHeader.deleted)
                    {
                        if (MessageBox.Show("Antal lådor: " + dataSalesLines.countSalesLineBoxes(dataSalesHeader) + ". Riktigt?", "Lådor", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No) return;

                        if (dataSalesHeader.paymentMethod == "FAKTURA")
                        {
                            if (this.preInvoiceBox.Checked)
                            {
                                dataSalesHeader.createPreInvoiceNo();
                            }
                        }
                        if ((dataSalesHeader.paymentMethod == "KONTANT") || (dataSalesHeader.paymentMethod == "KORT"))
                        {
                            dataSalesHeader.createPreInvoiceNo();
                        }

                        dataSalesHeader.save();

                        if (dataSetup.printOnLocalPrinter)
                        {
                            dataSalesHeader.ready = true;
                            dataSalesHeader.save();

                            if (MessageBox.Show("Vill du skriva ut?", "Skriv ut", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {

                                Printing printing = new Printing(smartDatabase);

                                if (!printing.init())
                                {
                                    if (MessageBox.Show("Kan ej få kontakt med skrivaren. Kontrollera skrivaren. Gå vidare?", "Fel", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    Cursor.Current = Cursors.WaitCursor;
                                    Cursor.Show();

                                    printing.printOrder(dataSalesHeader);

                                    if (MessageBox.Show("Vill du skriva ut en kopia?", "Kopia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                    {
                                        printing.setCopy();
                                        printing.printOrder(dataSalesHeader);
                                    }

                                    printing.close();

                                    Cursor.Current = Cursors.Default;
                                    Cursor.Hide();

                                }
                            }

                        }
                    }

                    if ((dataSetup.askSynchronization) && (!dataSalesHeader.deleted))
                    {
                        if (MessageBox.Show("Vill du synkronisera ordern?", "Synkronisera", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            dataSalesHeader.ready = true;
                            dataSalesHeader.save();

                            Sync sync = new Sync(smartDatabase, "sendOrders");
                            sync.ShowDialog();
                            sync.Dispose();

                            salesLineGrid.DataSource = null;
                            salesLineGrid.Dispose();

                        }
                    }
                }
            }
			
        }

        private void discountBox_GotFocus(object sender, EventArgs e)
        {
            if (!discountBox.ReadOnly)
            {
                DataItem dataItem = new DataItem();
                dataItem.description = "Fakturarabatt";
                /*QuantityForm discountForm = new QuantityForm(dataItem);
                discountForm.setCaption("Rabatt (%):");
                discountForm.setValue(discountBox.Text);
                discountForm.ShowDialog();
                if (discountForm.getStatus() == 1)
                {
                    discountBox.Text = discountForm.getValue("{0:f}");
                    if (discountBox.Text == "") discountBox.Text = "0";
                    this.Focus();
                }*/
            }
        }

        private void addLine_Click(object sender, EventArgs e)
        {
            int wizardLevel = 1;
            DataItem selectedItem = null;
            DataColor selectedColor = null;
            DataItemVariantDim selectedVariant = null;

            string searchString = "";
            string prodGroupCode = "";

            while ((wizardLevel != 6) && (wizardLevel != 0))
            {
                if (wizardLevel == 1)
                {

                    ItemList itemList = new ItemList(dataSalesHeader, smartDatabase, searchString);
                    itemList.setSelected(selectedItem);
                    if (prodGroupCode != "") itemList.setProdGroupCode(prodGroupCode);
                    itemList.ShowDialog();
                    selectedItem = itemList.getSelected();
                    selectedColor = null;
                    selectedVariant = null;
                    if (itemList.getStatus() == 1)
                    {
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                        System.Windows.Forms.Cursor.Show();

                        if (selectedItem.hasVariants())
                            wizardLevel = 7;
                        else
                        {
                            wizardLevel = 4;
                        }
                    }
                    if (itemList.getStatus() == 2)
                    {
                        wizardLevel = 5;
                    }
                    if (itemList.getStatus() == 0)
                    {
                        wizardLevel = 0;
                    }
                    itemList.Dispose();
                    GC.Collect();
                }


                if (wizardLevel == 4)
                {
                    OrderItem itemForm;
                    DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, smartDatabase);

                    if (selectedVariant != null)
                    {
                        dataSalesLine.itemNo = selectedItem.no;
                        dataSalesLine.baseUnit = selectedItem.baseUnit;
                        dataSalesLine.colorCode = selectedVariant.code;
                        dataSalesLine.description = selectedItem.description;

                        itemForm = new OrderItem(smartDatabase, dataSalesHeader, dataSalesLine, selectedItem);
                    }
                    else
                    {
                        dataSalesLine.itemNo = selectedItem.no;
                        dataSalesLine.baseUnit = selectedItem.baseUnit;
                        dataSalesLine.description = selectedItem.description;

                        itemForm = new OrderItem(smartDatabase, dataSalesHeader, dataSalesLine, selectedItem);

                    }

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    System.Windows.Forms.Cursor.Hide();

                    itemForm.hideButtons();
                    itemForm.ShowDialog();

                    if ((dataSalesHeader.deliveryDate == null) || (dataSalesHeader.deliveryDate == ""))
                    {
                        dataSalesHeader.deliveryDate = dataSalesLine.deliveryDate;
                    }

                    if (itemForm.getStatus() == 1)
                    {
                        wizardLevel = 6;
                    }
                    else
                        wizardLevel = 1;

                    itemForm.Dispose();
                }



                if (wizardLevel == 6)
                {
                    selectedColor = null;

                    updateGrid();
                }



            }
            if (scanCode.Enabled) scanCode.Focus();

        }

        private void deleteLine_Click(object sender, EventArgs e)
        {
            if (salesLineDataSet.Tables[0].Rows.Count > 0)
            {
                int seqNo = int.Parse(salesLineDataSet.Tables[0].Rows[salesLineGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString());

                DataSalesLine dataSalesLine = new DataSalesLine(seqNo, smartDatabase);

                dataSalesLine.delete();

                updateGrid();
                if (scanCode.Enabled) scanCode.Focus();
            }

        }

        private void showLine_Click(object sender, EventArgs e)
        {
            if (salesLineGrid.BindingContext[salesLineGrid.DataSource, ""].Count > 0)
            {
                if (dataSalesHeader.ready)
                {
                    System.Windows.Forms.MessageBox.Show("Antalet går inte att ändra när ordern är klarmarkerad.");
                }
                else
                {
                    int seqNo = int.Parse(salesLineDataSet.Tables[0].Rows[salesLineGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString());

                    DataSalesLine dataSalesLine = new DataSalesLine(seqNo, smartDatabase);

                    DataItem dataItem = new DataItem(dataSalesLine.itemNo, smartDatabase);

                    OrderItem itemForm = new OrderItem(smartDatabase, dataSalesHeader, dataSalesLine, dataItem);
                    itemForm.hideButtons();
                    itemForm.ShowDialog();
                    itemForm.Dispose();
                }

            }
            updateGrid();
            if (scanCode.Enabled) scanCode.Focus();

        }

        private void scanCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!dataSalesHeader.ready)
            {
                if ((e.KeyChar == 13) || (e.KeyChar == '>'))
                {
                    e.Handled = true;

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                    System.Windows.Forms.Cursor.Show();

                    currentDataItem = new DataItem(scanCode.Text, smartDatabase, true);
                    if (currentDataItem.barCodeFound)
                    {
                        DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, smartDatabase);
                        dataSalesLine.itemNo = currentDataItem.no;
                        dataSalesLine.description = currentDataItem.description;
                        dataSalesLine.baseUnit = currentDataItem.baseUnit;

                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                        System.Windows.Forms.Cursor.Hide();

                        OrderItem orderItem = new OrderItem(smartDatabase, dataSalesHeader, dataSalesLine, currentDataItem);
                        orderItem.ShowDialog();
                        orderItem.Dispose();
                        updateGrid();
                        scanCode.Text = "";
                        scanCode.Focus();

                    }
                    else
                    {
                        DataItemCrossReferences dataItemCrossReferences = new DataItemCrossReferences(smartDatabase);
                        currentCrossReference = dataItemCrossReferences.getEanCode(scanCode.Text);
                        if (currentCrossReference != null)
                        {
                            Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);


                            currentDataItem = currentCrossReference.item;
                            if ((currentCrossReference.variantDimCode != "") && (!dataItemCrossReferences.multipleMatches))
                            {

                                DataItemVariantDim currentVariant = new DataItemVariantDim(currentDataItem, currentCrossReference.variantDimCode, smartDatabase);

                                DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, smartDatabase);
                                dataSalesLine.itemNo = currentDataItem.no;
                                dataSalesLine.colorCode = currentVariant.code;
                                dataSalesLine.description = currentDataItem.description;
                                dataSalesLine.baseUnit = currentDataItem.baseUnit;

                                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                                System.Windows.Forms.Cursor.Hide();

                                OrderItem orderItem = new OrderItem(smartDatabase, dataSalesHeader, dataSalesLine, currentDataItem);
                                orderItem.ShowDialog();
                                orderItem.Dispose();
                                updateGrid();
                                scanCode.Text = "";
                                scanCode.Focus();
                            }
                            else
                            {
                                if (currentDataItem.hasVariants())
                                {
                                }
                                else
                                {

                                    DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, smartDatabase);
                                    dataSalesLine.itemNo = currentDataItem.no;
                                    dataSalesLine.description = currentDataItem.description;
                                    dataSalesLine.baseUnit = currentDataItem.baseUnit;

                                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                                    System.Windows.Forms.Cursor.Hide();

                                    OrderItem orderItem = new OrderItem(smartDatabase, dataSalesHeader, dataSalesLine, currentDataItem);
                                    orderItem.ShowDialog();
                                    orderItem.Dispose();
                                    updateGrid();
                                    scanCode.Text = "";
                                    scanCode.Focus();
                                }
                            }
                        }
                        else
                        {
                            Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
                            scanCode.Text = "";

                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                            System.Windows.Forms.Cursor.Hide();

                        }
                    }
                }
            }

        }


    }
}