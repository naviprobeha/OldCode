<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_PROFILEREORDER.ascx.cs" Inherits="WebInterface._taglib.UserDefined_PROFILEREORDER" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<input type="hidden" name="reorderSalesId" value="" />
<input type="hidden" name="reorderCommand" value="" />

<script type="text/javascript">

    function toggleReordering(salesId) {
        document.pageForm.reorderSalesId.value = salesId;
        document.pageForm.reorderCommand.value = "toggle";
        document.pageForm.submit();
    }

</script>

<%
 
    if (salesIdCollection.Count > 0)
    {
        %>
        <b><Infojet:Translate ID="reorderingHeader" runat="server" code="REORDERING" /></b>
        <asp:Label ID="errorMessageLabel" runat="server"></asp:Label>
        <table cellspacing="2" width="100%" class="salesPersonCart">
        <tr>
            <th valign="bottom"><Infojet:Translate runat="server" ID="groupLable" code="GROUP" /></th>
            <th valign="bottom"><Infojet:Translate runat="server" ID="statusLable" code="STATUS" /></th>
            <th valign="bottom">&nbsp;</th>
        </tr>   
        <%

        int i = 0;
        while (i < salesIdCollection.Count)
        {
            %>
            <tr>
                <td><%= salesIdCollection[i].description %></td>
                <td><% if (salesIdCollection[i].additionalOrder) { Response.Write(infojet.translate("REORDER_ACTIVE")); } else { Response.Write(infojet.translate("REORDER_INACTIVE")); } %></td>
                <td align="center"><input type="button" value="<% if (salesIdCollection[i].additionalOrder) { Response.Write(infojet.translate("REORDBTN_INACTIVE")); } else { Response.Write(infojet.translate("REORDBTN_ACTIVE")); } %>" name="<%= salesIdCollection[i].code %>" onclick="toggleReordering('<%= salesIdCollection[i].code %>');" class="Button" /></td>
            </tr>
            <%
            
            i++;
        }
        
        %></table><%
    }
%>

