<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Checkout_PAYMENT_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Checkout_PAYMENT_STANDARD" %>

<% if (paymentModule != null)
   { %>
<iframe width="100%" frameborder="true" src="<%= paymentModule.getUrl() %>" height="600"></iframe>
<% } %>
