<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_SENDORDERS_SALESID.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.UserDefined_SENDORDERS_SALESID" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<br />
<script type="text/javascript">

function submitSalesIds()
{
    document.pageForm.action = "<%= sendUrl %>";
    document.pageForm.submit();

}

</script>


<table class="tableView" style="width: 100%">
    <tr>
        <th><Infojet:Translate runat="server" ID="groupName" code="GROUP"/></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="Translate1" code="SOLD PACKAGES"/></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="Translate2" code="SHOWCASE"/></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="Translate3" code="RETURN PACKAGES"/></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="Translate4" code="ORDERS TO SEND"/></th>
        <th style="text-align: center;"><Infojet:Translate runat="server" ID="Translate5" code="COMBINE"/></th>
    </tr>

<asp:Repeater runat="server" ID="releasedSalesIdRepeater">
<ItemTemplate>
    <tr>
        <td><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td align="right"><%#DataBinder.Eval(Container.DataItem, "soldPackages")%></td>
        <td align="right"><%#DataBinder.Eval(Container.DataItem, "showCasePackages")%></td>
        <td align="right"><%#DataBinder.Eval(Container.DataItem, "returnPackages")%></td>
        <td align="right"><%#DataBinder.Eval(Container.DataItem, "packagesToSend")%></td>
        <td align="center"><input type="checkbox" name="<%#DataBinder.Eval(Container.DataItem, "code")%>" <%#DataBinder.Eval(Container.DataItem, "selected")%>/></td>
    </tr>
</ItemTemplate>
</asp:Repeater>

<asp:Repeater runat="server" ID="openSalesIdRepeater">
<ItemTemplate>
    <tr>
        <td><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td align="right"><%#DataBinder.Eval(Container.DataItem, "soldPackages")%></td>
        <td align="right"><%#DataBinder.Eval(Container.DataItem, "showCasePackages")%></td>
        <td align="right"><%#DataBinder.Eval(Container.DataItem, "returnPackages")%></td>
        <td align="right"><%#DataBinder.Eval(Container.DataItem, "packagesToSend")%></td>
        <td align="center" style="color: Red;"><%#DataBinder.Eval(Container.DataItem, "salesConcept")%></td>
    </tr>
</ItemTemplate>
</asp:Repeater>

</table>

<div style="float:left; width: 100%;">
<div style="float:left; text-align: left; padding-top: 5px; width: 50%;"><% if (prevWebPageUrl != "") { %><a href="<%= prevWebPageUrl %>"><img src="_assets/img/<%= infojet.translate("IMG BACK BTN") %>" alt="<%= infojet.translate("BACK") %>" /></a><% } %></div>
<div style="float:left; text-align: right; padding-top: 5px; width: 50%; color: #ef1c22;"><% if (sendUrl != "")
                                                                                             { %><a href="javascript:submitSalesIds()"><img src="_assets/img/<%= infojet.translate("IMG NEXT BTN") %>" alt="<%= infojet.translate("NEXT") %>" /></a><% } %></div>
</div>