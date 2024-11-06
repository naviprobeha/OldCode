<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_SALESIDSALESPERSON.ascx.cs" Inherits="WebInterface._taglib.UserDefined_SALESIDSALESPERSON" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<script type="text/javascript">

function confirmNext()
{
    <% if (allReleased) { %> document.location.href = "<%= nextWebPageUrl %>"; <% } %>
    <% if (!allReleased) { %> 
    
    //if (confirm("<%= infojet.translate("NOT RELEASED") %> <%= infojet.translate("CONTINUE") %>"))
    if (confirm("<%= infojet.translate("BEGIN ORDER 1") %> <%= countReleased %> <%= infojet.translate("BEGIN ORDER 2") %>"))
    {
        document.location.href = "<%= nextWebPageUrl %>";
    }
    
    <% } %>
}

function addNewSalesPerson()
{

    document.pageForm.command.value="addNewSP";
    document.pageForm.submit();    
}

</script>

<input type="hidden" name="command" value="" />
<div style="float:left; width: 100%;">
<div style="float:left; text-align: left; padding-bottom: 5px; width: 50%;">&nbsp;</div>
<div style="float:left; text-align: right; padding-bottom: 5px; width: 50%; color: #ef1c22;">
<% if (!allSalesPersonsRegistered)
   { 
        %><a href="javascript:addNewSalesPerson()" title="<%= infojet.translate("ADD NEW SP TEXT") %>"><img src="_assets/img/<%= infojet.translate("ADD NEW SP BTN") %>" alt="" /></a><%
   
   } %>
<% if (nextWebPageUrl != "")
                                                                                             { %>&nbsp;&nbsp;<a href="javascript:confirmNext()" title="<%= infojet.translate(nextWebPageText) %>"><img src="_assets/img/<%= infojet.translate(nextWebPageBtn) %>" alt="" /></a><% } %>
</div>
</div>
<asp:Repeater runat="server" ID="salesPersonRepeater">
<HeaderTemplate>
    <table class="tableView" style="width: 100%">
        <tr>
            <th><Infojet:Translate runat="server" ID="groupName" code="SALESPERSON"/></th>
            <th style="text-align: right;"><Infojet:Translate runat="server" ID="Translate1" code="SOLD PACKAGES"/></th>
            <th style="text-align: right;"><Infojet:Translate runat="server" ID="Translate2" code="STATUS"/></th>
            <th>&nbsp;</th>
        </tr>
</HeaderTemplate>

<ItemTemplate>
    <tr>
        <td><%#DataBinder.Eval(Container.DataItem, "name")%></td>
        <td align="right"><%#DataBinder.Eval(Container.DataItem, "soldPackages")%></td>
        <td align="right"><%#DataBinder.Eval(Container.DataItem, "released")%></td>
        <td align="center"><a href="<%#DataBinder.Eval(Container.DataItem, "pageUrl")%>" style="font-size: 10px; color: #ef1c22;"><Infojet:Translate runat="server" ID="Translate4" code="SHOW"/> / <Infojet:Translate runat="server" ID="Translate3" code="CHANGE"/></a></td>
    </tr>
</ItemTemplate>

<AlternatingItemTemplate>
    <tr>
        <td style="background-color: #ffffff;"><%#DataBinder.Eval(Container.DataItem, "name")%></td>
        <td style="background-color: #ffffff;" align="right"><%#DataBinder.Eval(Container.DataItem, "soldPackages")%></td>
        <td style="background-color: #ffffff;" align="right"><%#DataBinder.Eval(Container.DataItem, "released")%></td>
        <td style="background-color: #ffffff;" align="center"><a href="<%#DataBinder.Eval(Container.DataItem, "pageUrl")%>" style="font-size: 10px; color: #ef1c22;"><Infojet:Translate runat="server" ID="Translate4" code="SHOW"/> / <Infojet:Translate runat="server" ID="Translate3" code="CHANGE"/></a></td>
    </tr>

</AlternatingItemTemplate>

<FooterTemplate>
    </table>
</FooterTemplate>


</asp:Repeater>

