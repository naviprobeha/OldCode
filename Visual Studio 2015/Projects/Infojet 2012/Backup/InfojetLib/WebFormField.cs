using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class WebFormField
	{
		private Infojet infojetContext;

        public string webSiteCode;
		public string webFormCode;
		public string code;
        public string originalCode;
		public string fieldName;
		public int fieldType;
		public int fieldLength;
		public int sortOrder;
		public int placement;
		public int fieldSize;
		public bool required;
        public int connectionType;
        public string expression;
        public bool transferToOrderTextLine;

        public int valueTableNo;
        public string valueTableName;
        public string valueCodeFieldName;
        public string valueDescriptionFieldName;
        public string valueDescription2FieldName;
        public string valueDescription3FieldName;

        public bool readOnly;
        private WebFormFieldConditionCollection _webFormFieldConditionCollection;
        public WebFormFieldValue[] webFormFieldValueArray;

        public string fieldValue;
        public string translation;

        public WebFormField()
        {
        }


		public WebFormField(Infojet infojetContext, string webSiteCode, string webFormCode, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
				 
			this.webFormCode = webFormCode;
			this.code = code;
            this.webSiteCode = webSiteCode;

			getFromDatabase();
		}

		public WebFormField(Infojet infojetContext, DataRow dataRow)
		{
            this.infojetContext = infojetContext;

			this.webFormCode = dataRow.ItemArray.GetValue(0).ToString();
			this.code = dataRow.ItemArray.GetValue(1).ToString();
            this.originalCode = code;
			this.fieldName = dataRow.ItemArray.GetValue(2).ToString();
			this.fieldType = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.fieldLength = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.sortOrder = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
			this.placement = int.Parse(dataRow.ItemArray.GetValue(6).ToString());
			this.fieldSize = int.Parse(dataRow.ItemArray.GetValue(7).ToString());

			required = false;
			if (dataRow.ItemArray.GetValue(8).ToString() == "1") required = true;

            this.connectionType = int.Parse(dataRow.ItemArray.GetValue(9).ToString());

            this.valueTableNo = int.Parse(dataRow.ItemArray.GetValue(10).ToString());
            this.valueTableName = dataRow.ItemArray.GetValue(11).ToString();
            this.valueCodeFieldName = dataRow.ItemArray.GetValue(12).ToString();
            this.valueDescriptionFieldName = dataRow.ItemArray.GetValue(13).ToString();
            this.valueDescription2FieldName = dataRow.ItemArray.GetValue(14).ToString();

            readOnly = false;
            if (dataRow.ItemArray.GetValue(15).ToString() == "1") readOnly = true;

            this.valueDescription3FieldName = dataRow.ItemArray.GetValue(16).ToString();

            this.webSiteCode = dataRow.ItemArray.GetValue(17).ToString();
            this.expression = dataRow.ItemArray.GetValue(18).ToString();

            transferToOrderTextLine = false;
            if (dataRow.ItemArray.GetValue(19).ToString() == "1") transferToOrderTextLine = true;

		}

		private void getFromDatabase()
		{
            code = code.Replace(" ", "_");

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Form Code], [Code], [Field Name], [Field Type], [Field Length], [Sort Order], [Placement], [Field Size], [Required], [Connection Type], [Value Table No_], [Value Table Name], [Value Code Field Name], [Value Description Field Name], [Value Description 2 Field Name], [Read Only], [Value Description 3 Field Name], [Web Site Code], [Expression], [Transfer To Order Text Line] FROM [" + infojetContext.systemDatabase.getTableName("Web Form Field") + "] WHERE [Web Form Code] = @webFormCode AND REPLACE([Code], ' ', '_') = @code AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("code", code, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webFormCode", webFormCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				webFormCode = dataReader.GetValue(0).ToString();
				code = dataReader.GetValue(1).ToString();
                originalCode = dataReader.GetValue(1).ToString();
				fieldName = dataReader.GetValue(2).ToString();
				fieldType = dataReader.GetInt32(3);
				fieldLength = dataReader.GetInt32(4);
				sortOrder = dataReader.GetInt32(5);
				placement = dataReader.GetInt32(6);
				fieldSize = dataReader.GetInt32(7);
				
				required = false;
				if (dataReader.GetValue(8).ToString() == "1") required = true;

                connectionType = dataReader.GetInt32(9);

                valueTableNo = dataReader.GetInt32(10);
                valueTableName = dataReader.GetValue(11).ToString();
                valueCodeFieldName = dataReader.GetValue(12).ToString();
                valueDescriptionFieldName = dataReader.GetValue(13).ToString();
                valueDescription2FieldName = dataReader.GetValue(14).ToString();

                readOnly = false;
                if (dataReader.GetValue(15).ToString() == "1") readOnly = true;

                valueDescription3FieldName = dataReader.GetValue(16).ToString();
                webSiteCode = dataReader.GetValue(17).ToString();
                expression = dataReader.GetValue(18).ToString();

                transferToOrderTextLine = false;
                if (dataReader.GetValue(19).ToString() == "1") transferToOrderTextLine = true;

            }

			dataReader.Close();

            code = code.Replace(" ", "_");

		}

        public string getCaption()
        {
            return getCaption(infojetContext.languageCode, true);
        }

        public string getCaption(bool showRequired)
        {
            return getCaption(infojetContext.languageCode, showRequired);
        }

		public string getCaption(string languageCode, bool showRequired)
		{
            string requiredStr = "";
            if ((showRequired) && (this.required)) requiredStr = "*";

            WebFormFieldTranslation webFormFieldTranslation = new WebFormFieldTranslation(infojetContext.systemDatabase, this.webSiteCode, this.webFormCode, this.code, 0, "", languageCode);
			if ((webFormFieldTranslation.text != null) && (webFormFieldTranslation.text != "")) return webFormFieldTranslation.text + " " + requiredStr;

            return this.fieldName + " " + requiredStr;
		}

        public FieldValueCollection getValues(System.Web.UI.WebControls.Panel panel, WebForm webForm)
        {
            FieldValueCollection fieldValueCollection = new FieldValueCollection();


            WebFormFieldValues webFormFieldValues = new WebFormFieldValues(infojetContext.systemDatabase);
            DataSet valueDataSet = webFormFieldValues.getWebFormFieldValues(this.webSiteCode, this.webFormCode, this.code);
            int i = 0;
            while (i < valueDataSet.Tables[0].Rows.Count)
            {
                WebFormFieldValue webFormFieldValue = new WebFormFieldValue(infojetContext, valueDataSet.Tables[0].Rows[i]);
                fieldValueCollection.Add(webFormFieldValue.getFieldValue());    

                i++;
            }

            if (valueTableNo > 0)
            {
                DataSet tableValueDataSet = getTableValues(panel, webForm);

                int j = 0;
                while (j < tableValueDataSet.Tables[0].Rows.Count)
                {
                    FieldValue fieldValue = new FieldValue();
                    fieldValue.code = tableValueDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                    if (tableValueDataSet.Tables[0].Rows[j].ItemArray.Length > 1)
                    {
                        fieldValue.description = tableValueDataSet.Tables[0].Rows[j].ItemArray.GetValue(1).ToString();
                    }
                    if (tableValueDataSet.Tables[0].Rows[j].ItemArray.Length > 2)
                    {
                        fieldValue.description = fieldValue.description +", "+ tableValueDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString();
                    }
                    if (tableValueDataSet.Tables[0].Rows[j].ItemArray.Length > 3)
                    {
                        fieldValue.description = fieldValue.description + ", " + tableValueDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString();
                    }
                    fieldValueCollection.Add(fieldValue);

                    j++;
                }
            }

            return fieldValueCollection;
        }

        public string controlId
        {
            get
            {
                return code.Replace(" ", "_");
            }
        }

        public int width
        {
            get
            {
                if (fieldSize == 0)
                {
                    int size = (fieldLength * 3);
                    if (size > 30) size = 100;
                    return size;
                }
                return fieldSize;
            }
        }

        public WebFormFieldConditionCollection webFormFieldConditionCollection
        {
            get
            {
                if (_webFormFieldConditionCollection == null)
                {
                    _webFormFieldConditionCollection = getConditions();
                }
                return _webFormFieldConditionCollection;
            }
        }


        public string getValueDescription(string valueCode)
        {
            WebFormFieldValue webFormFieldValue = new WebFormFieldValue(infojetContext, this.webSiteCode, this.webFormCode, code, valueCode);
            if (webFormFieldValue.description != null) return webFormFieldValue.description;

            return "";

        }

        private DataSet getTableValues(System.Web.UI.WebControls.Panel panel, WebForm webForm)
        {
            if (this.valueCodeFieldName != "")
            {
                string fieldQuery = "[" + infojetContext.systemDatabase.transformName(this.valueCodeFieldName) + "] as vCode";
                if (this.valueDescriptionFieldName != "") fieldQuery = fieldQuery + ", [" + infojetContext.systemDatabase.transformName(this.valueDescriptionFieldName) + "] as vDesc1";
                if (this.valueDescription2FieldName != "") fieldQuery = fieldQuery + ", [" + infojetContext.systemDatabase.transformName(this.valueDescription2FieldName) + "] as vDesc2";
                if (this.valueDescription3FieldName != "") fieldQuery = fieldQuery + ", [" + infojetContext.systemDatabase.transformName(this.valueDescription3FieldName) + "] as vDesc3";

                string filters = getTableFilters(panel, webForm);
                if (filters != "") filters = " WHERE " + filters;

                string orderBy = "";
                if (this.valueDescriptionFieldName != "") orderBy = orderBy + "[" + infojetContext.systemDatabase.transformName(this.valueDescriptionFieldName) + "]";
                if (this.valueDescription2FieldName != "")
                {
                    if (orderBy != "") orderBy = orderBy + ", ";
                    orderBy = orderBy + "[" + infojetContext.systemDatabase.transformName(this.valueDescription2FieldName) + "]";
                }
                if (this.valueDescription3FieldName != "")
                {
                    if (orderBy != "") orderBy = orderBy + ", ";
                    orderBy = orderBy + "[" + infojetContext.systemDatabase.transformName(this.valueDescription3FieldName) + "]";
                }
                if (orderBy != "") orderBy = " ORDER BY " + orderBy;

                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT " + fieldQuery + " FROM [" + infojetContext.systemDatabase.getTableName(infojetContext.systemDatabase.convertTableName(valueTableName).Replace("/", "_")) + "]" + filters + orderBy);

                SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();

                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                return dataSet;
            }

            return null;
        }

        private string getTableFilters(System.Web.UI.WebControls.Panel panel, WebForm webForm)
        {
            string filters = "";

            WebFormFieldFilters webFormFieldFilters = new WebFormFieldFilters(infojetContext.systemDatabase);
            DataSet webFormFieldFilterDataSet = webFormFieldFilters.getWebFormFieldFilters(webSiteCode, webFormCode, code);

            int i = 0;
            while (i < webFormFieldFilterDataSet.Tables[0].Rows.Count)
            {
                WebFormFieldFilter webFormFieldFilter = new WebFormFieldFilter(infojetContext, webFormFieldFilterDataSet.Tables[0].Rows[i]);

                if (filters != "") filters = filters + " AND ";

                if (webFormFieldFilter.filterType == 0)
                {
                   
                    filters = filters + "[" + infojetContext.systemDatabase.transformName(webFormFieldFilter.valueTableFieldName) + "] = '" + getFieldValue(webFormFieldFilter.filterValue, panel, webForm) + "'";
                }
                if (webFormFieldFilter.filterType == 1)
                {
                    if (webFormFieldFilter.filterValue.Contains("*"))
                    {
                        webFormFieldFilter.filterValue = webFormFieldFilter.filterValue.Replace("*", "%");
                        filters = filters + "[" + infojetContext.systemDatabase.transformName(webFormFieldFilter.valueTableFieldName) + "] LIKE '" + webFormFieldFilter.filterValue + "'";
                    }
                    else
                    {
                        filters = filters + "[" + infojetContext.systemDatabase.transformName(webFormFieldFilter.valueTableFieldName) + "] = '" + webFormFieldFilter.filterValue + "'";
                    }
                }

                i++;
            }

            return filters;
        }

        public void doAssignments(System.Web.UI.WebControls.Panel panel, string valueCode, Hashtable formValueTable, WebForm webForm)
        {
            WebFormFieldAssignments webFormFieldAssignments = new WebFormFieldAssignments(infojetContext);

            DataSet sourceTableDataSet = null;
            string fields = webFormFieldAssignments.getAssignmentFields(webSiteCode, webFormCode, code);

            if (fields != "")
            {
                string filters = getTableFilters(panel, webForm);
                if (filters != "") filters = "AND "+filters;
                //throw new Exception("SELECT " + fields + " FROM [" + infojetContext.systemDatabase.getTableName(this.valueTableName) + "] WHERE [" + infojetContext.systemDatabase.transformName(this.valueCodeFieldName) + "] = '" + valueCode + "' " + filters);

                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT " + fields + " FROM [" + infojetContext.systemDatabase.getTableName(this.valueTableName) + "] WHERE [" + infojetContext.systemDatabase.transformName(this.valueCodeFieldName) + "] = '" + valueCode + "' " + filters);

                SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();

                sourceTableDataSet = new DataSet();
                sqlDataAdapter.Fill(sourceTableDataSet);


            }

            DataSet assignmentDataSet = webFormFieldAssignments.getWebFormFieldAssignments(webSiteCode, webFormCode, code);
            int i = 0;
            int fieldCount = 0;
            while (i < assignmentDataSet.Tables[0].Rows.Count)
            {
                WebFormFieldAssignment webFormFieldAssignment = new WebFormFieldAssignment(infojetContext, assignmentDataSet.Tables[0].Rows[i]);
              
                if ((webFormFieldAssignment.assignType == 0) && (sourceTableDataSet != null) && (sourceTableDataSet.Tables[0].Rows.Count > 0))
                {
                    string value = sourceTableDataSet.Tables[0].Rows[0].ItemArray.GetValue(fieldCount).ToString();
                    fieldCount++;

                    setFieldValue(webFormFieldAssignment.assignWebFormFieldCode, value, panel);

                }
                if (webFormFieldAssignment.assignType == 1)
                {
                    setFieldValue(webFormFieldAssignment.assignWebFormFieldCode, webFormFieldAssignment.assignValue, panel);
                }
                

                i++;
            }

        }

        private void setFieldValue(string webFormFieldCode, string value, System.Web.UI.WebControls.Panel panel)
        {
            WebFormField webFormField = new WebFormField(infojetContext, webSiteCode, webFormCode, webFormFieldCode);

            if (webFormField.fieldType == 0)
            {
                System.Web.UI.WebControls.TextBox control = (System.Web.UI.WebControls.TextBox)findControl(panel, webFormField.code);
                if (control != null) control.Text = value;
            }
            if (webFormField.fieldType == 1)
            {
                System.Web.UI.WebControls.DropDownList control = (System.Web.UI.WebControls.DropDownList)findControl(panel, webFormField.code);
                if (control != null) control.Text = value;
            }
            if (webFormField.fieldType == 2)
            {
                System.Web.UI.WebControls.CheckBox control = (System.Web.UI.WebControls.CheckBox)findControl(panel, webFormField.code);
                if ((control != null) && (value == "0")) control.Checked = false;
                if ((control != null) && (value == "1")) control.Checked = true;
            }
            if (webFormField.fieldType == 3)
            {
                System.Web.UI.WebControls.RadioButton control = (System.Web.UI.WebControls.RadioButton)findControl(panel, webFormField.code);
                if (control != null) control.Text = value;
            }

        }

        private string getFieldValue(string webFormFieldCode, System.Web.UI.WebControls.Panel panel, WebForm webForm)
        {
            WebFormField webFormField = new WebFormField(infojetContext, webSiteCode, webFormCode, webFormFieldCode);

            if (webFormField.fieldType == 0)
            {
                System.Web.UI.WebControls.TextBox control = (System.Web.UI.WebControls.TextBox)findControl(panel, webFormField.code);
                if (control != null) return control.Text;
            }
            if (webFormField.fieldType == 1)
            {
                System.Web.UI.WebControls.DropDownList control = (System.Web.UI.WebControls.DropDownList)findControl(panel, webFormField.code);
                if (control != null) return control.Text;
            }
            if (webFormField.fieldType == 2)
            {
                System.Web.UI.WebControls.CheckBox control = (System.Web.UI.WebControls.CheckBox)findControl(panel, webFormField.code);
                if ((control != null) && (control.Checked == false)) return "0";
                if ((control != null) && (control.Checked == true)) return "1";
            }
            if (webFormField.fieldType == 3)
            {
                System.Web.UI.WebControls.RadioButton control = (System.Web.UI.WebControls.RadioButton)findControl(panel, webFormField.code);
                if (control != null) return control.Text;
            }
            if ((webForm != null) && (webForm.getFormValues()[webFormField.code] != null)) return (string)webForm.getFormValues()[webFormField.code];

            return "";
        }

        private System.Web.UI.Control findControl(System.Web.UI.Control control, string id)
        {
            System.Web.UI.Control foundControl = control.FindControl(id);
            if (foundControl == null)
            {
                System.Web.UI.ControlCollection controlCollection = control.Controls;
                int i = 0;
                while ((i < controlCollection.Count) && (foundControl == null))
                {
                    System.Web.UI.Control childControl = controlCollection[i];
                    foundControl = findControl(childControl, id);

                    i++;
                }
            }

            return foundControl;
        }


        private WebFormFieldConditionCollection getConditions()
        {
            WebFormFieldConditionCollection webFormFieldConditionCollection = new WebFormFieldConditionCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Form Code], [Web Form Field Code], [Web Site Code], [Function], [Source Type], [Source Code], [Operator], [Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Form Field Condition") + "] WHERE [Web Form Code] = @webFormCode AND ([Web Form Field Code] = @fieldCode OR REPLACE([Web Form Field Code], ' ', '_') = @fieldCode) AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webFormCode", webFormCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("fieldCode", code, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebFormFieldCondition webFormFieldCondition = new WebFormFieldCondition(infojetContext, dataSet.Tables[0].Rows[i]);
                webFormFieldConditionCollection.Add(webFormFieldCondition);

                i++;
            }

            return webFormFieldConditionCollection;
        }


        public bool evaluateCondition(int function)
        {
            // Required Field: function == 0

            if (webFormFieldConditionCollection.Count == 0) return false;

            int i = 0;
            while (i < webFormFieldConditionCollection.Count)
            {
                if (webFormFieldConditionCollection[i].function == function)
                {
                    if (!webFormFieldConditionCollection[i].evaluate(infojetContext)) return false;
                }
                i++;
            }

            return true;
        }
	}
}
