<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsFlow_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.NewsFlow_STANDARD" %>

<div class="userControl">
<asp:Repeater ID="newsRepeater" runat="server" >
    <ItemTemplate>
        <div>
            <h2><%#DataBinder.Eval(Container.DataItem, "introHeader")%></h2>
            <%#DataBinder.Eval(Container.DataItem, "ingress")%>
        </div>
    </ItemTemplate>

</asp:Repeater>
</div>