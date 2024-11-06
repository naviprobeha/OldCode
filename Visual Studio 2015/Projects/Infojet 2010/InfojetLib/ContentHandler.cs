using System;
using System.IO;
using System.Web;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for SystemHandler.
    /// </summary>
    public class ContentHandler
    {
        private Infojet infojetContext;

        public ContentHandler(Infojet infojetContext)
        {
            //
            // TODO: Add constructor logic here
            //

            this.infojetContext = infojetContext;
        }

        public void handleRequests()
        {
            if (isDesignMode()) setDesignVersion();

            if (System.Web.HttpContext.Current.Request["contentCommand"] != null)
            {
                handleRequest(System.Web.HttpContext.Current.Request["contentCommand"]);
            }

        }

        private void handleRequest(string command)
        {
            if (command == "edit") editContent();
            if (command == "setDesignMode") setDesignMode();
            if (command == "closeDesignMode") closeDesignMode();

        }

  
        private void editContent()
        {            
            StringWriter stringWriter = new StringWriter();

            WebPageLine webPageLine = new WebPageLine(infojetContext, infojetContext.webSite.code, infojetContext.webPage.code, infojetContext.versionNo, int.Parse(System.Web.HttpContext.Current.Request["lineNo"]));

            EditorControl editorControl = new EditorControl(infojetContext, webPageLine);
            if (editorControl.isPostBack()) saveContent(editorControl);

            editorControl.Render(stringWriter);

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "text/html";
            System.Web.HttpContext.Current.Response.Charset = "utf-8";
            System.Web.HttpContext.Current.Response.Write(stringWriter.ToString());
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();

        }

        private void saveContent(EditorControl editorControl)
        {
            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "updateContent", editorControl));
            if (appServerConnection.processRequest())
            {
                infojetContext.versionNo = appServerConnection.serviceResponse.versionNo;
               
            }

        }

        private void setDesignMode()
        {           
            WebDesignSessionTicket webDesignSessionTicket = new WebDesignSessionTicket(infojetContext, infojetContext.webSite.code, System.Web.HttpContext.Current.Request["ticketId"]);
            //if (webDesignSessionTicket.createdDateTime.AddMinutes(15) > DateTime.Now)
            if (webDesignSessionTicket.createdDateTime.Year > 1753)
            {
                webDesignSessionTicket.sessionId = System.Web.HttpContext.Current.Session.SessionID;

                ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "setDesignSessionTicket", webDesignSessionTicket));
                if (appServerConnection.processRequest())
                {
                    // Design Session Ticket Set
                    System.Web.HttpContext.Current.Session["webDesignTicketId"] = webDesignSessionTicket.ticketId;
                    setDesignVersion();
                }

            }
        }

        private void closeDesignMode()
        {
            if (System.Web.HttpContext.Current.Session["webDesignTicketId"] != null)
            {

                WebDesignSessionTicket webDesignSessionTicket = new WebDesignSessionTicket(infojetContext, infojetContext.webSite.code, System.Web.HttpContext.Current.Session["webDesignTicketId"].ToString());
                System.Web.HttpContext.Current.Session["webDesignTicketId"] = null;
                webDesignSessionTicket.sessionId = "";

                ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "setDesignSessionTicket", webDesignSessionTicket));
                if (appServerConnection.processRequest())
                {
                    // Design Session Ticket Removed
                }
            }
            infojetContext.redirect(infojetContext.webPage.getUrl());
        }

        public bool isDesignMode()
        {
            if (System.Web.HttpContext.Current.Session["webDesignTicketId"] != null)
            {
                WebDesignSessionTicket webDesignSessionTicket = new WebDesignSessionTicket(infojetContext, infojetContext.webSite.code, System.Web.HttpContext.Current.Session["webDesignTicketId"].ToString());
                if (webDesignSessionTicket.sessionId == System.Web.HttpContext.Current.Session.SessionID)
                {
                    return true;
                }
            }
            return false;
        }

        public void setDesignVersion()
        {
            infojetContext.versionNo = infojetContext.webPage.workingVersionNo;
        }

    }
}
