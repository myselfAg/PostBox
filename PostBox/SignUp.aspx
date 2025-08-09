<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="PostBox.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="SignUp.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link
        href="https://fonts.googleapis.com/css2?family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=WDXL+Lubrifont+TC&display=swap"
        rel="stylesheet" />
</head>
<body>
    <div class="main">
        <form class="signup-box" runat="server">
            <h1>Sign Up</h1>
            <table>
                <tr>
                    <td>
                        <asp:TextBox runat="server" CssClass="text-boxes" ID="fname" placeholder="First Name"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox runat="server" class="text-boxes" ID="lname" placeholder="Last Name"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox runat="server" CssClass="text-boxes" ID="dob" placeholder="Date of Birth (mm-dd-yyyy)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox runat="server" CssClass="text-boxes" ID="mail" placeholder="@postbox.com"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox runat="server" CssClass="text-boxes" ID="mobNo" placeholder="Mobile No."></asp:TextBox>
                    </td>
                </tr>
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
                        <asp:TextBox runat="server" type="password" CssClass="text-boxes" ID="rePass" placeholder="Confirm Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="passMes" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButton runat="server" ID="male" name="gender" value="male" />
                        <asp:Label ID="maleLabel" runat="server" Text="male" CssClass="sex">Male</asp:Label>

                        <asp:RadioButton runat="server" ID="female" name="gender" value="female" />
                        <asp:Label ID="femaleLabel" runat="server" Text="female" CssClass="sex">Female</asp:Label>

                        <asp:RadioButton runat="server" ID="others" name="gender" value="others" />
                        <asp:Label ID="othersLabel" runat="server" Text="others" CssClass="sex">Others</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:FileUpload ID="file" name="photo" runat="server" /></td>
                </tr>
            </table>
            <div id="buttons">
                <asp:Button ID="back" runat="server" Text="Back" CssClass="bt" OnClick="back_Click" />
                <asp:Button ID="create" runat="server" Text="Create" CssClass="bt" OnClick="create_Click" />

            </div>
        </form>
    </div>
</body>
</html>
