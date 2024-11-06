using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    public class WebItemConfigDef
    {
        private string _webConfigModelNo;
        private string _optionCode;
        private string _description;
        private int _defaultType;
        private string _defaultValue;
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
        private int _dataType;
        private string _text;
        private string _expression;

        private Infojet infojetContext;

        public WebItemConfigDef(Infojet infojetContext, SqlDataReader dataReader)
        {
            this.infojetContext = infojetContext;

            _webConfigModelNo = dataReader.GetValue(0).ToString();
            _optionCode = dataReader.GetValue(1).ToString();
            _description = dataReader.GetValue(2).ToString();
            _defaultType = int.Parse(dataReader.GetValue(3).ToString());
            _defaultValue = dataReader.GetValue(4).ToString();
            if (dataReader.GetValue(5).ToString() == "1") _required = true;
            _method = int.Parse(dataReader.GetValue(6).ToString());
            _maxLength = int.Parse(dataReader.GetValue(7).ToString());
            if (dataReader.GetValue(8).ToString() == "1") _visible = true;
            if (dataReader.GetValue(9).ToString() == "1") _includeInDescription = true;
            _descriptionStyle = int.Parse(dataReader.GetValue(10).ToString());
            _sortOrder = dataReader.GetInt32(11);
            _webConfigRuleCode = dataReader.GetValue(12).ToString();
            _assemblyMethod = int.Parse(dataReader.GetValue(13).ToString());
            _separateItemQtyOption = dataReader.GetValue(14).ToString();
            _dataType = dataReader.GetInt32(15);
            _text = dataReader.GetValue(16).ToString();
            _expression = dataReader.GetValue(17).ToString();
            _width = dataReader.GetInt32(18);
        }

        public string webConfigModelNo { get { return _webConfigModelNo; } }
        public string optionCode { get { return _optionCode; } }
        public string description { get { return _description; } set { _description = value; } }
        public int defaultType { get { return _defaultType; } }
        public string defaultValue { get { return _defaultValue; } }
        public bool required { get { return _required; } }
        public int method { get { return _method; } }
        public int maxLength { get { return _maxLength; } }
        public bool visible { get { return _visible; } }
        public bool includeInDescription { get { return _includeInDescription; } }
        public int descriptionStyle { get { return _descriptionStyle; } }
        public int sortOrder { get { return _sortOrder; } }
        public string webConfigRuleCode { get { return _webConfigRuleCode; } }
        public int assemblyMethod { get { return _assemblyMethod; } }
        public string separateItemQtyOption { get { return _separateItemQtyOption; } }
        public int dataType { get { return _dataType; } }
        public string text { get { return _text; } }
        public string expression { get { return _expression; } }
        public int width { get { return _width; } }

        public static WebItemConfigDefCollection getConfigDef(Infojet infojetContext, string webConfigModelNo)
        {
            WebItemConfigDefCollection webItemConfigDefCollection = new WebItemConfigDefCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT d.[Web Config Model No_], d.[Option Code], d.[Description], d.[Default Type], d.[Default Value], d.[Required], d.[Method], d.[Max Length], d.[Visible], d.[Include In Description], d.[Description Style], d.[Sort Order], d.[Web Config Rule Code], d.[Assembling Method], d.[Separate Item Qty Option], d.[Data Type], d.[Text], d.[Expression], d.[Width], t.[Translation] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Config Def") + "] d LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Item Config Def Trans") + "] t ON t.[Translation Type] = 0 AND d.[Option Code] = t.[Option Code] AND t.[Language Code] = @languageCode WHERE d.[Web Config Model No_] = @webConfigModelNo ORDER BY d.[Sort Order]");
            databaseQuery.addStringParameter("webConfigModelNo", webConfigModelNo, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                WebItemConfigDef webItemConfigDef = new WebItemConfigDef(infojetContext, dataReader);
                if (!dataReader.IsDBNull(19)) webItemConfigDef.description = dataReader.GetValue(19).ToString();

                webItemConfigDefCollection.Add(webItemConfigDef);
            }
            dataReader.Close();



            return webItemConfigDefCollection;
        }
    }
}
