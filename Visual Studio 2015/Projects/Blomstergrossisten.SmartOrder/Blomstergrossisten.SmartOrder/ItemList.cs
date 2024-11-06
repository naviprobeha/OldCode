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
    public partial class ItemList : Form
    {

        private int status;
        private DateTime clickTime;
        private string searchString;
        private DataSetup setup;
        private SmartDatabase smartDatabase;
        private System.Windows.Forms.ComboBox prodGroupCodeBox;
        private DataSalesHeader dataSalesHeader;
        private DataItems dataItems;
        private bool updateGridFlag;
        private int searchMethod;
        private bool itemGridBound;
        private DataItem selectedItem;

        public ItemList(DataSalesHeader dataSalesHeader, SmartDatabase smartDatabase, string searchString)
        {
            setup = smartDatabase.getSetup();

            InitializeComponent();

            this.searchString = searchString;
            this.prodGroupCodeBox = new System.Windows.Forms.ComboBox();
            this.prodGroupCodeBox.Location = new System.Drawing.Point(0, 0);
            this.prodGroupCodeBox.Visible = false;
            this.prodGroupCodeBox.Size = new System.Drawing.Size(1, 1);

            this.smartDatabase = smartDatabase;
            this.dataItems = new DataItems(smartDatabase);

            this.dataSalesHeader = dataSalesHeader;


            status = 0;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            DataProductGroups productGroups = new DataProductGroups(smartDatabase);
            DataSet productGroupsDataSet = productGroups.getDataSet();

            prodGroupBox.Items.Add("Alla");
            prodGroupCodeBox.Items.Add("Alla");

            int i = 0;
            while (i < productGroupsDataSet.Tables[0].Rows.Count)
            {
                prodGroupBox.Items.Add((string)productGroupsDataSet.Tables[0].Rows[i].ItemArray.GetValue(1));
                prodGroupCodeBox.Items.Add((string)productGroupsDataSet.Tables[0].Rows[i].ItemArray.GetValue(0));
                i++;
            }

            /*
            DataSeasons seasons = new DataSeasons(smartDatabase);
            DataSet seasonsDataSet = seasons.getDataSet();

            seasonBox.Items.Add("Alla");
			
            i = 0;
            while (i < seasonsDataSet.Tables[0].Rows.Count)
            {
                seasonBox.Items.Add((string)seasonsDataSet.Tables[0].Rows[i].ItemArray.GetValue(0));
                i++;
            }
	
            */
            /*
            if (dataSalesHeader.seasonCode != null)
            {

                seasonBox.Text = dataSalesHeader.seasonCode;
            }
            else
            {
                seasonBox.Text = "Alla";
            }
             * */

            if (dataSalesHeader.productGroupCode != null)
            {
                prodGroupBox.Text = dataSalesHeader.productGroupCode;
            }
            else
            {
                prodGroupBox.Text = "Alla";
            }

            prodGroupBox.SelectedIndex = setup.internalProductGroupSelectedIndex;

            updateGridFlag = true;
            searchMethod = -1;
            this.updateSearchMethod(setup.itemSearchMethod, false);

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ItemList_Load(object sender, EventArgs e)
        {
            if (searchMethod >= 2)
            {
                updateGrid();
            }
            else
            {
                searchBox.Focus();
            }
        }

        private void updateGrid()
        {
            if (updateGridFlag)
            {
                itemGridBound = true;

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                System.Windows.Forms.Cursor.Show();

                System.Data.DataSet itemDataSet = dataItems.getDataSet("", prodGroupCodeBox.Text, searchMethod, searchString);

                // update price column
                /*
                System.Data.DataTable itemTable = itemDataSet.Tables[0];
                itemTable.Columns.Add("price");
                */

                itemGrid.DataSource = itemDataSet.Tables[0];

                /*
                int j = 0;
                while (j < itemTable.Rows.Count)
                {
                    DataItem dataItem = new DataItem((string)itemTable.Rows[j].ItemArray.GetValue(0));
                    DataItemPrice itemPrice = new DataItemPrice(dataItem, dataCustomer, smartDatabase);
                    itemGrid[j, 3] = Math.Round(itemPrice.amount, 2).ToString();

                    if (itemGrid[j, 3].ToString().IndexOf(",") > 0)
                    {
                        string decimals = itemGrid[j, 3].ToString().Substring(itemGrid[j, 3].ToString().IndexOf(",")+1);
                        if (decimals.Length < 2) 
                        {
                            itemGrid[j, 3] = itemGrid[j, 3].ToString() + "0";
                        }
                    }

                    j++;
                }
                */

                if (selectedItem != null)
                {
                    int i = 0;
                    bool found = false;

                    while ((i < itemGrid.BindingContext[itemGrid.DataSource, ""].Count) && !found)
                    {
                        if (itemGrid[i, 0].ToString().Equals(selectedItem.no))
                        {
                            itemGrid.CurrentRowIndex = i;
                            itemGrid.Select(i);
                            found = true;
                        }
                        i++;
                    }
                }

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                System.Windows.Forms.Cursor.Hide();
            }

            itemGrid.Focus();
        }

        private void itemGrid_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (selectedItem.no == itemGrid[itemGrid.CurrentRowIndex, 0].ToString())
                {
                    TimeSpan interval = DateTime.Now.Subtract(clickTime);
                    if (interval.TotalMilliseconds < 1500)
                    {
                        status = 1;
                        updateReUseData();
                        this.Close();

                    }
                }
            }
            else
            {
                try
                {
                    if (itemGrid.BindingContext[itemGrid.DataSource, ""].Count > 0)
                    {
                        selectedItem = new DataItem(itemGrid[itemGrid.CurrentRowIndex, 0].ToString(), smartDatabase);
                        clickTime = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    selectedItem = null;
                }
            }
        }

        public DataItem getSelected()
        {
            return selectedItem;
        }

        public void setSelected(DataItem dataItem)
        {
            selectedItem = dataItem;
        }

        public int getStatus()
        {
            return status;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!itemGridBound)
            {
                System.Windows.Forms.MessageBox.Show("Du måste välja en artikel.");
            }
            else
            {

                if ((itemGrid.BindingContext[itemGrid.DataSource, ""].Count == 0) || (itemGrid.CurrentRowIndex == -1))
                {
                    System.Windows.Forms.MessageBox.Show("Du måste välja en artikel.");
                }
                else
                {
                    selectedItem = new DataItem(itemGrid[itemGrid.CurrentRowIndex, 0].ToString(), smartDatabase);

                    status = 1;

                    updateReUseData();

                    this.Close();
                }
            }
        }

        private void updateReUseData()
        {
            //dataSalesHeader.seasonCode = seasonBox.Text;
            dataSalesHeader.productGroupCode = prodGroupBox.Text;

            setup.internalProductGroupSelectedIndex = prodGroupBox.SelectedIndex;
            setup.save();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            status = 0;
            //dataSalesHeader.seasonCode = seasonBox.Text;
            dataSalesHeader.productGroupCode = prodGroupBox.Text;
            this.Close();
        }

        private void prodGroupBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            prodGroupCodeBox.SelectedIndex = prodGroupBox.SelectedIndex;
            updateGrid();

        }

        private void updateSearchMethod(int searchMethod, bool update)
        {
            if (this.searchMethod != searchMethod)
            {
                this.searchMethod = searchMethod;

                searchBox.Text = "";

                if (searchMethod == 0)
                {
                    label1.Text = "Artikelnr:";
                    searchBox.Visible = true;
                    //seasonBox.Visible = false;

                    //menuItemNo.Checked = true;
                    //menuItemDesc.Checked = false;
                    //menuItemSeason.Checked = false;
                }
                if (searchMethod == 1)
                {
                    label1.Text = "Beskrivning:";
                    searchBox.Visible = true;
                    //seasonBox.Visible = false;

                    //menuItemNo.Checked = false;
                    //menuItemDesc.Checked = true;
                    //menuItemSeason.Checked = false;

                }
                if (searchMethod == 2)
                {
                    label1.Text = "Beskrivning:";
                    searchBox.Visible = false;
                    //seasonBox.Visible = true;

                    //menuItemNo.Checked = false;
                    //menuItemDesc.Checked = false;
                    //menuItemSeason.Checked = true;
                }

                if (searchMethod == 3)
                {
                    label1.Text = "";
                    searchBox.Visible = false;
                    //seasonBox.Visible = false;

                    //menuItemNo.Checked = false;
                    //menuItemDesc.Checked = false;
                    //menuItemSeason.Checked = false;
                }

                if (update) updateGrid();
            }
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            searchString = "searchDescription LIKE '" + searchBox.Text + "%' ";

        }

        private void searchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                updateGrid();
            }
        }

        public void setProdGroupCode(string prodGroupCode)
        {
            prodGroupCodeBox.Text = prodGroupCode;
            prodGroupBox.SelectedIndex = prodGroupCodeBox.SelectedIndex;
        }

        private void ItemList_Closing(object sender, CancelEventArgs e)
        {
            if (itemGrid.DataSource != null)
            {
                ((DataTable)itemGrid.DataSource).Clear();
            }
            //itemGrid.DataSource = null;
            itemGrid.Dispose();
        }

        private void searchBox_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void itemGrid_LostFocus(object sender, EventArgs e)
        {

        }

        private void searchBox_LostFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = false;
        }
    }
}