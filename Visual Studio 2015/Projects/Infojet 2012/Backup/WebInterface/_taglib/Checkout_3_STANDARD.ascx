<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Checkout_3_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Checkout_3_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />

<style type="text/css"> 
.checkboxes label 
{ 
    display: block; 
    float: left; 
    padding-right: 10px; 
    white-space: nowrap; 
} 
 
.checkboxes input 
{ 
    vertical-align: middle; 
} 
 
.checkboxes label span 
{ 
    vertical-align: middle; 
    font-weight: normal;
    font-size: 10px;
} 
</style> 

<script language="javascript">
    function ValidateChecked(oSrc, args)
    {
        if(document.all["<%=salesTerms.ClientID%>"].checked == false) args.IsValid = false;
    }
</script>


<div class="orderHistory">
<div style="color: red"><%= errorMessage %></div>
<div class="pane" style="width: 100%;">
    <div class="checkboxes" style="width: 100%;"><asp:label id="salesTermLabel" runat="server" AssociatedControlID="salesTerms"><span><Infojet:Translate runat="server" ID="confirmSalesTerms" code="CONFIRM SALES TERMS" /></span> <asp:CheckBox ID="salesTerms" runat="server"/></asp:label></div>
    <div><asp:CustomValidator ID="salesTermsValidator" runat="server" ClientValidationFunction="ValidateChecked" ValidationGroup="salesTerms" ForeColor="Red" ErrorMessage="Hepp"></asp:CustomValidator></div>   
</div>
<br />&nbsp;<br />

</div>
<div style="float:left; width: 100%;"><asp:Button ID="goBackButton" runat="server" CssClass="Button" />&nbsp;<asp:button ID="nextButton" runat="server" CssClass="Button" ValidationGroup="salesTerms" /></div>