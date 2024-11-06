<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_REORDER.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.UserDefined_REORDER" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<asp:Label runat="server" ID="errorMessageLabel" ForeColor="Red"></asp:Label>


<script type="text/javascript">

function submitReOrderSetup()
{

    if (getCheckedValue(document.pageForm.allowReOrder) == "")
    {
        alert("<%= infojet.translate("REORDER MSG") %>");
        return;
    }
    
    document.pageForm.action = "<%= sendUrl %>";
    document.pageForm.submit();

}

function getCheckedValue(radioObj) {
	if(!radioObj)
		return "";
	var radioLength = radioObj.length;
	if(radioLength == undefined)
		if(radioObj.checked)
			return radioObj.value;
		else
			return "";
	for(var i = 0; i < radioLength; i++) {
		if(radioObj[i].checked) {
			return radioObj[i].value;
		}
	}
	return "";
}


</script>


<table class="tableView" style="width: 100%">
<tr>
    <td align="center" style="width: 20px;"><input type="radio" name="allowReOrder" value="1" /></td>
    <td><b><Infojet:Translate ID="yes" runat="server" code="YES" /></b></td>
</tr>
<tr>
    <td align="center" style="width: 20px;"><input type="radio" name="allowReOrder" value="0" /></td>
    <td><b><Infojet:Translate ID="no" runat="server" code="NO" /></b></td>
</tr>
</table>


<div style="float:left; width: 100%;">
<div style="float:left; text-align: left; padding-top: 5px; width: 50%;"><% if (prevWebPageUrl != "") { %><a href="<%= prevWebPageUrl %>"><img src="_assets/img/<%= infojet.translate("IMG BACK BTN") %>" alt="<%= infojet.translate("BACK") %>" /></a><% } %></div>
<div style="float:left; text-align: right; padding-top: 5px; width: 50%; color: #ef1c22;"><% if (nextWebPageUrl != "")
                                                                                             { %><a href="javascript:submitReOrderSetup()"><img src="_assets/img/<%= infojet.translate("IMG NEXT BTN") %>" alt="<%= infojet.translate("NEXT") %>" /></a><% } %></div>
</div>