using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Navipro.SmartInventory
{
    public partial class ErrorMessage : Form
    {
        public ErrorMessage()
        {
            InitializeComponent();
        }

        public void setMessage(string message)
        {
            descriptionBox.Text = message;
        }

        public static void show(string message)
        {
            ErrorMessage errorMessage = new ErrorMessage();
            errorMessage.setMessage(message);
            errorMessage.ShowDialog();
            errorMessage.Dispose();
        }
    }
}
