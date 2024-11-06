﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class Message
    {
        private int _entryNo;
        private string _organizationNo;
        private string _fromName;
        private string _message;

        public Message()
        { }

        public Message(Navipro.SantaMonica.Common.Message message)
        {
            this._entryNo = message.entryNo;
            this._organizationNo = message.organizationNo;
            this._fromName = message.fromName;
            this._message = message.message;
        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public string organizationNo { get { return _organizationNo; } set { _organizationNo = value; } }
        public string fromName { get { return _fromName; } set { _fromName = value; } }
        public string message { get { return _message; } set { _message = value; } }
    }
}