using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicDrawingLib
{
    public class Interpeter
    {
        /*

            public void evaluateExpression(string expression)
            {
            }

            public void evaluateExpr(string expression)
            {

                if (expression.IndexOf("(") > 0)
                {
		
                    while (expression.IndexOf("(") > 0)
                    {

                        int startPos = expression.IndexOf("(");

                        bool found = false;
                        int currentPos = startPos;
                        int noOfSubExp = 1;
				
                        while(!found)
                        {
					
                            currentPos = currentPos + 1;
                            if (expression.Substring(currentPos, 1) == "(") noOfSubExp = noOfSubExp + 1;
                            if (expression.Substring(currentPos, 1) == ")") noOfSubExp = noOfSubExp - 1;

                            if (noOfSubExp = 0)
                            found = true
                            endPos = currentPos
                        End If
                        }

                    subExpr = mid(expression, startPos + 1, endPos - startPos - 1)
                    'Response.Write("Expr 2: " & subExpr & "<br>")

                    subExpr = evaluateExp(subExpr)

                    expression = mid(expression, 1, startPos - 1) & subExpr & mid(expression, endPos + 1)

                    'Response.Write("Total: " & expression & "<br>")
                Loop

            End if

            'Response.Write("Pre: " & expression & "<br>")
	
            If InStr(expression, "=") > 0 Then

                Do While InStr(expression, "=") > 0

                    op1 = mid(expression, InStr(expression, "[")+1)
                    op1 = mid(op1, 1, InStr(op1, "]")-1)
                    operator = mid(expression, InStr(expression, "]")+1)
                    operator = mid(operator, 1, InStr(operator, "[")-1)
                    op2 = mid(expression, InStr(expression, operator))
                    op2 = mid(op2, Instr(op2, "[")+1)
                    expressionEnd = op2
                    op2 = mid(op2, 1, Instr(op2, "]")-1)
                    expressionEnd = mid(expressionEnd, Instr(expressionEnd, "]")+1)
                    expression = mid(expression, 1, InStr(expression, "[")-1)
			
                    'Response.Write("Op1: "& op1 & "<br>")
                    'Response.Write("Operator: "& operator & "<br>")
                    'Response.Write("Op2: "& op2 & "<br>")

                    If op1 = op2 Then 
                        expression = expression & "1" & expressionEnd
                    Else
                        expression = expression & "0" & expressionEnd
                    End If

                    'Response.Write("Diff expr: " & expression & "<br>")

                Loop

            End If


            If InStr(expression, "<>") > 0 Then

                Do While InStr(expression, "<>") > 0

                    op1 = mid(expression, InStr(expression, "[")+1)
                    op1 = mid(op1, 1, InStr(op1, "]")-1)
                    operator = mid(expression, InStr(expression, "]")+1)
                    operator = mid(operator, 1, InStr(operator, "[")-1)
                    op2 = mid(expression, InStr(expression, operator))
                    op2 = mid(op2, Instr(op2, "[")+1)
                    expressionEnd = op2
                    op2 = mid(op2, 1, Instr(op2, "]")-1)
                    expressionEnd = mid(expressionEnd, InStr(expressionEnd, "]")+1)
                    expression = mid(expression, 1, InStr(expression, "[")-1)
			
                    'Response.Write("Op1: "& op1 & "<br>")
                    'Response.Write("Operator: "& operator & "<br>")
                    'Response.Write("Op2: "& op2 & "<br>")
                    'Response.Write("Rest: "& expressionEnd & "<br>")

                    If op1 <> op2 Then 
                        expression = expression & "1" & expressionEnd
                    Else
                        expression = expression & "0" & expressionEnd
                    End If

                Loop

            End If

            If InStr(expression, "<=") > 0 Then

                Do While InStr(expression, "<=") > 0

                    op1 = mid(expression, InStr(expression, "[")+1)
                    op1 = mid(op1, 1, InStr(op1, "]")-1)
                    operator = mid(expression, InStr(expression, "]")+1)
                    operator = mid(operator, 1, InStr(operator, "[")-1)
                    op2 = mid(expression, InStr(expression, operator))
                    op2 = mid(op2, Instr(op2, "[")+1)
                    expressionEnd = op2
                    op2 = mid(op2, 1, Instr(op2, "]")-1)
                    expressionEnd = mid(expressionEnd, Instr(expressionEnd, "]")+1)
                    expression = mid(expression, 1, InStr(expression, "[")-1)
			
                    'Response.Write("Op1: "& op1 & "<br>")
                    'Response.Write("Operator: "& operator & "<br>")
                    'Response.Write("Op2: "& op2 & "<br>")

                    If op1 <= op2 Then 
                        expression = "1" & expression
                    Else
                        expression = "0" & expression
                    End If

                Loop

            End If

            If InStr(expression, ">=") > 0 Then

                Do While InStr(expression, ">=") > 0

                    op1 = mid(expression, InStr(expression, "[")+1)
                    op1 = mid(op1, 1, InStr(op1, "]")-1)
                    operator = mid(expression, InStr(expression, "]")+1)
                    operator = mid(operator, 1, InStr(operator, "[")-1)
                    op2 = mid(expression, InStr(expression, operator))
                    op2 = mid(op2, Instr(op2, "[")+1)
                    expressionEnd = op2
                    op2 = mid(op2, 1, Instr(op2, "]")-1)
                    expressionEnd = mid(expressionEnd, Instr(expressionEnd, "]")+1)
                    expression = mid(expression, 1, InStr(expression, "[")-1)
			
                    'Response.Write("Op1: "& op1 & "<br>")
                    'Response.Write("Operator: "& operator & "<br>")
                    'Response.Write("Op2: "& op2 & "<br>")

                    If op1 >= op2 Then 
                        expression = "1" & expression
                    Else
                        expression = "0" & expression
                    End If

                Loop

            End If

            If InStr(expression, "<") > 0 Then

                Do While InStr(expression, "<") > 0

                    op1 = mid(expression, InStr(expression, "[")+1)
                    op1 = mid(op1, 1, InStr(op1, "]")-1)
                    operator = mid(expression, InStr(expression, "]")+1)
                    operator = mid(operator, 1, InStr(operator, "[")-1)
                    op2 = mid(expression, InStr(expression, operator))
                    op2 = mid(op2, Instr(op2, "[")+1)
                    expressionEnd = op2
                    op2 = mid(op2, 1, Instr(op2, "]")-1)
                    expressionEnd = mid(expressionEnd, Instr(expressionEnd, "]")+1)
                    expression = mid(expression, 1, InStr(expression, "[")-1)
			
                    'Response.Write("Op1: "& op1 & "<br>")
                    'Response.Write("Operator: "& operator & "<br>")
                    'Response.Write("Op2: "& op2 & "<br>")

                    If op1 < op2 Then 
                        expression = "1" & expression
                    Else
                        expression = "0" & expression
                    End If

                Loop

            End If

            If InStr(expression, ">") > 0 Then

                Do While InStr(expression, ">") > 0

                    op1 = mid(expression, InStr(expression, "[")+1)
                    op1 = mid(op1, 1, InStr(op1, "]")-1)
                    operator = mid(expression, InStr(expression, "]")+1)
                    operator = mid(operator, 1, InStr(operator, "[")-1)
                    op2 = mid(expression, InStr(expression, operator))
                    op2 = mid(op2, Instr(op2, "[")+1)
                    expressionEnd = op2
                    op2 = mid(op2, 1, Instr(op2, "]")-1)
                    expressionEnd = mid(expressionEnd, Instr(expressionEnd, "]")+1)
                    expression = mid(expression, 1, InStr(expression, "[")-1)
			
                    'Response.Write("Op1: "& op1 & "<br>")
                    'Response.Write("Operator: "& operator & "<br>")
                    'Response.Write("Op2: "& op2 & "<br>")

                    If op1 > op2 Then 
                        expression = "1" & expression
                    Else
                        expression = "0" & expression
                    End If

                Loop

            End If


            'Response.Write("Almost done: " & expression & "<br>")

            If InStr(expression, " ") > 0 Then

                Do While InStr(expression, " ") > 0

                    'Response.Write(expression & "<br>")

                    op1 = mid(expression, 1, InStr(expression, " ")-1)
                    operator = mid(expression, InStr(expression, " ")+1)
                    operator = mid(operator, 1, InStr(operator, " ")-1)
                    expression = mid(expression, InStr(expression, operator))		
                    op2 = mid(expression, InStr(expression, " ")+1)
                    expression = op2
                    If InStr(op2, " ") > 0 Then 
                        op2 = mid(op2, 1, InStr(op2, " ")-1)
                        expression = mid(expression, InStr(expression, " ")+1)
                    Else
                        expression = ""
                    End If
			
                    'Response.Write("Op1: "& op1 & "<br>")
                    'Response.Write("Operator: "& operator & "<br>")
                    'Response.Write("Op2: "& op2 & "<br>")

                    result = "0"

                    If (operator = "AND") AND ((op1 = "1") AND (op2 = "1")) Then result = "1"
                    If (operator = "OR") AND ((op1 = "1") OR (op2 = "1")) Then result = "1"
				
                    if expression <> "" then
                        expression = result & " " & expression
                    else
                        expression = result
                    end if
				
                Loop

            End If


            'Response.Write("Done: " & expression & "<br>")

            evaluateExp = expression

        End Function

        */
    }
}
