using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvensktTenn.Pick
{
    public class MyTextBox : TextBox
    {
        protected override bool IsInputKey(Keys key)
        {
            if (key == Keys.Enter)
                return true;
            return base.IsInputKey(key);
        }
    }
}
