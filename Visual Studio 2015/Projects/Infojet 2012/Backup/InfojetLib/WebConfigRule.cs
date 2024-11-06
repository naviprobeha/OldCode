using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for PostCode.
    /// </summary>
    public class WebConfigRule
    {
        private string _code;
        private string _description;
        private Infojet infojetContext;

        public WebConfigRule(Infojet infojetContext, string code)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this._code = code;

            //getFromDatabase();
        }

        public WebConfigRule(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this._code = dataRow.ItemArray.GetValue(0).ToString();
            this._description = dataRow.ItemArray.GetValue(1).ToString();
        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Code], [Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Config Rule") + "] WHERE [Code] = @code");
            databaseQuery.addStringParameter("code", _code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                _code = dataReader.GetValue(0).ToString();
                _description = dataReader.GetValue(1).ToString();

            }

            dataReader.Close();


        }

 
        public string getExpression(WebItemConfigHeader webItemConfigHeader)
        {
            string expression = "(";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Function], [Parenthesis], [Operator Type 1], [Operator 1], [Operation], [Operator Type 2], [Operator 2], [Logical Operator], [Parenthesis End] FROM [" + infojetContext.systemDatabase.getTableName("Web Config Rule Line") + "] WHERE [Rule Code] = @code ORDER BY [Line No_]");
            databaseQuery.addStringParameter("code", _code, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) > 0) expression = expression + "!";
				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()) > 0) expression = expression + "(";

                if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString()) == 0)
                {
                    if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() == "QUANTITY")
                    {
                        expression = expression + "[" + webItemConfigHeader.cartQuantity + "]";
                    }
                }

				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString()) == 1) 
                {
                    WebItemConfigLine webItemConfigLine = webItemConfigHeader.getWebItemConfigLine(dataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());
                    if (webItemConfigLine != null)
                    {
                        expression = expression + "["+webItemConfigLine.optionValue+"]";
                    }
                    else
                    {
                        expression = expression + "[]";
                    }
                }

				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString()) == 2) 
                {
                    if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() != "")
                    {
                        expression = expression + "["+dataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString()+"]";
                    }
                    else
                    {
                        expression = expression + "["+webItemConfigHeader.itemNo+"]";
                    }
                }

				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString()) == 1) expression = expression + "=";
				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString()) == 2) expression = expression + ">";
				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString()) == 3) expression = expression + "<";
				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString()) == 4) expression = expression + ">=";
				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString()) == 5) expression = expression + "<=";
				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString()) == 6) expression = expression + "<>";


				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString()) == 1) 
                {
                    expression = expression + "["+dataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString()+"]";
                }

				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString()) == 2) 
                {
                    WebItemConfigLine webItemConfigLine = webItemConfigHeader.getWebItemConfigLine(dataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString());
                    if (webItemConfigLine != null)
                    {
                        expression = expression + "["+webItemConfigLine.optionValue+"]";
                    }
                    else
                    {
                        expression = expression + "[]";
                    }
                }
	
				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString()) == 1) expression = expression + ") AND (";
				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString()) == 2) expression = expression + ") OR (";
			
				if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString()) == 1) expression = expression + ")";

                i++;
            }

            expression = expression + ")";


            return expression;

        }

        public static bool evaluateRule(Infojet infojetContext, WebItemConfigHeader webItemConfigHeader, string ruleCode)
        {
            WebConfigRule webConfigRule = new WebConfigRule(infojetContext, ruleCode);
            string expression = webConfigRule.getExpression(webItemConfigHeader);

            string result = webConfigRule.evaluateExpression(expression);

            if (result == "1") return true;
            return false;
        }

        private string evaluateExpression(string expression)
        {
            
            int endPos = 0;

            if (expression.IndexOf("(") > -1)
            {
                while (expression.IndexOf("(") > -1)
                {
                    int startPos = expression.IndexOf("(");
                    bool found = false;
                    int currentPos = startPos;
                    int noOfSubExp = 1;

                    while (!found)
                    {
                        currentPos++;

                        if (expression.Substring(currentPos, 1) == "(") noOfSubExp++;
                        if (expression.Substring(currentPos, 1) == ")") noOfSubExp--;

				        if (noOfSubExp == 0) 
                        {
                            found = true;
                            endPos = currentPos;
                        }
                    }

                    string subExpr = expression.Substring(startPos+1, endPos-startPos-1);
                    subExpr = evaluateExpression(subExpr);
                    //throw new Exception(subExpr + ", " + expression + ", " + startPos + ", " + endPos);
                    expression = expression.Substring(0, startPos)+subExpr+expression.Substring(endPos+1);

                }
            }


            if (expression.IndexOf("<>") > -1)
            {
                while (expression.IndexOf("<>") > -1)
                {
                    string op1 = expression.Substring(expression.IndexOf("[") + 1);
                    op1 = op1.Substring(0, op1.IndexOf("]"));
                    string operation = expression.Substring(expression.IndexOf("]") + 1);
                    operation = operation.Substring(0, operation.IndexOf("["));
                    string op2 = expression.Substring(expression.IndexOf(operation));
                    op2 = op2.Substring(op2.IndexOf("[") + 1);

                    string expressionEnd = op2;
                    op2 = op2.Substring(0, op2.IndexOf("]"));
                    expressionEnd = expressionEnd.Substring(expressionEnd.IndexOf("]") + 1);
                    expression = expression.Substring(0, expression.IndexOf("["));

                    if (op1 != op2)
                    {
                        expression = expression + "1" + expressionEnd;
                    }
                    else
                    {
                        expression = expression + "0" + expressionEnd;
                    }
                }
            }

            if (expression.IndexOf("<=") > -1)
            {
                while (expression.IndexOf("<=") > -1)
                {
                    string op1 = expression.Substring(expression.IndexOf("[") + 1);
                    op1 = op1.Substring(0, op1.IndexOf("]"));
                    string operation = expression.Substring(expression.IndexOf("]") + 1);
                    operation = operation.Substring(0, operation.IndexOf("["));
                    string op2 = expression.Substring(expression.IndexOf(operation));
                    op2 = op2.Substring(op2.IndexOf("[") + 1);

                    string expressionEnd = op2;
                    op2 = op2.Substring(0, op2.IndexOf("]"));
                    expressionEnd = expressionEnd.Substring(expressionEnd.IndexOf("]") + 1);
                    expression = expression.Substring(0, expression.IndexOf("["));

                    try
                    {
                        if (float.Parse(op1) <= float.Parse(op2))
                        {
                            expression = expression + "1" + expressionEnd;
                        }
                        else
                        {
                            expression = expression + "0" + expressionEnd;
                        }
                    }
                    catch (Exception)
                    {
                        expression = expression + "0" + expressionEnd;
                    }
                }
            }

            if (expression.IndexOf(">=") > -1)
            {
                while (expression.IndexOf(">=") > -1)
                {
                    string op1 = expression.Substring(expression.IndexOf("[") + 1);
                    op1 = op1.Substring(0, op1.IndexOf("]"));
                    string operation = expression.Substring(expression.IndexOf("]") + 1);
                    operation = operation.Substring(0, operation.IndexOf("["));
                    string op2 = expression.Substring(expression.IndexOf(operation));
                    op2 = op2.Substring(op2.IndexOf("[") + 1);

                    string expressionEnd = op2;
                    op2 = op2.Substring(0, op2.IndexOf("]"));
                    expressionEnd = expressionEnd.Substring(expressionEnd.IndexOf("]") + 1);
                    expression = expression.Substring(0, expression.IndexOf("["));

                    try
                    {
                        if (float.Parse(op1) >= float.Parse(op2))
                        {
                            expression = expression + "1" + expressionEnd;
                        }
                        else
                        {
                            expression = expression + "0" + expressionEnd;
                        }
                    }
                    catch (Exception)
                    {
                        expression = expression + "0" + expressionEnd;
                    }
                }
            }

            if (expression.IndexOf("=") > -1)
            {
                while (expression.IndexOf("=") > -1)
                {
                    string op1 = expression.Substring(expression.IndexOf("[") + 1);
                    op1 = op1.Substring(0, op1.IndexOf("]"));
                    string operation = expression.Substring(expression.IndexOf("]") + 1);
                    operation = operation.Substring(0, operation.IndexOf("["));
                    string op2 = expression.Substring(expression.IndexOf(operation));
                    op2 = op2.Substring(op2.IndexOf("[") + 1);

                    string expressionEnd = op2;
                    op2 = op2.Substring(0, op2.IndexOf("]"));
                    expressionEnd = expressionEnd.Substring(expressionEnd.IndexOf("]") + 1);
                    expression = expression.Substring(0, expression.IndexOf("["));

                    //throw new Exception(op1 + ", " + op2 + ", " + operation);

                    if (op1 == op2)
                    {
                        expression = expression + "1" + expressionEnd;
                    }
                    else
                    {
                        expression = expression + "0" + expressionEnd;
                    }
                }
            }

            if (expression.IndexOf("<") > -1)
            {
                while (expression.IndexOf("<") > -1)
                {
                    string op1 = expression.Substring(expression.IndexOf("[") + 1);
                    op1 = op1.Substring(0, op1.IndexOf("]"));
                    string operation = expression.Substring(expression.IndexOf("]") + 1);
                    operation = operation.Substring(0, operation.IndexOf("["));
                    string op2 = expression.Substring(expression.IndexOf(operation));
                    op2 = op2.Substring(op2.IndexOf("[") + 1);

                    string expressionEnd = op2;
                    op2 = op2.Substring(0, op2.IndexOf("]"));
                    expressionEnd = expressionEnd.Substring(expressionEnd.IndexOf("]") + 1);
                    expression = expression.Substring(0, expression.IndexOf("["));

                    try
                    {
                        if (float.Parse(op1) < float.Parse(op2))
                        {
                            expression = expression + "1" + expressionEnd;
                        }
                        else
                        {
                            expression = expression + "0" + expressionEnd;
                        }
                    }
                    catch (Exception)
                    {
                        expression = expression + "0" + expressionEnd;
                    }
                }
            }

            if (expression.IndexOf(">") > -1)
            {
                while (expression.IndexOf(">") > -1)
                {
                    string op1 = expression.Substring(expression.IndexOf("[") + 1);
                    op1 = op1.Substring(0, op1.IndexOf("]"));
                    string operation = expression.Substring(expression.IndexOf("]") + 1);
                    operation = operation.Substring(0, operation.IndexOf("["));
                    string op2 = expression.Substring(expression.IndexOf(operation));
                    op2 = op2.Substring(op2.IndexOf("[") + 1);

                    string expressionEnd = op2;
                    op2 = op2.Substring(0, op2.IndexOf("]"));
                    expressionEnd = expressionEnd.Substring(expressionEnd.IndexOf("]") + 1);
                    expression = expression.Substring(0, expression.IndexOf("["));

                    try
                    {
                        if (float.Parse(op1) > float.Parse(op2))
                        {
                            expression = expression + "1" + expressionEnd;
                        }
                        else
                        {
                            expression = expression + "0" + expressionEnd;
                        }
                    }
                    catch (Exception)
                    {
                        expression = expression + "0" + expressionEnd;
                    }
                }
            }


            if (expression.IndexOf(" ") > -1)
            {

                while (expression.IndexOf(" ") > -1)
                {


                    string op1 = expression.Substring(0, expression.IndexOf(" "));
                    string operation = expression.Substring(expression.IndexOf(" ")+1);
                    operation = operation.Substring(0, operation.IndexOf(" "));
                    expression = expression.Substring(expression.IndexOf(operation));
                    string op2 = expression.Substring(expression.IndexOf(" ")+1);
                    expression = op2;
                    if (op2.IndexOf(" ") > 0)
                    {
                        op2 = op2.Substring(0, op2.IndexOf(" "));
                        expression = expression.Substring(expression.IndexOf(" ")+1);
                    }
                    else
                    {
                        expression = "";
                    }



                    string result = "0";
                    if ((operation == "AND") && ((op1 == "1") && (op2 == "1"))) result = "1";
                    if ((operation == "OR") && ((op1 == "1") || (op2 == "1"))) result = "1";

                    if (expression != "")
                    {
                        expression = result + " " + expression;
                    }
                    else
                    {
                        expression = result;
                    }


                }
            }


            return expression;
        }





    }
}
