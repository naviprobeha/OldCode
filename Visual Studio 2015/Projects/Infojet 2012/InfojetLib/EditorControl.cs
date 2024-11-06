using System;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace Navipro.Infojet.Lib
{
    public class EditorControl : ServiceArgument
    {
        private string controlId;
        
        private Infojet infojetContext;
        private WebPageLine webPageLine;

        public EditorControl(Infojet infojetContext, WebPageLine webPageLine)
        {
            this.infojetContext = infojetContext;
            this.webPageLine = webPageLine;
            this.controlId = webPageLine.lineNo.ToString();
        }

        public bool isPostBack()
        {
            if (System.Web.HttpContext.Current.Request["editor_" + this.controlId + "_mode"] == "edit") return true;
            return false;
        }

        public string getPostedContent()
        {
            return System.Web.HttpContext.Current.Request["editor_" + this.controlId + "_content"];
        }

        public void Render(StringWriter output)
        {
            if (!isPostBack())
            {
                output.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");

                output.WriteLine("<html xmlns=\"http://www.w3.org/1999/xhtml\" >");
                output.WriteLine("<head><title>Redigera</title>");
                output.WriteLine("<link href=\"_assets/css/design.css\" rel=\"stylesheet\" type=\"text/css\" />");
                output.WriteLine("</head><body><form method=\"post\">");

                output.WriteLine("<input type=\"hidden\" name=\"editor_" + this.controlId + "_mode\" value=\"edit\" />");

                output.WriteLine("<div id=\"editor_" + this.controlId + "\" style=\"\">");


                output.WriteLine("<script type=\"text/javascript\" src=\""+infojetContext.webSite.siteLocation+"_assets/editor/jscripts/tiny_mce/tiny_mce.js\"></script>");
                output.WriteLine("<script type=\"text/javascript\">");

                output.WriteLine(" function myCustomInitInstance(inst) { if (inst.editorId != 'mce_fullscreen') inst.execCommand('mceFullScreen'); } ");

                output.WriteLine("tinyMCE.init({ ");
                output.WriteLine("mode : \"textareas\", ");
                output.WriteLine("theme : \"advanced\", ");
                output.WriteLine("language: \"sv\", ");
                //output.WriteLine("editor_selector : \"editor_" + this.controlId + "_content\", ");
                output.WriteLine("init_instance_callback : \"myCustomInitInstance\", ");

                output.WriteLine("plugins : \"safari,spellchecker,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,imagemanager\", ");

                output.WriteLine("theme_advanced_buttons1 : \"save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect\", ");
                output.WriteLine("theme_advanced_buttons2 : \"cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,insertimage,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor\", ");
                output.WriteLine("theme_advanced_buttons3 : \"tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl\", ");
                output.WriteLine("theme_advanced_buttons4 : \"insertlayer,moveforward,movebackward,absolute,|,styleprops,spellchecker,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,blockquote,pagebreak,|,insertfile,insertimage\", ");
                output.WriteLine("theme_advanced_toolbar_location : \"top\", ");
                output.WriteLine("theme_advanced_toolbar_align : \"left\", ");
                output.WriteLine("theme_advanced_statusbar_location : \"bottom\", ");
                output.WriteLine("theme_advanced_resizing : true, ");
                output.WriteLine("extended_valid_elements : \"iframe[src|width|height|name|align]\", ");

                output.WriteLine("content_css : \""+infojetContext.webSite.location+"_assets/css/"+infojetContext.webSite.code+"_visuals.css\" ");

                //output.Write("template_external_list_url : \"js/template_list.js\", ");
                //output.Write("external_link_list_url : \"js/link_list.js\", ");
                //output.Write("external_image_list_url : \"js/image_list.js\", ");
                //output.Write("media_external_list_url : \"js/media_list.js\", ");

                output.WriteLine("});");

                output.WriteLine("</script>");

                output.WriteLine("<textarea name=\"editor_" + this.controlId + "_content\" style=\"width: 100%\">");
                output.WriteLine(webPageLine.getText());
                output.WriteLine("</textarea>");

                output.WriteLine("</div>");
                output.WriteLine("</form></body></html>");

            }
            else
            {
                output.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");

                output.WriteLine("<html xmlns=\"http://www.w3.org/1999/xhtml\" >");
                output.WriteLine("<head><title>Redigera</title>");
                output.WriteLine("<link href=\"_assets/css/design.css\" rel=\"stylesheet\" type=\"text/css\" />");
                output.WriteLine("</head><body><form method=\"post\">");


                output.WriteLine("<script type=\"text/javascript\">");
                output.WriteLine("window.opener.refreshPage" + this.controlId + "();");
                output.WriteLine("window.close();");
                output.WriteLine("</script>");
                output.WriteLine("</form></body></html>");
              
            }
        }


        #region ServiceArgument Members

        public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDoc)
        {
            XmlElement xmlContentElement = xmlDoc.CreateElement("content");

            XmlAttribute webPageCodeAttribute = xmlDoc.CreateAttribute("webPageCode");
            webPageCodeAttribute.Value = webPageLine.webPageCode;
            xmlContentElement.Attributes.Append(webPageCodeAttribute);

            XmlAttribute lineNoAttribute = xmlDoc.CreateAttribute("lineNo");
            lineNoAttribute.Value = webPageLine.lineNo.ToString();
            xmlContentElement.Attributes.Append(lineNoAttribute);

            XmlAttribute versionNoAttribute = xmlDoc.CreateAttribute("versionNo");
            versionNoAttribute.Value = webPageLine.versionNo.ToString();
            xmlContentElement.Attributes.Append(versionNoAttribute);

            XmlAttribute languageCodeAttribute = xmlDoc.CreateAttribute("languageCode");
            languageCodeAttribute.Value = infojetContext.languageCode;
            xmlContentElement.Attributes.Append(languageCodeAttribute);

            string content = this.getPostedContent();
            while (content != "")
            {
                string textLine = "";
                if (content.Length > 200)
                {
                    int i = 200;
                    while ((content[i] != ' ') && (i > 0)) i--;
                    if (i == 0) i = 200;

                    textLine = content.Substring(0, i);
                    content = content.Substring(i);
                }
                else
                {
                    textLine = content;
                    content = "";
                }

                XmlElement xmlLineElement = xmlDoc.CreateElement("line");
                XmlText lineText = xmlDoc.CreateTextNode(textLine);
                xmlLineElement.AppendChild(lineText);

                xmlContentElement.AppendChild(xmlLineElement);

            }
            
            return xmlContentElement;

        }

        #endregion

    }
}
