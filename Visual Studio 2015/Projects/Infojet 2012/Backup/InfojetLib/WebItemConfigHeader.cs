using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    public class WebItemConfigHeader
    {
        private string _webConfigModelNo;
        private string _webConfigId;
        private string _itemNo;
        private float _quantity;
        private float _unitPrice;
        private string _description;
        private bool _validated;
        private bool _required;
        private int _referenceStyle;
        private int _cartEntryNo;
        private float _cartQuantity;

        private Panel _formPanel;

        private WebItemConfigLineCollection _webItemConfigLineCollection;
        private Infojet infojetContext;

        public WebItemConfigHeader(Infojet infojetContext, string itemNo, string webConfigModelNo)
        {
            this.infojetContext = infojetContext;

            this._itemNo = itemNo;
            this._webConfigModelNo = webConfigModelNo;

            this._webConfigId = Guid.NewGuid().ToString();
            this._quantity = 1;
            this._unitPrice = 0;

            loadDefaultConfiguration();
        }

        private void loadDefaultConfiguration()
        {
            _webItemConfigLineCollection = new WebItemConfigLineCollection();

            WebItemConfigDefCollection webItemConfigDefCollection = WebItemConfigDef.getConfigDef(infojetContext, _webConfigModelNo);

            int i = 0;
            while (i < webItemConfigDefCollection.Count)
            {
                WebItemConfigLine webItemConfigLine = new WebItemConfigLine(infojetContext, _webConfigId, webItemConfigDefCollection[i]);
                webItemConfigLine.setValue(webItemConfigLine.type, webItemConfigLine.optionValue);


                _webItemConfigLineCollection.Add(webItemConfigLine);

                i++;
            }

            WebItemConfigDefValueCollection webItemConfigDefValueCollection = WebItemConfigDefValue.getConfigDefValues(infojetContext, _webConfigModelNo);
            _webItemConfigLineCollection.applyValues(webItemConfigDefValueCollection);

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Reference Style] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Config Model") + "] WHERE [No_] = @modelNo");
            databaseQuery.addStringParameter("modelNo", this.webConfigModelNo, 20);
            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                _referenceStyle = dataReader.GetInt32(0);
            }
            dataReader.Close();

            if ((System.Web.HttpContext.Current.Request["cartEntryNo"] != null) && (System.Web.HttpContext.Current.Request["cartEntryNo"] != ""))
            {
                //Update existing cart line
                _cartEntryNo = int.Parse(System.Web.HttpContext.Current.Request["cartEntryNo"]);
                WebCartConfigLineCollection webCartConfigLineCollection = WebCartConfigLine.getCartConfigLines(infojetContext, _cartEntryNo, false);

                i = 0;
                while (i < _webItemConfigLineCollection.Count)
                {
                    int j = 0;
                    while (j < webCartConfigLineCollection.Count)
                    {
                        if (_webItemConfigLineCollection[i].optionCode == webCartConfigLineCollection[j].optionCode)
                        {
                            _webItemConfigLineCollection[i].optionValue = webCartConfigLineCollection[j].value;
                            _webItemConfigLineCollection[i].visible = webCartConfigLineCollection[j].visible;
                            _webItemConfigLineCollection[i].valueDescription = webCartConfigLineCollection[j].valueDescription;
                            
                        }
                        j++;
                    }
                    i++;
                }

            }

            webItemConfigLineCollection.updateLineAmount(this);
            updateUnitPrice();
        }

        public Panel getFormPanel()
        {

            if (_formPanel == null) _formPanel = new Panel();
            _formPanel.Controls.Clear();
            _formPanel.ID = "configForm_" + this._itemNo;

            Panel rowPanel = new Panel();
            rowPanel.Width = Unit.Percentage(100);
            rowPanel.Style.Add("float", "left");

            Panel colPanel = new Panel();
            colPanel.Style.Add("float", "left");

            Table table = new Table();


            //Kopiera konfigurering från annan rad
            //if (infojetContext.userSession.webUserAccount.userId.ToUpper() == "TSET")
            if (1 == 1) //Copy active
            {
                TableRow captionRowCopy = new TableRow();
                TableRow fieldRowCopy = new TableRow();
                TableCell captionCellCopy = new TableCell();
                TableCell fieldCellCopy = new TableCell();
                fieldCellCopy.Visible = false;

                LinkButton copyLinkButton = new LinkButton();
                copyLinkButton.Text = infojetContext.translate("COPY CONFIGURATION");
                copyLinkButton.Click += new EventHandler((object sender, EventArgs args) =>
                {
                    fieldCellCopy.Visible = true;
                });

                DropDownList dropDownList = new DropDownList();
                dropDownList.CssClass = "DropDown";

                ListItem FirstListItem = new ListItem("- Välj orderrad -", "");
                dropDownList.Items.Add(FirstListItem);

                WebCartLines webCartLines = new WebCartLines(infojetContext);
                System.Data.DataSet cartLineDataSet = webCartLines.getCartLines(infojetContext.sessionId, infojetContext.webSite.code);

                int k = 0;
                while (k < cartLineDataSet.Tables[0].Rows.Count)
                {
                    WebCartLine webCartLine = new WebCartLine(infojetContext, cartLineDataSet.Tables[0].Rows[k]);

                    if (webCartLine.itemNo == this._itemNo)
                    {
                        WebCartConfigLineCollection configCollection = webCartLine.getWebCartConfigLines();
                        string littera = "";
                        foreach (WebCartConfigLine webCartConfigLine in configCollection)
                        {
                            if (webCartConfigLine.optionCode == "LITTERA") littera = webCartConfigLine.value;
                        }
                        
                        ListItem listItem = new ListItem(webCartLine.itemNo+" "+littera, webCartLine.entryNo.ToString());
                        dropDownList.Items.Add(listItem);
                    }
                    k++;
                }

                LinkButton executeCopyLinkButton = new LinkButton();
                
                executeCopyLinkButton.Text = "&nbsp;&nbsp;"+infojetContext.translate("DO COPY");
                executeCopyLinkButton.Click += new EventHandler((object sender, EventArgs args) =>
                {
                    if (dropDownList.SelectedItem.Value != "")
                    {
                        WebCartLine webCartLine = new WebCartLine(infojetContext, int.Parse(dropDownList.SelectedItem.Value));

                        WebCartConfigLineCollection configCollection = webCartLine.getWebCartConfigLines();
                        foreach (WebCartConfigLine webCartConfigLine in configCollection)
                        {
                            WebItemConfigLine webItemConfigLine = this._webItemConfigLineCollection.getWebItemConfigLineFromOption(webCartConfigLine.optionCode);
                            if (webItemConfigLine != null)
                            {
                                //if (webItemConfigLine.optionCode == "STORLEK") throw new Exception("STORLEK: " + webCartConfigLine.value);
                                webItemConfigLine.setValue(webCartConfigLine.type, webCartConfigLine.value);
                                webItemConfigLine.updateLineAmount(this);

                            }
                        }
                        processLocalRules();
                        WebConfigGlobalRule.processGlobalRules(infojetContext, this);

                        updateConfigurationForm();

                        updateUnitPrice();

                        System.Web.HttpContext.Current.Session["itemConfiguration_" + _itemNo] = this;

                        infojetContext.redirect(infojetContext.webPage.getUrl() + "&category=" + System.Web.HttpContext.Current.Request["category"] + "&itemNo=" + _itemNo);
                    }
                });


                captionCellCopy.Controls.Add(copyLinkButton);
                fieldCellCopy.Controls.Add(dropDownList);
                fieldCellCopy.Controls.Add(executeCopyLinkButton);

                captionRowCopy.Cells.Add(captionCellCopy);
                captionRowCopy.Cells.Add(fieldCellCopy);

                table.Rows.Add(captionRowCopy);

            }
            //Kopiera - End

            int i = 0;
            while (i < this._webItemConfigLineCollection.Count)
            {
                WebItemConfigLine webItemConfigLine = _webItemConfigLineCollection[i];

                if (webItemConfigLine.maxLength == 0) webItemConfigLine.maxLength = 250;

                TableRow captionRow = new TableRow();
                TableRow fieldRow = new TableRow();
                TableCell captionCell = new TableCell();
                TableCell fieldCell = new TableCell();

                fieldRow = captionRow;
                captionRow.Cells.Add(captionCell);
                fieldRow.Cells.Add(fieldCell);

                if (webItemConfigLine.method == 0)
                {                    
                    Label label = new Label();
                    label.Text = webItemConfigLine.description;
                    label.ToolTip = webItemConfigLine.text;

                    DropDownList dropDownList = new DropDownList();
                    dropDownList.ID = webItemConfigLine.controlId;
                    dropDownList.CssClass = "DropDown";
                    if (webItemConfigLine.width > 0) dropDownList.Width = webItemConfigLine.width;
                    //dropDownList.Visible = webItemConfigLine.visible;

                    label.AssociatedControlID = dropDownList.ID;

                    dropDownList.SelectedIndexChanged += new EventHandler(dropDownList_SelectedIndexChanged);
                    dropDownList.AutoPostBack = true;

                    ListItem FirstListItem = new ListItem("- Välj -", "");
                    dropDownList.Items.Add(FirstListItem);

                    int j = 0;
                    while (j < webItemConfigLine.values.Count)
                    {
                        if (!webItemConfigLine.values[j].hide)
                        {
                            ListItem listItem = new ListItem(webItemConfigLine.values[j].description, webItemConfigLine.values[j].type + ";" + webItemConfigLine.values[j].value);
                            if ((webItemConfigLine.type == webItemConfigLine.values[j].type) && (webItemConfigLine.optionValue == webItemConfigLine.values[j].value)) listItem.Selected = true;


                            dropDownList.Items.Add(listItem);
                        }
                        j++;
                    }

                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(dropDownList);

                    webItemConfigLine.control = dropDownList;
                }

                if ((webItemConfigLine.method == 1) || (webItemConfigLine.method == 3))
                {
                    Label label = new Label();
                    label.Text = webItemConfigLine.description;
                    label.ToolTip = webItemConfigLine.text;

                    TextBox textBox = new TextBox();
                    textBox.ID = webItemConfigLine.controlId;
                    textBox.MaxLength = webItemConfigLine.maxLength;
                    if (webItemConfigLine.width > 0) textBox.Width = webItemConfigLine.width;
                    textBox.CssClass = "Textfield";
                    textBox.Text = webItemConfigLine.optionValue;
                    textBox.CausesValidation = true;

                    if (webItemConfigLine.method == 1)
                    {
                        textBox.AutoPostBack = true;
                        textBox.TextChanged += new EventHandler(textBox_TextChanged);
                    }

                    label.AssociatedControlID = textBox.ID;


                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(textBox);

                    webItemConfigLine.control = textBox;

                    if (webItemConfigLine.expression != "")
                    {
                        string regExp = "";
                        int k = 0;

                        while (k < webItemConfigLine.expression.Length)
                        {
                            if (webItemConfigLine.expression[k] == '#') regExp = regExp + "[0-9a-zA-Z]{1}";
                            if (webItemConfigLine.expression[k] == '$') regExp = regExp + "\\d{1}";
                            if ((webItemConfigLine.expression[k] != '#') && (webItemConfigLine.expression[k] != '$')) regExp = regExp + webItemConfigLine.expression[k];
                            k++;
                        }
                        regExp = regExp.Replace("*", ".*");
                        regExp = "^" + regExp + "$";

                        RegularExpressionValidator regularExpressionValidator = new RegularExpressionValidator();
                        regularExpressionValidator.ControlToValidate = textBox.ID;
                        regularExpressionValidator.ErrorMessage = "&nbsp; " + infojetContext.translate("EXPRESSION FAILED").Replace("%%", webItemConfigLine.expression);
                        regularExpressionValidator.ValidationExpression = regExp;
                        regularExpressionValidator.ForeColor = System.Drawing.Color.Red;

                        fieldCell.Controls.Add(regularExpressionValidator);


                    }


                }

                if (webItemConfigLine.method == 2)
                {
                    Label label = new Label();
                    label.Text = webItemConfigLine.description + "&nbsp;";
                    label.Font.Bold = true;
                    label.ToolTip = webItemConfigLine.text;

                    captionCell.Controls.Add(label);

                    label = new Label();
                    label.Text = "&nbsp;";

                    fieldCell.Controls.Add(label);

                }

                Label errorLabel = new Label();
                errorLabel.Text = webItemConfigLine.errorMessage;
                errorLabel.ForeColor = System.Drawing.Color.Red;

                fieldCell.Controls.Add(errorLabel);


                if (captionRow != null)
                {
                    table.Rows.Add(captionRow);
                    if ((captionRow != fieldRow) && (fieldRow != null)) table.Rows.Add(fieldRow);
                }

                fieldRow.Visible = webItemConfigLine.visible;

                i++;
            }

            colPanel.Controls.Add(table);
            rowPanel.Controls.Add(colPanel);
            _formPanel.Controls.Add(rowPanel);

            processLocalRules();
            WebConfigGlobalRule.processGlobalRules(infojetContext, this);
            updateConfigurationForm();

            updateUnitPrice();

            System.Web.HttpContext.Current.Session["itemConfiguration_" + _itemNo] = this;

            return _formPanel;
        }



        void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            WebItemConfigLine webItemConfigLine = this._webItemConfigLineCollection.getWebItemConfigLineFromControl(textBox.ID);
            if (webItemConfigLine != null)
            {
                //if (webItemConfigLine.optionValue != "") throw new Exception("Current value: " + webItemConfigLine.optionValue + ", new value: " + textBox.Text);

                webItemConfigLine.setValue(webItemConfigLine.type, textBox.Text);
                webItemConfigLine.updateLineAmount(this);
            }
            else
            {
                throw new Exception("Configuration mismatch. Textbox-ID: " + textBox.ID);
            }


            processLocalRules();
            WebConfigGlobalRule.processGlobalRules(infojetContext, this);
            updateConfigurationForm();

            updateUnitPrice();

            System.Web.HttpContext.Current.Session["itemConfiguration_" + _itemNo] = this;
            
        }


        void dropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDownList = (DropDownList)sender;
            //Panel panel = (Panel)dropDownList.Parent.Parent.Parent.Parent.Parent.Parent;

            ListItem selectedItem = dropDownList.Items[dropDownList.SelectedIndex];

            WebItemConfigLine webItemConfigLine = this._webItemConfigLineCollection.getWebItemConfigLineFromControl(dropDownList.ID);
            if (webItemConfigLine != null)
            {
                if (selectedItem.Value.IndexOf(';') > -1)
                {
                    webItemConfigLine.setValue(int.Parse(selectedItem.Value.Substring(0, 1)), selectedItem.Value.Substring(2));
                }
                else
                {
                    webItemConfigLine.setValue(0, "");
                }
                webItemConfigLine.updateLineAmount(this);
            }

            processLocalRules();
            WebConfigGlobalRule.processGlobalRules(infojetContext, this);
            updateConfigurationForm();

            updateUnitPrice();

            System.Web.HttpContext.Current.Session["itemConfiguration_" + _itemNo] = this;

            //infojetContext.redirect(infojetContext.webPage.getUrl()+"&itemNo="+_itemNo);

            
        }

        private void updateUnitPrice()
        {
            float unitPrice = 0;

            int i = 0;
            while (i < this._webItemConfigLineCollection.Count)
            {
                //if (_webItemConfigLineCollection[i].optionValue != "")
                if (_webItemConfigLineCollection[i].visible == true)
                {
                    unitPrice += _webItemConfigLineCollection[i].lineAmount;
                }
                i++;
            }

            this._unitPrice = (int)unitPrice;
           
        }

        public static WebItemConfigHeader getConfiguration(Infojet infojetContext, string itemNo)
        {
            WebItemSetting webItemSetting = new WebItemSetting(infojetContext, 0, itemNo);
            if (webItemSetting.webConfigModelNo == "") return null;

            if (System.Web.HttpContext.Current.Session["itemConfiguration_" + itemNo] != null)
            {
                WebItemConfigHeader webItemConfigHead = (WebItemConfigHeader)System.Web.HttpContext.Current.Session["itemConfiguration_" + itemNo];
                int cartEntryNo = 0;
                if ((System.Web.HttpContext.Current.Request["cartEntryNo"] != null) && (System.Web.HttpContext.Current.Request["cartEntryNo"] != ""))
                {
                    cartEntryNo = int.Parse(System.Web.HttpContext.Current.Request["cartEntryNo"]);
                }
                if (cartEntryNo == webItemConfigHead._cartEntryNo) return webItemConfigHead;
            }

            WebItemConfigHeader webItemConfigHeader = new WebItemConfigHeader(infojetContext, itemNo, webItemSetting.webConfigModelNo);
            webItemConfigHeader.required = webItemSetting.requireConfiguration;
                       
            System.Web.HttpContext.Current.Session.Add("itemConfiguration_" + itemNo, webItemConfigHeader);
            return webItemConfigHeader;
        }

        public bool validateConfiguration(float quantity)
        {
            _cartQuantity = quantity;
            _validated = true;
            _description = "";

            //Bör styras av inställning om konfigurering skall vara ett krav eller inte.
            if (_formPanel == null)
            {
                _validated = false;
                return _validated;
            }


            int i = 0;
            while (i < _webItemConfigLineCollection.Count)
            {
                WebItemConfigLine webItemConfigLine = _webItemConfigLineCollection[i];

                if (webItemConfigLine.method == 0)
                {
                    DropDownList dropDownList = (DropDownList)_formPanel.FindControl(webItemConfigLine.controlId);
                    if (dropDownList != null)
                    {
                        ListItem selectedItem = dropDownList.Items[dropDownList.SelectedIndex];

                        if (selectedItem.Value.IndexOf(';') > -1)
                        {
                            webItemConfigLine.type = int.Parse(selectedItem.Value.Substring(0, 1));
                            webItemConfigLine.optionValue = selectedItem.Value.Substring(2);
                        }
                        else
                        {
                            webItemConfigLine.type = 0;
                            webItemConfigLine.optionValue = "";
                        }
                    }
                }
                if ((webItemConfigLine.method == 1) || (webItemConfigLine.method == 3))
                {
                    TextBox textBox = (TextBox)_formPanel.FindControl(webItemConfigLine.controlId);
                    if (textBox != null)
                    {
                        webItemConfigLine.setValue(webItemConfigLine.type, textBox.Text);

                    }
                }

                if (webItemConfigLine.includeInDescription)
                {
                    if (_description != "") _description = _description + "/";
                    _description = _description + webItemConfigLine.optionValue;
                }

                webItemConfigLine.updateLineAmount(this);

                i++;
            }

            i = 0;
            while (i < _webItemConfigLineCollection.Count)
            {
                WebItemConfigLine webItemConfigLine = _webItemConfigLineCollection[i];
                webItemConfigLine.errorMessage = "";

                if ((webItemConfigLine.required) && (webItemConfigLine.visible) && (webItemConfigLine.optionValue == ""))
                {
                    webItemConfigLine.errorMessage = infojetContext.translate("REQUIRED VALUE");
                    _validated = false;
                }

                if ((webItemConfigLine.dataType == 1) && (webItemConfigLine.optionValue != ""))
                {
                    try
                    {
                        int testValue = int.Parse(webItemConfigLine.optionValue);
                    }
                    catch (Exception)
                    {
                        webItemConfigLine.errorMessage = infojetContext.translate("NUMERIC VALUE");
                        _validated = false;
                    }
                }
                if ((webItemConfigLine.dataType == 3) && (webItemConfigLine.optionValue != ""))
                {
                    try
                    {
                        float testValue = float.Parse(webItemConfigLine.optionValue);
                    }
                    catch (Exception)
                    {
                        webItemConfigLine.errorMessage = "Endast decimalt värde.";
                        _validated = false;
                    }
                }

                i++;
            }

            updateUnitPrice();

            System.Web.HttpContext.Current.Session["itemConfiguration_" + _itemNo] = this;

            return _validated;
        }

        public void applyConfiguration(float quantity, WebCartLine webCartLine)
        {
            this._quantity = quantity;
            string configDescription = getConfigDescription();

            int i = 0;
            while (i < _webItemConfigLineCollection.Count)
            {
                WebItemConfigLine webItemConfigLine = _webItemConfigLineCollection[i];

                

                if ((webItemConfigLine.assemblyMethod == 1) && (webItemConfigLine.type == 1))
                {

                    float qty = 1;
                    if (webItemConfigLine.separateItemQtyOption != "")
                    {

                        WebItemConfigLine qtyOption = _webItemConfigLineCollection.getWebItemConfigLineFromOption(webItemConfigLine.separateItemQtyOption);

                        try
                        {
                            qty = float.Parse(qtyOption.optionValue);
                        }
                        catch (Exception) { }

                    }

                    if (qty > 0) infojetContext.cartHandler.addItemToCart(webItemConfigLine.optionValue, (qty).ToString(), true, "", "", "", "", this._webConfigId, "");
                }

                if (webCartLine != null)
                {
                    WebCartConfigLine webCartConfigLine = new WebCartConfigLine(infojetContext, webItemConfigLine, webCartLine.entryNo);

                    webCartConfigLine.configDescription = configDescription;
                    webCartConfigLine.save();
                }

                i++;
            }



            System.Web.HttpContext.Current.Session.Remove("itemConfiguration_" + _itemNo);

        }

        public string description { get { return _description; } }
        public bool validated { get { return _validated; } }
        public string webConfigModelNo { get { return _webConfigModelNo; } }
        public string webConfigId { get { return _webConfigId; } }
        public string itemNo { get { return _itemNo; } }
        public float quantity { get { return _quantity; } }
        public float unitPrice { get { return _unitPrice; } }
        public float cartQuantity { get { return _cartQuantity; } }
        public bool required { get { return _required; } set { _required = value; } }
        public WebItemConfigLineCollection webItemConfigLineCollection { get { return _webItemConfigLineCollection; } }

        public string getConfigDescription()
        {
            string configDescription = "/";

            int i = 0;
            while (i < _webItemConfigLineCollection.Count)
            {
                WebItemConfigLine webItemConfigLine = _webItemConfigLineCollection[i];

                if (webItemConfigLine.includeInDescription) configDescription = configDescription + webItemConfigLine.optionValue + "/";
                i++;
            }

            return configDescription;

        }

        public WebItemConfigLine getWebItemConfigLine(string optionCode)
        {
            return _webItemConfigLineCollection.getWebItemConfigLineFromOption(optionCode);
        }

        private void updateConfigurationForm()
        {
            int i = 0;
            while (i < _webItemConfigLineCollection.Count)
            {
                if (_webItemConfigLineCollection[i].control != null)
                {
                    _webItemConfigLineCollection[i].control.Parent.Parent.Visible = _webItemConfigLineCollection[i].visible;
                    
                    if (_webItemConfigLineCollection[i].control.Parent.Parent.Visible == false)
                    {
                        if (_webItemConfigLineCollection[i].method == 0)
                        {
                            ((DropDownList)_webItemConfigLineCollection[i].control).SelectedIndex = 0;
                        }
                        if (_webItemConfigLineCollection[i].method == 1)
                        {
                            ((TextBox)_webItemConfigLineCollection[i].control).Text = "";
                        }
                    }
                }
                i++;
            }

        }

        private void processLocalRules()
        {
            int i = 0;
            while (i < _webItemConfigLineCollection.Count)
            {
                if (_webItemConfigLineCollection[i].webConfigRuleCode != "")
                {
                    if (WebConfigRule.evaluateRule(infojetContext, this, _webItemConfigLineCollection[i].webConfigRuleCode))
                    {
                        _webItemConfigLineCollection[i].visible = true;
                    }
                    else
                    {
                        _webItemConfigLineCollection[i].visible = false;
                        _webItemConfigLineCollection[i].optionValue = "";
                        
                    }
                }

                int j = 0;
                while (j < _webItemConfigLineCollection[i].values.Count)
                {
                    if (_webItemConfigLineCollection[i].values[j].webConfigRuleCode != "")
                    {
                        if (!WebConfigRule.evaluateRule(infojetContext, this, _webItemConfigLineCollection[i].values[j].webConfigRuleCode))
                        {

                            _webItemConfigLineCollection[i].values[j].hide = true;                           
                        }
                        else
                        {
                            _webItemConfigLineCollection[i].values[j].hide = false;
                        }
                    }

                    j++;
                }
                if (_webItemConfigLineCollection[i].method == 0) updateDropDownValues(_webItemConfigLineCollection[i]);

                i++;
            }           
        }

        public string getReference()
        {
            if (_referenceStyle == 0) return webConfigModelNo + ": " + description;
            if (_referenceStyle == 1) return description;
            if (_referenceStyle == 2) return "";
            return "";
        }

        private void updateDropDownValues(WebItemConfigLine webItemConfigLine)
        {
            if (webItemConfigLine.method == 0)
            {
                int i = 0;
                while (i < webItemConfigLine.values.Count)
                {
                    try
                    {
                        if (webItemConfigLine.values[i].hide == false)
                        {
                            ListItem listItem = ((DropDownList)webItemConfigLine.control).Items.FindByValue(webItemConfigLine.values[i].type + ";" + webItemConfigLine.values[i].value);
                            if (listItem == null)
                            {
                                listItem = new ListItem(webItemConfigLine.values[i].description, webItemConfigLine.values[i].type + ";" + webItemConfigLine.values[i].value);
                                if (i < ((DropDownList)webItemConfigLine.control).Items.Count)
                                {
                                    ((DropDownList)webItemConfigLine.control).Items.Insert(i + 1, listItem);
                                }
                                else
                                {
                                    ((DropDownList)webItemConfigLine.control).Items.Insert(((DropDownList)webItemConfigLine.control).Items.Count, listItem);
                                }
                            }
                        }
                        if (webItemConfigLine.values[i].hide == true)
                        {
                            ListItem listItem = ((DropDownList)webItemConfigLine.control).Items.FindByValue(webItemConfigLine.values[i].type + ";" + webItemConfigLine.values[i].value);
                            if (listItem != null) ((DropDownList)webItemConfigLine.control).Items.Remove(listItem);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    i++;
                }
            }
        }
    }
}
