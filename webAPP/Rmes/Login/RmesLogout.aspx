<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_Login_RmesLogout" Title="" Codebehind="RmesLogout.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <table width="100%" >
        <tr>
            <td style="width: 100%; height:367px;background-image: url(<%=Page.ResolveUrl("~/Rmes/Pub/images/Re_Out.png")%>); background-position:center; text-align: center; background-repeat:no-repeat;">
                
                <br /><br /><br /><br />
                <asp:Label ID="Label1" runat="server" Text="欢迎使用RMES制造执行系统，是否确认退出？"  Font-Names="Arial" Font-Size="12pt" ForeColor="yellow"  ></asp:Label>
                <br />
                <br />
                <asp:ImageButton ID="Button_Confirm" OnClick="Button_Confirm_Click"  ToolTip="确定" runat="server" ImageUrl="~/Rmes/Pub/images/logIn.png" Width="50px" ForeColor="Transparent" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="Button_Cancel" OnClick="Button_Cancel_Click"  ToolTip="取消"  runat="server" ImageUrl="~/Rmes/Pub/images/exit.png" Width="50px" ForeColor="Transparent" />
            </td>
        </tr>
    </table>
</asp:Content>

