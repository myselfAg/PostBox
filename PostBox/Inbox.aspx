<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inbox.aspx.cs" Inherits="PostBox.Inbox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Inbox.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link
        href="https://fonts.googleapis.com/css2?family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=WDXL+Lubrifont+TC&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css">
</head>
<body>
    <form class="main" runat="server">
        <div class="top">
            <div class="dp">
                <asp:Image ID="img" runat="server" CssClass="dp-img" />
            </div>
            <asp:Label ID="user" runat="server" Text="User"></asp:Label>
            <div class="search-sec">
                <asp:TextBox ID="search" runat="server" name="search" placeholder="Search"></asp:TextBox>
                <asp:Button ID="searcBtn" runat="server" Text="Search" CssClass="searchBtn" OnClick="searcBtn_Click" />
            </div>
            <asp:Button ID="logout" runat="server" Text="Logout" CssClass="logout" OnClick="logout_Click" />
        </div>
        <div class="bottom">
            <div class="nav">
                <asp:Button ID="compose" CssClass="more edit" runat="server" Text="Compose" OnClick="compose_Click" />


                <asp:Button ID="inboBt" CssClass="more inbox" runat="server" Text="Inbox" OnClick="inboBt_Click" />
                <asp:Button ID="sent" CssClass="more sent" runat="server" Text="Sent" OnClick="sent_Click" />
                <asp:Button ID="draft" CssClass="more draft" runat="server" Text="Draft" OnClick="draft_Click" />
                <asp:Button ID="settings" CssClass="more settings" runat="server" Text="Settings" OnClick="settings_Click" />

                <h1>POSTBOX</h1>
            </div>
            <div class="content">
                <div class="content-heading">
                    <h1>Inbox</h1>
                </div>

                <!-- Inbox Maiils -->
                <div class="draftMails">
                    <asp:GridView ID="mails" runat="server" CssClass="allMails"
                        AutoGenerateSelectButton="false"
                        OnSelectedIndexChanged="mails_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ShowSelectButton="true" SelectText="Open Mail" HeaderText="" ItemStyle-CssClass="select-link" />
                        </Columns>
                    </asp:GridView>
                </div>
                <!-- Sent -->
                <div class="hide" id="sentSection" runat="server">
                    <div class="sentSection">
                        <div class="sentSectionContentHeading">
                            <h1>Sent</h1>
                        </div>
                        <asp:GridView ID="sentMails" runat="server" CssClass="allMails"
                            AutoGenerateSelectButton="false"
                            OnSelectedIndexChanged="sentMails_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" SelectText="Open Mail" HeaderText="" ItemStyle-CssClass="select-link" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <!-- Drafts -->
                <div class="hide" id="hideDraftSection" runat="server">
                    <div class="draftSection">
                        <div class="draftContentHeading">
                            <h1>Drafts</h1>
                        </div>
                        <asp:GridView ID="draftMails" runat="server" CssClass="allMails"
                            AutoGenerateSelectButton="false"
                            OnSelectedIndexChanged="draftMails_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" SelectText="Edit Draft" HeaderText="" ItemStyle-CssClass="select-link" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <!-- Selected Mail Floating Window -->

                <div id="hideSelectedMailWindow" class="hide" runat="server">
                    <div id="selectedMail" class="selectedMail">
                        <div id="selectedMailHeader" class="selectedMailHeader">
                            <asp:Label ID="fromWhom" CssClass="fromWhom" runat="server" Text=""></asp:Label>
                            <asp:Button ID="closeSelectedMail" CssClass="closeBtn" runat="server" Text="X" OnClick="closeSelectedMail_Click1" />
                        </div>
                        <div class="selectedMailSubject">
                            <asp:Label ID="selectedMailSubjectText" runat="server" Text=""></asp:Label>
                            <asp:Label ID="selectedMailDateTime" runat="server" Text=""></asp:Label>
                        </div>
                        <div id="selectedMailBody" class="selectedMailBody">
                            <asp:Label ID="selectedMailBodyText" CssClass="selectedMailBodyText" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:Button ID="delete" CssClass="deleteBtn" runat="server" Text="Delete" OnClick="delete_Click" />
                    </div>
                </div>

                <!-- Compose Mail -->
                <div id="composeHide" class="hide" runat="server">
                    <div id="composeMail" class="composeMail" runat="server">
                        <h1>Compose</h1>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" CssClass="text-boxes" ID="to" placeholder="To"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" type="text" CssClass="text-boxes" ID="subject" placeholder="Subject"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" type="text" CssClass="text-boxes" ID="body" placeholder="Body"></asp:TextBox>
                                </td>
                            </tr>
                            <!-- <tr>
                                <td>
                                    <asp:FileUpload ID="file" name="photo" runat="server" />
                                </td>
                            </tr> -->
                        </table>
                        <div class="buttons">
                            <asp:Button ID="back" CssClass="moreBt" runat="server" Text="Back" OnClick="back_Click" />
                            <asp:Button ID="clear" CssClass="moreBt" runat="server" Text="Clear" OnClick="clear_Click1" />
                            <asp:Button ID="send" CssClass="bt" runat="server" Text="Send" OnClick="send_Click" />

                        </div>

                    </div>
                </div>

                <!-- Settings -->
                <div id="hideSettingsWindow" class="hide" runat="server">
                    <div class="settingsWindow">
                        <h1>Settings</h1>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="userSet" CssClass="settingsTextBoxes" runat="server" placeholder="Username"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox type="password" ID="oldPass" CssClass="settingsTextBoxes" runat="server" placeholder="Old Password"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox type="password" ID="newPass" CssClass="settingsTextBoxes" runat="server" placeholder="New Password"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox type="password" ID="reNewPass" CssClass="settingsTextBoxes" runat="server" placeholder="Confirm Password"></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                        <div class="buttons">
                            <asp:Button ID="setBack" CssClass="bt" runat="server" Text="Back" OnClick="setBack_Click" />
                            <asp:Button ID="setSave" CssClass="bt" runat="server" Text="Save" OnClick="setSave_Click" />
                        </div>
                    </div> 
                </div>

            </div>

        </div>
        </div>
    </form>
</body>

</html>
