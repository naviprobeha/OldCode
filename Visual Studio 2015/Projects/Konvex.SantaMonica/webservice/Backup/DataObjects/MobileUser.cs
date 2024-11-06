using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class MobileUser
    {
        private int _entryNo;
        private string _organizationNo;
        private string _name;
        private string _password;

        public MobileUser()
        { }

        public MobileUser(Navipro.SantaMonica.Common.MobileUser mobileUser)
        {
            _entryNo = mobileUser.entryNo;
            _organizationNo = mobileUser.organizationNo;
            _name = mobileUser.name;
            _password = mobileUser.passWord;
        }
               
        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public string organizationNo { get { return _organizationNo; } set { _organizationNo = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public string password { get { return _password; } set { _password = value; } }
    }
}
