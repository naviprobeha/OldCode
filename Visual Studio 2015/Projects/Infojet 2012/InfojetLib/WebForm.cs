using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class WebForm
	{
		private Infojet infojetContext;

        public string webSiteCode;
		public string code;
        public string description;
        public int sendFormTo;
        public string emailToAddress;
        public string emailSubject;
        public string webFormSubmitTextConstantCode;
        public string confirmWebCategoryCode;

        public WebFormField[] webFormFieldArray;
        private WebCheckout parentControl;

        private Hashtable formValueTable;

        public WebForm()
        {
        }

		public WebForm(Infojet infojetContext, string webSiteCode, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.infojetContext = infojetContext;
				 
			this.code = code;
            this.webSiteCode = webSiteCode;

            WebUserAccount webUserAccount = null;
            if (infojetContext.userSession != null) webUserAccount = infojetContext.userSession.webUserAccount;
            this.formValueTable = getConnectionValues(webUserAccount);

			getFromDatabase();


		}

		public WebForm(Infojet infojetContext, DataRow dataRow)
		{
			this.infojetContext = infojetContext;

			this.code = dataRow.ItemArray.GetValue(0).ToString();
			this.description = dataRow.ItemArray.GetValue(1).ToString();
			this.sendFormTo = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.emailToAddress = dataRow.ItemArray.GetValue(3).ToString();
			this.emailSubject = dataRow.ItemArray.GetValue(4).ToString();
            this.webFormSubmitTextConstantCode = dataRow.ItemArray.GetValue(5).ToString();
            this.confirmWebCategoryCode = dataRow.ItemArray.GetValue(6).ToString();
            this.webSiteCode = dataRow.ItemArray.GetValue(7).ToString();

            this.formValueTable = new Hashtable();

		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Code], [Description], [Send Form To], [E-mail To Address], [E-mail Subject], [Send Button Text Constant Code], [Confirm Web Category Code], [Web Site Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Form") + "] WHERE [Code] = @code AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("code", code, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				code = dataReader.GetValue(0).ToString();
				description = dataReader.GetValue(1).ToString();
				sendFormTo = int.Parse(dataReader.GetValue(2).ToString());
				emailToAddress = dataReader.GetValue(3).ToString();
				emailSubject = dataReader.GetValue(4).ToString();
                webFormSubmitTextConstantCode = dataReader.GetValue(5).ToString();
                confirmWebCategoryCode = dataReader.GetValue(6).ToString();
                webSiteCode = dataReader.GetValue(7).ToString();
			}

			dataReader.Close();


		}

        public DataSet getFields()
        {
            WebFormFields webFormFields = new WebFormFields(infojetContext.systemDatabase);
            return webFormFields.getWebFormFields(this.webSiteCode, this.code);
        }

        public void setFormValues(Hashtable formValueTable)
        {
            this.formValueTable = formValueTable;
        }

        public Hashtable getFormValues()
        {
            return this.formValueTable;
        }

        public Panel getFormPanel()
        {

            Panel formPanel = new Panel();
            formPanel.ID = "form_"+this.code;

            DataSet webFormFieldDataSet = this.getFields();

            Panel rowPanel = new Panel();
            rowPanel.Width = Unit.Percentage(100);
            rowPanel.Style.Add("float", "left");

            Panel colPanel = new Panel();
            colPanel.Style.Add("float", "left");           

            Table table = new Table();

            int i = 0;
            while (i < webFormFieldDataSet.Tables[0].Rows.Count)
            {
                WebFormField webFormField = new WebFormField(infojetContext, webFormFieldDataSet.Tables[0].Rows[i]);

                if (webFormField.fieldLength == 0) webFormField.fieldLength = 250;

                TableRow captionRow = new TableRow();
                TableRow fieldRow = new TableRow();
                TableCell captionCell = new TableCell();
                TableCell fieldCell = new TableCell();
                fieldCell.Wrap = false;

                if (webFormField.placement == 0)
                {
                    fieldRow = captionRow;
                    captionRow.Cells.Add(captionCell);
                    fieldRow.Cells.Add(fieldCell);
                }
                if (webFormField.placement == 1)
                {
                    captionCell.ColumnSpan = 2;
                    captionRow.Cells.Add(captionCell);
                    fieldCell.ColumnSpan = 2;
                    fieldRow.Cells.Add(fieldCell);
                }

                if (webFormField.placement == 2)
                {
                    captionCell.ColumnSpan = 2;
                    captionRow.Cells.Add(captionCell);
                    fieldCell.ColumnSpan = 2;
                    fieldRow.Cells.Add(fieldCell);
                }

                if (webFormField.fieldType == 0)
                {
                    Label label = new Label();
                    label.Text = webFormField.getCaption();

                    TextBox textBox = new TextBox();
                    textBox.ID = webFormField.controlId;
                    textBox.MaxLength = webFormField.fieldLength;
                    textBox.Width = webFormField.width;
                    textBox.CssClass = "Textfield";
                    textBox.MaxLength = webFormField.fieldLength;
                    textBox.Text = (string)formValueTable[webFormField.code];
                    
                    if (textBox.Text == "")
                    {
                        if ((System.Web.HttpContext.Current.Request["formField_" + webFormField.controlId] != null) && (System.Web.HttpContext.Current.Request["formField_" + webFormField.controlId] != ""))
                        {
                            textBox.Text = System.Web.HttpContext.Current.Request["formField_" + webFormField.controlId];
                        }
                    }

                    if (webFormField.readOnly) textBox.Enabled = false;

                    label.AssociatedControlID = textBox.ID;

                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(textBox);

                    if (!webFormField.required) webFormField.required = webFormField.evaluateCondition(0);

                    if (webFormField.required)
                    {
                        RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
                        requiredFieldValidator.ControlToValidate = textBox.ID;
                        requiredFieldValidator.ErrorMessage = "&nbsp;" + infojetContext.translate("MISSING FORM VALUE");
                        requiredFieldValidator.ForeColor = System.Drawing.Color.Red;
                        requiredFieldValidator.ValidationGroup = this.code;
                        requiredFieldValidator.Width = new Unit(75);

                        fieldCell.Controls.Add(requiredFieldValidator);

                    }

                    if (webFormField.expression != "")
                    {
                        string regExp = "";
                        int k = 0;

                        while (k < webFormField.expression.Length)
                        {
                            if (webFormField.expression[k] == '#') regExp = regExp + "[0-9a-zA-Z]{1}";
                            if (webFormField.expression[k] == '$') regExp = regExp + "\\d{1}";
                            if ((webFormField.expression[k] != '#') && (webFormField.expression[k] != '$')) regExp = regExp + webFormField.expression[k];
                            k++;
                        }
                        regExp = regExp.Replace("*", ".*");
                        regExp = "^" + regExp + "$";

                        RegularExpressionValidator regularExpressionValidator = new RegularExpressionValidator();
                        regularExpressionValidator.ControlToValidate = textBox.ID;
                        regularExpressionValidator.ErrorMessage = "&nbsp; " + infojetContext.translate("EXPRESSION FAILED").Replace("%%", webFormField.expression);
                        regularExpressionValidator.ValidationExpression = regExp;                        
                        regularExpressionValidator.ValidationGroup = this.code;
                        regularExpressionValidator.ForeColor = System.Drawing.Color.Red;

                        fieldCell.Controls.Add(regularExpressionValidator);


                    }
                }

                if (webFormField.fieldType == 1)
                {

                    Label label = new Label();
                    label.Text = webFormField.getCaption();

                    DropDownList dropDownList = new DropDownList();
                    dropDownList.ID = webFormField.controlId;
                    dropDownList.CssClass = "DropDown";
                    dropDownList.Width = webFormField.width;
                    if (webFormField.readOnly) dropDownList.Enabled = false;

                    if (webFormField.valueTableNo > 0)
                    {
                        dropDownList.SelectedIndexChanged += new EventHandler(dropDownList_ValueTableSelectedIndexChanged);
                        dropDownList.AutoPostBack = true;
                    }

                    label.AssociatedControlID = dropDownList.ID;

                    fillDropDownWithValues(webFormField, formPanel, dropDownList);

                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(dropDownList);

                    if (webFormField.required)
                    {
                        RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
                        requiredFieldValidator.ControlToValidate = dropDownList.ID;
                        requiredFieldValidator.ErrorMessage = "&nbsp;" + infojetContext.translate("MISSING FORM VALUE");
                        requiredFieldValidator.ForeColor = System.Drawing.Color.Red;
                        requiredFieldValidator.ValidationGroup = this.code;
                        requiredFieldValidator.InitialValue = "";

                        fieldCell.Controls.Add(requiredFieldValidator);

                    }

                }

                if (webFormField.fieldType == 2)
                {
                    if (webFormField.placement == 0)
                    {
                        fieldCell.VerticalAlign = VerticalAlign.Top;
                    }

                    Label label = new Label();
                    label.Text = webFormField.getCaption();

                    CheckBox checkBox = new CheckBox();
                    checkBox.ID = webFormField.controlId;

                    if ((string)formValueTable[webFormField.code] == "1") checkBox.Checked = true;
                    if (webFormField.readOnly) checkBox.Enabled = false;

                    label.AssociatedControlID = checkBox.ID;

                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(checkBox);
                    fieldCell.VerticalAlign = VerticalAlign.Middle;

                }

                if (webFormField.fieldType == 3)
                {
                    captionCell.VerticalAlign = VerticalAlign.Top;

                    Label label = new Label();
                    label.Text = webFormField.getCaption();

                    FieldValueCollection fieldValueCollection = webFormField.getValues(formPanel, this);

                    RadioButtonList radioButtonList = new RadioButtonList();
                    radioButtonList.ID = webFormField.controlId;
                    radioButtonList.RepeatLayout = RepeatLayout.Table;
                    if (webFormField.placement == 2) radioButtonList.RepeatDirection = RepeatDirection.Horizontal;
                    radioButtonList.CssClass = "radioButtonsWrap";
                    if (webFormField.readOnly) radioButtonList.Enabled = false;

                    label.AssociatedControlID = radioButtonList.ID;

                    int j = 0;
                    while (j < fieldValueCollection.Count)
                    {
                        FieldValue fieldValue = fieldValueCollection[j];

                        ListItem listItem = new ListItem(fieldValue.description, fieldValue.code);

                        radioButtonList.Items.Add(listItem);
                        
                        if (formValueTable[webFormField.code] == null)
                        {
                            if (fieldValue.defaultValue) radioButtonList.SelectedIndex = radioButtonList.Items.IndexOf(listItem);
                        }
                        else
                        {
                            if (fieldValue.code == (string)formValueTable[webFormField.code]) radioButtonList.SelectedIndex = radioButtonList.Items.IndexOf(listItem);
                        }


                        j++;
                    }

                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(radioButtonList);

                    if (webFormField.required)
                    {
                        RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
                        requiredFieldValidator.ControlToValidate = radioButtonList.ID;
                        requiredFieldValidator.ErrorMessage = "&nbsp;" + infojetContext.translate("MISSING FORM VALUE");
                        requiredFieldValidator.ForeColor = System.Drawing.Color.Red;
                        requiredFieldValidator.ValidationGroup = this.code;

                        fieldCell.Controls.Add(requiredFieldValidator);

                    }

                }

                if (webFormField.fieldType == 4)
                {
                    captionCell.VerticalAlign = VerticalAlign.Top;

                    Label label = new Label();
                    label.Text = webFormField.getCaption();

                    TextBox textBox = new TextBox();
                    textBox.TextMode = TextBoxMode.MultiLine;
                    textBox.ID = webFormField.controlId;
                    textBox.MaxLength = webFormField.fieldLength;
                    textBox.Width = webFormField.width;
                    textBox.CssClass = "Textfield";
                    textBox.Height = 100;
                    textBox.MaxLength = webFormField.fieldLength;
                    textBox.Text = (string)formValueTable[webFormField.code];
                    if (webFormField.readOnly) textBox.Enabled = false;

                    label.AssociatedControlID = textBox.ID;

                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(textBox);

                    if (webFormField.required)
                    {
                        RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
                        requiredFieldValidator.ControlToValidate = textBox.ID;
                        requiredFieldValidator.ErrorMessage = "&nbsp;" + infojetContext.translate("MISSING FORM VALUE");
                        requiredFieldValidator.ForeColor = System.Drawing.Color.Red;
                        requiredFieldValidator.ValidationGroup = this.code;

                        fieldCell.Controls.Add(requiredFieldValidator);

                    }

                    if (webFormField.fieldLength > 0)
                    {
                        RegularExpressionValidator regularExpressionValidator = new RegularExpressionValidator();
                        regularExpressionValidator.ControlToValidate = textBox.ID;
                        regularExpressionValidator.ErrorMessage = "&nbsp; " + infojetContext.translate("EXCEEDING MAX LENGTH").Replace("%%", webFormField.fieldLength.ToString());
                        regularExpressionValidator.ValidationExpression = "^[\\s\\S]{0," + webFormField.fieldLength + "}$";
                        regularExpressionValidator.ValidationGroup = this.code;
                        regularExpressionValidator.ForeColor = System.Drawing.Color.Red;

                        fieldCell.Controls.Add(regularExpressionValidator);
                    }

                }

                if (webFormField.fieldType == 5)
                {
                    Label label = new Label();
                    label.Text = webFormField.getCaption();

                    FileUpload fileUpload = new FileUpload();
                    fileUpload.ID = webFormField.controlId;
                    fileUpload.Width = webFormField.width;
                    fileUpload.CssClass = "Textfield";
                    if (webFormField.readOnly) fileUpload.Enabled = false;


                    label.AssociatedControlID = fileUpload.ID;

                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(fileUpload);

                    if (webFormField.required)
                    {
                        RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
                        requiredFieldValidator.ControlToValidate = fileUpload.ID;
                        requiredFieldValidator.ErrorMessage = "&nbsp;" + infojetContext.translate("MISSING FORM VALUE");
                        requiredFieldValidator.ForeColor = System.Drawing.Color.Red;
                        requiredFieldValidator.ValidationGroup = this.code;

                        fieldCell.Controls.Add(requiredFieldValidator);

                    }

                }

                if (webFormField.fieldType == 6)
                {
                    Label label = new Label();
                    label.Text = webFormField.getCaption()+"&nbsp;";
                    label.Font.Bold = true;

                    captionCell.Controls.Add(label);
                    captionCell.ColumnSpan = 2;

                    fieldCell = null;

                }

                if (webFormField.fieldType == 7)
                {
                    Label label = new Label();
                    label.Text = webFormField.getCaption();

                    CheckBox checkBox = new CheckBox();
                    checkBox.ID = webFormField.controlId + "_checkbox";
                    if (webFormField.readOnly) checkBox.Enabled = false;


                    Label spaceLabel = new Label();
                    spaceLabel.Text = "&nbsp;";

                    TextBox textBox = new TextBox();
                    textBox.ID = webFormField.controlId + "_text";
                    textBox.MaxLength = webFormField.fieldLength;
                    textBox.Width = webFormField.width;
                    textBox.CssClass = "Textfield";
                    textBox.MaxLength = webFormField.fieldLength;
                    textBox.Text = (string)formValueTable[webFormField.code];
                    if (webFormField.readOnly) textBox.Enabled = false;

                    label.AssociatedControlID = textBox.ID;

                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(checkBox);
                    fieldCell.Controls.Add(spaceLabel);
                    fieldCell.Controls.Add(textBox);

                    if (webFormField.required)
                    {
                        RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
                        requiredFieldValidator.ControlToValidate = textBox.ID;
                        requiredFieldValidator.ErrorMessage = "&nbsp;" + infojetContext.translate("MISSING FORM VALUE");
                        requiredFieldValidator.ForeColor = System.Drawing.Color.Red;
                        requiredFieldValidator.ValidationGroup = this.code;

                        fieldCell.Controls.Add(requiredFieldValidator);

                    }


                }

                if (webFormField.fieldType == 8)
                {
                    HiddenField hiddenField = new HiddenField();
                    hiddenField.ID = webFormField.controlId;
                    hiddenField.Value = (string)formValueTable[webFormField.code];

                    if (hiddenField.Value == "")
                    {
                        if (webFormField.code == "URL")
                        {
                            hiddenField.Value = System.Web.HttpContext.Current.Request.Url.ToString();
                        }
                        if ((System.Web.HttpContext.Current.Request["formField_" + webFormField.controlId] != null) && (System.Web.HttpContext.Current.Request["formField_" + webFormField.controlId] != ""))
                        {
                            hiddenField.Value = System.Web.HttpContext.Current.Request["formField_" + webFormField.controlId];
                        }

                    }


                    fieldCell.Controls.Add(hiddenField);

                }

                if (webFormField.fieldType == 9)
                {
                    captionCell.VerticalAlign = VerticalAlign.Top;

                    Label label = new Label();
                    label.Text = webFormField.getCaption();

                    FieldValueCollection fieldValueCollection = webFormField.getValues(formPanel, this);

                    CheckBoxList checkBoxList = new CheckBoxList();
                    checkBoxList.ID = webFormField.controlId;
                    checkBoxList.RepeatLayout = RepeatLayout.Table;
                    if (webFormField.placement == 2) checkBoxList.RepeatDirection = RepeatDirection.Horizontal;

                    int j = 0;
                    while (j < fieldValueCollection.Count)
                    {
                        FieldValue fieldValue = fieldValueCollection[j];

                        ListItem listItem = new ListItem(fieldValue.description, fieldValue.code);

                        if (fieldValue.defaultValue) listItem.Selected = true;
                        checkBoxList.Items.Add(listItem);
                       

                        j++;
                    }

                    label.AssociatedControlID = checkBoxList.ID;
                    if (webFormField.readOnly) checkBoxList.Enabled = false;


                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(checkBoxList);


                }

                if (webFormField.fieldType == 10)
                {
                    Label label = new Label();
                    label.Text = webFormField.getCaption();

                    TextBox textBox = new TextBox();
                    textBox.TextMode = TextBoxMode.Password;
                    textBox.ID = webFormField.controlId;
                    textBox.MaxLength = webFormField.fieldLength;
                    textBox.Width = webFormField.width;
                    textBox.CssClass = "Textfield";
                    textBox.MaxLength = webFormField.fieldLength;
                    textBox.Text = (string)formValueTable[webFormField.code];
                    if (webFormField.readOnly) textBox.Enabled = false;

                    label.AssociatedControlID = textBox.ID;

                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(textBox);

                    if (webFormField.required)
                    {
                        RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
                        requiredFieldValidator.ControlToValidate = textBox.ID;
                        requiredFieldValidator.ErrorMessage = "&nbsp;" + infojetContext.translate("MISSING FORM VALUE");
                        requiredFieldValidator.ForeColor = System.Drawing.Color.Red;
                        requiredFieldValidator.ValidationGroup = this.code;

                        fieldCell.Controls.Add(requiredFieldValidator);

                    }

                }

                if (webFormField.fieldType == 11)
                {
                    Label label = new Label();
                    label.Text = "&nbsp;";

                    if (webFormField.fieldSize > 0) label.Width = Unit.Pixel(webFormField.fieldSize);

                    captionCell.Controls.Add(label);

                    label = new Label();
                    label.Text = webFormField.getCaption() + "&nbsp;";

                    fieldCell.Controls.Add(label);

                }

                if (webFormField.fieldType == 12)
                {

                    Label label = new Label();
                    label.Text = webFormField.getCaption();
                    label.Font.Bold = false;

                    string dateValue = "";

                    if (formValueTable[webFormField.code] != null)
                    {
                        dateValue = (string)formValueTable[webFormField.code];
                    }
                    if ((dateValue == null) || (dateValue == "")) dateValue = DateTime.Today.ToString("yyyy-MM-dd");

                    DropDownList dateListYear = new DropDownList();
                    dateListYear.ID = webFormField.controlId + "_YEAR";
                    dateListYear.CssClass = "DropDown";
                    dateListYear.Width = 60;
                    if (webFormField.readOnly) dateListYear.Enabled = false;


                    int year = DateTime.Today.Year;
                    while (year <= DateTime.Today.Year + 1)
                    {
                        ListItem listItem = new ListItem(year.ToString(), year.ToString());
                        dateListYear.Items.Add(listItem);

                        if (dateValue.Substring(0, 4) == year.ToString()) dateListYear.SelectedIndex = dateListYear.Items.IndexOf(listItem);

                        year++;
                    }


                    DropDownList dateListMonth = new DropDownList();
                    dateListMonth.ID = webFormField.controlId + "_MONTH";
                    dateListMonth.CssClass = "DropDown";
                    dateListMonth.Width = 40;
                    if (webFormField.readOnly) dateListMonth.Enabled = false;

                    int month = 0;
                    while (month < 12)
                    {
                        month++;
                        ListItem listItem = new ListItem(month.ToString().PadLeft(2, '0'), month.ToString().PadLeft(2, '0'));
                        dateListMonth.Items.Add(listItem);

                        if (dateValue.Substring(5, 2) == month.ToString().PadLeft(2, '0')) dateListMonth.SelectedIndex = dateListMonth.Items.IndexOf(listItem);

                    }

                    DropDownList dateListDay = new DropDownList();
                    dateListDay.ID = webFormField.controlId + "_DAY";
                    dateListDay.CssClass = "DropDown";
                    dateListDay.Width = 40;
                    if (webFormField.readOnly) dateListDay.Enabled = false;

                    int day = 0;
                    while (day < 31)
                    {
                        day++;
                        ListItem listItem = new ListItem(day.ToString().PadLeft(2, '0'), day.ToString().PadLeft(2, '0'));
                        dateListDay.Items.Add(listItem);

                        if (dateValue.Substring(8, 2) == day.ToString().PadLeft(2, '0')) dateListDay.SelectedIndex = dateListDay.Items.IndexOf(listItem);
                    }

                    Label separatorLabel = new Label();
                    separatorLabel.Text = "-";

                    Label separatorLabel2 = new Label();
                    separatorLabel2.Text = "-";

                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(dateListYear);
                    fieldCell.Controls.Add(separatorLabel);
                    fieldCell.Controls.Add(dateListMonth);
                    fieldCell.Controls.Add(separatorLabel2);
                    fieldCell.Controls.Add(dateListDay);

                }

                if (webFormField.fieldType == 13)
                {

                    Label label = new Label();
                    label.Text = webFormField.getCaption();

                    string timeValue = DateTime.Now.ToString("HH:mm");
                    if (formValueTable[webFormField.code] != null)
                    {
                        timeValue = (string)formValueTable[webFormField.code];
                    }

                    DropDownList timeListHour = new DropDownList();
                    timeListHour.ID = webFormField.controlId + "_HOUR";
                    timeListHour.CssClass = "DropDown";
                    timeListHour.Width = 40;
                    if (webFormField.readOnly) timeListHour.Enabled = false;

                    int hour = 0;
                    while (hour < 24)
                    {
                        ListItem listItem = new ListItem(hour.ToString().PadLeft(2, '0'), hour.ToString().PadLeft(2, '0'));
                        timeListHour.Items.Add(listItem);

                        if (timeValue.Substring(0, 2) == hour.ToString().PadLeft(2, '0')) timeListHour.SelectedIndex = timeListHour.Items.IndexOf(listItem);
                        hour++;

                    }

                    DropDownList timeListMinute = new DropDownList();
                    timeListMinute.ID = webFormField.controlId + "_MINUTE";
                    timeListMinute.CssClass = "DropDown";
                    timeListMinute.Width = 40;
                    if (webFormField.readOnly) timeListMinute.Enabled = false;

                    int minute = 0;
                    while (minute < 60)
                    {
                        ListItem listItem = new ListItem(minute.ToString().PadLeft(2, '0'), minute.ToString().PadLeft(2, '0'));
                        timeListMinute.Items.Add(listItem);

                        if (timeValue.Substring(3, 2) == minute.ToString().PadLeft(2, '0')) timeListMinute.SelectedIndex = timeListMinute.Items.IndexOf(listItem);
                        minute = minute + 10;

                    }

                    Label separatorLabel = new Label();
                    separatorLabel.Text = ":";

                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(timeListHour);
                    fieldCell.Controls.Add(separatorLabel);
                    fieldCell.Controls.Add(timeListMinute);

                }
                if (webFormField.fieldType == 14)
                {
                    //Column break
                    colPanel.Controls.Add(table);
                    rowPanel.Controls.Add(colPanel);
                    colPanel = new Panel();
                    colPanel.Style.Add("float", "left");
                    colPanel.Style.Add("margin-left", "10px");

                    table = new Table();

                    captionRow = null;
                }

                if (webFormField.fieldType == 15)
                {
                    //Row break
                    colPanel.Controls.Add(table);
                    rowPanel.Controls.Add(colPanel);
                    formPanel.Controls.Add(rowPanel);

                    rowPanel = new Panel();
                    rowPanel.Width = Unit.Percentage(100);
                    rowPanel.Style.Add("float", "left");
                    rowPanel.Style.Add("margin-bottom", "10px");
                    
                    colPanel = new Panel();
                    colPanel.Style.Add("float", "left");

                    table = new Table();

                    captionRow = null;

                }

                if (webFormField.fieldType == 16)
                {
                    //Image
                    WebImage webImage = new WebImage(infojetContext, webFormField.fieldName);
                    if (webFormField.fieldSize == 0) webFormField.fieldSize = 250;

                    Image image = new Image();
                    image.ImageUrl = webImage.getUrl(webFormField.fieldSize, webFormField.fieldSize);

                    captionCell.Controls.Add(image);
                    captionCell.ColumnSpan = 2;

                    fieldCell = null;

                }

                if (webFormField.fieldType == 17)
                {
                    //Captcha
                    webFormField.required = true;

                    captionCell.VerticalAlign = VerticalAlign.Top;

                    Label label = new Label();
                    label.Text = webFormField.getCaption();


                    Captcha captcha = new Captcha();
                    string captchaText = captcha.generateCode();

                    Image image = new Image();
                    image.ImageUrl = infojetContext.webPage.getUrl() + "&systemCommand=getCaptchaImage";

                    TextBox textBox = new TextBox();
                    textBox.ID = webFormField.controlId;
                    textBox.Width = webFormField.width;
                    textBox.CssClass = "Textfield";

                    label.AssociatedControlID = textBox.ID;

                    HiddenField hiddenField = new HiddenField();
                    hiddenField.ID = webFormField.controlId + "_VALUE";
                    hiddenField.Value = captchaText;

                    captionCell.Controls.Add(label);
                    fieldCell.Controls.Add(image);

                    System.Web.UI.HtmlControls.HtmlGenericControl lineBreak = new System.Web.UI.HtmlControls.HtmlGenericControl("br");

                    fieldCell.Controls.Add(lineBreak);
                    fieldCell.Controls.Add(textBox);
                    fieldCell.Controls.Add(hiddenField);

                    

                    RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
                    requiredFieldValidator.ControlToValidate = textBox.ID;
                    requiredFieldValidator.ErrorMessage = "&nbsp;" + infojetContext.translate("MISSING FORM VALUE");
                    requiredFieldValidator.ForeColor = System.Drawing.Color.Red;
                    requiredFieldValidator.ValidationGroup = this.code;
                    requiredFieldValidator.Width = new Unit(75);

                    fieldCell.Controls.Add(requiredFieldValidator);

                    RegularExpressionValidator regularExpressionValidator = new RegularExpressionValidator();
                    regularExpressionValidator.ControlToValidate = textBox.ID;
                    regularExpressionValidator.ErrorMessage = "&nbsp;" + infojetContext.translate("WRONG CAPTCHA VALUE");
                    regularExpressionValidator.ValidationExpression = "^"+captcha.generateCode()+"$";
                    regularExpressionValidator.ValidationGroup = this.code;
                    regularExpressionValidator.ForeColor = System.Drawing.Color.Red;
                    requiredFieldValidator.Width = new Unit(75);

                    fieldCell.Controls.Add(regularExpressionValidator);


                }

                if (captionRow != null)
                {
                    table.Rows.Add(captionRow);
                    if ((captionRow != fieldRow) && (fieldRow != null)) table.Rows.Add(fieldRow);
                }
                
                i++;
            }

            colPanel.Controls.Add(table);
            rowPanel.Controls.Add(colPanel);
            formPanel.Controls.Add(rowPanel);


            return formPanel;
        }

        public Panel getFormPanelWithButton()
        {
            Panel formPanel = getFormPanel();
            formPanel.DefaultButton = "btn_submit";

            Button submitButton = new Button();
            submitButton.Text = infojetContext.translate(webFormSubmitTextConstantCode);
            submitButton.ValidationGroup = this.code;
            submitButton.CssClass = "Button";
            submitButton.Click += new EventHandler(submitButton_Click);
            submitButton.ID = "btn_submit";


            Panel buttonPanel = new Panel();
            buttonPanel.CssClass = "submitBtn";

            buttonPanel.Controls.Add(submitButton);
            formPanel.Controls.Add(buttonPanel);

            return formPanel;
        }

        void dropDownList_ValueTableSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDownList = (DropDownList)sender;
            Panel panel = (Panel)dropDownList.Parent.Parent.Parent.Parent.Parent.Parent;

            //updateFormValueTable(panel);
            ListItem selectedItem = dropDownList.Items[dropDownList.SelectedIndex];

            WebFormField webFormField = new WebFormField(infojetContext, webSiteCode, code, dropDownList.ID);
            webFormField.doAssignments(panel, selectedItem.Value, formValueTable, this);

            formValueTable[webFormField.code] = selectedItem.Value;

            WebFormFieldFilters webFormFieldFilters = new WebFormFieldFilters(infojetContext.systemDatabase);
            DataSet filterConnectionDataSet = webFormFieldFilters.getConnectedFieldFilters(webSiteCode, code, webFormField.code);
            int i = 0;
            while (i < filterConnectionDataSet.Tables[0].Rows.Count)
            {
                WebFormFieldFilter webFormFieldFilter = new WebFormFieldFilter(infojetContext, filterConnectionDataSet.Tables[0].Rows[i]);

                WebFormField webFormFieldConnection = new WebFormField(infojetContext, webSiteCode, code, webFormFieldFilter.webFormFieldCode);  
                DropDownList connectedDropDownList = (DropDownList)panel.FindControl(webFormFieldConnection.controlId);

                fillDropDownWithValues(webFormFieldConnection, panel, connectedDropDownList);

                i++;
            }
            //infojetContext.redirect(infojetContext.webPage.getUrl());

            if (parentControl != null) parentControl.processForm(panel);
        }

        public void setParentControl(WebCheckout webCheckout)
        {
            parentControl = webCheckout;
        }

        void submitButton_Click(object sender, EventArgs e)
        {
            WebFormDocument webFormDocument = readForm((Panel)((Button)sender).Parent);

            submitForm(webFormDocument);

            if ((this.confirmWebCategoryCode != null) && (this.confirmWebCategoryCode != ""))
            {
                WebPage webPage = infojetContext.webSite.getWebPageByCategory(this.confirmWebCategoryCode, infojetContext.userSession);
                if (webPage != null)
                {
                    infojetContext.redirect(webPage.getUrl());
                }
            }
        }

        public void submitForm(WebFormDocument webFormDocument)
        {
            if (this.sendFormTo == 0) sendFormToEmail(infojetContext, webFormDocument);
            if (this.sendFormTo == 1) sendFormToNavision(infojetContext, webFormDocument);
        }

		public WebFormDocument readForm(Panel formPanel)
		{
			WebFormFields webFormFields = new WebFormFields(infojetContext.systemDatabase);
			DataSet webFormFieldDataSet = webFormFields.getWebFormFields(this.webSiteCode, this.code);

			ArrayList keyCodeList = new ArrayList();
			ArrayList keyList = new ArrayList();
			ArrayList valueList = new ArrayList();
			ArrayList valueCodeList = new ArrayList();
			ArrayList fileList = new ArrayList();

			int i = 0;
			while (i < webFormFieldDataSet.Tables[0].Rows.Count)
			{
				WebFormField webFormField = new WebFormField(infojetContext, webFormFieldDataSet.Tables[0].Rows[i]);


				keyList.Add(webFormField.fieldName);
				keyCodeList.Add(webFormField.code);
			
				if ((webFormField.fieldType == 0) || (webFormField.fieldType == 4))
				{
                    TextBox textBox = (TextBox)formPanel.FindControl(webFormField.controlId);
                    string valueCode = textBox.Text;

                    valueList.Add(valueCode);
					valueCodeList.Add(valueCode);

				}

				if ((webFormField.fieldType == 1) || (webFormField.fieldType == 3))
				{
                    string valueCode = "";

                    if (webFormField.fieldType == 1)
                    {
                        DropDownList dropDownList = (DropDownList)formPanel.FindControl(webFormField.controlId);
                        if (dropDownList.SelectedIndex > -1)
                        {
                            valueCode = dropDownList.Items[dropDownList.SelectedIndex].Value;
                        }
                        else
                        {
                            valueCode = "";
                        }
                    }

                    if (webFormField.fieldType == 3)
                    {
                        RadioButtonList radioButtonList = (RadioButtonList)formPanel.FindControl(webFormField.controlId);
                        if (radioButtonList.SelectedIndex > -1)
                        {
                            valueCode = radioButtonList.Items[radioButtonList.SelectedIndex].Value;
                        }
                        else
                        {
                            valueCode = "";
                        }
                    }

                    valueList.Add(webFormField.getValueDescription(valueCode));
                    valueCodeList.Add(valueCode);

				}

				if (webFormField.fieldType == 2)
				{
                    CheckBox checkBox = (CheckBox)formPanel.FindControl(webFormField.controlId);

                    if (checkBox.Checked)
					{
						valueList.Add("Ja");
						valueCodeList.Add("Ja");
					}
					else
					{
						valueList.Add("Nej");
						valueCodeList.Add("Nej");
					}

				}

                if (webFormField.fieldType == 5)
                {
                    if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("attachments")))
                    {
                        System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("attachments"));
                    }

                    FileUpload fileUpload = (FileUpload)formPanel.FindControl(webFormField.controlId);

                    string fileName = "";

                    if (fileUpload.FileName != "")
                    {

                        fileName = "Att" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileUpload.FileName.Substring(fileUpload.FileName.Length - 4);

                        fileUpload.SaveAs(System.Web.HttpContext.Current.Server.MapPath("attachments") + "\\" + fileName);

                        fileList.Add(fileName);
                    }
                    valueList.Add(fileName);
                    valueCodeList.Add(fileName);
                }

				if (webFormField.fieldType == 6)
				{
					valueList.Add(" ");					
					valueCodeList.Add(" ");
				}

				if (webFormField.fieldType == 7)
				{
                    CheckBox checkBox = (CheckBox)formPanel.FindControl(webFormField.controlId+"_checkbox");
                    TextBox textBox = (TextBox)formPanel.FindControl(webFormField.controlId + "_text");

					string textValue = textBox.Text;

                    if (checkBox.Checked)
					{
						if (textValue != "")
						{
							valueList.Add("Ja, "+textValue);
							valueCodeList.Add("Ja, "+textValue);
						}
						else
						{
							valueList.Add("Ja");
							valueCodeList.Add("Ja");
						}
					}
					else
					{
						if (textValue != "")
						{
							valueList.Add("Nej, "+textValue);
							valueCodeList.Add("Nej, "+textValue);
						}
						else
						{
							valueList.Add("Nej");
							valueCodeList.Add("Nej");
						}
					}
				}

				if (webFormField.fieldType == 8)
				{
                    HiddenField hiddenField = (HiddenField)formPanel.FindControl(webFormField.controlId);
                    string valueCode = hiddenField.Value;

                    valueList.Add(valueCode);
					valueCodeList.Add(valueCode);
				}

				if (webFormField.fieldType == 9)
				{
                    CheckBoxList checkBoxList = (CheckBoxList)formPanel.FindControl(webFormField.controlId);

					string values = "";
					string valueTexts = "";

					int j = 0;
					while (j < checkBoxList.Items.Count)
					{
                        ListItem listItem = checkBoxList.Items[j];


						if (listItem.Selected)
						{
							if (values != "") values = values + ";";
							if (valueTexts != "") valueTexts = valueTexts + ";";
							values = values + listItem.Value;
                            valueTexts = valueTexts + listItem.Text;
						}

						j++;
					}

					valueList.Add(valueTexts);
					valueCodeList.Add(values);
				}

                if (webFormField.fieldType == 10)
                {
                    TextBox textBox = (TextBox)formPanel.FindControl(webFormField.controlId);
                    string valueCode = textBox.Text;

                    valueList.Add(valueCode);
                    valueCodeList.Add(valueCode);

                }

                if (webFormField.fieldType == 11)
                {
                    valueList.Add(" ");
                    valueCodeList.Add(" ");
                }

                if (webFormField.fieldType == 12)
                {
                    DropDownList dateListYear = (DropDownList)formPanel.FindControl(webFormField.controlId+"_YEAR");
                    DropDownList dateListMonth = (DropDownList)formPanel.FindControl(webFormField.controlId + "_MONTH");
                    DropDownList dateListDay = (DropDownList)formPanel.FindControl(webFormField.controlId + "_DAY");
                    string valueStr = dateListYear.Items[dateListYear.SelectedIndex].Value + "-" + dateListMonth.Items[dateListMonth.SelectedIndex].Value + "-" + dateListDay.Items[dateListDay.SelectedIndex].Value;

                    valueList.Add(valueStr);
                    valueCodeList.Add(valueStr);
                }

                if (webFormField.fieldType == 13)
                {
                    DropDownList timeListHour = (DropDownList)formPanel.FindControl(webFormField.controlId + "_HOUR");
                    DropDownList timeListMinute = (DropDownList)formPanel.FindControl(webFormField.controlId + "_MINUTE");
                    string valueStr = timeListHour.Items[timeListHour.SelectedIndex].Value + ":" + timeListMinute.Items[timeListMinute.SelectedIndex].Value;

                    valueList.Add(valueStr);
                    valueCodeList.Add(valueStr);
                }

                if (webFormField.fieldType == 14)
                {
                    valueList.Add(" ");
                    valueCodeList.Add(" ");
                }

                if (webFormField.fieldType == 15)
                {
                    valueList.Add(" ");
                    valueCodeList.Add(" ");
                }

                if (webFormField.fieldType == 16)
                {
                    valueList.Add(" ");
                    valueCodeList.Add(" ");
                }

                if (webFormField.fieldType == 17)
                {
                    valueList.Add(" ");
                    valueCodeList.Add(" ");

                    string captchaEntered = ((TextBox)formPanel.FindControl(webFormField.controlId)).Text;
                    string captchaValue = ((HiddenField)formPanel.FindControl(webFormField.controlId+"_VALUE")).Value;
                    if (captchaValue != captchaEntered) throw new Exception("Invalid captcha input.");

                }

				i++;
			}

            WebFormDocument webFormDocument = new WebFormDocument(this, keyCodeList, valueCodeList, fileList);
            return webFormDocument;
            
		}

		private void sendFormToEmail(Infojet infojetContext, WebFormDocument webFormDocument)
		{
			Configuration configuration = infojetContext.configuration;
            string senderAddress = configuration.smtpSender;
            if (infojetContext.webSite.emailSenderAddress != "") senderAddress = infojetContext.webSite.emailSenderAddress;

			System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
	
			mailMessage.From = new System.Net.Mail.MailAddress(senderAddress);
			mailMessage.To.Add(this.emailToAddress);
			mailMessage.Subject = this.emailSubject;
			mailMessage.IsBodyHtml = true;
			
	
			
			string body = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\" width=\"400\">";
			body = body + "<tr>";
			body = body + "<td colspan=\"2\" style=\"font-family: Verdana; font-size: 12px\"><b>"+this.description+"</b><br>&nbsp;</td>";
			body = body + "</tr>";
			
			int i = 0;
			while (i < webFormDocument.keyList.Count)
			{
				try
				{
                    WebFormField webFormField = new WebFormField(infojetContext, this.webSiteCode, this.code, webFormDocument.keyList[i].ToString());

					body = body + "<tr>";

                    if (webFormField.fieldType == 11)
                    {
                        body = body + "<td style=\"font-family: Verdana; font-size: 11px\" valign=\"top\" colspan=\"2\">" + webFormField.fieldName + "&nbsp;&nbsp;&nbsp;</td>";
                    }
                    else
                    {
                        body = body + "<td style=\"font-family: Verdana; font-size: 11px\" valign=\"top\" nowrap>" + webFormField.fieldName + "&nbsp;&nbsp;&nbsp;</td>";
                        body = body + "<td style=\"font-family: Verdana; font-size: 11px\">" + webFormDocument.valueList[i].ToString() + "</td>";
                    }

					body = body + "</tr>";
				}
				catch(Exception)
				{
                    throw new Exception("Value not found for key: " + webFormDocument.keyList[i].ToString() + ", index: " + i.ToString());
				}

				i++;
			}

			body = body + "</table>";

			mailMessage.Body = body;

			i = 0;
			while (i < webFormDocument.fileList.Count)
			{
                mailMessage.Attachments.Add(new System.Net.Mail.Attachment(System.Web.HttpContext.Current.Server.MapPath("attachments") + "\\" + webFormDocument.fileList[i].ToString()));

				i++;
			}


			try
			{
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.Host = configuration.smtpServer;
                smtpClient.Port = configuration.smtpPort;

                if (configuration.smtpAuthenticate == 1)
                {
                    System.Net.NetworkCredential smtpAuthentication = new System.Net.NetworkCredential(configuration.smtpUserName, configuration.smtpPassword);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = smtpAuthentication;
                    
                }

                smtpClient.Send(mailMessage);
			}
			catch(Exception e)
			{
				throw new Exception("Error sending mail from: "+configuration.smtpSender+", to: "+this.emailToAddress+", Server: "+configuration.smtpServer+", Exception: "+e.Message);
			}
					
		}

		private void sendFormToNavision(Infojet infojetContext, WebFormDocument webFormDocument)
		{
			
			ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "postWebForm", webFormDocument));
			appServerConnection.processRequest();

            if (appServerConnection.serviceResponse.status == "200")
            {
                WebPage webPage = infojetContext.webSite.getWebPageByCategory(this.confirmWebCategoryCode, infojetContext.userSession);
                infojetContext.redirect(webPage.getUrl());
            }
		}


        private Hashtable getConnectionValues(WebUserAccount webUserAccount)
        {
            Hashtable connectionValueTable = new Hashtable();


            WebFormFields webFormFields = new WebFormFields(infojetContext.systemDatabase);
            DataSet webFormFieldDataSet = webFormFields.getWebFormFields(webSiteCode, code);
            int i = 0;
            while (i < webFormFieldDataSet.Tables[0].Rows.Count)
            {
                WebFormField webFormField = new WebFormField(infojetContext, webFormFieldDataSet.Tables[0].Rows[i]);
                if (webUserAccount != null)
                {
                    if (webFormField.connectionType == 1) connectionValueTable.Add(webFormField.code, webUserAccount.companyName);
                    if (webFormField.connectionType == 2) connectionValueTable.Add(webFormField.code, webUserAccount.address);
                    if (webFormField.connectionType == 3) connectionValueTable.Add(webFormField.code, webUserAccount.address2);
                    if (webFormField.connectionType == 4) connectionValueTable.Add(webFormField.code, webUserAccount.postCode);
                    if (webFormField.connectionType == 5) connectionValueTable.Add(webFormField.code, webUserAccount.city);
                    if (webFormField.connectionType == 6) connectionValueTable.Add(webFormField.code, webUserAccount.countryCode);
                    if (webFormField.connectionType == 7) connectionValueTable.Add(webFormField.code, webUserAccount.billToCompanyName);
                    if (webFormField.connectionType == 8) connectionValueTable.Add(webFormField.code, webUserAccount.billToAddress);
                    if (webFormField.connectionType == 9) connectionValueTable.Add(webFormField.code, webUserAccount.billToAddress2);
                    if (webFormField.connectionType == 10) connectionValueTable.Add(webFormField.code, webUserAccount.billToPostCode);
                    if (webFormField.connectionType == 11) connectionValueTable.Add(webFormField.code, webUserAccount.billToCity);
                    if (webFormField.connectionType == 12) connectionValueTable.Add(webFormField.code, webUserAccount.billToCountryCode);
                    if (webFormField.connectionType == 13) connectionValueTable.Add(webFormField.code, webUserAccount.registrationNo);
                    if (webFormField.connectionType == 14) connectionValueTable.Add(webFormField.code, webUserAccount.email);
                    if (webFormField.connectionType == 15) connectionValueTable.Add(webFormField.code, webUserAccount.phoneNo);
                    if (webFormField.connectionType == 16) connectionValueTable.Add(webFormField.code, webUserAccount.cellPhoneNo);
                    if (webFormField.connectionType == 17) connectionValueTable.Add(webFormField.code, webUserAccount.companyRole);
                    if (webFormField.connectionType == 18) connectionValueTable.Add(webFormField.code, webUserAccount.name);
                    if (webFormField.connectionType == 19) connectionValueTable.Add(webFormField.code, webUserAccount.userId);
                    if (webFormField.connectionType == 20) connectionValueTable.Add(webFormField.code, webUserAccount.password);
                    if (webFormField.connectionType == 21) connectionValueTable.Add(webFormField.code, webUserAccount.customerNo);

                    if ((webFormField.connectionType == 0) || (webFormField.connectionType > 21)) connectionValueTable.Add(webFormField.code, webUserAccount.getHistoryProfileValue(code, webFormField.code));

                }
                if (webFormField.connectionType == 27) connectionValueTable.Add(webFormField.code, infojetContext.marketCountryCode);

                i++;
            }

            return connectionValueTable;
        }

        private void updateFormValueTable(Panel formPanel)
        {
            DataSet webFormFieldDataSet = getFields();

            int i = 0;
            while (i < webFormFieldDataSet.Tables[0].Rows.Count)
            {
                WebFormField webFormField = new WebFormField(infojetContext, webFormFieldDataSet.Tables[0].Rows[i]);

                if ((webFormField.fieldType == 0) || (webFormField.fieldType == 4))
                {
                    TextBox textBox = (TextBox)formPanel.FindControl(webFormField.controlId);
                    string valueCode = textBox.Text;

                    formValueTable[webFormField.code] = valueCode;

                }

                if ((webFormField.fieldType == 1) || (webFormField.fieldType == 3))
                {
                    string valueCode = "";

                    if (webFormField.fieldType == 1)
                    {
                        DropDownList dropDownList = (DropDownList)formPanel.FindControl(webFormField.controlId);
                        if (dropDownList.Items.Count >= dropDownList.SelectedIndex)
                        {

                            valueCode = dropDownList.Items[dropDownList.SelectedIndex].Value;
                        }
                    }

                    if (webFormField.fieldType == 3)
                    {
                        RadioButtonList radioButtonList = (RadioButtonList)formPanel.FindControl(webFormField.controlId);
                        valueCode = radioButtonList.Items[radioButtonList.SelectedIndex].Value;
                    }

                    formValueTable[webFormField.code] = valueCode;

                }

                if (webFormField.fieldType == 2)
                {
                    CheckBox checkBox = (CheckBox)formPanel.FindControl(webFormField.controlId);

                    if (checkBox.Checked)
                    {
                        formValueTable[webFormField.code] = "1";
                    }
                    else
                    {
                        formValueTable[webFormField.code] = "0";
                    }

                }



                if (webFormField.fieldType == 7)
                {
                    CheckBox checkBox = (CheckBox)formPanel.FindControl(webFormField.controlId + "_checkbox");
                    TextBox textBox = (TextBox)formPanel.FindControl(webFormField.controlId + "_text");

                    string textValue = textBox.Text;

                    if (checkBox.Checked)
                    {
                        if (textValue != "")
                        {
                            formValueTable[webFormField.code] = textValue;
                        }
                    }
                }

                if (webFormField.fieldType == 8)
                {
                    HiddenField hiddenField = (HiddenField)formPanel.FindControl(webFormField.controlId);
                    string valueCode = hiddenField.Value;

                    formValueTable[webFormField.code] = valueCode;
                }

                if (webFormField.fieldType == 9)
                {
                    CheckBoxList checkBoxList = (CheckBoxList)formPanel.FindControl(webFormField.controlId);

                    string values = "";
                    string valueTexts = "";

                    int j = 0;
                    while (j < checkBoxList.Items.Count)
                    {
                        ListItem listItem = checkBoxList.Items[j];


                        if (listItem.Selected)
                        {
                            if (values != "") values = values + ";";
                            if (valueTexts != "") valueTexts = valueTexts + ";";
                            values = values + listItem.Value;
                            valueTexts = valueTexts + listItem.Text;
                        }

                        j++;
                    }

                    formValueTable[webFormField.code] = values;
                }

                if (webFormField.fieldType == 10)
                {
                    TextBox textBox = (TextBox)formPanel.FindControl(webFormField.controlId);
                    string valueCode = textBox.Text;

                    formValueTable[webFormField.code] = valueCode;

                }

 
                if (webFormField.fieldType == 12)
                {
                    DropDownList dateListYear = (DropDownList)formPanel.FindControl(webFormField.controlId + "_YEAR");
                    DropDownList dateListMonth = (DropDownList)formPanel.FindControl(webFormField.controlId + "_MONTH");
                    DropDownList dateListDay = (DropDownList)formPanel.FindControl(webFormField.controlId + "_DAY");
                    string valueStr = dateListYear.Items[dateListYear.SelectedIndex].Value + "-" + dateListMonth.Items[dateListMonth.SelectedIndex].Value + "-" + dateListDay.Items[dateListDay.SelectedIndex].Value;

                    formValueTable[webFormField.code] = valueStr;
                }

                if (webFormField.fieldType == 13)
                {
                    DropDownList timeListHour = (DropDownList)formPanel.FindControl(webFormField.controlId + "_HOUR");
                    DropDownList timeListMinute = (DropDownList)formPanel.FindControl(webFormField.controlId + "_MINUTE");
                    string valueStr = timeListHour.Items[timeListHour.SelectedIndex].Value + ":" + timeListMinute.Items[timeListMinute.SelectedIndex].Value;

                    formValueTable[webFormField.code] = valueStr;
                }

                i++;
            }

        }

        private void fillDropDownWithValues(WebFormField webFormField, Panel panel, DropDownList dropDownList)
        {
            dropDownList.Items.Clear();

            FieldValueCollection fieldValueCollection = webFormField.getValues(panel, this);

            int j = 0;
            while (j < fieldValueCollection.Count)
            {
                FieldValue fieldValue = fieldValueCollection[j];

                ListItem listItem = new ListItem(fieldValue.description, fieldValue.code);
                dropDownList.Items.Add(listItem);
                if (formValueTable[webFormField.code] == null)
                {
                    if (fieldValue.defaultValue) dropDownList.SelectedIndex = dropDownList.Items.IndexOf(listItem);
                }
                else
                {
                    if (fieldValue.code == (string)formValueTable[webFormField.code]) dropDownList.SelectedIndex = dropDownList.Items.IndexOf(listItem);
                }

                j++;
            }


        }

        public void updateArrays(WebUserAccount webUserAccount)
        {
            WebFormFieldTranslation[] webFormFieldTranslationArray = WebFormFieldTranslation.getWebFormFieldTranslationArray(infojetContext, webSiteCode, code, infojetContext.languageCode);

            WebFormFields webFormFields = new WebFormFields(infojetContext.systemDatabase);
            webFormFieldArray = webFormFields.getWebFormFieldArray(infojetContext, webSiteCode, code);

            Hashtable formFieldValueTable = getConnectionValues(webUserAccount);

            int i = 0;
            while (i < webFormFieldArray.Length)
            {
                webFormFieldArray[i].fieldName = webFormFieldArray[i].getCaption();
                if (formFieldValueTable[webFormFieldArray[i].code] != null) webFormFieldArray[i].fieldValue = formFieldValueTable[webFormFieldArray[i].code].ToString();

                WebFormFieldConditionCollection wffcc = webFormFieldArray[i].webFormFieldConditionCollection;

                WebFormFieldValues webFormFieldValues = new WebFormFieldValues(infojetContext.systemDatabase);
                webFormFieldArray[i].webFormFieldValueArray = webFormFieldValues.getWebFormFieldValueArray(infojetContext, webSiteCode, code, webFormFieldArray[i].code);

                int x = 0;
                int y = 0;

                while (x < webFormFieldArray[i].webFormFieldValueArray.Length)
                {
                    y = 0;
                    while (y < webFormFieldTranslationArray.Length)
                    {
                        if ((webFormFieldTranslationArray[y].fieldCode == webFormFieldArray[i].webFormFieldValueArray[x].fieldCode) && (webFormFieldTranslationArray[y].type == 1) && (webFormFieldTranslationArray[y].valueCode == webFormFieldArray[i].webFormFieldValueArray[x].code))
                        {
                            webFormFieldArray[i].webFormFieldValueArray[x].description = webFormFieldTranslationArray[y].text;
                        }

                        y++;
                    }

                    x++;
                }

                y = 0;
                while (y < webFormFieldTranslationArray.Length)
                {
                    if ((webFormFieldTranslationArray[y].fieldCode == webFormFieldArray[i].code) && (webFormFieldTranslationArray[y].type == 0))
                    {
                        webFormFieldArray[i].fieldName = webFormFieldTranslationArray[y].text;
                    }

                    y++;
                }


                i++;
            }
        }
	}
}
