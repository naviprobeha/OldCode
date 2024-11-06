﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class Agent
    {
        public string _code;
        public string _description;
        public string _organizationNo;

        public Agent()
        { }

        public Agent(Navipro.SantaMonica.Common.Agent agent)
        {
            _code = agent.code;
            _description = agent.description;
            _organizationNo = agent.organizationNo;
        }

        public string code { get { return _code; } set { _code = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string organizationNo { get { return _organizationNo; } set { _organizationNo = value; } }
    }
}