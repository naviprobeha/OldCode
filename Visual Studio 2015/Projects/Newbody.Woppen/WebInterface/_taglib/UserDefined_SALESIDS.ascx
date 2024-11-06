<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_SALESIDS.ascx.cs" Inherits="WebInterface._taglib.UserDefined_SALESIDS" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<br />
<table class="tableView" style="width: 100%">
    <tr>
        <th title="<%= infojet.translate("GROUP TEXT") %>"><Infojet:Translate runat="server" ID="groupName" code="GROUP"/></th>
        <th style="text-align: center;" title="<%= infojet.translate("SOLD PACKAGES TEXT") %>"><Infojet:Translate runat="server" ID="Translate1" code="SOLD PACKAGES"/></th>
        <th style="text-align: center;" title="<%= infojet.translate("SHOWCASE TEXT") %>"><Infojet:Translate runat="server" ID="Translate2" code="SHOWCASE"/></th>
        <th style="text-align: center;" title="<%= infojet.translate("RETURN PACKAGES TEXT") %>"><Infojet:Translate runat="server" ID="Translate3" code="RETURN PACKAGES"/></th>
        <th style="text-align: center;" title="<%= infojet.translate("ORDERS TEXT") %>"><Infojet:Translate runat="server" ID="Translate4" code="ORDERS TO SEND"/></th>
        <th style="text-align: center;" title="<%= infojet.translate("PROFIT TEXT") %>"><Infojet:Translate runat="server" ID="Translate5" code="PROFIT"/></th>
        <th>&nbsp;</th>
    </tr>

<asp:Repeater runat="server" ID="salesIdRepeater">

<ItemTemplate>
    <tr>
        <td><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td align="center"><div style="width: 50px; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "soldPackages")%></div></td>
        <td align="center"><div style="width: 50px; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "showCasePackages")%></div></td>
        <td align="center"><div style="width: 50px; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "returnPackages")%></div></td>
        <td align="center"><div style="width: 50px; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "packagesToSend")%></div></td>
        <td align="center"><div style="width: 50px; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "profit", "{0:n2}")%></div></td>
        <td align="center"><a href="<%#DataBinder.Eval(Container.DataItem, "pageUrl")%>" style="font-size: 10px; color: #ef1c22;"><Infojet:Translate runat="server" ID="Translate4" code="COMBINE AND SEND"/></a></td>
    </tr>
</ItemTemplate>

</asp:Repeater>

</table>

<div style="float:left; width: 100%;">
<div style="float:left; text-align: left; padding-top: 5px; width: 50%;">&nbsp;</div>
<div style="float:left; text-align: right; padding-top: 5px; width: 50%; color: #ef1c22;"><% if (sendOrderLink != "")
                                                                                             { %><a href="<%= sendOrderLink %>" title="<%= infojet.translate("SEND ORDER TEXT") %>"><img src="_assets/img/<%= infojet.translate("IMG BIG ORDER BTN") %>" alt=""/></a><% } %></div>
</div>
