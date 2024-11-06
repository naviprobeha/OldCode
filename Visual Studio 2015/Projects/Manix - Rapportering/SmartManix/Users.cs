using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Navipro.SmartInventory
{
    public partial class Users : Form
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;
        private DataUserCollection _dataUserCollection;

        private DataUser _dataUser = null;


        public Users(Configuration configuration, SmartDatabase smartDatabase, DataUserCollection dataUserCollection)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            _dataUserCollection = dataUserCollection;

            updateGrid();
        }

        private void updateGrid()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();
           
            userGrid.DataSource = _dataUserCollection.getDataSet().Tables[0];

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

        }


        public DataUser getUser()
        {
            return _dataUser;
        }

        private void Users_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (userGrid.BindingContext[userGrid.DataSource, ""].Count > 0)
            {
                _dataUser = _dataUserCollection[userGrid.CurrentRowIndex];
                this.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Det finns inga användare i listan."), Translation.translate(configuration.languageCode, "Fel"), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}