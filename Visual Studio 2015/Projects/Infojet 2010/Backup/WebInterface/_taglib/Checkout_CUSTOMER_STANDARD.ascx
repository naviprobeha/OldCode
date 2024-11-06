<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Checkout_CUSTOMER_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Checkout_CUSTOMER_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<div class="orderHistory">

<div class="pane">
    <div style="width: 400px">
        <asp:label ID="searchNameLabel" runat="server" AssociatedControlID="searchNameBox" Font-Size="11px"><Infojet:Translate runat="server" ID="searchName" code="SEARCH NAME" /></asp:label> <asp:TextBox ID="searchNameBox" runat="server" CssClass="Textfield" Width="200px"></asp:TextBox> <asp:Button ID="searchButton" runat="server" CssClass="Button" />
   </div>          
</div>

</div>
<br />&nbsp;<br />


<table class="tableView" style="width: 100%">
    <tr>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="customerNo" code="CUSTOMER NO"/></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="name" code="NAME"/></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="address" code="ADDRESS"/></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="city" code="CITY"/></th>            
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="contact" code="CONTACT NAME"/></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="phoneNo" code="PHONE NO"/></th>
        <th class="itemListHeader">&nbsp;</th>
    </tr>


    <asp:Repeater runat="server" ID="customerRepeater" OnItemCommand="customerList_ItemCommand">
    <ItemTemplate>
        <tr>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "no")%>&nbsp;</td>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "name")%>&nbsp;</td>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "address")%>&nbsp;</td>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "city")%>&nbsp;</td>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "contactName")%>&nbsp;</td>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "phoneNo")%>&nbsp;</td>
            <td class="itemListAltLine" align="center"><asp:LinkButton ID="chooseCustomer" runat="server" CommandName="choose" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "no")%>'><Infojet:Translate ID="update" runat="server" code="CHOOSE" /></asp:LinkButton></td>
        </tr>
    </ItemTemplate>

    <AlternatingItemTemplate>
        <tr>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "no")%>&nbsp;</td>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "name")%>&nbsp;</td>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "address")%>&nbsp;</td>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "city")%>&nbsp;</td>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "contactName")%>&nbsp;</td>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "phoneNo")%>&nbsp;</td>
            <td class="itemListLine" align="center"><asp:LinkButton ID="chooseCustomer" runat="server" CommandName="choose" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "no")%>'><Infojet:Translate ID="update" runat="server" code="CHOOSE" /></asp:LinkButton></td>
        </tr>
    </AlternatingItemTemplate>    
    
    </asp:Repeater>

</table>
