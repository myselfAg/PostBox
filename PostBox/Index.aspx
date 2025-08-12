 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PostBox.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Index.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link
      href="https://fonts.googleapis.com/css2?family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=WDXL+Lubrifont+TC&display=swap"
      rel="stylesheet" />

</head>
<body>
    <div class="header">
      <asp:Image ID="logo" runat="server" ImageUrl="~/UploadedImages/logo.png" AlternateText="postbox" />

      <ul id="pages">
        <li>
            <asp:HyperLink ID="home" runat="server" CssClass="links">Home</asp:HyperLink></li>
        <li id="login-button"> 
            <asp:HyperLink ID="login" runat="server" CssClass="links" NavigateUrl="~/Login.aspx" >Login</asp:HyperLink></li>
      </ul>
    </div>
    <div class="main">
      <div class="left">
        <div class="img-part">
            <asp:Image ID="bgImg" CssClass="img-part" runat="server" ImageUrl="~/UploadedImages/home.jpg" AlternateText="cover" />
        </div>
      </div>
      <div class="right">
        <h1>POSTBOX</h1>
        <h3>Mail Application</h3>
        <p>
          Stay connected, organized, and secure with our modern Mail App — your
          all-in-one email solution. Designed for speed and simplicity, it lets
          you manage multiple accounts, categorize messages, schedule emails,
          and receive real-time notifications with ease.
        </p>
          <form runat="server">
            <asp:Button ID="signUp" CssClass="signUp" runat="server" Text="Sign Up" OnClick="signUp_Click" />
          </form>
      </div>
    </div>
</body>
</html>
