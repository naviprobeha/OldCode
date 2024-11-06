using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace Navipro.Infojet.Lib
{
    public class EditorManagerControl
    {
        private string controlId;
        
        private Infojet infojetContext;
        private WebPageLine webPageLine;

        public EditorManagerControl(Infojet infojetContext, WebPageLine webPageLine)
        {
            this.infojetContext = infojetContext;
            this.webPageLine = webPageLine;
            this.controlId = webPageLine.lineNo.ToString();

        }


        public void Render(StringWriter output)
        {
          
            output.WriteLine("<script type=\"text/javascript\">");
            output.WriteLine(" var mouseX = 0;");
            output.WriteLine(" var mouseY = 0;");

            output.WriteLine(" function getMouseXYPos" + this.controlId + "(e) { if (!e) var e = window.event; mouseX = e.clientX + document.body.scrollLeft; mouseY = e.clientY + document.body.scrollTop; return true; } ");
            output.WriteLine(" window.document.onmousemove = getMouseXYPos" + this.controlId + ";");


            output.WriteLine(" function showManager"+this.controlId+"(editorId) {");
            output.WriteLine(" obj = document.getElementById(editorId);");
            output.WriteLine(" obj.style.top = mouseY;");
            output.WriteLine(" obj.style.left = mouseX-40;");
            output.WriteLine(" obj.style.visibility = \"visible\";");
            output.WriteLine(" }");

            output.WriteLine(" function hideManager" + this.controlId + "(editorId) {");
            output.WriteLine(" obj = document.getElementById(editorId);");
            output.WriteLine(" obj.style.top = 0;");
            output.WriteLine(" obj.style.left = 0;");
            output.WriteLine(" obj.style.visibility = \"hidden\";");
            output.WriteLine(" }");

            output.WriteLine(" function refreshPage" + this.controlId + "() {");
            output.WriteLine(" document.location.href = '"+infojetContext.webPage.getUrl()+"';");
            output.WriteLine(" }");

            output.WriteLine(" function closeDesignMode" + this.controlId + "() {");
            output.WriteLine(" document.location.href = '" + infojetContext.webPage.getUrl() + "&contentCommand=closeDesignMode';");
            output.WriteLine(" }");

            output.WriteLine(" function openEditWindow" + this.controlId + "() { window.open(\"" + infojetContext.webPage.getUrl() + "&contentCommand=edit&lineNo=" + webPageLine.lineNo + "\",\"Redigera\",\"left=100, top=30, width=800, height=800, toolbar=no, location=no, scrollbars=yes, resizable=yes\"); } ");

            output.WriteLine("</script>");

            output.WriteLine("<div id=\"content_" + this.controlId + "\">");
            output.WriteLine("<div style=\"float: right; align: right;\" onclick=\"showManager" + this.controlId + "('content_" + this.controlId + "_manager');\"><a href=\"#\"><img src=\""+infojetContext.webSite.location+"/_assets/img/cms_arrow.gif\" alt=\"\"/></a></div>");
            output.WriteLine(webPageLine.getText());
            output.WriteLine("</div>");

            output.WriteLine("<table cellspacing=\"1\" cellpadding=\"2\" id=\"content_" + this.controlId + "_manager\" style=\"border: 1px solid #777777; position: absolute; visibility: hidden;\" onmouseout=\"setTimeout(function() { hideManager" + this.controlId + "('content_" + this.controlId + "_manager') }, 3000);\">");
            output.WriteLine("<tr><td style=\"font-size: 11px; background-color: #ffffff;\"><a href=\"#\" onclick=\"openEditWindow" + this.controlId + "()\" style=\"color: #000000; text-decoration: none;\">&nbsp;Redigera&nbsp;</a></td></tr>");
            output.WriteLine("<tr><td style=\"font-size: 11px; background-color: #ffffff;\"><a href=\"#\" onclick=\"closeDesignMode" + this.controlId + "()\" style=\"color: #000000; text-decoration: none;\">&nbsp;Stäng&nbsp;</a></td></tr>");
            output.WriteLine("</table>");
        }
    }
}
