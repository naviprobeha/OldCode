<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Newsletter_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Newsletter_STANDARD" %>

<script runat="server">

public void Page_PreRender(object sender, System.EventArgs e)
{
    emailAddressBox.Text = infojet.translate("ENTER EMAIL ADDRESS");
    emailAddressBox.Attributes.Add("onfocus", "this.value=''");
}

public void registerBtn_Click(object sender, EventArgs e)
{
    Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

    Navipro.Infojet.Lib.NewsletterAddress.addAddress(infojet, emailAddressBox.Text);

}

</script>

<div class="userControl">
    <div class="registerNewsletter">
        <asp:TextBox ID="emailAddressBox" runat="server" CssClass="Textfield"></asp:TextBox>&nbsp;<asp:Button ID="registerBtn" runat="server" CssClass="Button" onclick="registerBtn_Click"/>
    </div>
</div>