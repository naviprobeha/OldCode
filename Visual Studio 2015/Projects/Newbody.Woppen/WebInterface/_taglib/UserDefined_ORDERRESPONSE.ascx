<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_ORDERRESPONSE.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.UserDefined_ORDERRESPONSE" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />

<div class="inputForm" style="margin-right: 2px;">

<div class="pane">
    <div style="width: 100%;">
        <table>
            <tr>
                <td><a target="_blank" href="<%= orderConfirmationPdfUrl %>"><Infojet:Translate runat="server" ID="Translate7" code="GET ORDER CONF" /></a></td>
            </tr>
            <tr>
                <td><a target="_blank" href="<%= pickListPdfUrl %>"><Infojet:Translate runat="server" ID="Translate1" code="GET PICK LIST" /></a></td>
            </tr>
        </table>        
    </div>
</div>

</div>


