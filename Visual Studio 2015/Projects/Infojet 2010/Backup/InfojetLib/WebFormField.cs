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

		public string webFormCode;
		public string code;
		public string fieldName;
		public int fieldType;
		public int fieldLength;
		public int sortOrder;
		public int placement;
		public int fieldSize;
		public bool required;
		public string webClassCode;
        public int connectionType;

        public int valueTableNo;
        public string valueTableName;
        public string valueCodeFieldName;
        public string valueDescriptionFieldName;
        public string valueDescription2FieldName;

        public bool readOnly;

		public WebFormField(Infojet infojetContext, string webFormCode, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
				 
			this.webFormCode = webFormCode;
			this.code = code;

			getFromDatabase();
		}

		public WebFormField(Infojet infojetContext, DataRow dataRow)
		{
            this.infojetContext = infojetContext;

			this.webFormCode = dataRow.ItemArray.GetValue(0).ToString();
			this.code = dataRow.ItemArray.GetValue(1).ToString();
			this.fieldName = dataRow.ItemArray.GetValue(2).ToString();
			this.fieldType = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.fieldLength = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.sortOrder = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
			this.placement = int.Parse(dataRow.ItemArray.GetValue(6).ToString());
			this.fieldSize = int.Parse(dataRow.ItemArray.GetValue(7).ToString());

			required = false;
			if (dataRow.ItemArray.GetValue(8).ToString() == "1") required = true;

			this.webClassCode = dataRow.ItemArray.GetValue(9).ToString();

            this.connectionType = int.Parse(dataRow.ItemArray.GetValue(10).ToString());

            this.valueTableNo = int.Parse(dataRow.ItemArray.GetValue(11).ToString());
            this.valueTableName = dataRow.ItemArray.GetValue(12).ToString();
            this.valueCodeFieldName = dataRow.ItemArray.GetValue(13).ToString();
            this.valueDescriptionFieldName = dataRow.ItemArray.GetValue(14).ToString();
            this.valueDescription2FieldName = dataRow.ItemArray.GetValue(15).ToString();

            readOnly = false;
            if (dataRow.ItemArray.GetValue(16).ToString() == "1") readOnly = true;

		}

		private void getFromDatabase()
		{
            code = code.Replace(" ", "_");

            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Web Form Code], [Code], [Field Name], [Field Type], [Field Length], [Sort Order], [Placement], [Field Size], [Required], [Web Class Code], [Connection Type], [Value Table No_], [Value Table Name], [Value Code Field Name], [Value Description Field Name], [Value Description 2 Field Name], [Read Only] FROM [" + infojetContext.systemDatabase.getTableName("Web Form Field") + "] WHERE [Web Form Code] = '" + this.webFormCode + "' AND REPLACE([Code], ' ', '_') = '" + this.code + "'");
			if (dataReader.Read())
			{

				webFormCode = dataReader.GetValue(0).ToString();
				code = dataReader.GetValue(1).ToString();
				fieldName = dataReader.GetValue(2).ToString();
				fieldType = dataReader.GetInt32(3);
				fieldLength = dataReader.GetInt32(4);
				sortOrder = dataReader.GetInt32(5);
				placement = dataReader.GetInt32(6);
				fieldSize = dataReader.GetInt32(7);
				
				required = false;
				if (dataReader.GetValue(8).ToString() == "1") required = true;

				webClassCode = dataReader.GetValue(9).ToString();
                connectionType = dataReader.GetInt32(10);

                valueTableNo = dataReader.GetInt32(11);
                valueTableName = dataReader.GetValue(12).ToString();
                valueCodeFieldName = dataReader.GetValue(13).ToString();
                valueDescriptionFieldName = dataReader.GetValue(14).ToString();
                valueDescription2FieldName = dataReader.GetValue(15).ToString();

                readOnly = false;
                if (dataReader.GetValue(16).ToString() == "1") readOnly = true;

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

            WebFormFieldTranslation webFormFieldTranslation = new WebFormFieldTranslation(infojetContext.systemDatabase, this.webFormCode, this.code, 0, "", languageCode);
            if ((webFormFieldTranslation.text != null))
            {
                if (required) return webFormFieldTranslation.text + " " + requiredStr;
                return webFormFieldTranslation.text;
            }

            return this.fieldName + " " + requiredStr;
		}

        public FieldValueCollection getValues(Hashtable formValueTable)
        {
            FieldValueCollection fieldValueCollection = new FieldValueCollection();

            WebFormFieldValues webFormFieldValues = new WebFormFieldValues(infojetContext.systemDatabase);
            DataSet valueDataSet = webFormFieldValues.getWebFormFieldValues(this.webFormCode, this.code);
            int i = 0;
            while (i < valueDataSet.Tables[0].Rows.Count)
            {
                WebFormFieldValue webFormFieldValue = new WebFormFieldValue(infojetContext, valueDataSet.Tables[0].Rows[i]);
                fieldValueCollection.Add(webFormFieldValue.getFieldValue());    

                i++;
            }

            if (valueTableNo > 0)
            {
                DataSet tableValueDataSet = getTableValues(formValueTable);

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

        public string getValueDescription(string valueCode)
        {
            WebFormFieldValue webFormFieldValue = new WebFormFieldValue(infojetContext, this.webFormCode, code, valueCode);
            if (webFormFieldValue.description != null) return webFormFieldValue.description;

            return "";

        }

        private DataSet getTableValues(Hashtable formValueTable)
        {
            if (this.valueCodeFieldName != "")
            {
                string fieldQuery = "[" + this.valueCodeFieldName + "]";
                if (this.valueDescriptionFieldName != "") fieldQuery = fieldQuery + ", [" + infojetContext.systemDatabase.transformName(this.valueDescriptionFieldName) + "]";
                if (this.valueDescription2FieldName != "") fieldQuery = fieldQuery + ", [" + infojetContext.systemDatabase.transformName(this.valueDescription2FieldName) + "]";

                string filters = getTableFilters(formValueTable);
                if (filters != "") filters = " WHERE " + filters;

                valueTableName = valueTableName.Replace("/", "_");

                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT "+fieldQuery+" FROM [" + infojetContext.systemDatabase.getTableName(valueTableName) + "]"+filters);

                SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();

                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                return dataSet;
            }

            return null;
        }

        private string getTableFilters(Hashtable formValueTable)
        {
            string filters = "";

            WebFormFieldFilters webFormFieldFilters = new WebFormFieldFilters(infojetContext.systemDatabase);
            DataSet webFormFieldFilterDataSet = webFormFieldFilters.getWebFormFieldFilters(webFormCode, code);

            int i = 0;
            while (i < webFormFieldFilterDataSet.Tables[0].Rows.Count)
            {
                WebFormFieldFilter webFormFieldFilter = new WebFormFieldFilter(infojetContext, webFormFieldFilterDataSet.Tables[0].Rows[i]);

                if (filters != "") filters = filters + " AND ";

                if (webFormFieldFilter.filterType == 0)
                {
                    filters = filters + "[" + infojetContext.systemDatabase.transformName(webFormFieldFilter.valueTableFieldName) + "] = '" + formValueTable[webFormFieldFilter.filterValue] + "'";
                }
                if (webFormFieldFilter.filterType == 1)
                {
                    filters = filters + "[" + infojetContext.systemDatabase.transformName(webFormFieldFilter.valueTableFieldName) + "] = '" + webFormFieldFilter.filterValue + "'";
                }

                i++;
            }

            return filters;
        }


        public void doAssignments(System.Web.UI.WebControls.Panel panel, string valueCode, Hashtable formValueTable)
        {
            WebFormFieldAssignments webFormFieldAssignments = new WebFormFieldAssignments(infojetContext);

            DataSet sourceTableDataSet = null;
            string fields = webFormFieldAssignments.getAssignmentFields(webFormCode, code);

            if (fields != "")
            {
                string filters = getTableFilters(formValueTable);
                if (filters != "") filters = "AND "+filters;

                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT " + fields + " FROM [" + infojetContext.systemDatabase.getTableName(this.valueTableName) + "] WHERE [" + infojetContext.systemDatabase.transformName(this.valueCodeFieldName) + "] = '" + valueCode + "' " + filters);

                SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();

                sourceTableDataSet = new DataSet();
                sqlDataAdapter.Fill(sourceTableDataSet);


            }

            DataSet assignmentDataSet = webFormFieldAssignments.getWebFormFieldAssignments(webFormCode, code);
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
            WebFormField webFormField = new WebFormField(infojetContext, webFormCode, webFormFieldCode);

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

	}
}
