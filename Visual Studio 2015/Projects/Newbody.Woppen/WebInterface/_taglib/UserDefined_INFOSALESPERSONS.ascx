<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_INFOSALESPERSONS.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.UserDefined_INFOSALESPERSONS" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<asp:Label runat="server" ID="errorMessageLabel" ForeColor="Red"></asp:Label>
<br />


<table class="tableView" style="width: 100%">
    <tr>
        <th><Infojet:Translate runat="server" ID="groupName" code="SALESPERSON"/></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="Translate1" code="SOLD PACKAGES"/></th>
    </tr>

<asp:Repeater runat="server" ID="salesIdRepeater">

<ItemTemplate>
    <tr>
        <td align="left"><b><%#DataBinder.Eval(Container.DataItem, "description")%></b></td>
        <td align="right">&nbsp;</td>
    </tr>


    <asp:Repeater runat="server" ID="salesPersonRepeater" datasource='<%#DataBinder.Eval(Container.DataItem, "activeSalesPersons")%>'>

    <ItemTemplate>
        <tr>
            <td><%#DataBinder.Eval(Container.DataItem, "name")%></td>
            <td align="right"><%#DataBinder.Eval(Container.DataItem, "historyPackages")%></td>
        </tr>
    </ItemTemplate>


    
    </asp:Repeater>
    
</ItemTemplate>

</asp:Repeater>

</table>

   
