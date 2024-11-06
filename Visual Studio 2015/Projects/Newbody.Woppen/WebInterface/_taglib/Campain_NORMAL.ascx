<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Campain_NORMAL.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Campain_NORMAL" %>

<asp:Repeater ID="campainRepeater" runat="server" EnableTheming="True">
   
   <ItemTemplate>
        <div style="float:left; width: 257px; height: 210px; border: solid 1px #dedfde; margin-top: 5px;margin-right: 4px;">
            <div style="float: left; height: 190px;">
                <div style="float: left; font-size: 11px; padding: 2px; width: 120px;"><h3><%#DataBinder.Eval(Container.DataItem, "no")%> <%#DataBinder.Eval(Container.DataItem, "description")%></h3>
                    <%#DataBinder.Eval(Container.DataItem, "extendedText")%><br /><br />
                    <%#DataBinder.Eval(Container.DataItem, "inventoryText")%><br />                    
                    <br /><span style="color: Red; font-size: 14px; font-weight: bold;"><%#DataBinder.Eval(Container.DataItem, "formatedUnitListPrice")%></span>
                </div>         
                <div style="float: right; text-align: right;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><img src="<%#DataBinder.Eval(Container.DataItem, "productImageUrl")%>" alt="<%#DataBinder.Eval(Container.DataItem, "productImageUrl")%>" /></a></div>            
            </div>
            <div style="float:left; width: 257px; height: 20px; background-color: #f7f7f7;">
                <div style="float: left; font-size: 11px; padding-left: 2px;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><%= infojet.translate("READ MORE") %></a> | <a href="<%#DataBinder.Eval(Container.DataItem, "buyLink") %>"><%= infojet.translate("BUY") %></a></div>
            </div>
        </div>
   </ItemTemplate>
</asp:Repeater>
