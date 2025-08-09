<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PostBox.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Login.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link
        href="https://fonts.googleapis.com/css2?family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=WDXL+Lubrifont+TC&display=swap"
        rel="stylesheet" />
</head>
<body>
    <div class="main">
        <form class="login-box" runat="server">
            <h1>Login</h1>
            <table>
                <tr>
                    <td>
                        <asp:TextBox runat="server" CssClass="text-boxes" ID="user" placeholder="Username"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox runat="server" type="password" CssClass="text-boxes" ID="pass" placeholder="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="invalidPass" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <div id="buttons">
                <asp:Button ID="back" runat="server" Text="Back" CssClass="bt" OnClick="back_Click" />
                <asp:Button ID="login" runat="server" Text="Login" CssClass="bt" OnClick="login_Click" />
            </div>
        </form>
    </div>
</body>
</html>
