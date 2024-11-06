using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{
    public class WebItemConfigLine
    {
        private string _webConfigModelNo;
        private string _webConfigId;
        private string _optionCode;
        private string _description;
        private int _type;
        private string _optionValue;
        private bool _required;
        private int _method;
        private int _maxLength;
        private int _width;
        private bool _visible;
        private bool _includeInDescription;
        private int _descriptionStyle;
        private int _sortOrder;
        private string _webConfigRuleCode;
        private int _assemblyMethod;
        private string _separateItemQtyOption;
        private string _errorMessage;
        private int _dataType;
        private float _lineAmount;
        private string _valueDescription;
        private string _text;
        private string _expression;

        private Infojet infojetContext;
        private WebItemConfigDefValueCollection _values;
        private System.Web.UI.WebControls.WebControl _control;

        public WebItemConfigLine(Infojet infojetContext, string webConfigId, WebItemConfigDef webItemConfigDef)
        {
            this.infojetContext = infojetContext;

            _webConfigModelNo = webItemConfigDef.webConfigModelNo;
            _webConfigId = webConfigId;
            _optionCode = webItemConfigDef.optionCode;
            _description = webItemConfigDef.description;
            _type = webItemConfigDef.defaultType;
            _optionValue = webItemConfigDef.defaultValue;
            _required = webItemConfigDef.required;
            _method = webItemConfigDef.method;
            _maxLength = webItemConfigDef.maxLength;
            _width = webItemConfigDef.width;
            _visible = webItemConfigDef.visible;
            _includeInDescription = webItemConfigDef.includeInDescription;
            _descriptionStyle = webItemConfigDef.descriptionStyle;
            _sortOrder = webItemConfigDef.sortOrder;
            _webConfigRuleCode = webItemConfigDef.webConfigRuleCode;
            _assemblyMethod = webItemConfigDef.assemblyMethod;
            _separateItemQtyOption = webItemConfigDef.separateItemQtyOption;
            _dataType = webItemConfigDef.dataType;
            _text = webItemConfigDef.text;
            _expression = webItemConfigDef.expression;
        }

        public void applyValues(WebItemConfigDefValueCollection webItemConfigDefValueCollection)
        {
            _values = new WebItemConfigDefValueCollection();

            int i = 0;
            while (i < webItemConfigDefValueCollection.Count)
            {
                if (webItemConfigDefValueCollection[i].optionCode == this.optionCode)
                {
                    WebItemConfigDefValue webItemConfigDefValue = new WebItemConfigDefValue(infojetContext, webItemConfigDefValueCollection[i]);
                    
                    _values.Add(webItemConfigDefValue);

                    webItemConfigDefValueCollection.RemoveAt(i);
                    i--;
                }
                i++;
            }
        }

        public void updateLineAmount(WebItemConfigHeader webItemConfigHeader)
        {
            if (this.optionCode == "") _lineAmount = 0;
            if (this.optionValue == "") _lineAmount = 0;

            _lineAmount = WebItemConfigDefValue.calcConfigDefValuePrice(infojetContext, webItemConfigHeader, optionCode, type, optionValue);
            float discount = WebItemConfigDefValue.calcConfigDefValueDiscount(infojetContext, webItemConfigHeader, optionCode, type, optionValue);
            if (discount > 0) _lineAmount = _lineAmount - (_lineAmount * (discount / 100));

        }

        public string webConfigModelNo { get { return _webConfigModelNo; } }
        public string webConfigId { get { return _webConfigId; } }
        public string optionCode { get { return _optionCode; } }
        public string description { get { return _description; } }
        public int type { get { return _type; } set { _type = value; } }
        public string optionValue { get { return _optionValue; } set { _optionValue = value; } }
        public bool required { get { return _required; } }
        public int method { get { return _method; } }
        public int maxLength { get { return _maxLength; } set { _maxLength = value; } }
        public bool visible { get { return _visible; } set { _visible = value; } }
        public bool includeInDescription { get { return _includeInDescription; } }
        public int descriptionStyle { get { return _descriptionStyle; } }
        public int sortOrder { get { return _sortOrder; } }
        public string webConfigRuleCode { get { return _webConfigRuleCode; } }
        public string controlId { get { return optionCode.Replace(" ", "_"); } }
        public WebItemConfigDefValueCollection values { get { return _values; } }
        public int assemblyMethod { get { return _assemblyMethod; } }
        public string separateItemQtyOption { get { return _separateItemQtyOption; } }
        public string errorMessage { get { return _errorMessage; } set { _errorMessage = value; } }
        public string text { get { return _text; } set { _text = value; } }
        public string expression { get { return _expression; } set { _expression = value; } }
        public int dataType { get { return _dataType; } }
        public int width { get { return _width; } }
        public float lineAmount { get { return _lineAmount; } }
        public System.Web.UI.WebControls.WebControl control { get { return _control; } set { _control = value; } }
        public string valueDescription
        {
            get
            {
                return _valueDescription;
            }
            set
            {
                _valueDescription = value;
            }
        }

        public void setValue(int type, string value)
        {
            _type = type;
            _optionValue = value;
            _valueDescription = "";

            
            WebItemConfigDefValue webItemConfigDefValue = WebItemConfigDefValue.getConfigDefValue(infojetContext, _webConfigModelNo, _optionCode, _type, _optionValue);
            if (webItemConfigDefValue != null)
            {
                _valueDescription = webItemConfigDefValue.description;
            }
            if ((_valueDescription == "") || (_valueDescription == null)) _valueDescription = _optionValue;


        }

    }
}
