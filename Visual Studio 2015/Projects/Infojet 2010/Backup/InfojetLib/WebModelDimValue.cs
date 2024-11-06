﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{
    public class WebModelDimValue
    {
        private string _code;
        private string _description;
        private string _itemNo;
        private string _itemVariantCode;

        private WebModelDimValueCollection _subLevelValues;

        public WebModelDimValue(WebItemVarDimValue webItemVarDimValue)
        {
            this._code = webItemVarDimValue.code;
            this._description = webItemVarDimValue.description;

        }

        public WebModelDimValue(WebModelDimValue webModelDimValue)
        {
            this._code = webModelDimValue.code;
            this._description = webModelDimValue.description;
            this._itemNo = webModelDimValue.itemNo;
            this._itemVariantCode = webModelDimValue.itemVariantCode;
        }

        public string code { get { return _code; } }
        public string description { get { return _description; } }
        public WebModelDimValueCollection subLevelValues { get { return _subLevelValues; } set { _subLevelValues = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string itemVariantCode { get { return _itemVariantCode; } set { _itemVariantCode = value; } }

        public WebModelDimValue clone()
        {
            return new WebModelDimValue(this);
        }
    }
}