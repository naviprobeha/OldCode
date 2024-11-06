<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_SALESIDUSERS.ascx.cs" Inherits="WebInterface._taglib.UserDefined_SALESIDUSERS" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<br />
<div class="inputForm" style="margin-right: 2px;">

<div class="pane">
    <div style="width: 100%;">
        <table>
            <tr>
                <td style="width: 100px"><label><Infojet:Translate runat="server" ID="Translate8" code="SEARCH CUSTOMER NO" />:</label></td>
                <td style="width: 400px"><asp:TextBox runat="server" ID="searchCustomerNo" CssClass="Textfield" Width="100px"></asp:TextBox>&nbsp;<asp:Button runat="server" ID="searchButton" CssClass="Button" /></td>
            </tr>
        </table>        
    </div>
</div>
<br />&nbsp;<br />
</div>



<table class="tableView" style="width: 100%">
    <tr>
        <th><Infojet:Translate runat="server" ID="groupId" code="SALESID"/></th>
        <th><Infojet:Translate runat="server" ID="Translate6" code="DESCRIPTION"/></th>
        <th><Infojet:Translate runat="server" ID="Translate5" code="CUSTOMER NO"/></th>
        <th style="text-align: center;"><Infojet:Translate runat="server" ID="Translate4" code="ORDERS TO SEND"/></th>
        <th><Infojet:Translate runat="server" ID="Translate1" code="CONTACT PERSON"/></th>
        <th><Infojet:Translate runat="server" ID="Translate2" code="USER ID"/></th>
        <th><Infojet:Translate runat="server" ID="Translate3" code="PASSWORD"/></th>
        <th>&nbsp;</th>
    </tr>

<asp:Repeater runat="server" ID="salesIdRepeater">

<ItemTemplate>
    <tr>
        <td><%#DataBinder.Eval(Container.DataItem, "code")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "customerNo")%></td>
        <td align="center"><div style="width: 50px; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "packagesToSend")%></div></td>
        <td><%#DataBinder.Eval(Container.DataItem, "contactName")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "contactUserId")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "contactPassword")%></td>
        <td align="center"><a href="<%= infojet.webPage.getUrl() %>&scommand=login&userNo=<%#DataBinder.Eval(Container.DataItem, "contactWebUserAccountNo")%>" style="font-size: 10px; color: #ef1c22;" target="_blank"><Infojet:Translate runat="server" ID="Translate4" code="LOGIN"/></a></td>
    </tr>
</ItemTemplate>

</asp:Repeater>

</table>


