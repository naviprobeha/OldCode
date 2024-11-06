<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Campain_NORMAL.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Campain_NORMAL" %>

<%

    Navipro.Infojet.Lib.WebItemCampain webItemCampain = new Navipro.Infojet.Lib.WebItemCampain(infojet, webPageLine.code);
    Navipro.Infojet.Lib.ProductItemCollection campainItems = webItemCampain.getItemCampains(infojet, webPageLine);
    campainItems.setSize(120, 120);
    campainRepeater.DataSource = campainItems;
    campainRepeater.DataBind();


%>

<div class="campainSet">
<asp:Repeater ID="campainRepeater" runat="server" EnableTheming="True">
   
   <ItemTemplate>
        <div style="float:left; width: 257px; height: 210px; border: solid 1px #dedfde; margin-top: 5px;margin-right: 4px;">
            <div style="float: left; height: 190px;">
                <div style="float: left; font-size: 11px; padding: 2px; width: 120px;"><h3><%#DataBinder.Eval(Container.DataItem, "no")%> <%#DataBinder.Eval(Container.DataItem, "description")%></h3>
                    <%#DataBinder.Eval(Container.DataItem, "extendedText")%><br /><br />
                    <%#DataBinder.Eval(Container.DataItem, "inventoryText")%><br />                    
                    <br /><span style="color: Red; font-size: 14px; font-weight: bold;"><%#DataBinder.Eval(Container.DataItem, "formatedUnitPrice")%></span>
                </div>         
                <div style="float: right; text-align: right;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><img src="<%#DataBinder.Eval(Container.DataItem, "productImageUrl")%>" alt="<%#DataBinder.Eval(Container.DataItem, "productImageUrl")%>" /></a></div>            
            </div>
            <div style="float:left; width: 257px; height: 20px; background-color: #f7f7f7;">
                <div style="float: left; font-size: 11px; padding-left: 2px;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><%= infojet.translate("READ MORE") %></a> <% if (infojet.webSite.allowPurchaseNotLoggedIn)
                                                                                                                                                                                      { %>| <a href="<%#DataBinder.Eval(Container.DataItem, "buyLink") %>"><%= infojet.translate("BUY")%></a><% } %></div>
            </div>
        </div>
   </ItemTemplate>
</asp:Repeater>
</div>
