<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_TOPLIST.ascx.cs" Inherits="WebInterface._taglib.UserDefined_TOPLIST" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<br />

<div class="inputForm" style="margin-right: 2px;">

<div class="pane">
    <div style="width: 100%;">
        <table width="200">
            <tr>
                <td><Infojet:Translate runat="server" ID="Translate5" code="SOLD PACKAGES"/>:</td>
                <td align="right"><%= salesId.soldPackages %></td>
            </tr>
            <tr>
                <td><Infojet:Translate runat="server" ID="Translate6" code="PROFIT"/>:</td>
                <td align="right"><%= infojet.systemDatabase.formatCurrency(salesId.profit) %></td>
            </tr>
        </table>
    </div>
</div>
<br />&nbsp;<br />
</div>

<table class="tableView" style="width: 100%">
    <tr>
        <th style="text-align: right; width: 30px;"><Infojet:Translate runat="server" ID="rank" code="RANK"/></th>
        <th><Infojet:Translate runat="server" ID="Translate1" code="NAME"/></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="Translate2" code="SOLD PACKAGES"/></th>
    </tr>

<asp:Repeater runat="server" ID="salesPersonRepeater">
<ItemTemplate>
    <tr>
        <td align="right"><%#DataBinder.Eval(Container.DataItem, "rank")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "name")%></td>
        <td align="right"><%#DataBinder.Eval(Container.DataItem, "soldPackages")%></td>
    </tr>
</ItemTemplate>

</asp:Repeater>

</table>
