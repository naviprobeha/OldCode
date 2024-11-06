using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Test interpreter");

            string expression = "(([1]<[0]) AND ([2]>=[1]) OR ([3]>[1]))";
            Console.WriteLine("Expression: " + expression);
            Console.WriteLine("Response: " + evaluateExpression(expression));
            Console.ReadLine();
            */
            string no = "ABCDEF";
            Console.WriteLine("Increase no " + no + ": "+increaseStr(no));
            Console.ReadLine();
        }


        public static string evaluateExpression(string expression)
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

                    string subExpr = expression.Substring(startPos + 1, endPos - startPos - 1);
                    subExpr = evaluateExpression(subExpr);
                    //throw new Exception(subExpr + ", " + expression + ", " + startPos + ", " + endPos);
                    expression = expression.Substring(0, startPos) + subExpr + expression.Substring(endPos + 1);

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
                    string operation = expression.Substring(expression.IndexOf(" ") + 1);
                    operation = operation.Substring(0, operation.IndexOf(" "));
                    expression = expression.Substring(expression.IndexOf(operation));
                    string op2 = expression.Substring(expression.IndexOf(" ") + 1);
                   
                    expression = op2;
                    if (op2.IndexOf(" ") > 0)
                    {
                        op2 = op2.Substring(0, op2.IndexOf(" "));
                        expression = expression.Substring(expression.IndexOf(" ") + 1);
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


        private static string increaseStr(string stringNo)
        {
            int position = stringNo.Length;
            while (position > 0)
            {
                position--;
                if (!System.Char.IsDigit(stringNo[position])) break;
            }
            if (position + 1 == stringNo.Length) return "";

            string digitPart = stringNo.Substring(position+1, stringNo.Length - (position + 1));
            
            int digitInteger = int.Parse(digitPart);
            digitInteger++;
            string newDigitPart = digitInteger.ToString().PadLeft(digitPart.Length, '0');
            newDigitPart = stringNo.Substring(0, position+1) + newDigitPart;
            return newDigitPart;
        }
    }
}
