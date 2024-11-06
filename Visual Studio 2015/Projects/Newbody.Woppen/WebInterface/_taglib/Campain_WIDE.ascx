<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Campain_WIDE.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Campain_WIDE" %>
<asp:Repeater ID="campainRepeater" runat="server">
   <ItemTemplate>
        <div style="float:left; width: 787px; border: solid 1px #dedfde;">
            <div style="float: left">
                <div style="float: left; font-size: 11px; padding: 2px; width: 496px;"><h3><%#DataBinder.Eval(Container.DataItem, "no")%> <%#DataBinder.Eval(Container.DataItem, "description")%></h3>
                    <%#DataBinder.Eval(Container.DataItem, "extendedText")%><br /><br />
                    <%#DataBinder.Eval(Container.DataItem, "inventoryText")%><br />                    
                    <br /><span style="color: Red; font-size: 14px; font-weight: bold;"><%#DataBinder.Eval(Container.DataItem, "formatedUnitListPrice")%></span>
                </div>         
                <div style="float: right; text-align: right;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><img src="<%#DataBinder.Eval(Container.DataItem, "productImageUrl")%>" alt="<%#DataBinder.Eval(Container.DataItem, "productImageUrl")%>" /></a></div>            
            </div>
            <div style="float:left; width: 784px; height: 20px; background-color: #f7f7f7;">
                <div style="float: left; font-size: 11px; padding-left: 2px;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><%= infojet.translate("READ MORE") %></a> | <a href="<%#DataBinder.Eval(Container.DataItem, "buyLink") %>"><%= infojet.translate("BUY") %></a></div>
            </div>
        </div>
   </ItemTemplate>
</asp:Repeater>
